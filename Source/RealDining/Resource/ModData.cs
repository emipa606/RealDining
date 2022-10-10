using System;
using System.Collections.Generic;
using Verse;

namespace YC.RealDining.Resource;

public class ModData : GameComponent
{
    public static HadAteFoodType lastFoodType = new HadAteFoodType();

    public static HadAteFoodType llastFoodType = new HadAteFoodType();

    public static Dictionary<string, float> foodClassRandomVal = new Dictionary<string, float>();

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
        if (lastFoodType == null)
        {
            lastFoodType = new HadAteFoodType();
        }

        return lastFoodType.GetHadAteFoodType(pawn);
    }

    public static void SetLastFoodType(Pawn pawn, string foodType)
    {
        if (lastFoodType == null)
        {
            lastFoodType = new HadAteFoodType();
        }

        lastFoodType.SetHadAteFoodType(pawn, foodType);
    }

    public static string GetLlastFoodType(Pawn pawn)
    {
        if (llastFoodType == null)
        {
            llastFoodType = new HadAteFoodType();
        }

        return llastFoodType.GetHadAteFoodType(pawn);
    }

    public static void SetLlastFoodType(Pawn pawn, string foodType)
    {
        if (llastFoodType == null)
        {
            llastFoodType = new HadAteFoodType();
        }

        llastFoodType.SetHadAteFoodType(pawn, foodType);
    }

    public override void ExposeData()
    {
        Scribe_Deep.Look(ref lastFoodType, "lastFoodType", Array.Empty<object>());
        Scribe_Deep.Look(ref llastFoodType, "llastFoodType", Array.Empty<object>());
    }
}