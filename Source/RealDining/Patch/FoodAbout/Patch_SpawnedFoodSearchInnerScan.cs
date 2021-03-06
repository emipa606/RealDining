﻿using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout
{
	// Token: 0x0200000B RID: 11
	[HarmonyPatch(typeof(FoodUtility))]
	[HarmonyPatch("SpawnedFoodSearchInnerScan")]
	[HarmonyPatch(new Type[]
	{
		typeof(Pawn),
		typeof(IntVec3),
		typeof(List<Thing>),
		typeof(PathEndMode),
		typeof(TraverseParms),
		typeof(float),
		typeof(Predicate<Thing>)
	})]
	internal class Patch_SpawnedFoodSearchInnerScan
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003494 File Offset: 0x00001694
		[HarmonyPrefix]
		private static bool Prefix(ref Thing __result, Pawn eater, IntVec3 root, List<Thing> searchSet, PathEndMode peMode, TraverseParms traverseParams, float maxDistance = 9999f, Predicate<Thing> validator = null)
		{
            if (eater.RaceProps.Humanlike)
            {
                ModData.foodClassRandomVal.Clear();
            }
            return true;
		}
	}
}
