using Terraheim.ArmorEffects;
using HarmonyLib;
using ValheimLib;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
    public static class BlockPowerPatch
    {
        public static void Prefix(ref Humanoid __instance)
        {
            if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Block Power Bonus"))
            {
                SE_BlockPowerBonus effect = __instance.GetSEMan().GetStatusEffect("Block Power Bonus") as SE_BlockPowerBonus;
                Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_blockPower);
                var baseWeapon = Prefab.Cache.GetPrefab<ItemDrop>(__instance.GetCurrentBlocker().m_dropPrefab.name);
                __instance.GetCurrentBlocker().m_shared.m_blockPower = baseWeapon.m_itemData.m_shared.m_blockPower * (1+effect.GetBlockPower());
                Log.LogWarning("Human " + __instance.GetCurrentBlocker().m_shared.m_blockPower);
            }
        }
    }
}
