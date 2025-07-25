using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(ThinkNode_Priority_GetJoy), nameof(ThinkNode_Priority_GetJoy.GetPriority), typeof(Pawn))]
internal class ThinkNode_Priority_GetJoy_GetPriority
{
    private static bool Prefix(ref float __result, Pawn pawn)
    {
        if (pawn.needs.joy == null || Find.TickManager.TicksGame < 5000)
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

        __result = JoyUtility.LordPreventsGettingJoy(pawn) ? 0f : 7f;
        return false;
    }
}