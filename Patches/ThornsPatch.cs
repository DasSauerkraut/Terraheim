using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
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
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");



        [HarmonyPatch(typeof(SEMan), "OnDamaged")]
        public static void Postfix(SEMan __instance, HitData hit, ref Character attacker)
        {
            //Log.LogMessage("Enemy Damaged!");
            if (attacker == null || attacker.IsPlayer() || attacker.m_seman == null)
            {
                return;
            }
                //Log.LogMessage("Trying FOr Thorns!");
            if (__instance.HaveStatusEffect("Thorns") && !__instance.m_character.IsBlocking())
            {
                SE_Thorns effect = __instance.GetStatusEffect("Thorns") as SE_Thorns;
                //Log.LogMessage($"Damage dealt: {hit.GetTotalDamage()} thorns %${effect.GetReflectPercent()}");
                HitData reflectedDamage = new HitData();
                reflectedDamage.m_damage.Add(hit.m_damage);
                reflectedDamage.m_damage.m_blunt *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_chop *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_damage *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_fire *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_frost *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_lightning *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_pickaxe *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_pierce *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_poison *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_slash *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_spirit *= effect.GetReflectPercent();
                reflectedDamage.m_staggerMultiplier = 0;

                //Log.LogMessage($"Reflected Damage ${reflectedDamage.m_damage.GetTotalDamage()}");
                if (attacker.GetHealth() <= reflectedDamage.GetTotalDamage() && attacker.GetHealthPercentage() >= (float)balance["thornsKillThreshold"])
                {
                    var totalDamage = attacker.GetHealth() - 1;
                    reflectedDamage.m_damage.m_blunt = totalDamage * (reflectedDamage.m_damage.m_blunt / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_chop = totalDamage * (reflectedDamage.m_damage.m_chop / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_damage = totalDamage * (reflectedDamage.m_damage.m_damage / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_fire = totalDamage * (reflectedDamage.m_damage.m_fire / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_frost = totalDamage * (reflectedDamage.m_damage.m_frost / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_lightning = totalDamage * (reflectedDamage.m_damage.m_lightning / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_pickaxe = totalDamage * (reflectedDamage.m_damage.m_pickaxe / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_pierce = totalDamage * (reflectedDamage.m_damage.m_pierce / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_poison = totalDamage * (reflectedDamage.m_damage.m_poison / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_slash = totalDamage * (reflectedDamage.m_damage.m_slash / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_spirit = totalDamage * (reflectedDamage.m_damage.m_spirit / reflectedDamage.GetTotalDamage());
                    //Log.LogMessage($"Would Kill attacker! New damage: {reflectedDamage.m_damage.GetTotalDamage()}, attacker health: {attacker.GetHealth()}");
                }

                attacker.ApplyDamage(reflectedDamage, true, false);

                var vfx = Object.Instantiate(AssetHelper.FXThorns, attacker.GetCenterPoint(), Quaternion.identity);
                ParticleSystem[] children = vfx.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in children)
                {
                    particle.Play();
                    //Log.LogMessage("Playing particle");
                }
            }

            if (__instance.HaveStatusEffect("Wolftears") && __instance.m_character.GetHealth() <= hit.m_damage.GetTotalDamage() && !__instance.HaveStatusEffect("Tear Protection Exhausted"))
            {
                Log.LogInfo($"Would Kill defender! Damage: {hit.m_damage.GetTotalDamage()}, attacker health: {__instance.m_character.GetHealth()}");
                /*var totalDamage = __instance.m_character.GetHealth() - 1;
                hit.m_damage.m_blunt = totalDamage * (hit.m_damage.m_blunt / hit.GetTotalDamage());
                hit.m_damage.m_chop = totalDamage * (hit.m_damage.m_chop / hit.GetTotalDamage());
                hit.m_damage.m_damage = totalDamage * (hit.m_damage.m_damage / hit.GetTotalDamage());
                hit.m_damage.m_fire = totalDamage * (hit.m_damage.m_fire / hit.GetTotalDamage());
                hit.m_damage.m_frost = totalDamage * (hit.m_damage.m_frost / hit.GetTotalDamage());
                hit.m_damage.m_lightning = totalDamage * (hit.m_damage.m_lightning / hit.GetTotalDamage());
                hit.m_damage.m_pickaxe = totalDamage * (hit.m_damage.m_pickaxe / hit.GetTotalDamage());
                hit.m_damage.m_pierce = totalDamage * (hit.m_damage.m_pierce / hit.GetTotalDamage());
                hit.m_damage.m_poison = totalDamage * (hit.m_damage.m_poison / hit.GetTotalDamage());
                hit.m_damage.m_slash = totalDamage * (hit.m_damage.m_slash / hit.GetTotalDamage());
                hit.m_damage.m_spirit = totalDamage * (hit.m_damage.m_spirit / hit.GetTotalDamage());*/
                hit.m_damage.Modify(0);
                __instance.AddStatusEffect("Tear Protection Exhausted");
                __instance.m_character.SetHealth(1f);
            }
            /*
            if (__instance.HaveStatusEffect("Wolftears"))
            {
                var effect = __instance.GetStatusEffect("Wolftears") as SE_Wolftears;
                if(__instance.m_character.GetHealthPercentage() <= effect.GetActivationHP())
                {
                    effect.SetIcon();
                }
            }

            if (__instance.HaveStatusEffect("Battle Furor"))
            {
                var effect = __instance.GetStatusEffect("Battle Furor") as SE_FullHPDamageBonus;
                //Log.LogWarning("HP Percentage " + __instance.m_character.GetHealthPercentage() + " Activation Threshold " + effect.GetActivationHP());
                if (__instance.m_character.GetHealthPercentage() < effect.GetActivationHP() && effect.m_icon != null)
                {
                    effect.ClearIcon();
                }
            }*/
        }
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), "ApplyDamage")]
        public static void DamagePostfix(Character __instance, ref HitData hit)
        {
            Character attacker = hit.GetAttacker();
            if (attacker == null || attacker.IsPlayer() || attacker.m_seman == null)
            {
                return;
            }

            if (__instance.GetSEMan().HaveStatusEffect("Wolftears"))
            {
                var effect = __instance.GetSEMan().GetStatusEffect("Wolftears") as SE_Wolftears;
                effect.SetIcon();
                
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
