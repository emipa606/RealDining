using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using YC.RealDining.Resource;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(JoyUtility), nameof(JoyUtility.JoyTickCheckEnd), typeof(Pawn), typeof(JoyTickFullJoyAction),
    typeof(float), typeof(Building))]
internal class JoyUtility_JoyTickCheckEnd
{
    private static bool Prefix(Pawn pawn, JoyTickFullJoyAction fullJoyAction = JoyTickFullJoyAction.EndJob,
        float extraJoyGainFactor = 1f, Building joySource = null)
    {
        var curJob = pawn.CurJob;
        if (curJob.def.joyKind == null)
        {
            Log.Warning("This method can only be called for jobs with joyKind.");
            return false;
        }

        if (joySource != null)
        {
            if (joySource.def.building.joyKind != null &&
                pawn.CurJob.def.joyKind != joySource.def.building.joyKind)
            {
                Log.ErrorOnce(
                    $"Joy source joyKind and jobDef.joyKind are not the same. building={joySource.ToStringSafe()}, jobDef={pawn.CurJob.def.ToStringSafe()}",
                    joySource.thingIDNumber ^ 876598732);
            }

            extraJoyGainFactor *= joySource.GetStatValue(StatDefOf.JoyGainFactor);
        }

        if (pawn.needs.joy == null)
        {
            pawn.jobs.curDriver.EndJobWith(JobCondition.InterruptForced);
            return false;
        }

        var timeAssignmentDef =
            pawn.timetable == null ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment;
        if (timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef)
        {
            return true;
        }

        pawn.needs.joy.GainJoy(extraJoyGainFactor * curJob.def.joyGainRate * 0.36f / 2500f, curJob.def.joyKind);
        if (curJob.def.joySkill != null)
        {
            pawn.skills.GetSkill(curJob.def.joySkill).Learn(curJob.def.joyXpPerTick);
        }

        if (!(pawn.needs.food.CurLevelPercentage <
              pawn.RaceProps.FoodLevelPercentageWantEat + ModSetting.eatThreshold) || !pawn.IsHashIntervalTick(60))
        {
            return false;
        }

        var desperate = pawn.needs.food.CurCategory == HungerCategory.Starving;
        if (!FoodUtility.TryFindBestFoodSourceFor(pawn, pawn, desperate, out _, out _, true, true, false, true,
                false, pawn.IsWildMan()))
        {
            return false;
        }

        if (fullJoyAction == JoyTickFullJoyAction.EndJob)
        {
            pawn.jobs.curDriver.EndJobWith(JobCondition.Succeeded);
            return false;
        }

        if (fullJoyAction == JoyTickFullJoyAction.GoToNextToil)
        {
            pawn.jobs.curDriver.ReadyForNextToil();
        }

        return false;
    }
}