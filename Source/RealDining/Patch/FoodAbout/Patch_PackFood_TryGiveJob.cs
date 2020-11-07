using System;
using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout
{
	// Token: 0x0200000C RID: 12
	[HarmonyPatch(typeof(JobGiver_PackFood))]
	[HarmonyPatch("TryGiveJob")]
	[HarmonyPatch(new Type[]
	{
		typeof(Pawn)
	})]
	internal class Patch_PackFood_TryGiveJob
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000034D4 File Offset: 0x000016D4
		[HarmonyPrefix]
		private static bool Prefix(ref Job __result, Pawn pawn)
		{
            if (pawn.RaceProps.Humanlike)
            {
                ModData.foodClassRandomVal.Clear();
            }
            return true;
		}
	}
}
