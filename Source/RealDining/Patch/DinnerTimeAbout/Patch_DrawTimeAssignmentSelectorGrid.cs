using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using YC.RealDining.Resource;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout;

[HarmonyPatch(typeof(TimeAssignmentSelector))]
[HarmonyPatch("DrawTimeAssignmentSelectorGrid")]
[HarmonyPatch(new[]
{
    typeof(Rect)
})]
internal class Patch_DrawTimeAssignmentSelectorGrid
{
    [HarmonyPrefix]
    private static bool Prefix(Rect rect)
    {
        rect.yMax -= 2f;
        var rect2 = rect;
        rect2.xMax = rect2.center.x;
        rect2.yMax = rect2.center.y;
        if (ModSetting.dinnerTimeMode == 0)
        {
            rect2.x += rect2.width * 3f;
        }
        else
        {
            if (ModSetting.dinnerTimeMode == 1)
            {
                rect2.x += rect2.width * 4f;
            }
            else
            {
                rect2.x += rect2.width * 6f;
            }
        }

        DrawTimeAssignmentSelectorFor(rect2, TimeAssignmentDefDinner.DinnerDef);
        return true;
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