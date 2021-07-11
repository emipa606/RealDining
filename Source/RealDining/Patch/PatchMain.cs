using System.Reflection;
using HarmonyLib;
using Verse;

namespace YC.RealDining.Patch
{
    // Token: 0x02000007 RID: 7
    [StaticConstructorOnStartup]
    public class PatchMain
    {
        // Token: 0x0600001A RID: 26 RVA: 0x00002D4C File Offset: 0x00000F4C
        static PatchMain()
        {
            var harmony = new Harmony("YC.RealDining");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}