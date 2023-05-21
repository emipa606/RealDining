using System;
using System.Collections.Generic;
using Mlie;
using UnityEngine;
using Verse;

namespace YC.RealDining.Resource;

public class ModMain : Mod
{
    private static string currentVersion;

    public ModMain(ModContentPack content) : base(content)
    {
        GetSettings<ModSetting>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    public override string SettingsCategory()
    {
        return "RealDining_Mod_Settings".Translate();
    }


    public override void DoSettingsWindowContents(Rect inRect)
    {
        var rect = new Rect(0f, 0f, inRect.width * 0.98f, inRect.height);
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
            (float)Math.Round(listing_Standard.Slider(ModSetting.lastFoodInfluenceX * 10f, 0f, 10f)) / 10f;
        listing_Standard.Label("Repeated_food_selection_value2".Translate() + "   " +
                               (ModSetting.llastFoodInfluenceX * 10f).ToString());
        ModSetting.llastFoodInfluenceX =
            (float)Math.Round(listing_Standard.Slider(ModSetting.llastFoodInfluenceX * 10f, 0f, 10f)) / 10f;
        listing_Standard.Label("Repeated_value_explain".Translate());
        listing_Standard.Label("Repeated_value_explain2".Translate());
        listing_Standard.GapLine(20f);
        listing_Standard.Label("Eat_threshold".Translate() + "   " + ModSetting.eatThreshold.ToString());
        ModSetting.eatThreshold = (float)Math.Round(listing_Standard.Slider(ModSetting.eatThreshold, 0f, 0.7f), 2);
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

        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("RealDining_Version".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.EndScrollView(inRect.width, ref rect);
    }
}