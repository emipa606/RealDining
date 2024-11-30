using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(ThoughtWorker_NeedFood), "CurrentStateInternal", typeof(Pawn))]
internal class ThoughtWorker_NeedFood_CurrentStateInternal
{
    private static bool Prefix(ref ThoughtState __result, Pawn p)
    {
        if (p.needs.food == null)
        {
            __result = ThoughtState.Inactive;
            return false;
        }

        if (p.RaceProps.Humanlike && p.timetable != null)
        {
            var currentAssignment = p.timetable.CurrentAssignment;
            if (currentAssignment != TimeAssignmentDefDinner.DinnerDef)
            {
                var food = p.needs.food;
                if (p.timetable.GetAssignment((GenLocalDate.HourOfDay(p) + 1) % 24) ==
                    TimeAssignmentDefDinner.DinnerDef &&
                    food.CurLevelPercentage > p.RaceProps.FoodLevelPercentageWantEat * 0.45f &&
                    food.CurCategory >= HungerCategory.Hungry)
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
                var firstHediffOfDef = p.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.Malnutrition);
                var num = firstHediffOfDef?.CurStageIndex ?? 0;
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