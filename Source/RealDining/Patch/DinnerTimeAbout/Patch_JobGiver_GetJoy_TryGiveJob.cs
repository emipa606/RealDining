using System;
using System.Collections.Generic;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using YC.RealDining.Resource.DefClass;

namespace YC.RealDining.Patch.DinnerTimeAbout
{
	// Token: 0x0200000D RID: 13
	[HarmonyPatch(typeof(JobGiver_GetJoy))]
	[HarmonyPatch("TryGiveJob")]
	[HarmonyPatch(new Type[]
	{
		typeof(Pawn)
	})]
	internal class Patch_JobGiver_GetJoy_TryGiveJob
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003514 File Offset: 0x00001714
		[HarmonyPrefix]
		private static bool Prefix(JobGiver_GetJoy __instance, ref Job __result, Pawn pawn)
		{
			TimeAssignmentDef timeAssignmentDef = (pawn.timetable == null) ? TimeAssignmentDefOf.Anything : pawn.timetable.CurrentAssignment;
			var flag = timeAssignmentDef != TimeAssignmentDefDinner.DinnerDef;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				var flag2 = pawn.InBed() && HealthAIUtility.ShouldSeekMedicalRest(pawn);
				if (flag2)
				{
					__result = null;
					result = false;
				}
				else
				{
					List<JoyGiverDef> allDefsListForReading = DefDatabase<JoyGiverDef>.AllDefsListForReading;
					JoyToleranceSet tolerances = pawn.needs.joy.tolerances;
					for (var i = 0; i < allDefsListForReading.Count; i++)
					{
						JoyGiverDef joyGiverDef = allDefsListForReading[i];
                        joyGiverChances[joyGiverDef] = 0f;
						var flag3 = joyGiverDef.joyKind != JoyKindDefOf.Gluttonous && joyGiverDef.defName != "SocialRelax";
						if (!flag3)
						{
							var flag4 = !pawn.needs.joy.tolerances.BoredOf(joyGiverDef.joyKind) && joyGiverDef.Worker.CanBeGivenTo(pawn);
							if (flag4)
							{
								var flag5 = joyGiverDef.pctPawnsEverDo < 1f;
								if (flag5)
								{
									Rand.PushState(pawn.thingIDNumber ^ 63216713);
									var flag6 = Rand.Value >= joyGiverDef.pctPawnsEverDo;
									if (flag6)
									{
										Rand.PopState();
										goto IL_196;
									}
									Rand.PopState();
								}
								var num = tolerances[joyGiverDef.joyKind];
								var num2 = Mathf.Pow(1f - num, 5f);
								num2 = Mathf.Max(0.001f, num2);
                                joyGiverChances[joyGiverDef] = joyGiverDef.Worker.GetChance(pawn) * num2;
							}
							IL_196:;
						}
					}
					var num3 = 0;
					Job job;
					for (;;)
					{
						var joyGiverDef2 = new JoyGiverDef();
						bool flag7;
						if (num3 < joyGiverChances.Count)
						{
							flag7 = allDefsListForReading.TryRandomElementByWeight((JoyGiverDef d) => joyGiverChances[d], out joyGiverDef2);
						}
						else
						{
							flag7 = false;
						}
						if (!flag7)
						{
							goto Block_17;
						}
						var flag8 = pawn.needs.joy.CurLevel < 0.95f || joyGiverDef2.joyKind != JoyKindDefOf.Gluttonous;
						if (flag8)
						{
							job = TryGiveJobFromJoyGiverDefDirect(joyGiverDef2, pawn);
							var flag9 = job != null;
							if (flag9)
							{
								break;
							}
						}
                        joyGiverChances[joyGiverDef2] = 0f;
						num3++;
					}
					__result = job;
					return false;
					Block_17:
					__result = null;
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000378C File Offset: 0x0000198C
		protected static Job TryGiveJobFromJoyGiverDefDirect(JoyGiverDef def, Pawn pawn)
		{
			return def.Worker.TryGiveJob(pawn);
		}

		// Token: 0x04000025 RID: 37
		private static readonly DefMap<JoyGiverDef, float> joyGiverChances = new DefMap<JoyGiverDef, float>();
	}
}
