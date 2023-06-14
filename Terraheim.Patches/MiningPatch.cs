using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class MiningPatch
    {
        public void Awake()
        {
            Log.LogInfo("Block Patching Complete");
        }

        [HarmonyPatch(typeof(MineRock), nameof(MineRock.Damage))]
        static void Prefix(ref HitData hit)
        {
            if (hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit.GetAttacker().m_seman == null)
                return;

            if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Mining Bonus"))
            {
                SE_MiningBonus effect = UtilityFunctions.GetStatusEffectFromName("Mining Bonus", hit.GetAttacker().GetSEMan()) as SE_MiningBonus;
                hit.m_damage.Modify(1 + effect.getDamageBonus());
            }
        }
        
        [HarmonyPatch(typeof(MineRock5), nameof(MineRock5.Damage))]
        [HarmonyPrefix]
        static void RockHitPrefix(ref HitData hit)
        {
            if (hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit.GetAttacker().m_seman == null)
                return;

            if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Mining Bonus"))
            {
                SE_MiningBonus effect = UtilityFunctions.GetStatusEffectFromName("Mining Bonus", hit.GetAttacker().GetSEMan()) as SE_MiningBonus;
                hit.m_damage.Modify(1 + effect.getDamageBonus());
            }
        }
    }
}
