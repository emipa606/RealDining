using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout;

[HarmonyPatch(typeof(JobGiver_PackFood))]
[HarmonyPatch("TryGiveJob")]
[HarmonyPatch([
    typeof(Pawn)
])]
internal class Patch_PackFood_TryGiveJob
{
    [HarmonyPrefix]
    private static bool Prefix(Pawn pawn)
    {
        if (pawn.RaceProps.Humanlike)
        {
            ModData.foodClassRandomVal.Clear();
        }

        return true;
    }
}