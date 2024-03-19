using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout;

[HarmonyPatch(typeof(FoodUtility))]
[HarmonyPatch("FoodOptimality")]
[HarmonyPatch([
    typeof(Pawn),
    typeof(Thing),
    typeof(ThingDef),
    typeof(float),
    typeof(bool)
])]
internal class Patch_FoodOptimality
{
    private static readonly SimpleCurve FoodOptimalityEffectFromMoodCurve =
    [
        new CurvePoint(-100f, -600f),
        new CurvePoint(-10f, -100f),
        new CurvePoint(-5f, -70f),
        new CurvePoint(-1f, -50f),
        new CurvePoint(0f, 0f),
        new CurvePoint(100f, 800f)
    ];

    [HarmonyPrefix]
    private static bool Prefix(ref float __result, Pawn eater, Thing foodSource, ThingDef foodDef, float dist,
        bool takingToInventory = false)
    {
        var num = 300f;
        num -= dist;
        switch (foodDef.ingestible.preferability)
        {
            case FoodPreferability.NeverForNutrition:
                __result = -9999999f;
                return false;
            case FoodPreferability.DesperateOnly:
                num -= 150f;
                break;
            case FoodPreferability.DesperateOnlyForHumanlikes:
            {
                var humanlike = eater.RaceProps.Humanlike;
                if (humanlike)
                {
                    num -= 150f;
                }

                break;
            }
        }

        var compRottable = foodSource.TryGetComp<CompRottable>();
        if (compRottable != null)
        {
            if (compRottable.Stage == RotStage.Dessicated)
            {
                __result = -9999999f;
                return false;
            }

            if (!takingToInventory && compRottable.Stage == RotStage.Fresh &&
                compRottable.TicksUntilRotAtCurrentTemp < 30000)
            {
                num += 13.3f;
            }
        }

        var moodTrigger = false;
        if (eater.needs?.mood != null)
        {
            var num2 = ModSetting.moodInfluenceX;
            if (eater.needs.mood.CurLevel < eater.mindState.mentalBreaker.BreakThresholdExtreme)
            {
                num2 += 0.77f;
            }
            else
            {
                if (eater.needs.mood.CurLevel < eater.mindState.mentalBreaker.BreakThresholdMajor)
                {
                    num2 += 0.6f;
                }
                else
                {
                    if (eater.needs.mood.CurLevel < eater.mindState.mentalBreaker.BreakThresholdMinor)
                    {
                        num2 += 0.43f;
                    }
                    else
                    {
                        if (eater.needs.mood.CurLevel < eater.mindState.mentalBreaker.BreakThresholdMinor + 0.06f)
                        {
                            num2 += 0.25f;
                        }
                    }
                }
            }

            var list = FoodUtility.ThoughtsFromIngesting(eater, foodSource, foodDef);
            for (var i = 0; i < list.Count; i++)
            {
                num += FoodOptimalityEffectFromMoodCurve.Evaluate(list[i].thought.stages[0].baseMoodEffect) * num2;
                if (list[i].thought.stages[0].baseMoodEffect < 0f)
                {
                    moodTrigger = true;
                }
            }
        }

        if (foodDef.ingestible != null)
        {
            var humanlike2 = eater.RaceProps.Humanlike;
            if (humanlike2)
            {
                num += foodDef.ingestible.optimalityOffsetHumanlikes;
            }
            else
            {
                var animal = eater.RaceProps.Animal;
                if (animal)
                {
                    num += foodDef.ingestible.optimalityOffsetFeedingAnimals;
                }
            }
        }

        if (!moodTrigger && compRottable != null && eater.RaceProps.Humanlike)
        {
            float num3;
            if (!ModData.foodClassRandomVal.TryGetValue(foodDef.defName, out var value))
            {
                num3 = Rand.Range(0f, ModSetting.randomLevel);
                ModData.foodClassRandomVal[foodDef.defName] = num3;
            }
            else
            {
                num3 = value;
            }

            var num4 = 1f;
            if (ModSetting.priorityRoomFood && ModData.findedInventoryFoodID == foodSource.GetUniqueLoadID())
            {
                ModData.findedInventoryFoodID = null;
                num4 = 0.2f;
            }

            if (ModData.GetLastFoodType(eater) == foodDef.defName)
            {
                num += num3 * ModSetting.lastFoodInfluenceX * num4;
            }
            else
            {
                if (ModData.GetLlastFoodType(eater) == foodDef.defName)
                {
                    num += num3 * ModSetting.llastFoodInfluenceX * num4;
                }
                else
                {
                    num += num3 * num4;
                }
            }
        }

        __result = num;
        return false;
    }
}