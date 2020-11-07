using System;
using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout
{
    // Token: 0x02000011 RID: 17
    [HarmonyPatch(typeof(ThinkNode_Priority_GetJoy))]
    [HarmonyPatch("GetPriority")]
    [HarmonyPatch(new Type[]
    {
        typeof(Pawn)
    })]
    internal class Patch_ThinkNode_Priority_GetJoy_GetPriority
    {
        // Token: 0x06000034 RID: 52 RVA: 0x00003BEC File Offset: 0x00001DEC
        [HarmonyPrefix]
        private static bool Prefix(ref float __result, Pawn pawn)
        {
            if (pawn.needs.joy == null)
            {
                __result = 0f;
                return false;
            }
            if (Find.TickManager.TicksGame < 5000)
            {
                __result = 0f;
                return false;
            }
            TimeAssignmentDef timeAssignmentDef = (pawn.timetable == null) ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment;
            if (timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef)
            {
                return true;
            }
            __result = JoyUtility.LordPreventsGettingJoy(pawn) ? 0f : 7f;
            return false;
        }
    }
}
