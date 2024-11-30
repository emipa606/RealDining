using System.Linq;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPriority(Priority.Last)]
[HarmonyPatch(typeof(TimeAssignmentSelector), "DrawTimeAssignmentSelectorGrid")]
internal class TimeAssignmentSelector_DrawTimeAssignmentSelectorGrid
{
    private static void Prepare()
    {
        PatchMain.harmony.Unpatch(typeof(TimeAssignmentSelector).GetMethod("DrawTimeAssignmentSelectorGrid"),
            HarmonyPatchType.All);
    }

    private static void Prefix(ref Rect rect)
    {
        var shorternBy = rect.width * 0.135f;
        for (var i = 0; i < PatchMain.AmountOfTimeTypes + PatchMain.NonRimworldTimeTypes.Count(); i++)
        {
            if (i > 7)
            {
                rect.width -= shorternBy;
            }
        }
    }

    private static void Postfix(Rect rect)
    {
        var rect2 = rect;
        rect2.xMax = rect2.center.x;
        rect2.yMax = rect2.center.y;
        rect2.x += rect2.width * PatchMain.AmountOfTimeTypes;
        foreach (var nonRimworldTimeType in PatchMain.NonRimworldTimeTypes)
        {
            DrawTimeAssignmentSelectorFor(rect2, nonRimworldTimeType);
            rect2.x += rect2.width;
        }
    }

    private static void DrawTimeAssignmentSelectorFor(Rect rect, TimeAssignmentDef ta)
    {
        rect = rect.ContractedBy(2f);
        GUI.DrawTexture(rect, ta.ColorTexture);
        if (Widgets.ButtonInvisible(rect))
        {
            TimeAssignmentSelector.selectedAssignment = ta;
            SoundDefOf.Tick_High.PlayOneShotOnCamera();
        }

        GUI.color = Color.white;
        if (Mouse.IsOver(rect))
        {
            Widgets.DrawHighlight(rect);
        }

        Text.Font = GameFont.Small;
        Text.Anchor = TextAnchor.MiddleCenter;
        GUI.color = Color.white;
        Widgets.Label(rect, ta.LabelCap);
        Text.Anchor = TextAnchor.UpperLeft;
        if (TimeAssignmentSelector.selectedAssignment == ta)
        {
            Widgets.DrawBox(rect, 2);
        }
    }
}