using HarmonyLib;
using Jotunn.Managers;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches;

[HarmonyPatch(typeof(Humanoid), nameof(Humanoid.BlockAttack))]
public static class BlockPowerPatch
{
	public static void Prefix(ref Humanoid __instance)
	{
		if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Block Power Bonus") && __instance.GetCurrentWeapon() != __instance.m_unarmedWeapon.m_itemData)
		{
			SE_BlockPowerBonus sE_BlockPowerBonus = UtilityFunctions.GetStatusEffectFromName("Block Power Bonus", __instance.GetSEMan()) as SE_BlockPowerBonus;
			ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>(__instance.GetCurrentBlocker().m_dropPrefab.name);
			__instance.GetCurrentBlocker().m_shared.m_blockPower = prefab.m_itemData.m_shared.m_blockPower * (1f + sE_BlockPowerBonus.GetBlockPower());
		}
		if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Parry Bonus Increase") && __instance.GetCurrentWeapon() != __instance.m_unarmedWeapon.m_itemData)
		{
			Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_timedBlockBonus);
			SE_ParryBonus sE_ParryBonus = UtilityFunctions.GetStatusEffectFromName("Parry Bonus Increase", __instance.GetSEMan()) as SE_ParryBonus;
			ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>(__instance.GetCurrentBlocker().m_dropPrefab.name);
			Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_timedBlockBonus);
			__instance.GetCurrentBlocker().m_shared.m_timedBlockBonus = prefab2.m_itemData.m_shared.m_timedBlockBonus * (1f + sE_ParryBonus.GetParryBonus());
			Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_timedBlockBonus);
		}
	}
}
