using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;
using YC.RealDining.Resource;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout
{
    // Token: 0x02000013 RID: 19
    [HarmonyPatch(typeof(TimeAssignmentSelector))]
    [HarmonyPatch("DrawTimeAssignmentSelectorGrid")]
    [HarmonyPatch(new[]
    {
        typeof(Rect)
    })]
    internal class Patch_DrawTimeAssignmentSelectorGrid
    {
        // Token: 0x06000038 RID: 56 RVA: 0x00003E30 File Offset: 0x00002030
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

        // Token: 0x06000039 RID: 57 RVA: 0x00003F04 File Offset: 0x00002104
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
}