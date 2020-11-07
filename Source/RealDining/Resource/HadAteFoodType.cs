using System.Collections.Generic;
using Verse;

namespace YC.RealDining.Resource
{
    // Token: 0x02000002 RID: 2
    public class HadAteFoodType : IExposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string GetHadAteFoodType(Pawn pawn)
		{
            if (map == null)
			{
				map = new Dictionary<string, string>();
			}
            string result;
			if (!map.ContainsKey(pawn.GetUniqueLoadID()))
			{
				result = StrEmpty;
			}
			else
			{
				result = map[pawn.GetUniqueLoadID()];
			}
			return result;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020AC File Offset: 0x000002AC
		public void SetHadAteFoodType(Pawn pawn, string foodType)
		{
            if (map == null)
			{
				map = new Dictionary<string, string>();
			}
			map[pawn.GetUniqueLoadID()] = foodType;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E5 File Offset: 0x000002E5
		public void ExposeData()
		{
			Scribe_Collections.Look(ref map, "LastFoodType_Map", LookMode.Value, LookMode.Value);
		}

		// Token: 0x04000001 RID: 1
		private Dictionary<string, string> map = new Dictionary<string, string>();

		// Token: 0x04000002 RID: 2
		public static readonly string StrEmpty = "-1";
	}
}
