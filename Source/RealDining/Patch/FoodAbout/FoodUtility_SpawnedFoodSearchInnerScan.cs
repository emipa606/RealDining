using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout;

[HarmonyPatch(typeof(FoodUtility), "SpawnedFoodSearchInnerScan", typeof(Pawn), typeof(IntVec3), typeof(List<Thing>),
    typeof(PathEndMode), typeof(TraverseParms), typeof(float), typeof(Predicate<Thing>))]
internal class FoodUtility_SpawnedFoodSearchInnerScan
{
    private static bool Prefix(Pawn eater)
    {
        if (eater.RaceProps.Humanlike)
        {
            ModData.foodClassRandomVal.Clear();
        }

        return true;
    }
}