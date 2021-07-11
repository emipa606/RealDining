using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace YC.RealDining.Resource
{
    public static class ListingExtension
    {
        public static void BeginScrollView(this Listing_Standard listingStandard, Rect rect,
            ref Vector2 scrollPosition, ref Rect viewRect)
        {
            Widgets.BeginScrollView(rect, ref scrollPosition, viewRect);
            rect.height = 100000f;
            rect.width -= 20f;
            listingStandard.Begin(rect.AtZero());
        }

        public static void EndScrollView(this Listing_Standard listingStandard, float width, ref Rect viewRect)
        {
            viewRect = new Rect(0f, 0f, width, listingStandard.CurHeight);
            Widgets.EndScrollView();
            listingStandard.End();
        }
    }

    // Token: 0x02000003 RID: 3
    public class ModMain : Mod
    {
        // Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
        public ModMain(ModContentPack content) : base(content)
        {
            GetSettings<ModSetting>();
        }

        // Token: 0x06000007 RID: 7 RVA: 0x00002130 File Offset: 0x00000330
        public override string SettingsCategory()
        {
            return "RealDining_Mod_Settings".Translate();
        }


        // Token: 0x06000008 RID: 8 RVA: 0x00002154 File Offset: 0x00000354
        public override void DoSettingsWindowContents(Rect inRect)
        {
            var rect = new Rect(0f, 0f, inRect.width *= 0.94f, inRect.height);
            var listing_Standard = new Listing_Standard();

            listing_Standard.BeginScrollView(inRect, ref ModSetting.scrollPos, ref rect);
            if (listing_Standard.ButtonText("Restore_default".Translate()))
            {
                ModSetting.InitSetting();
            }

            listing_Standard.GapLine(20f);
            if (listing_Standard.ButtonTextLabeled("Random_degree_of_food_selection".Translate(),
                ModSetting.ValLevelLabel(ModSetting.randomLevel).Translate()))
            {
                var list = new List<FloatMenuOption>();
                for (var i = 1; i <= 5; i++)
                {
                    var tmp = i;
                    list.Add(new FloatMenuOption(ModSetting.levelLabel[i].Translate(),
                        delegate { ModSetting.randomLevel = ModSetting.randomLevelVal[tmp]; }));
                }

                Find.WindowStack.Add(new FloatMenu(list));
            }

            listing_Standard.Label("Random_degree_explain".Translate());
            listing_Standard.GapLine(20f);
            if (listing_Standard.ButtonTextLabeled("The_importance_of_the_mood".Translate(),
                ModSetting.ValLevelLabel(ModSetting.moodInfluenceX).Translate()))
            {
                var list2 = new List<FloatMenuOption>();
                for (var j = 1; j <= 5; j++)
                {
                    var tmp = j;
                    list2.Add(new FloatMenuOption(ModSetting.levelLabel[j].Translate(),
                        delegate { ModSetting.moodInfluenceX = ModSetting.moodLevelVal[tmp]; }));
                }

                Find.WindowStack.Add(new FloatMenu(list2));
            }

            listing_Standard.Label("Mood_degree_explain".Translate());
            listing_Standard.GapLine(20f);
            listing_Standard.Label("Repeated_food_selection_value".Translate() + "   " +
                                   (ModSetting.lastFoodInfluenceX * 10f).ToString());
            ModSetting.lastFoodInfluenceX =
                (float) Math.Round(listing_Standard.Slider(ModSetting.lastFoodInfluenceX * 10f, 0f, 10f)) / 10f;
            listing_Standard.Label("Repeated_food_selection_value2".Translate() + "   " +
                                   (ModSetting.llastFoodInfluenceX * 10f).ToString());
            ModSetting.llastFoodInfluenceX =
                (float) Math.Round(listing_Standard.Slider(ModSetting.llastFoodInfluenceX * 10f, 0f, 10f)) / 10f;
            listing_Standard.Label("Repeated_value_explain".Translate());
            listing_Standard.Label("Repeated_value_explain2".Translate());
            listing_Standard.GapLine(20f);
            listing_Standard.Label("Eat_threshold".Translate() + "   " + ModSetting.eatThreshold.ToString());
            ModSetting.eatThreshold = (float) Math.Round(listing_Standard.Slider(ModSetting.eatThreshold, 0f, 0.7f), 2);
            listing_Standard.Label("Eat_threshold_explain".Translate());
            listing_Standard.GapLine(20f);
            if (listing_Standard.ButtonTextLabeled("Priority_NotInventory_Food".Translate(),
                ModSetting.priorityRoomFood.ToStringYesNo()))
            {
                ModSetting.priorityRoomFood = !ModSetting.priorityRoomFood;
            }

            if (listing_Standard.ButtonTextLabeled("Dinner_Time_Mode".Translate(),
                ModSetting.GetDinnerTimeModeStr().Translate()))
            {
                ModSetting.dinnerTimeMode++;
                ModSetting.dinnerTimeMode %= 3;
            }

            listing_Standard.EndScrollView(inRect.width, ref rect);
        }
    }
}