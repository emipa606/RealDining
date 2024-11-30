using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(JobGiver_GetFood), nameof(JobGiver_GetFood.GetPriority), typeof(Pawn))]
internal class JobGiver_GetFood_GetPriority
{
    private static bool Prefix(HungerCategory ___minCategory, float ___maxLevelPercentage, ref float __result,
        Pawn pawn)
    {
        var food = pawn.needs.food;
        if (food == null)
        {
            __result = 0f;
            return false;
        }

        if (pawn.needs.food.CurCategory < HungerCategory.Starving && FoodUtility.ShouldBeFedBySomeone(pawn))
        {
            __result = 0f;
            return false;
        }

        if (food.CurCategory < ___minCategory)
        {
            __result = 0f;
            return false;
        }

        if (food.CurLevelPercentage > ___maxLevelPercentage)
        {
            __result = 0f;
            return false;
        }

        if (pawn.RaceProps.Humanlike)
        {
            if (food.CurLevelPercentage < pawn.RaceProps.FoodLevelPercentageWantEat + ModSetting.eatThreshold)
            {
                if (pawn.timetable == null)
                {
                    __result = food.CurLevelPercentage < pawn.RaceProps.FoodLevelPercentageWantEat ? 9.5f : 0f;
                    return false;
                }

                var timeAssignmentDef = pawn.timetable == null
                    ? TimeAssignmentDefOf.Anything
                    : pawn.timetable.CurrentAssignment;
                if (timeAssignmentDef == TimeAssignmentDefDinner.DinnerDef)
                {
                    __result = food.CurLevelPercentage < pawn.RaceProps.FoodLevelPercentageWantEat ? 9.5f : 7.25f;
                    return false;
                }

                if (pawn.timetable.GetAssignment((GenLocalDate.HourOfDay(pawn) + 1) % 24) ==
                    TimeAssignmentDefDinner.DinnerDef &&
                    food.CurLevelPercentage > pawn.RaceProps.FoodLevelPercentageWantEat * 0.48f)
                {
                    __result = 0f;
                    return false;
                }

                if (pawn.timetable.GetAssignment((GenLocalDate.HourOfDay(pawn) + 2) % 24) ==
                    TimeAssignmentDefDinner.DinnerDef &&
                    food.CurLevelPercentage > pawn.RaceProps.FoodLevelPercentageWantEat * 0.8f)
                {
                    __result = 0f;
                    return false;
                }

                __result = food.CurLevelPercentage < pawn.RaceProps.FoodLevelPercentageWantEat ? 9.5f : 0f;
                return false;
            }

            __result = 0f;
        }
        else
        {
            __result = food.CurLevelPercentage < pawn.RaceProps.FoodLevelPercentageWantEat ? 9.5f : 0f;
        }

        return false;
    }
}