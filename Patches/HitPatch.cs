using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using UnityEngine;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{
    
    [HarmonyPatch]
    class HitPatch
    {
        public void Awake()
        {
            Log.LogInfo("Hit Patching Complete");
        }

        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        [HarmonyPatch(typeof(MonsterAI), "OnDamaged")]
        static void Prefix(MonsterAI __instance, ref float damage, ref Character attacker)
        {
            //Log.LogMessage("Enemy Damaged!");
            if (attacker == null || !attacker.IsPlayer() || attacker.m_seman == null)
            {
                return;
            }
            try
            { 
                //Log.LogMessage("Trying FOr Life Steal!");
                if (attacker.GetSEMan().HaveStatusEffect("Life Steal"))
                {
                    SE_HPOnHit effect = attacker.GetSEMan().GetStatusEffect("Life Steal") as SE_HPOnHit;
                    if (effect.getLastHitMelee())
                    {
                        attacker.Heal(damage * effect.getHealAmount());
                        var lifestealVfx = Object.Instantiate(AssetHelper.FXLifeSteal, attacker.GetCenterPoint(), Quaternion.identity);
                        ParticleSystem[] children = lifestealVfx.GetComponentsInChildren<ParticleSystem>();
                        foreach(ParticleSystem particle in children)
                        {
                            particle.Play();
                        }
                    }
                }
                
                /*if (attacker.GetSEMan().HaveStatusEffect("Wyrdarrow"))
                {
                    (attacker.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter).IncreaseCounter();
                }*/
                } catch
            {
                return;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "ApplyDamage")]
        public static void DamagePrefix(Character __instance, HitData hit)
        {
            if (hit.HaveAttacker() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Damage Vs Low HP"))
            {
                SE_DamageVSLowHP effect = hit.GetAttacker().GetSEMan().GetStatusEffect("Damage Vs Low HP") as SE_DamageVSLowHP;
                if (__instance.GetHealthPercentage() <= effect.GetHealthThreshold())
                {
                    //Log.LogWarning("Haha get fucked");
                    hit.m_damage.m_blunt += hit.m_damage.m_blunt * effect.GetDamageBonus();
                    hit.m_damage.m_chop += hit.m_damage.m_chop * effect.GetDamageBonus();
                    hit.m_damage.m_damage += hit.m_damage.m_damage * effect.GetDamageBonus();
                    hit.m_damage.m_fire += hit.m_damage.m_fire * effect.GetDamageBonus();
                    hit.m_damage.m_frost += hit.m_damage.m_frost * effect.GetDamageBonus();
                    hit.m_damage.m_lightning += hit.m_damage.m_lightning * effect.GetDamageBonus();
                    hit.m_damage.m_pickaxe += hit.m_damage.m_pickaxe * effect.GetDamageBonus();
                    hit.m_damage.m_pierce += hit.m_damage.m_pierce * effect.GetDamageBonus();
                    hit.m_damage.m_poison += hit.m_damage.m_poison * effect.GetDamageBonus();
                    hit.m_damage.m_slash += hit.m_damage.m_slash * effect.GetDamageBonus();
                    hit.m_damage.m_spirit += hit.m_damage.m_spirit * effect.GetDamageBonus();

                    var executionVFX = Object.Instantiate(AssetHelper.FXExecution, __instance.GetCenterPoint(), Quaternion.identity);
                    ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                    foreach(ParticleSystem particle in children)
                    {
                        particle.Play();
                    }

                    var audioSource = hit.GetAttacker().GetComponent<AudioSource>();
                    if (audioSource == null)
                    {
                        audioSource = hit.GetAttacker().gameObject.AddComponent<AudioSource>();
                        audioSource.playOnAwake = false;
                    }
                    audioSource.PlayOneShot(AssetHelper.SFXExecution);
                }
            }

            if(hit.HaveAttacker() && __instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
            {
                Log.LogMessage("Increasing damage");
                var effect = __instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath;
                hit.m_damage.m_blunt += hit.m_damage.m_blunt * effect.GetDamageBonus();
                hit.m_damage.m_chop += hit.m_damage.m_chop * effect.GetDamageBonus();
                hit.m_damage.m_damage += hit.m_damage.m_damage * effect.GetDamageBonus();
                hit.m_damage.m_fire += hit.m_damage.m_fire * effect.GetDamageBonus();
                hit.m_damage.m_frost += hit.m_damage.m_frost * effect.GetDamageBonus();
                hit.m_damage.m_lightning += hit.m_damage.m_lightning * effect.GetDamageBonus();
                hit.m_damage.m_pickaxe += hit.m_damage.m_pickaxe * effect.GetDamageBonus();
                hit.m_damage.m_pierce += hit.m_damage.m_pierce * effect.GetDamageBonus();
                hit.m_damage.m_poison += hit.m_damage.m_poison * effect.GetDamageBonus();
                hit.m_damage.m_slash += hit.m_damage.m_slash * effect.GetDamageBonus();
                hit.m_damage.m_spirit += hit.m_damage.m_spirit * effect.GetDamageBonus();

                effect.DecreaseHitsRemaining();

                if ((bool)balance["enableMarkedForDeathFX"])
                {
                    var executionVFX = Object.Instantiate(AssetHelper.FXMarkedForDeathHit, __instance.GetCenterPoint(), Quaternion.identity);
                    ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem particle in children)
                    {
                        particle.Play();
                    }
                }

                var audioSource = hit.GetAttacker().GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = hit.GetAttacker().gameObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                }
                audioSource.PlayOneShot(AssetHelper.SFXExecution);
            }

            if (hit.HaveAttacker() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Death Mark"))
            {
                var effect = hit.GetAttacker().GetSEMan().GetStatusEffect("Death Mark") as SE_DeathMark;

                if (!__instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
                {
                    Log.LogInfo(effect.GetLastHitThrowing());
                    if (__instance.GetSEMan().HaveStatusEffect("Marked For Death") && effect.GetLastHitThrowing())
                    {
                        //increase counter
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).IncreaseCounter();
                        Log.LogMessage($"Death Mark Counter : {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).m_count}");
                    }
                    else if (effect.GetLastHitThrowing())
                    {
                        Log.LogMessage("Adding Death Mark");
                        //add marked for death counter
                        __instance.GetSEMan().AddStatusEffect("Marked For Death");
                        //(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).IncreaseCounter();
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).SetActivationCount(effect.GetThreshold());
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).SetDamageBonus(effect.GetDamageBonus());
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).SetHitDuration(effect.GetHitDuration());
                        Log.LogMessage($"Death Mark Counter : {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).m_count}, " +
                            $"Activation: {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).GetActivationCount()} " +
                            $"Damage Bonus: {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).GetDamageBonus()} " +
                            $"Hit Amount: {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).GetHitDuration()}");
                    }
                }
               
            }

            if(hit.HaveAttacker() && __instance.GetHealth() <= hit.GetTotalDamage() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Bloodrush Listener"))
            {
                if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Bloodrush"))
                {
                    (hit.GetAttacker().GetSEMan().GetStatusEffect("Bloodrush") as SE_MoveSpeedOnKill).OnKill();
                }
                else
                {
                    hit.GetAttacker().GetSEMan().AddStatusEffect("Bloodrush");
                    (hit.GetAttacker().GetSEMan().GetStatusEffect("Bloodrush") as SE_MoveSpeedOnKill).SetSpeedBonus((hit.GetAttacker().GetSEMan().GetStatusEffect("Bloodrush Listener") as SE_MoveSpeedOnKillListener).GetSpeedBonus());
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "Damage")]
        static void OnDamagedPrefix(HitData hit)
        {
            if (hit.HaveAttacker() && hit.GetAttacker().IsPlayer() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Wyrdarrow"))
            {
                (hit.GetAttacker().GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter).IncreaseCounter();
            }
        }
    }
}
