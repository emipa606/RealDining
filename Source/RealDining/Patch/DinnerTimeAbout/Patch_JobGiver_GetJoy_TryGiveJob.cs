using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(JobGiver_GetJoy))]
[HarmonyPatch("TryGiveJob")]
[HarmonyPatch(new[]
{
    typeof(Pawn)
})]
internal class Patch_JobGiver_GetJoy_TryGiveJob
{
    private static readonly DefMap<JoyGiverDef, float> joyGiverChances = new DefMap<JoyGiverDef, float>();

    [HarmonyPrefix]
    private static bool Prefix(JobGiver_GetJoy __instance, ref Job __result, Pawn pawn)
    {
        var timeAssignmentDef =
            pawn.timetable == null ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment;
        bool result;
        if (timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef)
        {
            result = true;
        }
        else
        {
            if (pawn.InBed() && HealthAIUtility.ShouldSeekMedicalRest(pawn))
            {
                __result = null;
                result = false;
            }
            else
            {
                var allDefsListForReading = DefDatabase<JoyGiverDef>.AllDefsListForReading;
                var tolerances = pawn.needs.joy.tolerances;
                foreach (var joyGiverDef in allDefsListForReading)
                {
                    joyGiverChances[joyGiverDef] = 0f;
                    if (joyGiverDef.joyKind != JoyKindDefOf.Gluttonous &&
                        joyGiverDef.defName != "SocialRelax")
                    {
                        continue;
                    }

                    if (!pawn.needs.joy.tolerances.BoredOf(joyGiverDef.joyKind) &&
                        joyGiverDef.Worker.CanBeGivenTo(pawn))
                    {
                        if (joyGiverDef.pctPawnsEverDo < 1f)
                        {
                            Rand.PushState(pawn.thingIDNumber ^ 63216713);
                            if (Rand.Value >= joyGiverDef.pctPawnsEverDo)
                            {
                                Rand.PopState();
                                goto IL_196;
                            }

                            Rand.PopState();
                        }

                        var num = tolerances[joyGiverDef.joyKind];
                        var num2 = Mathf.Pow(1f - num, 5f);
                        num2 = Mathf.Max(0.001f, num2);
                        joyGiverChances[joyGiverDef] = joyGiverDef.Worker.GetChance(pawn) * num2;
                    }

                    IL_196: ;
                }

                var num3 = 0;
                Job job;
                for (;;)
                {
                    var joyGiverDef2 = new JoyGiverDef();
                    bool hasJoyGiver;
                    if (num3 < joyGiverChances.Count)
                    {
                        hasJoyGiver = allDefsListForReading.TryRandomElementByWeight(d => joyGiverChances[d],
                            out joyGiverDef2);
                    }
                    else
                    {
                        hasJoyGiver = false;
                    }

                    if (!hasJoyGiver)
                    {
                        goto Block_17;
                    }

                    if (pawn.needs.joy.CurLevel < 0.95f || joyGiverDef2.joyKind != JoyKindDefOf.Gluttonous)
                    {
                        job = TryGiveJobFromJoyGiverDefDirect(joyGiverDef2, pawn);
                        if (job != null)
                        {
                            break;
                        }
                    }

                    joyGiverChances[joyGiverDef2] = 0f;
                    num3++;
                }

                __result = job;
                return false;
                Block_17:
                __result = null;
                result = false;
            }
        }

        return result;
    }

    protected static Job TryGiveJobFromJoyGiverDefDirect(JoyGiverDef def, Pawn pawn)
    {
        return def.Worker.TryGiveJob(pawn);
    }
}