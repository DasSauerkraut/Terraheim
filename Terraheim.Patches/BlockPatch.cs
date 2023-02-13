using HarmonyLib;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class BlockPatch
{
	private static JObject balance = Terraheim.balance;

	private static float baseStaminaUse = (float)balance["baseBlockStaminaUse"];

	public void Awake()
	{
		Log.LogInfo("Block Patching Complete");
	}

	[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
	private static void Prefix(ref float ___m_blockStaminaDrain, ref SEMan ___m_seman)
	{
		if (___m_seman.HaveStatusEffect("Block Stamina Use") && (___m_seman.m_character as Humanoid).GetCurrentWeapon() != (___m_seman.m_character as Humanoid).m_unarmedWeapon.m_itemData)
		{
			SE_BlockStamUse sE_BlockStamUse = ___m_seman.GetStatusEffect("Block Stamina Use") as SE_BlockStamUse;
			___m_blockStaminaDrain = baseStaminaUse * (1f - sE_BlockStamUse.getBlockStaminaUse());
		}
		else if (___m_blockStaminaDrain != baseStaminaUse)
		{
			___m_blockStaminaDrain = baseStaminaUse;
		}
	}

	[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
	private static void Postfix(HitData hit, Character attacker, ref SEMan ___m_seman, float ___m_blockTimer)
	{
		if (___m_seman.HaveStatusEffect("Heal On Block") && (bool)(___m_seman.m_character as Humanoid) && (___m_seman.m_character as Humanoid).m_leftItem != (___m_seman.m_character as Humanoid).m_unarmedWeapon.m_itemData && (___m_seman.m_character as Humanoid).m_leftItem != null)
		{
			SE_HealOnBlock sE_HealOnBlock = ___m_seman.GetStatusEffect("Heal On Block") as SE_HealOnBlock;
			ItemDrop.ItemData leftItem = (___m_seman.m_character as Humanoid).m_leftItem;
			bool flag = ___m_blockTimer != -1f && (float)balance["perfectBlockWindow"] >= ___m_blockTimer;
			if (leftItem.m_shared.m_name.Contains("tower") || leftItem.m_shared.m_name.Contains("shield_serpentscale"))
			{
				if ((___m_seman.m_character as Humanoid).HaveStamina() || flag)
				{
					float hp = leftItem.GetBaseBlockPower() * sE_HealOnBlock.GetBlockHeal();
					Log.LogInfo("Terraheim | Heal on Block: Block Power: " + leftItem.GetBaseBlockPower() + " Heal Amount: " + hp);
					___m_seman.m_character.Heal(hp);
				}
			}
			else if (leftItem.m_shared.m_name.Contains("shield") && flag)
			{
				float hp2 = leftItem.GetBaseBlockPower() * sE_HealOnBlock.GetBlockHeal() + leftItem.GetBaseBlockPower() * leftItem.m_shared.m_timedBlockBonus * sE_HealOnBlock.GetBlockHeal();
				Log.LogInfo("Terraheim | Heal on Parry: Block Power: " + leftItem.GetBaseBlockPower() + " Parry Bonus: " + leftItem.m_shared.m_timedBlockBonus + " Heal Amount: " + hp2);
				___m_seman.m_character.Heal(hp2);
			}
		}
		if (___m_seman.HaveStatusEffect("Chosen"))
		{
			SE_Chosen sE_Chosen = ___m_seman.GetStatusEffect("Chosen") as SE_Chosen;
			if (___m_blockTimer != -1f && (float)balance["perfectBlockWindow"] >= ___m_blockTimer)
			{
				sE_Chosen.OnParry();
			}
		}
		if (___m_seman.HaveStatusEffect("ShieldFireParryListener"))
		{
			Log.LogInfo("Has Effect");
			if (___m_blockTimer != -1f && (float)balance["perfectBlockWindow"] >= ___m_blockTimer && !(attacker as Humanoid).GetSEMan().HaveStatusEffect("AfterburnFire") && !(attacker as Humanoid).GetSEMan().HaveStatusEffect("Afterburn Cooldown"))
			{
				Log.LogInfo("Adding Afterburn");
				SE_Afterburn sE_Afterburn = ScriptableObject.CreateInstance<SE_Afterburn>();
				attacker.GetSEMan().AddStatusEffect((StatusEffect)sE_Afterburn, false);
			}
		}
	}
}
