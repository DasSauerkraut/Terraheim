﻿using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class TreeDamagePatch
    {
        //static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        //static float baseStaminaUse = (float)balance["baseBlockStaminaUse"];
        public void Awake()
        {
            Log.LogInfo("Block Patching Complete");
        }

        [HarmonyPatch(typeof(TreeBase), nameof(TreeBase.Damage))]
        static void Prefix(ref HitData hit)
        {
            if (hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit.GetAttacker().m_seman == null)
                return;

            //Log.LogWarning("Hit Tree!");
            if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Tree Damage Bonus"))
            {
                SE_TreeDamageBonus effect = UtilityFunctions.GetStatusEffectFromName("Tree Damage Bonus", hit.GetAttacker().GetSEMan()) as SE_TreeDamageBonus;
                //Log.LogMessage("Has Effect");
                //Log.LogMessage(hit.m_damage.GetTotalDamage());
                hit.m_damage.Modify(1 + effect.getDamageBonus());
                //Log.LogMessage(hit.m_damage.GetTotalDamage());
                //Log.LogWarning("Stamina Use: " + ___m_blockStaminaDrain);
            }
        }

        [HarmonyPatch(typeof(TreeLog), nameof(TreeLog.Damage))]
        [HarmonyPrefix]
        static void LogHitPrefix(ref HitData hit)
        {
            if (hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit.GetAttacker().m_seman == null)
                return;

            //Log.LogWarning("Hit Tree!");
            if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Tree Damage Bonus"))
            {
                SE_TreeDamageBonus effect = UtilityFunctions.GetStatusEffectFromName("Tree Damage Bonus", hit.GetAttacker().GetSEMan()) as SE_TreeDamageBonus;
                //Log.LogMessage("Has Effect");
                //Log.LogMessage(hit.m_damage.GetTotalDamage());
                hit.m_damage.Modify(1 + effect.getDamageBonus());
                //Log.LogMessage(hit.m_damage.GetTotalDamage());
                //Log.LogWarning("Stamina Use: " + ___m_blockStaminaDrain);
            }
        }
    }
}
