using System;
using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout
{
    // Token: 0x02000012 RID: 18
    [HarmonyPatch(typeof(ThoughtWorker_NeedFood))]
    [HarmonyPatch("CurrentStateInternal")]
    [HarmonyPatch(new Type[]
    {
        typeof(Pawn)
    })]
    internal class Patch_ThoghWokr_NedFod_CurrentStateInternal
    {
        // Token: 0x06000036 RID: 54 RVA: 0x00003C98 File Offset: 0x00001E98
        [HarmonyPrefix]
        private static bool Prefix(ref ThoughtState __result, Pawn p)
        {
            if (p.needs.food == null)
            {
                __result = ThoughtState.Inactive;
                return false;
            }
            if (p.RaceProps.Humanlike && p.timetable != null)
            {
                TimeAssignmentDef currentAssignment = p.timetable.CurrentAssignment;
                if (currentAssignment != TimeAssignmentDefDinner.DinnerDef)
                {
                    Need_Food food = p.needs.food;
                    if (p.timetable.GetAssignment((GenLocalDate.HourOfDay(p) + 1) % 24) == TimeAssignmentDefDinner.DinnerDef && food.CurLevelPercentage > p.RaceProps.FoodLevelPercentageWantEat * 0.45f && food.CurCategory >= HungerCategory.Hungry)
                    {
                        __result = ThoughtState.ActiveAtStage(7);
                        return false;
                    }
                }
            }
            switch (p.needs.food.CurCategory)
            {
                case HungerCategory.Fed:
                    __result = ThoughtState.Inactive;
                    return false;
                case HungerCategory.Hungry:
                    __result = ThoughtState.ActiveAtStage(0);
                    return false;
                case HungerCategory.UrgentlyHungry:
                    __result = ThoughtState.ActiveAtStage(1);
                    return false;
                case HungerCategory.Starving:
                    {
                        Hediff firstHediffOfDef = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Malnutrition, false);
                        var num = (firstHediffOfDef == null) ? 0 : firstHediffOfDef.CurStageIndex;
                        if (num > 4)
                        {
                            num = 4;
                        }
                        __result = ThoughtState.ActiveAtStage(2 + num);
                        return false;
                    }
            }
            return true;
        }
    }
}
