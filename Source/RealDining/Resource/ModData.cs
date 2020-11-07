using System;
using System.Collections.Generic;
using Verse;

namespace YC.RealDining.Resource
{
	// Token: 0x02000004 RID: 4
	public class ModData : GameComponent
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000256C File Offset: 0x0000076C
		public ModData(Game game)
		{
			this.game = game;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000257D File Offset: 0x0000077D
		public ModData()
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002588 File Offset: 0x00000788
		public static string GetLastFoodType(Pawn pawn)
		{
            if (lastFoodType == null)
			{
                lastFoodType = new HadAteFoodType();
			}
			return lastFoodType.GetHadAteFoodType(pawn);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000025BC File Offset: 0x000007BC
		public static void SetLastFoodType(Pawn pawn, string foodType)
		{
            if (lastFoodType == null)
			{
                lastFoodType = new HadAteFoodType();
			}
            lastFoodType.SetHadAteFoodType(pawn, foodType);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000025F0 File Offset: 0x000007F0
		public static string GetLlastFoodType(Pawn pawn)
		{
            if (llastFoodType == null)
			{
                llastFoodType = new HadAteFoodType();
			}
			return llastFoodType.GetHadAteFoodType(pawn);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002624 File Offset: 0x00000824
		public static void SetLlastFoodType(Pawn pawn, string foodType)
		{
            if (llastFoodType == null)
			{
                llastFoodType = new HadAteFoodType();
			}
            llastFoodType.SetHadAteFoodType(pawn, foodType);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002655 File Offset: 0x00000855
		public override void ExposeData()
		{
			Scribe_Deep.Look(ref lastFoodType, "lastFoodType", Array.Empty<object>());
			Scribe_Deep.Look(ref llastFoodType, "llastFoodType", Array.Empty<object>());
		}

		// Token: 0x04000003 RID: 3
		public static HadAteFoodType lastFoodType = new HadAteFoodType();

		// Token: 0x04000004 RID: 4
		public static HadAteFoodType llastFoodType = new HadAteFoodType();

		// Token: 0x04000005 RID: 5
		public static Dictionary<string, float> foodClassRandomVal = new Dictionary<string, float>();

		// Token: 0x04000006 RID: 6
		public static string findedInventoryFoodID;

		// Token: 0x04000007 RID: 7
		public Game game;
	}
}
