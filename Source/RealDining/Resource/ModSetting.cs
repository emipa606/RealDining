using UnityEngine;
using Verse;

namespace YC.RealDining.Resource;

public class ModSetting : ModSettings
{
    private const float moodInfluenceXDefault = 0.88f;

    private const float randomLevelDefault = 103f;

    private const float lastFoodInfluenceXDefault = 0.4f;

    private const float llastFoodInfluenceXDefault = 0.6f;

    private const float eatThresholdDefault = 0.25f;

    public const int dinnerTimeModeDefault = 0;

    private const float randomLevelHighest = 300f;

    private const float randomLevelHigh = 200f;

    private const float randomLevelMid = 103f;

    private const float randomLevelLow = 50f;

    private const float randomLevelLowest = 0f;

    private const float moodInfLevelHighest = 1.5f;

    private const float moodInfLevelHigh = 1.25f;

    private const float moodInfLevelMid = 1.03f;

    private const float moodInfLevelLow = 0.88f;

    private const float moodInfLevelLowest = 0.68f;

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

    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref moodInfluenceX, "moodInfluenceX", moodInfluenceXDefault);
        Scribe_Values.Look(ref randomLevel, "randomLevel", randomLevelDefault);
        Scribe_Values.Look(ref lastFoodInfluenceX, "lastFoodInfluenceX", lastFoodInfluenceXDefault);
        Scribe_Values.Look(ref llastFoodInfluenceX, "llastFoodInfluenceX", llastFoodInfluenceXDefault);
        Scribe_Values.Look(ref eatThreshold, "eatThreshold", eatThresholdDefault);
        Scribe_Values.Look(ref priorityRoomFood, "priorityRoomFood");
    }

    public static void InitSetting()
    {
        moodInfluenceX = 0.88f;
        randomLevel = 103f;
        lastFoodInfluenceX = 0.4f;
        llastFoodInfluenceX = 0.6f;
        eatThreshold = 0.25f;
        priorityRoomFood = false;
    }

    public static string ValLevelLabel(float val)
    {
        if (val.Equals(moodInfluenceX))
        {
            switch (val)
            {
                case moodInfLevelLowest:
                    return "Lowest";
                case moodInfLevelLow:
                    return "Low";
                case moodInfLevelMid:
                    return "Medium";
                case moodInfLevelHigh:
                    return "High";
                case moodInfLevelHighest:
                    return "Highest";
            }
        }
        else
        {
            if (!val.Equals(randomLevel))
            {
                return "ValLevelLabel Error";
            }

            switch (val)
            {
                case randomLevelLowest:
                    return "Lowest";
                case randomLevelLow:
                    return "Low";
                case randomLevelMid:
                    return "Medium";
                case randomLevelHigh:
                    return "High";
                case randomLevelHighest:
                    return "Highest";
            }
        }

        return "ValLevelLabel Error";
    }

    public static float ValLevelNum(float val)
    {
        if (val.Equals(moodInfluenceX))
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
            if (!val.Equals(randomLevel))
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
        if (val.Equals(moodInfluenceX))
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
            if (!val.Equals(randomLevel))
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
}