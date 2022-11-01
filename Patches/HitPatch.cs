using HarmonyLib;
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

        [HarmonyPatch(typeof(MonsterAI), nameof(MonsterAI.OnDamaged))]
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
                        foreach (ParticleSystem particle in children)
                        {
                            particle.Play();
                        }
                    }
                }

            }
            catch
            {
                return;
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "ApplyDamage")]
        public static void DamagePrefix(Character __instance, ref HitData hit)
        {
            if (!hit.HaveAttacker() || hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit == null)
                return;

            Character attacker = hit.GetAttacker();

            if (attacker.GetSEMan().HaveStatusEffect("Damage Vs Low HP"))
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
                    foreach (ParticleSystem particle in children)
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

            if (__instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
            {
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
            if (hit.m_statusEffect == "Marked For Death" && !__instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
            {
                __instance.GetSEMan().AddStatusEffect("Marked For Death FX");
            }
            if (attacker.GetSEMan().HaveStatusEffect("Death Mark"))
            {
                var effect = hit.GetAttacker().GetSEMan().GetStatusEffect("Death Mark") as SE_DeathMark;

                if (!__instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
                {
                    //Log.LogInfo(effect.GetLastHitThrowing());
                    if (__instance.GetSEMan().HaveStatusEffect("Marked For Death") && effect.GetLastHitThrowing())
                    {
                        //increase counter
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).IncreaseCounter();
                        //Log.LogMessage($"Death Mark Counter : {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).m_count}");
                    }
                    else if (effect.GetLastHitThrowing())
                    {
                        //Log.LogMessage("Adding Death Mark");
                        //add marked for death counter
                        __instance.GetSEMan().AddStatusEffect("Marked For Death");
                        //(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).IncreaseCounter();
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).SetActivationCount(effect.GetThreshold());
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).SetDamageBonus(effect.GetDamageBonus());
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).SetHitDuration(effect.GetHitDuration());
                        //Log.LogInfo($"Death Mark Counter : {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).m_count}, " +
                        //$"Activation: {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).GetActivationCount()} " +
                        //$"Damage Bonus: {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).GetDamageBonus()} " +
                        //$"Hit Amount: {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).GetHitDuration()}");
                    }
                }

            }

            if (__instance.GetHealth() <= hit.GetTotalDamage() && attacker.GetSEMan().HaveStatusEffect("Bloodrush Listener"))
            {
                if (attacker.GetSEMan().HaveStatusEffect("Bloodrush"))
                {
                    (attacker.GetSEMan().GetStatusEffect("Bloodrush") as SE_MoveSpeedOnKill).OnKill();
                }
                else
                {
                    attacker.GetSEMan().AddStatusEffect("Bloodrush");
                    (attacker.GetSEMan().GetStatusEffect("Bloodrush") as SE_MoveSpeedOnKill).SetSpeedBonus((attacker.GetSEMan().GetStatusEffect("Bloodrush Listener") as SE_MoveSpeedOnKillListener).GetSpeedBonus());
                }
            }

            if (attacker.GetSEMan().HaveStatusEffect("Pinning") && !__instance.GetSEMan().HaveStatusEffect("Pinned") && !__instance.GetSEMan().HaveStatusEffect("Pinned Cooldown"))
            {
                if (UtilityFunctions.CheckIfVulnerable(__instance, hit) || (attacker as Player).GetCurrentWeapon().m_shared.m_name.Contains("mace_fire"))
                {
                    var effect = attacker.GetSEMan().GetStatusEffect("Pinning") as SE_Pinning;
                    __instance.GetSEMan().AddStatusEffect("Pinned");
                    (__instance.GetSEMan().GetStatusEffect("Pinned") as SE_Pinned).SetPinTTL(effect.GetPinTTL());
                    (__instance.GetSEMan().GetStatusEffect("Pinned") as SE_Pinned).SetPinCooldownTTL(effect.GetPinCooldownTTL());
                }
            }

            if (attacker.GetSEMan().HaveStatusEffect("Poison Vulnerable"))
            {
                if (UtilityFunctions.CheckIfVulnerable(__instance, hit))
                {
                    var effect = attacker.GetSEMan().GetStatusEffect("Poison Vulnerable") as SE_PoisonVulnerable;
                    __instance.AddPoisonDamage(hit.GetTotalDamage() * effect.GetDamageBonus());
                    //hit.m_damage.m_poison += hit.GetTotalDamage() * effect.GetDamageBonus();
                    Log.LogInfo($"Poison damage {hit.GetTotalDamage() * effect.GetDamageBonus()} damage {hit.GetTotalDamage()}");
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), "ApplyDamage")]
        public static void DamagePostfix(Character __instance, HitData hit)
        {
            SEMan seman = __instance.GetSEMan();
            hit.ApplyArmor(__instance.GetBodyArmor());
            if (seman.HaveStatusEffect("Wolftears") && __instance.GetHealth() <= hit.m_damage.GetTotalDamage() && !seman.HaveStatusEffect("Tear Protection Exhausted"))
            {
                Log.LogInfo($"Would Kill defender! Damage: {hit.m_damage.GetTotalDamage()}, attacker health: {__instance.GetHealth()}");
                hit.m_damage.Modify(0);
                seman.AddStatusEffect("Tear Protection Exhausted");
                __instance.SetHealth(1f);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Character), "Damage")]
        static void OnDamagedPrefix(Character __instance, ref HitData hit)
        {
            if (hit.HaveAttacker() && hit.GetAttacker().IsPlayer() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Wyrdarrow"))
            {
                (hit.GetAttacker().GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter).IncreaseCounter();
            }
            if (hit.HaveAttacker() && hit.GetAttacker().IsPlayer() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Brassflesh Listener"))
            {
                if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Brassflesh"))
                    (hit.GetAttacker().GetSEMan().GetStatusEffect("Brassflesh") as SE_ArmorOnHit).OnHit();
                else
                {
                    SEMan seman = hit.GetAttacker().GetSEMan();
                    float maxArmor = (seman.GetStatusEffect("Brassflesh Listener") as SE_ArmorOnHitListener).GetMaxArmor();
                    seman.AddStatusEffect("Brassflesh");
                    (seman.GetStatusEffect("Brassflesh") as SE_ArmorOnHit).SetMaxArmor(maxArmor);
                }
            }

            if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Challenge Move Speed"))
            {
                //Log.LogInfo($"starting damage: {hit.GetTotalDamage()}");
                float damageMod = 2f;
                hit.m_damage.m_blunt *= damageMod;
                hit.m_damage.m_chop *= damageMod;
                hit.m_damage.m_damage *= damageMod;
                hit.m_damage.m_fire *= damageMod;
                hit.m_damage.m_frost *= damageMod;
                hit.m_damage.m_lightning *= damageMod;
                hit.m_damage.m_pickaxe *= damageMod;
                hit.m_damage.m_pierce *= damageMod;
                hit.m_damage.m_poison *= damageMod;
                hit.m_damage.m_slash *= damageMod;
                hit.m_damage.m_spirit *= damageMod;
                //Log.LogInfo($"ending damage: {hit.GetTotalDamage()}");
            }

            if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Brassflesh"))
            {
                //Log.LogInfo($"starting damage: {hit.GetTotalDamage()}");
                float damageMod = (__instance.GetSEMan().GetStatusEffect("Brassflesh") as SE_ArmorOnHit).GetCurrentDamageReduction();
                hit.m_damage.m_blunt *= 1 - damageMod;
                hit.m_damage.m_chop *= 1 - damageMod;
                hit.m_damage.m_damage *= 1 - damageMod;
                hit.m_damage.m_fire *= 1 - damageMod;
                hit.m_damage.m_frost *= 1 - damageMod;
                hit.m_damage.m_lightning *= 1 - damageMod;
                hit.m_damage.m_pickaxe *= 1 - damageMod;
                hit.m_damage.m_pierce *= 1 - damageMod;
                hit.m_damage.m_poison *= 1 - damageMod;
                hit.m_damage.m_slash *= 1 - damageMod;
                hit.m_damage.m_spirit *= 1 - damageMod;
                //Log.LogInfo($"ending damage: {hit.GetTotalDamage()}");
            }
        }
    }
}
