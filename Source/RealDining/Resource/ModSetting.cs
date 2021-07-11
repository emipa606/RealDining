using UnityEngine;
using Verse;

namespace YC.RealDining.Resource
{
    // Token: 0x02000005 RID: 5
    public class ModSetting : ModSettings
    {
        // Token: 0x04000008 RID: 8
        public const float moodInfluenceXDefault = 0.88f;

        // Token: 0x04000009 RID: 9
        public const float randomLevelDefault = 103f;

        // Token: 0x0400000A RID: 10
        public const float lastFoodInfluenceXDefault = 0.4f;

        // Token: 0x0400000B RID: 11
        public const float llastFoodInfluenceXDefault = 0.6f;

        // Token: 0x0400000C RID: 12
        public const float eatThresholdDefault = 0.25f;

        // Token: 0x0400000D RID: 13
        public const int dinnerTimeModeDefault = 0;

        // Token: 0x04000011 RID: 17
        public const float randomLevelHighest = 300f;

        // Token: 0x04000012 RID: 18
        public const float randomLevelHigh = 200f;

        // Token: 0x04000013 RID: 19
        public const float randomLevelMid = 103f;

        // Token: 0x04000014 RID: 20
        public const float randomLevelLow = 50f;

        // Token: 0x04000015 RID: 21
        public const float randomLevelLowest = 0f;

        // Token: 0x04000016 RID: 22
        public const float moodInfLevelHighest = 1.5f;

        // Token: 0x04000017 RID: 23
        public const float moodInfLevelHigh = 1.25f;

        // Token: 0x04000018 RID: 24
        public const float moodInfLevelMid = 1.03f;

        // Token: 0x04000019 RID: 25
        public const float moodInfLevelLow = 0.88f;

        // Token: 0x0400001A RID: 26
        public const float moodInfLevelLowest = 0.68f;

        // Token: 0x0400000E RID: 14
        public static string[] levelLabel =
        {
            "",
            "Lowest",
            "Low",
            "Medium",
            "High",
            "Highest"
        };

        // Token: 0x0400000F RID: 15
        public static float[] moodLevelVal =
        {
            0f,
            0.68f,
            0.88f,
            1.03f,
            1.25f,
            1.5f
        };

        // Token: 0x04000010 RID: 16
        public static float[] randomLevelVal =
        {
            0f,
            0f,
            50f,
            103f,
            200f,
            300f
        };

        // Token: 0x0400001B RID: 27
        public static float moodInfluenceX = 0.88f;

        // Token: 0x0400001C RID: 28
        public static float randomLevel = 103f;

        // Token: 0x0400001D RID: 29
        public static float lastFoodInfluenceX = 0.4f;

        // Token: 0x0400001E RID: 30
        public static float llastFoodInfluenceX = 0.6f;

        // Token: 0x0400001F RID: 31
        public static float eatThreshold = 0.25f;

        // Token: 0x04000020 RID: 32
        public static Vector2 scrollPos = Vector2.zero;

        // Token: 0x04000021 RID: 33
        public static bool priorityRoomFood;

        // Token: 0x04000022 RID: 34
        public static int dinnerTimeMode;

        // Token: 0x06000011 RID: 17 RVA: 0x000026A4 File Offset: 0x000008A4
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

        // Token: 0x06000012 RID: 18 RVA: 0x0000274C File Offset: 0x0000094C
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

        // Token: 0x06000013 RID: 19 RVA: 0x00002798 File Offset: 0x00000998
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

        // Token: 0x06000014 RID: 20 RVA: 0x00002920 File Offset: 0x00000B20
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

        // Token: 0x06000015 RID: 21 RVA: 0x00002AA8 File Offset: 0x00000CA8
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

        // Token: 0x06000016 RID: 22 RVA: 0x00002C30 File Offset: 0x00000E30
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
}