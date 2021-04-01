using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
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

        [HarmonyPatch(typeof(MineRock), "Damage")]
        static void Prefix(ref HitData hit)
        {
            if (!hit.GetAttacker().IsPlayer())
                return;

            if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Mining Bonus"))
            {
                SE_MiningBonus effect = hit.GetAttacker().GetSEMan().GetStatusEffect("Mining Bonus") as SE_MiningBonus;
                hit.m_damage.Modify(1 + effect.getDamageBonus());
            }
        }
        
        [HarmonyPatch(typeof(MineRock5), "Damage")]
        [HarmonyPrefix]
        static void RockHitPrefix(ref HitData hit)
        {
            if (!hit.GetAttacker().IsPlayer())
                return;

            if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Mining Bonus"))
            {
                SE_MiningBonus effect = hit.GetAttacker().GetSEMan().GetStatusEffect("Mining Bonus") as SE_MiningBonus;
                hit.m_damage.Modify(1 + effect.getDamageBonus());
            }
        }
    }
}
