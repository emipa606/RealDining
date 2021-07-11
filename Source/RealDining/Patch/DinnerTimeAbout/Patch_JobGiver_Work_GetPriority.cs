using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout
{
    // Token: 0x0200000F RID: 15
    [HarmonyPatch(typeof(JobGiver_Work))]
    [HarmonyPatch("GetPriority")]
    [HarmonyPatch(new[]
    {
        typeof(Pawn)
    })]
    internal class Patch_JobGiver_Work_GetPriority
    {
        // Token: 0x06000030 RID: 48 RVA: 0x0000390C File Offset: 0x00001B0C
        [HarmonyPrefix]
        private static bool Prefix(ref float __result, Pawn pawn)
        {
            if (pawn.workSettings == null || !pawn.workSettings.EverWork)
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
}