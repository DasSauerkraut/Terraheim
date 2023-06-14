using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Terraheim.Patches
{

    [HarmonyPatch]
    class HitPatch
    {
        public void Awake()
        {
            Log.LogInfo("Hit Patching Complete");
        }

        static JObject balance = Terraheim.balance;

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
                    SE_HPOnHit effect = UtilityFunctions.GetStatusEffectFromName("Life Steal", attacker.GetSEMan()) as SE_HPOnHit;
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
        [HarmonyPatch(typeof(Character), nameof(Character.RPC_Damage))]
        public static void DamagePrefix(Character __instance, ref long sender, ref HitData hit)
        {
            if (!hit.HaveAttacker() || hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit == null)
                return;

            Character attacker = hit.GetAttacker();

            //Damage vs Low HP | Execution
            if (attacker.m_nview.GetZDO().GetBool("hasDamageVsLowHp"))
            {
                if (__instance.GetHealthPercentage() <= attacker.m_nview.GetZDO().GetFloat("damageVsLowHpThreshold"))
                {
                    //Log.LogWarning("Haha get fucked");
                    float damageBonus = attacker.m_nview.GetZDO().GetFloat("damageVsLowHpDamage");
                    hit.m_damage.m_blunt += hit.m_damage.m_blunt * damageBonus;
                    hit.m_damage.m_chop += hit.m_damage.m_chop * damageBonus;
                    hit.m_damage.m_damage += hit.m_damage.m_damage * damageBonus;
                    hit.m_damage.m_fire += hit.m_damage.m_fire * damageBonus;
                    hit.m_damage.m_frost += hit.m_damage.m_frost * damageBonus;
                    hit.m_damage.m_lightning += hit.m_damage.m_lightning * damageBonus;
                    hit.m_damage.m_pickaxe += hit.m_damage.m_pickaxe * damageBonus;
                    hit.m_damage.m_pierce += hit.m_damage.m_pierce * damageBonus;
                    hit.m_damage.m_poison += hit.m_damage.m_poison * damageBonus;
                    hit.m_damage.m_slash += hit.m_damage.m_slash * damageBonus;
                    hit.m_damage.m_spirit += hit.m_damage.m_spirit * damageBonus;

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

            //Marked for Death
            if (__instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Marked For Death", __instance.GetSEMan()) as SE_MarkedForDeath;
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
            if (attacker.m_nview.GetZDO().GetBool("hasDeathMark"))
            {
                if (!__instance.GetSEMan().HaveStatusEffect("Marked For Death FX"))
                {
                    if (__instance.GetSEMan().HaveStatusEffect("Marked For Death"))
                    {
                        //increase counter
                        (UtilityFunctions.GetStatusEffectFromName("Marked For Death", __instance.GetSEMan()) as SE_MarkedForDeath).IncreaseCounter();
                    }
                    else
                    {
                        //add marked for death counter
                        __instance.GetSEMan().AddStatusEffect("Marked For Death");
                        (UtilityFunctions.GetStatusEffectFromName("Marked For Death", __instance.GetSEMan()) as SE_MarkedForDeath).SetActivationCount(attacker.m_nview.GetZDO().GetInt("deathMarkThreshold"));
                        (UtilityFunctions.GetStatusEffectFromName("Marked For Death", __instance.GetSEMan()) as SE_MarkedForDeath).SetDamageBonus(attacker.m_nview.GetZDO().GetInt("deathMarkDamageBonus"));
                        (UtilityFunctions.GetStatusEffectFromName("Marked For Death", __instance.GetSEMan()) as SE_MarkedForDeath).SetHitDuration(attacker.m_nview.GetZDO().GetInt("deathMarkTTL"));
                    }
                }

            }

            //Bloodrush
            if (__instance.GetHealth() <= hit.GetTotalDamage() && attacker.GetSEMan().HaveStatusEffect("Bloodrush Listener"))
            {
                if (attacker.GetSEMan().HaveStatusEffect("Bloodrush"))
                {
                    (UtilityFunctions.GetStatusEffectFromName("Bloodrush", attacker.GetSEMan()) as SE_MoveSpeedOnKill).OnKill();
                }
                else
                {
                    attacker.GetSEMan().AddStatusEffect("Bloodrush");
                    (UtilityFunctions.GetStatusEffectFromName("Bloodrush", attacker.GetSEMan()) as SE_MoveSpeedOnKill).SetSpeedBonus((UtilityFunctions.GetStatusEffectFromName("Bloodrush Listener", attacker.GetSEMan()) as SE_MoveSpeedOnKillListener).GetSpeedBonus());
                }
            }

            if(__instance.GetHealth() <= hit.GetTotalDamage() && (attacker as Player).GetCurrentBlocker().m_shared.m_name.Contains("_shield_fire_tower"))
            {
                SE_ShieldFireListener effect = UtilityFunctions.GetStatusEffectFromName("Svalinn", attacker.GetSEMan()) as SE_ShieldFireListener;
                float hpToHeal = effect.OnKill(__instance.GetMaxHealth());
                if(hpToHeal != -1f)
                {
                    Collider[] hitColliders = Physics.OverlapSphere(attacker.GetCenterPoint(), 4f);
                    foreach (var obj in hitColliders)
                    {
                        //Log.LogInfo(obj.name);
                        if(obj.gameObject.GetComponent<Player>() != null)
                        {
                            Player plr = obj.GetComponent<Player>();
                            //Log.LogWarning(plr.m_name);
                            SE_ShieldFireHeal toHeal = ScriptableObject.CreateInstance<SE_ShieldFireHeal>();
                            toHeal.m_healthOverTime = hpToHeal;
                            plr.GetSEMan().AddStatusEffect(toHeal);
                        }
                    }
                    Object.Instantiate(AssetHelper.ShieldTowerFireHeal, attacker.GetCenterPoint(), Quaternion.identity);
                }
            }

            //Chosen
            if (attacker.GetSEMan().HaveStatusEffect("Chosen"))
            {
                //Log.LogInfo(__instance.m_name);
                if (__instance.GetHealth() <= hit.GetTotalDamage())
                {
                    (UtilityFunctions.GetStatusEffectFromName("Chosen", attacker.GetSEMan()) as SE_Chosen).OnKill();
                }
                else if (UtilityFunctions.IsBoss(__instance.m_name))
                {
                    System.Random dice = new System.Random();
                    if(dice.Next(1, 8 + 1) >= 8)
                        (UtilityFunctions.GetStatusEffectFromName("Chosen", attacker.GetSEMan()) as SE_Chosen).OnKill();
                }
            }

            if (attacker.GetSEMan().HaveStatusEffect("Bloodlust"))
            {
                SE_Bloodlust effect = UtilityFunctions.GetStatusEffectFromName("Bloodlust", attacker.GetSEMan()) as SE_Bloodlust;
                //Log.LogInfo($"Has Bloodlust, Enemy Health {__instance.GetHealthPercentage() * 100}%, Threshold {effect.GetThreshold() * 100}%");
                if(__instance.GetHealthPercentage() >= effect.GetThreshold())
                {
                    //Log.LogWarning($"Haha get fucked. Starting Damage {hit.GetTotalDamage()}");
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
                    //Log.LogInfo($"Ending Damage {hit.GetTotalDamage()}");
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

            if(attacker.GetSEMan().HaveStatusEffect("Maddening Visions") && (UtilityFunctions.GetStatusEffectFromName("Maddening Visions", attacker.GetSEMan()) as SE_MaddeningVisions).IsActive())
            {
                float modifier;
                if((attacker as Player).GetCurrentWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                {
                    modifier = (UtilityFunctions.GetStatusEffectFromName("Maddening Visions", attacker.GetSEMan()) as SE_MaddeningVisions).GetRangedMalus();

                }
                else
                {
                    modifier = (UtilityFunctions.GetStatusEffectFromName("Maddening Visions", attacker.GetSEMan()) as SE_MaddeningVisions).GetMeleeMalus();

                }
                hit.m_damage.m_blunt -= hit.m_damage.m_blunt * modifier;
                hit.m_damage.m_chop -= hit.m_damage.m_chop * modifier;
                hit.m_damage.m_damage -= hit.m_damage.m_damage * modifier;
                hit.m_damage.m_fire -= hit.m_damage.m_fire * modifier;
                hit.m_damage.m_frost -= hit.m_damage.m_frost * modifier;
                hit.m_damage.m_lightning -= hit.m_damage.m_lightning * modifier;
                hit.m_damage.m_pickaxe -= hit.m_damage.m_pickaxe * modifier;
                hit.m_damage.m_pierce -= hit.m_damage.m_pierce * modifier;
                hit.m_damage.m_poison -= hit.m_damage.m_poison * modifier;
                hit.m_damage.m_slash -= hit.m_damage.m_slash * modifier;
                hit.m_damage.m_spirit -= hit.m_damage.m_spirit * modifier;
            }
            //Pinning
            if (attacker.m_nview.GetZDO().GetBool("hasPinning") && !__instance.GetSEMan().HaveStatusEffect("Pinned") && !__instance.GetSEMan().HaveStatusEffect("Pinned Cooldown"))
            {
                if (UtilityFunctions.CheckIfVulnerable(__instance, hit) || (attacker as Player).GetCurrentWeapon().m_shared.m_name.Contains("mace_fire"))
                {
                    __instance.m_nview.InvokeRPC("RPC_AddStatusEffect", "Pinned", false, 0, 0f);
                    (UtilityFunctions.GetStatusEffectFromName("Pinned", __instance.GetSEMan()) as SE_Pinned).SetPinTTL(attacker.m_nview.GetZDO().GetFloat("pinTTL"));
                    (UtilityFunctions.GetStatusEffectFromName("Pinned", __instance.GetSEMan()) as SE_Pinned).SetPinCooldownTTL(attacker.m_nview.GetZDO().GetFloat("pinCooldownTTL"));
                }
            }

            if (attacker.m_nview.GetZDO().GetBool("hasPoisonVulnerable"))
            {
                if (UtilityFunctions.CheckIfVulnerable(__instance, hit))
                {
                    __instance.AddPoisonDamage(hit.GetTotalDamage() * attacker.m_nview.GetZDO().GetFloat("poisonVulnerableDamageBonus"));
                    Log.LogInfo($"Poison damage {hit.GetTotalDamage() * attacker.m_nview.GetZDO().GetFloat("poisonVulnerableDamageBonus")} damage {hit.GetTotalDamage()}");
                }
            }

            if (attacker.m_nview.GetZDO().GetBool("hasCoinDrop") && hit.GetTotalDamage() > 10)
            {
                System.Random rnd = new System.Random();
                int roll = rnd.Next(1, 100);
                Log.LogInfo($"coin roll {roll}, target {attacker.m_nview.GetZDO().GetFloat("coinDropChance")}");
                if (roll < attacker.m_nview.GetZDO().GetFloat("coinDropChance"))
                {
                    //drop coins
                    List<KeyValuePair<GameObject, int>> list = new List<KeyValuePair<GameObject, int>>();
                    ItemDrop coin = Jotunn.Managers.PrefabManager.Cache.GetPrefab<ItemDrop>("Coins");
                    for (int i = 0; i < attacker.m_nview.GetZDO().GetFloat("coinDropBonus"); i++)
                    {
                        list.Add(new KeyValuePair<GameObject, int>(coin.gameObject, 1));
                    }
                    CharacterDrop.DropItems(list, __instance.GetCenterPoint(), 0.5f);
                    
                    var audioSource = __instance.GetComponent<AudioSource>();
                    if (audioSource == null)
                    {
                        audioSource = __instance.gameObject.AddComponent<AudioSource>();
                        audioSource.playOnAwake = false;
                    }
                    audioSource.PlayOneShot(AssetHelper.SFXCoin);
                }
            }

            if (attacker.m_nview.GetZDO().GetBool("hasRestoreResources"))
            {
                attacker.AddStamina(attacker.m_nview.GetZDO().GetFloat("restoreResourcesStamina"));
                System.Random rnd = new System.Random();
                int roll = rnd.Next(1, 100);
                Log.LogInfo($"ammo refund roll {roll}, target {attacker.m_nview.GetZDO().GetFloat("restoreResourcesChance")}");
                if (roll < attacker.m_nview.GetZDO().GetFloat("restoreResourcesChance") && (attacker as Player).GetCurrentWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                {
                    ItemDrop.ItemData weapon = (attacker as Player).GetCurrentWeapon();
                    Player character = attacker as Player;
                    if (string.IsNullOrEmpty(weapon.m_shared.m_ammoType))
                    {
                        return;
                    }

                    //First check if bow has ammo available
                    bool hasAmmo = true;
                    ItemDrop.ItemData ammoItem = character.GetAmmoItem();
                    if (ammoItem != null && (!character.GetInventory().ContainsItem(ammoItem) || ammoItem.m_shared.m_ammoType != weapon.m_shared.m_ammoType))
                    {
                        ammoItem = null;
                    }
                    if (ammoItem == null)
                    {
                        ammoItem = character.GetInventory().GetAmmoItem(weapon.m_shared.m_ammoType);
                    }
                    if (ammoItem == null)
                    {
                        return;
                    }
                    if (ammoItem.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
                    {
                        hasAmmo = character.CanConsumeItem(ammoItem);
                    }
                    //Log.LogWarning("Stack " + ammoItem.m_stack);

                    //if so, add 1 of the selected ammo type
                    if (hasAmmo)
                    {
                        //Add 1 Ammo
                        ammoItem.m_stack += 1;
                        //Log.LogWarning("Stack " + ammoItem.m_stack);

                        //character.GetInventory().AddItem(ammoItem);
                    }
                }
            }
            //Log.LogInfo(1);
            if(attacker.m_nview.GetZDO().GetBool("hasStaggerDamage") && __instance.IsStaggering())
            {
                var effect = attacker.m_nview.GetZDO().GetFloat("staggerDamageBonus");
                //Log.LogInfo("Previous Damage " + hit.GetTotalDamage());
                hit.m_damage.m_blunt += hit.m_damage.m_blunt * effect;
                hit.m_damage.m_chop += hit.m_damage.m_chop * effect;
                hit.m_damage.m_damage += hit.m_damage.m_damage * effect;
                hit.m_damage.m_fire += hit.m_damage.m_fire * effect;
                hit.m_damage.m_frost += hit.m_damage.m_frost * effect;
                hit.m_damage.m_lightning += hit.m_damage.m_lightning * effect;
                hit.m_damage.m_pickaxe += hit.m_damage.m_pickaxe * effect;
                hit.m_damage.m_pierce += hit.m_damage.m_pierce * effect;
                hit.m_damage.m_poison += hit.m_damage.m_poison * effect;
                hit.m_damage.m_slash += hit.m_damage.m_slash * effect;
                hit.m_damage.m_spirit += hit.m_damage.m_spirit * effect;
                //Log.LogInfo("New Damage " + hit.GetTotalDamage());
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
                (UtilityFunctions.GetStatusEffectFromName("Wyrdarrow", hit.GetAttacker().GetSEMan()) as SE_AoECounter).IncreaseCounter();
            }
            if (hit.HaveAttacker() && hit.GetAttacker().IsPlayer() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Rooting"))
            {
                    (UtilityFunctions.GetStatusEffectFromName("Rooting", hit.GetAttacker().GetSEMan()) as SE_Rooting).IncreaseCounter();
            }
            if(hit.m_statusEffect == "Rooted Listener" && __instance.IsPlayer())
            {
                hit.m_damage.m_poison = 0f;
            }
            if(hit.m_statusEffect == "Rooted Listener" && !__instance.GetSEMan().HaveStatusEffect("Rooted") && !__instance.GetSEMan().HaveStatusEffect("Rooted Listener") && !__instance.IsPlayer())
            {
                float duration = 8f;
                if(hit.HaveAttacker() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Rooting"))
                {
                    SE_Rooting sE_Rooted = UtilityFunctions.GetStatusEffectFromName("Rooting", hit.GetAttacker().GetSEMan()) as SE_Rooting;
                    duration = sE_Rooted.GetRootedDuration();
                }
                __instance.GetSEMan().AddStatusEffect("Rooted");
                (UtilityFunctions.GetStatusEffectFromName("Rooted", __instance.GetSEMan()) as SE_Rooted).SetRootedTTL(duration);
            }
            if (hit.HaveAttacker() && hit.GetAttacker().IsPlayer() && hit.GetAttacker().GetSEMan().HaveStatusEffect("Brassflesh Listener"))
            {
                if (hit.GetAttacker().GetSEMan().HaveStatusEffect("Brassflesh"))
                    (UtilityFunctions.GetStatusEffectFromName("Brassflesh", hit.GetAttacker().GetSEMan()) as SE_ArmorOnHit).OnHit();
                else
                {
                    SEMan seman = hit.GetAttacker().GetSEMan();
                    float maxArmor = (UtilityFunctions.GetStatusEffectFromName("Brassflesh Listener", seman) as SE_ArmorOnHitListener).GetMaxArmor();
                    seman.AddStatusEffect("Brassflesh");
                    (UtilityFunctions.GetStatusEffectFromName("Brassflesh", seman) as SE_ArmorOnHit).SetMaxArmor(maxArmor);
                }
            }

            if(hit.m_statusEffect == "Retaliation Cooldown")
            {
                if (__instance.IsPlayer())
                {
                    hit.m_damage.m_blunt = 0;
                    hit.m_damage.m_chop = 0;
                    hit.m_damage.m_damage = 0;
                    hit.m_damage.m_fire = 0;
                    hit.m_damage.m_frost = 0;
                    hit.m_damage.m_lightning = 0;
                    hit.m_damage.m_pickaxe = 0;
                    hit.m_damage.m_pierce = 0;
                    hit.m_damage.m_poison = 0;
                    hit.m_damage.m_slash = 0;
                    hit.m_damage.m_spirit = 0;
                } else
                {
                    hit.m_staggerMultiplier *= 3f;
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

            if(__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Chosen") && !__instance.IsBlocking() && !__instance.InAttack())
            {
                (UtilityFunctions.GetStatusEffectFromName("Chosen", __instance.GetSEMan()) as SE_Chosen).OnTakeDamage();
            }

            //Brassflesh
            if (__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Brassflesh"))
            {
                //Log.LogInfo($"starting damage: {hit.GetTotalDamage()}");
                float damageMod = (UtilityFunctions.GetStatusEffectFromName("Brassflesh", __instance.GetSEMan()) as SE_ArmorOnHit).GetCurrentDamageReduction();
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

            if(__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Hyperarmor") && __instance.InAttack())
            {
                hit.m_staggerMultiplier = 0;
                hit.m_pushForce = 0;
                Log.LogInfo($"starting damage: {hit.GetTotalDamage()}");
                float damageMod = (UtilityFunctions.GetStatusEffectFromName("Hyperarmor", __instance.GetSEMan()) as SE_Hyperarmor).GetArmor();
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
                Log.LogInfo($"ending damage: {hit.GetTotalDamage()}");
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), "Stagger")]
        static void OnStaggerPostfix(Character __instance)
        {
            if (__instance.IsPlayer() && __instance.m_seman.HaveStatusEffect("Retaliation") && !__instance.m_seman.HaveStatusEffect("Retaliation Cooldown"))
            {
                SE_Retaliation effect = UtilityFunctions.GetStatusEffectFromName("Retaliation", __instance.m_seman) as SE_Retaliation;
                if (effect.GetStored() > 0)
                {
                    Log.LogInfo($"Retaliation! Dealing {effect.GetStored()} pierce damage.");
                    AssetHelper.RetaliationExplosion.GetComponent<Aoe>().m_damage.m_pierce = effect.GetStored();
                    Object.Instantiate(AssetHelper.RetaliationExplosion, __instance.GetCenterPoint(), Quaternion.identity);
                    effect.ResetStored();
                }
                 __instance.m_seman.AddStatusEffect("Retaliation Cooldown");
            }
        }
    }
}
