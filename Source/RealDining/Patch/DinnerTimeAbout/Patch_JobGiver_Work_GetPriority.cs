using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(JobGiver_Work))]
[HarmonyPatch("GetPriority")]
[HarmonyPatch([
    typeof(Pawn)
])]
internal class Patch_JobGiver_Work_GetPriority
{
    [HarmonyPrefix]
    private static bool Prefix(ref float __result, Pawn pawn)
    {
        if (pawn.workSettings is not { EverWork: true })
        {
            __result = 0f;
            return false;
        }

        var timeAssignmentDef =
            pawn.timetable == null ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment;
        if (timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef)
        {
            return true;
        }

        __result = 2f;
        return false;
    }
}