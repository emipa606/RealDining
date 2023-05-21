using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout;

[HarmonyPatch(typeof(FoodUtility), "BestFoodInInventory_NewTemp")]
internal class Patch_BestFoodInInventory_NewTemp
{
    [HarmonyPostfix]
    private static void Postfix(ref Thing __result, Pawn holder, Pawn eater = null)
    {
        if (!ModSetting.priorityRoomFood)
        {
            return;
        }

        ModData.findedInventoryFoodID = null;
        if (holder?.inventory == null)
        {
            return;
        }

        if (!holder.IsColonist)
        {
            return;
        }

        if (eater != holder)
        {
            return;
        }

        if (!holder.RaceProps.Humanlike || !holder.Spawned)
        {
            return;
        }

        var needs = holder.needs;
        float? num;
        if (needs == null)
        {
            num = null;
        }
        else
        {
            var food = needs.food;
            num = food != null ? new float?(food.CurLevelPercentage) : null;
        }

        if ((num ?? 0f) > holder.RaceProps.FoodLevelPercentageWantEat * 0.45f &&
            holder.Map.areaManager.Home[holder.Position])
        {
            ModData.findedInventoryFoodID = __result?.GetUniqueLoadID();
        }
    }
}