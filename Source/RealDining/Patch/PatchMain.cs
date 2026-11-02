using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace YC.RealDining.Patch;

[StaticConstructorOnStartup]
public static class PatchMain
{
    public static readonly int AmountOfTimeTypes;
    public static readonly IEnumerable<TimeAssignmentDef> NonRimworldTimeTypes;

    static PatchMain()
    {
        new Harmony("YC.RealDining").PatchAll(Assembly.GetExecutingAssembly());
        AmountOfTimeTypes =
            DefDatabase<TimeAssignmentDef>.AllDefsListForReading.Count(def =>
                def.modContentPack is { IsOfficialMod: true });

        NonRimworldTimeTypes =
            DefDatabase<TimeAssignmentDef>.AllDefsListForReading
                .Where(def => def.modContentPack is not { IsOfficialMod: true })
                .OrderBy(def => def.label ?? string.Empty)
                .ToList();
    }
}