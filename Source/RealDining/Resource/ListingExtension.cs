using UnityEngine;
using Verse;

namespace YC.RealDining.Resource;

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