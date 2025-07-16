using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace YC.RealDining.Patch;

[StaticConstructorOnStartup]
public class PatchMain
{
    public static readonly int AmountOfTimeTypes;
    public static readonly IEnumerable<TimeAssignmentDef> NonRimworldTimeTypes;

    static PatchMain()
    {
        new Harmony("YC.RealDining").PatchAll(Assembly.GetExecutingAssembly());
        AmountOfTimeTypes =
            DefDatabase<TimeAssignmentDef>.AllDefsListForReading.Count(def => def.modContentPack.IsOfficialMod);

        NonRimworldTimeTypes =
            DefDatabase<TimeAssignmentDef>.AllDefsListForReading.Where(def => !def.modContentPack.IsOfficialMod)
                .OrderBy(def => def.label);
    }
}