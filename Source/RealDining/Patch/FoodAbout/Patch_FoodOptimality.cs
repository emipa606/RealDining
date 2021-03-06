﻿using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout
{
    // Token: 0x02000009 RID: 9
    [HarmonyPatch(typeof(FoodUtility))]
    [HarmonyPatch("FoodOptimality")]
    [HarmonyPatch(new Type[]
    {
        typeof(Pawn),
        typeof(Thing),
        typeof(ThingDef),
        typeof(float),
        typeof(bool)
    })]
    internal class Patch_FoodOptimality
    {
        // Token: 0x0600001E RID: 30 RVA: 0x00002E5C File Offset: 0x0000105C
        [HarmonyPrefix]
        private static bool Prefix(ref float __result, Pawn eater, Thing foodSource, ThingDef foodDef, float dist, bool takingToInventory = false)
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
            CompRottable compRottable = foodSource.TryGetComp<CompRottable>();
            if (compRottable != null)
            {
                if (compRottable.Stage == RotStage.Dessicated)
                {
                    __result = -9999999f;
                    return false;
                }
                if (!takingToInventory && compRottable.Stage == RotStage.Fresh && compRottable.TicksUntilRotAtCurrentTemp < 30000)
                {
                    num += 13.3f;
                }
            }
            var moodTrigger = false;
            if (eater.needs != null && eater.needs.mood != null)
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
                List<ThoughtDef> list = FoodUtility.ThoughtsFromIngesting(eater, foodSource, foodDef);
                for (var i = 0; i < list.Count; i++)
                {
                    num += FoodOptimalityEffectFromMoodCurve.Evaluate(list[i].stages[0].baseMoodEffect) * num2;
                    if (list[i].stages[0].baseMoodEffect < 0f)
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
                if (!ModData.foodClassRandomVal.ContainsKey(foodDef.defName))
                {
                    num3 = Rand.Range(0f, ModSetting.randomLevel);
                    ModData.foodClassRandomVal[foodDef.defName] = num3;
                }
                else
                {
                    num3 = ModData.foodClassRandomVal[foodDef.defName];
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

        // Token: 0x04000024 RID: 36
        private static readonly SimpleCurve FoodOptimalityEffectFromMoodCurve = new SimpleCurve
        {
            {
                new CurvePoint(-100f, -600f),
                true
            },
            {
                new CurvePoint(-10f, -100f),
                true
            },
            {
                new CurvePoint(-5f, -70f),
                true
            },
            {
                new CurvePoint(-1f, -50f),
                true
            },
            {
                new CurvePoint(0f, 0f),
                true
            },
            {
                new CurvePoint(100f, 800f),
                true
            }
        };
    }
}
