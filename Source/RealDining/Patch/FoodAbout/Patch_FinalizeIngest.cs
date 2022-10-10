using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using YC.RealDining.Resource;

namespace YC.RealDining.Patch.FoodAbout;

[HarmonyPatch(typeof(Toils_Ingest))]
[HarmonyPatch("FinalizeIngest")]
[HarmonyPatch(new[]
{
    typeof(Pawn),
    typeof(TargetIndex)
})]
internal class Patch_FinalizeIngest
{
    [HarmonyPrefix]
    private static bool Prefix(ref Toil __result, Pawn ingester, TargetIndex ingestibleInd)
    {
        var toil = new Toil();
        toil.initAction = delegate
        {
            var actor = toil.actor;
            var curJob = actor.jobs.curJob;
            var thing = curJob.GetTarget(ingestibleInd).Thing;
            if (ingester.needs.mood != null && thing.def.IsNutritionGivingIngestible &&
                thing.def.ingestible.chairSearchRadius > 10f)
            {
                if (!(ingester.Position + ingester.Rotation.FacingCell).HasEatSurface(actor.Map) &&
                    ingester.GetPosture() == PawnPosture.Standing && !ingester.IsWildMan())
                {
                    ingester.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.AteWithoutTable);
                }

                if (ingester.GetRoom(RegionType.Set_Passable) != null)
                {
                    var scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(ingester
                        .GetRoom(RegionType.Set_Passable).GetStat(RoomStatDefOf.Impressiveness));
                    if (ThoughtDefOf.AteInImpressiveDiningRoom.stages[scoreStageIndex] != null)
                    {
                        ingester.needs.mood.thoughts.memories.TryGainMemory(
                            ThoughtMaker.MakeThought(ThoughtDefOf.AteInImpressiveDiningRoom, scoreStageIndex));
                    }
                }
            }

            var num = ingester.needs.food.NutritionWanted;
            if (curJob.overeat)
            {
                num = Mathf.Max(num, 0.75f);
            }

            var num2 = thing.Ingested(ingester, num);
            if (!ingester.Dead)
            {
                ingester.needs.food.CurLevel += num2;
            }

            ingester.records.AddTo(RecordDefOf.NutritionEaten, num2);
            if (!thing.def.IsNutritionGivingIngestible || !ingester.RaceProps.Humanlike)
            {
                return;
            }

            MoodAdd(ingester, thing);
            RecordLastFood(ingester, thing);
        };
        toil.defaultCompleteMode = ToilCompleteMode.Instant;
        __result = toil;
        return false;
    }

    public static void RecordLastFood(Pawn pawn, Thing thing)
    {
        var lastFoodType = ModData.GetLastFoodType(pawn);
        if (lastFoodType != HadAteFoodType.StrEmpty)
        {
            ModData.SetLlastFoodType(pawn, lastFoodType);
        }

        ModData.SetLastFoodType(pawn, thing.def.defName);
    }

    public static void MoodAdd(Pawn pawn, Thing thing)
    {
        if (IsBadFood(pawn, thing))
        {
            return;
        }

        var lastFoodType = ModData.GetLastFoodType(pawn);
        if (lastFoodType == HadAteFoodType.StrEmpty || lastFoodType == thing.def.defName)
        {
            return;
        }

        pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("EatFirstDifferentFood"));
        var llastFoodType = ModData.GetLlastFoodType(pawn);
        if (llastFoodType != HadAteFoodType.StrEmpty && llastFoodType != thing.def.defName &&
            llastFoodType != lastFoodType)
        {
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDef.Named("EatSecondDifferentFood"));
        }
    }

    public static bool IsBadFood(Pawn pawn, Thing thing)
    {
        var list = FoodUtility.ThoughtsFromIngesting(pawn, thing, thing.def);
        for (var i = 0; i < list.Count; i++)
        {
            if (list[i].thought.stages[0].baseMoodEffect < 0f)
            {
                return true;
            }
        }

        return false;
    }
}