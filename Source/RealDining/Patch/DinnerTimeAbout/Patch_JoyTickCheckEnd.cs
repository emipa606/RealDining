using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using YC.RealDining.Resource;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout
{
    // Token: 0x02000010 RID: 16
    [HarmonyPatch(typeof(JoyUtility))]
    [HarmonyPatch("JoyTickCheckEnd")]
    [HarmonyPatch(new Type[]
    {
        typeof(Pawn),
        typeof(JoyTickFullJoyAction),
        typeof(float),
        typeof(Building)
    })]
    internal class Patch_JoyTickCheckEnd
    {
        // Token: 0x06000032 RID: 50 RVA: 0x00003988 File Offset: 0x00001B88
        [HarmonyPrefix]
        private static bool Prefix(Pawn pawn, JoyTickFullJoyAction fullJoyAction = JoyTickFullJoyAction.EndJob, float extraJoyGainFactor = 1f, Building joySource = null)
        {
            Job curJob = pawn.CurJob;
            if (curJob.def.joyKind == null)
            {
                Log.Warning("This method can only be called for jobs with joyKind.", false);
                return false;
            }
            if (joySource != null)
            {
                var flag3 = joySource.def.building.joyKind != null && pawn.CurJob.def.joyKind != joySource.def.building.joyKind;
                if (flag3)
                {
                    Log.ErrorOnce("Joy source joyKind and jobDef.joyKind are not the same. building=" + joySource.ToStringSafe() + ", jobDef=" + pawn.CurJob.def.ToStringSafe(), joySource.thingIDNumber ^ 876598732, false);
                }
                extraJoyGainFactor *= joySource.GetStatValue(StatDefOf.JoyGainFactor, true);
            }
            if (pawn.needs.joy == null)
            {
                pawn.jobs.curDriver.EndJobWith(JobCondition.InterruptForced);
                return false;
            }
            TimeAssignmentDef timeAssignmentDef = (pawn.timetable == null) ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment;
            if (timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef)
            {
                return true;
            }
            pawn.needs.joy.GainJoy(extraJoyGainFactor * curJob.def.joyGainRate * 0.36f / 2500f, curJob.def.joyKind);
            if (curJob.def.joySkill != null)
            {
                pawn.skills.GetSkill(curJob.def.joySkill).Learn(curJob.def.joyXpPerTick, false);
            }
            if (pawn.needs.food.CurLevelPercentage < pawn.RaceProps.FoodLevelPercentageWantEat + ModSetting.eatThreshold && pawn.IsHashIntervalTick(60))
            {
                var desperate = pawn.needs.food.CurCategory == HungerCategory.Starving;
                if (FoodUtility.TryFindBestFoodSourceFor(pawn, pawn, desperate, out _, out _, true, true, false, true, false, pawn.IsWildMan(), false, false, FoodPreferability.Undefined))
                {
                    if (fullJoyAction == JoyTickFullJoyAction.EndJob)
                    {
                        pawn.jobs.curDriver.EndJobWith(JobCondition.Succeeded);
                        return false;
                    }
                    if (fullJoyAction == JoyTickFullJoyAction.GoToNextToil)
                    {
                        pawn.jobs.curDriver.ReadyForNextToil();
                    }
                }
            }
            return false;
        }
    }
}
