using HarmonyLib;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class DrawMoveSpeedPatch
{
	private static JObject balance = Terraheim.balance;

	public void Awake()
	{
		Log.LogInfo("Draw Move Speed Patching Complete");
	}

	[HarmonyPatch(typeof(Player), nameof(Humanoid.GetJogSpeedFactor))]
	public static void Postfix(Player __instance, ref float __result)
	{
		if (__instance.GetAttackDrawPercentage() > 0f)
		{
			float num = (float)balance["baseBowDrawMoveSpeeed"];
			SE_DrawMoveSpeed sE_DrawMoveSpeed = UtilityFunctions.GetStatusEffectFromName("Draw Move Speed", __instance.GetSEMan()) as SE_DrawMoveSpeed;
			if (__instance.GetCurrentWeapon().m_shared.m_name.Contains("bow_fireTH"))
			{
				num = 0f;
			}
			if (sE_DrawMoveSpeed != null)
			{
				num += sE_DrawMoveSpeed.GetDrawMoveSpeed();
			}
			__result *= num;
		}
		else if (__instance.GetSEMan().HaveStatusEffect("Bloodrush"))
		{
			SE_MoveSpeedOnKill sE_MoveSpeedOnKill = UtilityFunctions.GetStatusEffectFromName("Bloodrush", __instance.GetSEMan()) as SE_MoveSpeedOnKill;
			__result *= 1f + sE_MoveSpeedOnKill.GetCurrentSpeedBonus();
		}
	}

	[HarmonyPatch(typeof(Player), "GetRunSpeedFactor")]
	[HarmonyPostfix]
	public static void RunPostfix(Player __instance, ref float __result)
	{
		if (__instance.GetSEMan().HaveStatusEffect("Bloodrush"))
		{
			SE_MoveSpeedOnKill sE_MoveSpeedOnKill = UtilityFunctions.GetStatusEffectFromName("Bloodrush", __instance.GetSEMan()) as SE_MoveSpeedOnKill;
			__result *= 1f + sE_MoveSpeedOnKill.GetCurrentSpeedBonus();
		}
	}
}
