using HarmonyLib;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class DodgePatch
{
	private static JObject balance = Terraheim.balance;

	private static float baseStaminaUse = (float)balance["baseDodgeStaminaUse"];

	public void Awake()
	{
		Log.LogInfo("Dodge Patching Complete");
	}

	[HarmonyPatch(typeof(Player), nameof(Player.Dodge))]
	private static void Prefix(ref float ___m_dodgeStaminaUsage, ref SEMan ___m_seman)
	{
		if (___m_seman.HaveStatusEffect("Dodge Stamina Use"))
		{
			SE_DodgeStamUse sE_DodgeStamUse = ___m_seman.GetStatusEffect("Dodge Stamina Use") as SE_DodgeStamUse;
			___m_dodgeStaminaUsage = baseStaminaUse * (1f - sE_DodgeStamUse.getDodgeStaminaUse());
		}
		else if (___m_seman.HaveStatusEffect("Challenge Dodge Bonus"))
		{
			SE_ChallengeDodgeBonus sE_ChallengeDodgeBonus = ___m_seman.GetStatusEffect("Challenge Dodge Bonus") as SE_ChallengeDodgeBonus;
			___m_dodgeStaminaUsage = baseStaminaUse * (1f - sE_ChallengeDodgeBonus.GetDodgeBonus());
		}
		else if (___m_dodgeStaminaUsage != baseStaminaUse)
		{
			___m_dodgeStaminaUsage = baseStaminaUse;
		}
	}
}
