using Terraheim.ArmorEffects;
using HarmonyLib;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
    public static class BlockPowerPatch
    {
        public static void Prefix(ref Humanoid __instance)
        {
            if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Block Power Bonus") && __instance.GetCurrentWeapon() != __instance.m_unarmedWeapon.m_itemData)
            {
                SE_BlockPowerBonus effect = __instance.GetSEMan().GetStatusEffect("Block Power Bonus") as SE_BlockPowerBonus;
                //Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_blockPower);
                var baseWeapon = PrefabManager.Cache.GetPrefab<ItemDrop>(__instance.GetCurrentBlocker().m_dropPrefab.name);
                __instance.GetCurrentBlocker().m_shared.m_blockPower = baseWeapon.m_itemData.m_shared.m_blockPower * (1+effect.GetBlockPower());
                //Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_blockPower);
            }
            if(__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Parry Bonus Increase") && __instance.GetCurrentWeapon() != __instance.m_unarmedWeapon.m_itemData)
            {
                Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_blockPower);
                SE_ParryBonus effect = __instance.GetSEMan().GetStatusEffect("Parry Bonus Increase") as SE_ParryBonus;
                var baseWeapon = PrefabManager.Cache.GetPrefab<ItemDrop>(__instance.GetCurrentBlocker().m_dropPrefab.name);
                __instance.GetCurrentBlocker().m_shared.m_blockPower = baseWeapon.m_itemData.m_shared.m_timedBlockBonus * (1 + effect.GetParryBonus());
                Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_blockPower);
            }
        }
    }
}
