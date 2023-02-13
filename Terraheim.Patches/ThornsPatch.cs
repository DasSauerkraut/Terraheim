using HarmonyLib;
using Terraheim.ArmorEffects;
using UnityEngine;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{

    [HarmonyPatch]
    class ThornsPatch
    {
        public void Awake()
        {
            Log.LogInfo("Thorns Patching Complete");
        }
        static JObject balance = Terraheim.balance;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        public static void DamagePostfix(Character __instance, ref HitData hit)
        {

            SEMan seman = __instance.GetSEMan();

            Character attacker = hit.GetAttacker();
            if (attacker == null || attacker.IsPlayer() || attacker.m_seman == null)
            {
                return;
            }

            if (__instance.GetSEMan().HaveStatusEffect("Wolftears"))
            {
                var effect = __instance.GetSEMan().GetStatusEffect("Wolftears") as SE_Wolftears;
                effect.SetIcon();
                if (seman.m_character.GetHealth() <= hit.m_damage.GetTotalDamage() && !seman.HaveStatusEffect("Tear Protection Exhausted"))
                {
                    Log.LogInfo($"Would Kill defender! Damage: {hit.m_damage.GetTotalDamage()}, attacker health: {__instance.GetHealth()}");

                    hit.m_damage.Modify(0);
                    seman.AddStatusEffect("Tear Protection Exhausted");
                    __instance.SetHealth(1f);
                }
            }
            if (__instance.GetSEMan().HaveStatusEffect("Battle Furor"))
            {
                var effect = __instance.GetSEMan().GetStatusEffect("Battle Furor") as SE_FullHPDamageBonus;
                //Log.LogWarning("HP Percentage " + __instance.m_character.GetHealthPercentage() + " Activation Threshold " + effect.GetActivationHP());
                if (__instance.GetHealthPercentage() < effect.GetActivationHP())
                {
                    effect.ClearIcon();
                }
            }
        }
    }
}
