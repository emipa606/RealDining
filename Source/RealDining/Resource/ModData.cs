using System.Collections.Generic;
using Verse;

namespace YC.RealDining.Resource;

public class ModData : GameComponent
{
    private static HadAteFoodType lastFoodType = new();

    private static HadAteFoodType llastFoodType = new();

    public static readonly Dictionary<string, float> foodClassRandomVal = new();

    public static string findedInventoryFoodID;

    public Game game;

    public ModData(Game game)
    {
        this.game = game;
    }

    public ModData()
    {
    }

    public static string GetLastFoodType(Pawn pawn)
    {
        lastFoodType ??= new HadAteFoodType();

        return lastFoodType.GetHadAteFoodType(pawn);
    }

    public static void SetLastFoodType(Pawn pawn, string foodType)
    {
        lastFoodType ??= new HadAteFoodType();

        lastFoodType.SetHadAteFoodType(pawn, foodType);
    }

    public static string GetLlastFoodType(Pawn pawn)
    {
        llastFoodType ??= new HadAteFoodType();

        return llastFoodType.GetHadAteFoodType(pawn);
    }

    public static void SetLlastFoodType(Pawn pawn, string foodType)
    {
        llastFoodType ??= new HadAteFoodType();

        llastFoodType.SetHadAteFoodType(pawn, foodType);
    }

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref lastFoodType, "lastFoodType");
        Scribe_Deep.Look(ref llastFoodType, "llastFoodType");
    }
}