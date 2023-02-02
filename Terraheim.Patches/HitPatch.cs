using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
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
        [HarmonyPatch(typeof(Character), nameof(Character.ApplyDamage))]
        public static void DamagePrefix(Character __instance, ref HitData hit)
        {
            if (!hit.HaveAttacker() || hit.GetAttacker() == null || !hit.GetAttacker().IsPlayer() || hit == null)
                return;

            Character attacker = hit.GetAttacker();

            //Damage vs Low HP | Execurtion
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

            //Marked for Death
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
                    if (__instance.GetSEMan().HaveStatusEffect("Marked For Death"))
                    {
                        //increase counter
                        (__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).IncreaseCounter();
                        //Log.LogMessage($"Death Mark Counter : {(__instance.GetSEMan().GetStatusEffect("Marked For Death") as SE_MarkedForDeath).m_count}");
                    }
                    else
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

            //Bloodrush
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

            if(__instance.GetHealth() <= hit.GetTotalDamage() && (attacker as Player).GetCurrentBlocker().m_shared.m_name.Contains("_shield_fire_tower"))
            {
                SE_ShieldFireListener effect = attacker.GetSEMan().GetStatusEffect("Svalinn") as SE_ShieldFireListener;
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
                    (attacker.GetSEMan().GetStatusEffect("Chosen") as SE_Chosen).OnKill();
                }
                else if (UtilityFunctions.IsBoss(__instance.m_name))
                {
                    System.Random dice = new System.Random();
                    if(dice.Next(1, 8 + 1) >= 8)
                        (attacker.GetSEMan().GetStatusEffect("Chosen") as SE_Chosen).OnKill();
                }
            }

            if (attacker.GetSEMan().HaveStatusEffect("Bloodlust"))
            {
                SE_Bloodlust effect = attacker.GetSEMan().GetStatusEffect("Bloodlust") as SE_Bloodlust;
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

            if(attacker.GetSEMan().HaveStatusEffect("Maddening Visions") && (attacker.GetSEMan().GetStatusEffect("Maddening Visions") as SE_MaddeningVisions).IsActive())
            {
                float modifier;
                if((attacker as Player).GetCurrentWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                {
                    modifier = (attacker.GetSEMan().GetStatusEffect("Maddening Visions") as SE_MaddeningVisions).GetRangedMalus();

                }
                else
                {
                    modifier = (attacker.GetSEMan().GetStatusEffect("Maddening Visions") as SE_MaddeningVisions).GetMeleeMalus();

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

            if (attacker.GetSEMan().HaveStatusEffect("Coin Drop") && hit.GetTotalDamage() > 10)
            {
                SE_CoinDrop status = attacker.GetSEMan().GetStatusEffect("Coin Drop") as SE_CoinDrop;
                System.Random rnd = new System.Random();
                int roll = rnd.Next(1, 100);
                Log.LogInfo($"coin roll {roll}, target {status.GetChance()}");
                if (roll < status.GetChance())
                {
                    //drop coins
                    List<KeyValuePair<GameObject, int>> list = new List<KeyValuePair<GameObject, int>>();
                    ItemDrop coin = Jotunn.Managers.PrefabManager.Cache.GetPrefab<ItemDrop>("Coins");
                    list.Add(new KeyValuePair<GameObject, int>(coin.gameObject, 1));
                    list.Add(new KeyValuePair<GameObject, int>(coin.gameObject, 1));
                    list.Add(new KeyValuePair<GameObject, int>(coin.gameObject, 1));
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

            if (attacker.GetSEMan().HaveStatusEffect("Restore Resources"))
            {
                SE_RestoreResources status = attacker.GetSEMan().GetStatusEffect("Restore Resources") as SE_RestoreResources;
                attacker.AddStamina(status.GetStaminaAmount());
                System.Random rnd = new System.Random();
                int roll = rnd.Next(1, 100);
                Log.LogInfo($"ammo refund roll {roll}, target {status.GetChance()}");
                if (roll < status.GetChance() && (attacker as Player).GetCurrentWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
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
            if(attacker.GetSEMan().HaveStatusEffect("Stagger Damage") && __instance.IsStaggering())
            {
                var effect = attacker.GetSEMan().GetStatusEffect("Stagger Damage") as SE_StaggerDamage;
                //Log.LogInfo("Previous Damage " + hit.GetTotalDamage());
                hit.m_damage.m_blunt += hit.m_damage.m_blunt * effect.GetStaggerBns();
                hit.m_damage.m_chop += hit.m_damage.m_chop * effect.GetStaggerBns();
                hit.m_damage.m_damage += hit.m_damage.m_damage * effect.GetStaggerBns();
                hit.m_damage.m_fire += hit.m_damage.m_fire * effect.GetStaggerBns();
                hit.m_damage.m_frost += hit.m_damage.m_frost * effect.GetStaggerBns();
                hit.m_damage.m_lightning += hit.m_damage.m_lightning * effect.GetStaggerBns();
                hit.m_damage.m_pickaxe += hit.m_damage.m_pickaxe * effect.GetStaggerBns();
                hit.m_damage.m_pierce += hit.m_damage.m_pierce * effect.GetStaggerBns();
                hit.m_damage.m_poison += hit.m_damage.m_poison * effect.GetStaggerBns();
                hit.m_damage.m_slash += hit.m_damage.m_slash * effect.GetStaggerBns();
                hit.m_damage.m_spirit += hit.m_damage.m_spirit * effect.GetStaggerBns();
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
                (__instance.GetSEMan().GetStatusEffect("Chosen") as SE_Chosen).OnTakeDamage();
            }

            //Brassflesh
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

            if(__instance.IsPlayer() && __instance.GetSEMan().HaveStatusEffect("Hyperarmor") && __instance.InAttack())
            {
                hit.m_staggerMultiplier = 0;
                hit.m_pushForce = 0;
                Log.LogInfo($"starting damage: {hit.GetTotalDamage()}");
                float damageMod = (__instance.GetSEMan().GetStatusEffect("Hyperarmor") as SE_Hyperarmor).GetArmor();
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
                SE_Retaliation effect = __instance.m_seman.GetStatusEffect("Retaliation") as SE_Retaliation;
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
