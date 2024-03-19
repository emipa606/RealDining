using UnityEngine;
using Verse;

namespace YC.RealDining.Resource;

public class ModSetting : ModSettings
{
    public const float moodInfluenceXDefault = 0.88f;

    public const float randomLevelDefault = 103f;

    public const float lastFoodInfluenceXDefault = 0.4f;

    public const float llastFoodInfluenceXDefault = 0.6f;

    public const float eatThresholdDefault = 0.25f;

    public const int dinnerTimeModeDefault = 0;

    public const float randomLevelHighest = 300f;

    public const float randomLevelHigh = 200f;

    public const float randomLevelMid = 103f;

    public const float randomLevelLow = 50f;

    public const float randomLevelLowest = 0f;

    public const float moodInfLevelHighest = 1.5f;

    public const float moodInfLevelHigh = 1.25f;

    public const float moodInfLevelMid = 1.03f;

    public const float moodInfLevelLow = 0.88f;

    public const float moodInfLevelLowest = 0.68f;

    public static readonly string[] levelLabel =
    [
        "",
        "Lowest",
        "Low",
        "Medium",
        "High",
        "Highest"
    ];

    public static readonly float[] moodLevelVal =
    [
        0f,
        0.68f,
        0.88f,
        1.03f,
        1.25f,
        1.5f
    ];

    public static readonly float[] randomLevelVal =
    [
        0f,
        0f,
        50f,
        103f,
        200f,
        300f
    ];

    public static float moodInfluenceX = 0.88f;

    public static float randomLevel = 103f;

    public static float lastFoodInfluenceX = 0.4f;

    public static float llastFoodInfluenceX = 0.6f;

    public static float eatThreshold = 0.25f;

    public static Vector2 scrollPos = Vector2.zero;

    public static bool priorityRoomFood;

    public static int dinnerTimeMode;

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref moodInfluenceX, "moodInfluenceX", 0.88f);
        Scribe_Values.Look(ref randomLevel, "randomLevel", 103f);
        Scribe_Values.Look(ref lastFoodInfluenceX, "lastFoodInfluenceX", 0.4f);
        Scribe_Values.Look(ref llastFoodInfluenceX, "llastFoodInfluenceX", 0.6f);
        Scribe_Values.Look(ref eatThreshold, "eatThreshold", 0.25f);
        Scribe_Values.Look(ref priorityRoomFood, "priorityRoomFood");
        Scribe_Values.Look(ref dinnerTimeMode, "dinnerTimeMode");
    }

    public static void InitSetting()
    {
        moodInfluenceX = 0.88f;
        randomLevel = 103f;
        lastFoodInfluenceX = 0.4f;
        llastFoodInfluenceX = 0.6f;
        eatThreshold = 0.25f;
        priorityRoomFood = false;
        dinnerTimeMode = 0;
    }

    public static string ValLevelLabel(float val)
    {
        if (val == moodInfluenceX)
        {
            switch (val)
            {
                case 0.68f:
                    return "Lowest";
                case 0.88f:
                    return "Low";
                case 1.03f:
                    return "Medium";
                case 1.25f:
                    return "High";
                case 1.5f:
                    return "Highest";
            }
        }
        else
        {
            if (val != randomLevel)
            {
                return "ValLevelLabel Error";
            }

            switch (val)
            {
                case 0f:
                    return "Lowest";
                case 50f:
                    return "Low";
                case 103f:
                    return "Medium";
                case 200f:
                    return "High";
                case 300f:
                    return "Highest";
            }
        }

        return "ValLevelLabel Error";
    }

    public static float ValLevelNum(float val)
    {
        if (val == moodInfluenceX)
        {
            switch (val)
            {
                case 0.68f:
                    return 1f;
                case 0.88f:
                    return 2f;
                case 1.03f:
                    return 3f;
                case 1.25f:
                    return 4f;
                case 1.5f:
                    return 5f;
            }
        }
        else
        {
            if (val != randomLevel)
            {
                return -1f;
            }

            switch (val)
            {
                case 0f:
                    return 1f;
                case 50f:
                    return 2f;
                case 103f:
                    return 3f;
                case 200f:
                    return 4f;
                case 300f:
                    return 5f;
            }
        }

        return -1f;
    }

    public static float ValNumLevel(float val, float num)
    {
        if (val == moodInfluenceX)
        {
            switch (num)
            {
                case 1f:
                    return 0.68f;
                case 2f:
                    return 0.88f;
                case 3f:
                    return 1.03f;
                case 4f:
                    return 1.25f;
                case 5f:
                    return 1.5f;
            }
        }
        else
        {
            if (val != randomLevel)
            {
                return -100f;
            }

            switch (num)
            {
                case 1f:
                    return 0f;
                case 2f:
                    return 50f;
                case 3f:
                    return 103f;
                case 4f:
                    return 200f;
                case 5f:
                    return 300f;
            }
        }

        return -100f;
    }

    public static string GetDinnerTimeModeStr()
    {
        switch (dinnerTimeMode)
        {
            case 0:
                return "Dinner_Time_Mode0";
            case 1:
                return "Dinner_Time_Mode1";
            case 2:
                return "Dinner_Time_Mode2";
            default:
                return "dinnerTimeMode null";
        }
    }
}