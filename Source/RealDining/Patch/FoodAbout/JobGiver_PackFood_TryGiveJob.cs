using HarmonyLib;
using RimWorld;
using Verse;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout;

[HarmonyPatch(typeof(JobGiver_PackFood), "TryGiveJob", typeof(Pawn))]
internal class JobGiver_PackFood_TryGiveJob
{
    private static bool Prefix(Pawn pawn)
    {
        if (pawn.RaceProps.Humanlike)
        {
            ModData.foodClassRandomVal.Clear();
        }

        return true;
    }
}