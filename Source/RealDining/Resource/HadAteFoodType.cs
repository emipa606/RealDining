using System.Collections.Generic;
using Verse;

namespace YC.RealDining.Resource;

public class HadAteFoodType : IExposable
{
    public static readonly string StrEmpty = "-1";

    private Dictionary<string, string> map = new Dictionary<string, string>();

    public void ExposeData()
    {
        Scribe_Collections.Look(ref map, "LastFoodType_Map", LookMode.Value, LookMode.Value);
    }

    public string GetHadAteFoodType(Pawn pawn)
    {
        if (map == null)
        {
            map = new Dictionary<string, string>();
        }

        var result = !map.ContainsKey(pawn.GetUniqueLoadID()) ? StrEmpty : map[pawn.GetUniqueLoadID()];

        return result;
    }

    public void SetHadAteFoodType(Pawn pawn, string foodType)
    {
        if (map == null)
        {
            map = new Dictionary<string, string>();
        }

        map[pawn.GetUniqueLoadID()] = foodType;
    }
}