using System.Reflection;
using HarmonyLib;
using Verse;

namespace YC.RealDining.Patch;

[StaticConstructorOnStartup]
public class PatchMain
{
    static PatchMain()
    {
        var harmony = new Harmony("YC.RealDining");
        harmony.PatchAll(Assembly.GetExecutingAssembly());
    }
}