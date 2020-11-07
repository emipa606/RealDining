using System;
using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout
{
	// Token: 0x02000008 RID: 8
	[HarmonyPatch(typeof(FoodUtility))]
	[HarmonyPatch("BestFoodInInventory")]
	[HarmonyPatch(new Type[]
	{
		typeof(Pawn),
		typeof(Pawn),
		typeof(FoodPreferability),
		typeof(FoodPreferability),
		typeof(float),
		typeof(bool)
	})]
	internal class Patch_BestFoodInInventory
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002D7C File Offset: 0x00000F7C
		[HarmonyPostfix]
		private static void Postfix(ref Thing __result, Pawn holder, Pawn eater = null, FoodPreferability minFoodPref = FoodPreferability.NeverForNutrition, FoodPreferability maxFoodPref = FoodPreferability.MealLavish, float minStackNutrition = 0f, bool allowDrug = false)
        {
            if (!ModSetting.priorityRoomFood)
            {
                return;
            }
            ModData.findedInventoryFoodID = null;
            if (holder == null || holder.inventory == null)
            {
                return;
            }
            if (eater != null && eater.GetUniqueLoadID() != holder.GetUniqueLoadID())
            {
                return;
            }
            if (!holder.IsColonist)
            {
                return;
            }
            if (!holder.RaceProps.Humanlike || !holder.Spawned || holder.needs.food.CurLevelPercentage <= holder.RaceProps.FoodLevelPercentageWantEat * 0.45f || !holder.Map.areaManager.Home[holder.Position])
            {
                return;
            }
            ModData.findedInventoryFoodID = __result.GetUniqueLoadID();
        }
    }
}
