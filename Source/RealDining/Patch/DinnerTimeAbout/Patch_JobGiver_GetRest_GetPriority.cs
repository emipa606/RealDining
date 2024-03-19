using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI.Group;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(JobGiver_GetRest))]
[HarmonyPatch("GetPriority")]
[HarmonyPatch([
    typeof(Pawn)
])]
internal class Patch_JobGiver_GetRest_GetPriority
{
    [HarmonyPrefix]
    private static bool Prefix(RestCategory ___minCategory, float ___maxLevelPercentage, ref float __result,
        Pawn pawn)
    {
        var rest = pawn.needs.rest;
        if (rest == null)
        {
            __result = 0f;
            return false;
        }

        if (rest.CurCategory < ___minCategory)
        {
            __result = 0f;
            return false;
        }

        if (rest.CurLevelPercentage > ___maxLevelPercentage)
        {
            __result = 0f;
            return false;
        }

        if (Find.TickManager.TicksGame < pawn.mindState.canSleepTick)
        {
            __result = 0f;
            return false;
        }

        var humanlike = pawn.RaceProps.Humanlike;
        if (!humanlike)
        {
            return true;
        }

        var timeAssignmentDef = pawn.timetable == null
            ? TimeAssignmentDefOf.Anything
            : pawn.timetable.CurrentAssignment;
        if (timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef)
        {
            return true;
        }

        var lord = pawn.GetLord();
        if (lord != null && !lord.CurLordToil.AllowSatisfyLongNeeds)
        {
            __result = 0f;
            return false;
        }

        var curLevel = rest.CurLevel;
        __result = curLevel < 0.3f ? 8f : 0f;
        return false;
    }
}