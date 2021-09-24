﻿using HarmonyLib;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class AttackPatch
    {
        public void Awake()
        {
            Log.LogInfo("Attack Patching Complete");
        }

        [HarmonyPatch(typeof(Attack), "Start")]
        public static void Prefix(ref Attack __instance, Humanoid character, ref ItemDrop.ItemData weapon)
        {
            //Log.LogWarning("Attack Start lol");
            if ((character.m_unarmedWeapon && weapon == character?.m_unarmedWeapon.m_itemData) || (weapon.m_dropPrefab == null || weapon.m_dropPrefab.name == null))
            {
                          //Log.LogWarning(89);
                return;
            }
            //Log.LogWarning(1);
            //Log.LogWarning(weapon.m_dropPrefab.name);
            //Log.LogWarning(12);
            //Get base weapon
            var baseWeapon = PrefabManager.Cache.GetPrefab<ItemDrop>(weapon.m_dropPrefab.name);
            //Log.LogWarning(13);
            if (baseWeapon == null)
            {
                Log.LogMessage("Terraheim (AttackPatch Start) | Weapon is null, grabbing directly");
                baseWeapon = ObjectDB.instance.GetItemPrefab(weapon.m_dropPrefab.name).GetComponent<ItemDrop>();
            }
            //Log.LogWarning(2);

            //set all damages to default values to prevent forever increasing damages
            weapon.m_shared.m_backstabBonus = baseWeapon.m_itemData.m_shared.m_backstabBonus;
            weapon.m_shared.m_damages.m_frost = baseWeapon.m_itemData.m_shared.m_damages.m_frost;
            weapon.m_shared.m_damages.m_spirit = baseWeapon.m_itemData.m_shared.m_damages.m_spirit;
            weapon.m_shared.m_damages.m_damage = baseWeapon.m_itemData.m_shared.m_damages.m_damage;
            weapon.m_shared.m_attack.m_damageMultiplier = baseWeapon.m_itemData.m_shared.m_attack.m_damageMultiplier;
            //Bow Damage Effect
            //Log.LogWarning(3);
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                if (character.GetSEMan().GetStatusEffect("Life Steal"))
                {
                    SE_HPOnHit effect = character.GetSEMan().GetStatusEffect("Life Steal") as SE_HPOnHit;
                    effect.setLastHitMelee(false);
                }
                if (character.GetSEMan().HaveStatusEffect("Ranged Damage Bonus"))
                {
                    SE_RangedDmgBonus effect = character.GetSEMan().GetStatusEffect("Ranged Damage Bonus") as SE_RangedDmgBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();
                }
             } 
            else if(character.GetSEMan().GetStatusEffect("Life Steal"))
            {
                SE_HPOnHit effect = character.GetSEMan().GetStatusEffect("Life Steal") as SE_HPOnHit;
                effect.setLastHitMelee(true);

            }

            //Melee Damage Effect
            //Log.LogWarning(4);
            if (weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
            {
                if (character.GetSEMan().HaveStatusEffect("Melee Damage Bonus"))
                {
                    SE_MeleeDamageBonus effect = character.GetSEMan().GetStatusEffect("Melee Damage Bonus") as SE_MeleeDamageBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();
                    //Log.LogMessage("weapon  damage " + weapon.m_shared.m_attack.m_damageMultiplier);
                }
            }

            //Throwing Damage Effect
            //Log.LogWarning(5);
            if (weapon.m_shared.m_name.Contains("_throwingaxe") || 
                (weapon.m_shared.m_name.Contains("_spear") && __instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation) ||
                weapon.m_shared.m_name.Contains("bomb"))
            {
                if (character.GetSEMan().HaveStatusEffect("Throwing Damage Bonus"))
                {
                    SE_ThrowingDamageBonus effect = character.GetSEMan().GetStatusEffect("Throwing Damage Bonus") as SE_ThrowingDamageBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();
                    //Log.LogMessage("weapon  damage " + weapon.m_shared.m_attack.m_damageMultiplier);
                }
                if (character.GetSEMan().HaveStatusEffect("Death Mark"))
                {
                     (character.GetSEMan().GetStatusEffect("Death Mark") as SE_DeathMark).SetLastHitThrowing(true);
                    //Log.LogMessage("weapon  damage " + weapon.m_shared.m_attack.m_damageMultiplier);
                }
            }
            else
            {
                if (character.GetSEMan().HaveStatusEffect("Death Mark"))
                {
                    (character.GetSEMan().GetStatusEffect("Death Mark") as SE_DeathMark).SetLastHitThrowing(false);
                    //Log.LogMessage("weapon  damage " + weapon.m_shared.m_attack.m_damageMultiplier);
                }
            }

            //Log.LogWarning(6);
            //Dagger/Spear Damage Effect
            if ((weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife")))
            {
                if(character.GetSEMan().HaveStatusEffect("Dagger/Spear Damage Bonus"))
                {
                    SE_DaggerSpearDmgBonus effect = character.GetSEMan().GetStatusEffect("Dagger/Spear Damage Bonus") as SE_DaggerSpearDmgBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();
                }
            }

           // Log.LogWarning(7);
            //One Hand Damage Effect
            if (weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon && character.GetLeftItem() == null)
            {
                //Log.LogMessage("Weapon only in right hand");
                if (character.GetSEMan().HaveStatusEffect("One Hand Damage Bonus"))
                {
                    SE_OneHandDamageBonus effect = character.GetSEMan().GetStatusEffect("One Hand Damage Bonus") as SE_OneHandDamageBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();

                }
            }

            //Log.LogWarning(8);
            //Two Handed Damage Effect
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                if (character.GetSEMan().HaveStatusEffect("Two Handed Damage Bonus"))
                {
                    SE_TwoHandedDmgBonus effect = character.GetSEMan().GetStatusEffect("Two Handed Damage Bonus") as SE_TwoHandedDmgBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();

                }
            }

            //Log.LogWarning(9);
            //TODO: Audio Effect on Activation!
            //Crit Damage Effect
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                if (character.GetSEMan().HaveStatusEffect("Crit Chance"))
                {
                    SE_CritChance effect = character.GetSEMan().GetStatusEffect("Crit Chance") as SE_CritChance;
                    System.Random rand = new System.Random();

                    int roll = rand.Next(1, 100);
                    if(roll <= effect.GetCritChance())
                    {
                        Log.LogWarning("Crit!");
                        weapon.m_shared.m_attack.m_damageMultiplier *= effect.GetCritBonus();
                    }
                }
            }

            //Log.LogWarning(10);
            //Ammo Consumption Effect
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow && character.GetSEMan().HaveStatusEffect("Ammo Consumption"))
            {
                System.Random rand = new System.Random();
                SE_AmmoConsumption effect = character.GetSEMan().GetStatusEffect("Ammo Consumption") as SE_AmmoConsumption;

                int roll = rand.Next(1, 100);
                if (roll <= effect.getAmmoConsumption())
                { 
                    if (string.IsNullOrEmpty(weapon.m_shared.m_ammoType))
                    {
                        return;
                    }

                    //First check if bow has ammo available
                    bool hasAmmo = true ;
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

            //Log.LogWarning(11);
            //Backstab Bonus Effect
            if (character.GetSEMan().HaveStatusEffect("Backstab Bonus"))
            {
                SE_BackstabBonus effect = character.GetSEMan().GetStatusEffect("Backstab Bonus") as SE_BackstabBonus;
                weapon.m_shared.m_backstabBonus = baseWeapon.m_itemData.m_shared.m_backstabBonus + effect.getBackstabBonus();
            }

            //Log.LogWarning(12);
            //Silver Damage Bonus Effect
            weapon.m_shared.m_damages.m_spirit = baseWeapon.m_itemData.m_shared.m_damages.m_spirit;
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife"))
            {
                if (character.GetSEMan().HaveStatusEffect("Silver Damage Bonus")) {
                    SE_SilverDamageBonus effect = character.GetSEMan().GetStatusEffect("Silver Damage Bonus") as SE_SilverDamageBonus;

                    var totalDamage = weapon.GetDamage().GetTotalDamage();
                    var elementDamage = (totalDamage * effect.GetDamageBonus()) / 2;
                    weapon.m_shared.m_damages.m_frost += elementDamage;
                    weapon.m_shared.m_damages.m_spirit += elementDamage;
                    //Log.LogMessage("elemental damage " + elementDamage);
                    //Log.LogMessage("weapon Element damage " + weapon.m_shared.m_damages.m_frost);
                }
            }

            //Log.LogWarning(13);
            //Ranger weapon bonus
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife"))
            {
                //Log.LogInfo(1);
                if (character.GetSEMan().HaveStatusEffect("Ranger Weapon Bonus"))
                {
                    //Log.LogInfo(2);
                    weapon.m_shared.m_attack.m_damageMultiplier += (character.GetSEMan().GetStatusEffect("Ranger Weapon Bonus") as SE_RangerWeaponBonus).GetDamageBonus();
                }
            }

            //Log.LogWarning(14);
            //Add Spirit damage to all weapons
            if (character.GetSEMan().HaveStatusEffect("Spirit Damage Bonus"))
            {
                SE_SpiritDamageBonus effect = character.GetSEMan().GetStatusEffect("Spirit Damage Bonus") as SE_SpiritDamageBonus;
                weapon.m_shared.m_damages.m_spirit += effect.GetDamageBonus();
                //Log.LogMessage("weapon spirit damage " + weapon.m_shared.m_damages.m_spirit);
            }

            //Log.LogWarning(15);
            //Red Tearstone Ring
            if (character.GetSEMan().HaveStatusEffect("Wolftears"))
            {
                SE_Wolftears effect = character.GetSEMan().GetStatusEffect("Wolftears") as SE_Wolftears;
                weapon.m_shared.m_attack.m_damageMultiplier += effect.GetDamageBonus();
                //Log.LogInfo("Damage Bonus " + effect.GetDamageBonus());
            }

            //Log.LogWarning(16);
            //Damage Bonus on Full HP
            if (character.GetSEMan().HaveStatusEffect("Battle Furor"))
            {
                SE_FullHPDamageBonus effect = character.GetSEMan().GetStatusEffect("Battle Furor") as SE_FullHPDamageBonus;
                if (character.GetHealthPercentage() >= effect.GetActivationHP())
                {
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.GetDamageBonus();
                }
            }

            //Log.LogWarning(17);
            if (character.GetSEMan().HaveStatusEffect("Chosen") && weapon.m_shared.m_name.Contains("Obsidian Dagger") && __instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation)
            {
                (character.GetSEMan().GetStatusEffect("Chosen") as SE_Chosen).OnKnifeUse();
            }

            //Log.LogWarning(18);
            if (character.GetSEMan().HaveStatusEffect("ShieldFireParryListener"))
            {
                if (weapon.m_shared.m_name.Contains("knife_fire"))
                    weapon.m_shared.m_damages.m_damage += 5f;
                else if (weapon.m_shared.m_name.Contains("spear_fire"))
                    weapon.m_shared.m_damages.m_damage += 15f;
            }

            //Log.LogWarning(19);
            //Wyrdarrow
            if (character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
            {
                Log.LogInfo("Wyrd Active");
                if (weapon.m_shared.m_name.Contains("_knife") && __instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation)
                {
                    var effect = character.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter;
                    var damageBonus = (weapon.m_shared.m_damages.GetTotalDamage() * effect.GetDamageBonus()) / 2;
                    AssetHelper.TestProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_radius = effect.GetAoESize();

                    AssetHelper.TestProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_damage.m_frost = damageBonus;
                    AssetHelper.TestProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_damage.m_spirit = damageBonus;

                    //Log.LogInfo("Terraheim | Aoe deals " + damageBonus + " frost and " + damageBonus + " spirit damage.");

                    __instance.m_attackProjectile = AssetHelper.TestProjectile;
                    __instance.m_attackType = Attack.AttackType.Projectile;
                }
            }

            if (character.GetSEMan().HaveStatusEffect("Mercenary") && (__instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation || __instance.m_attackAnimation == weapon.m_shared.m_attack.m_attackAnimation))
            {
                SE_Mercenary effect = character.GetSEMan().GetStatusEffect("Mercenary") as SE_Mercenary;
                if(effect.GetCurrentDamage() > 0f &&  __instance.m_attackStamina < (character as Player).GetStamina())
                {
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.GetCurrentDamage();
                    character.GetInventory().RemoveItem("$item_coins", 1);
                    Log.LogWarning(character.GetInventory().CountItems("$item_coins"));
                }
            }

            if(character.GetSEMan().HaveStatusEffect("Stagger Damage"))
            {
                //Log.LogInfo("Stagger Dmg " + weapon.m_shared.m_attack.m_staggerMultiplier);
                weapon.m_shared.m_attack.m_staggerMultiplier += (character.GetSEMan().GetStatusEffect("Stagger Damage") as SE_StaggerDamage).GetStaggerDmg();
                //Log.LogInfo("Stagger Dmg " + weapon.m_shared.m_attack.m_staggerMultiplier);
            }
        }

        [HarmonyPatch(typeof(Attack), "GetAttackStamina")]
        [HarmonyPrefix]
        public static void GetStaminaUsagePrefix(ref Attack __instance, ref Humanoid ___m_character, ref ItemDrop.ItemData ___m_weapon, ref ItemDrop.ItemData ___m_ammoItem)
        {
            //Log.LogMessage("Has Stamina Effect base " + __instance.m_weapon.m_dropPrefab.name);
            if ((___m_character.m_unarmedWeapon && ___m_weapon == ___m_character.m_unarmedWeapon.m_itemData) || ___m_weapon.m_dropPrefab == null )
            {
                //check for all damage bonus
                return;
            }
            var weapon = PrefabManager.Cache.GetPrefab<ItemDrop>(___m_weapon.m_dropPrefab.name);
            if (weapon == null)
            {
                Log.LogMessage("Terraheim (AttackPatch GetStaminaUsage) | Weapon is null, grabbing directly");
                weapon = ObjectDB.instance.GetItemPrefab(___m_weapon.m_dropPrefab.name).GetComponent<ItemDrop>();
            }
            __instance.m_attackStamina = weapon.m_itemData.m_shared.m_attack.m_attackStamina;
            if (___m_character.GetSEMan().HaveStatusEffect("Attack Stamina Use"))
            {
                SE_AttackStaminaUse effect = ___m_character.GetSEMan().GetStatusEffect("Attack Stamina Use") as SE_AttackStaminaUse;               
                Log.LogMessage("Base Stamina " + weapon.m_itemData.m_shared.m_attack.m_attackStamina + " * " + (1f - effect.GetStaminaUse()));
                __instance.m_attackStamina = weapon.m_itemData.m_shared.m_attack.m_attackStamina * (1f-effect.GetStaminaUse());
                Log.LogMessage("modded " + __instance.m_attackStamina);
            }
        }

        [HarmonyPatch(typeof(Attack), "FireProjectileBurst")]
        [HarmonyPrefix]
        public static void FireProjectileBurstPrefix(ref Attack __instance)
        {
            //Log.LogInfo("Firing Proj");
            if (__instance.GetWeapon().m_shared.m_name.Contains("throwingaxe_"))
            {
                Log.LogInfo(__instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_stayAfterHitStatic);
                Log.LogInfo(__instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_hideOnHit);
                __instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_hideOnHit = __instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile;
                //__instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_stayAfterHitStatic = true;
            }
            if (__instance.m_character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
            {
                var effect = __instance.m_character.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter;

                AssetHelper.TestExplosion.GetComponent<Aoe>().m_radius = effect.GetAoESize();
                if(__instance.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                {
                    var damageBonus = (__instance.GetWeapon().GetDamage().GetTotalDamage() + __instance.m_ammoItem.m_shared.m_damages.GetTotalDamage()
                        * effect.GetDamageBonus()) / 2;
                    if (__instance.GetWeapon().m_shared.m_name.Contains("bow_fireTH"))
                    {
                        AssetHelper.FlamebowWyrdExplosion.GetComponent<Aoe>().m_damage.m_spirit = damageBonus;
                        AssetHelper.FlamebowWyrdExplosion.GetComponent<Aoe>().m_damage.m_frost = damageBonus;

                        Log.LogInfo("Terraheim | Aoe deals " + damageBonus + " frost and " + damageBonus + " spirit damage.");

                        __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.FlamebowWyrdExplosion;
                        __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1;
                    }
                    else
                    {
                        AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_spirit = damageBonus;
                        AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_frost = damageBonus;

                        Log.LogInfo("Terraheim | Aoe deals " + damageBonus + " frost and " + damageBonus + " spirit damage.");

                        __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.TestExplosion;
                        __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1;
                    }
                    
                } else if (__instance.GetWeapon().m_shared.m_name.Contains("spear"))
                {
                    var damageBonus = (__instance.GetWeapon().GetDamage().GetTotalDamage() * effect.GetDamageBonus()) / 2;
                    AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_spirit = damageBonus;
                    AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_frost = damageBonus;
                    Log.LogInfo("Terraheim | Aoe deals " + damageBonus + " frost and " + damageBonus + " spirit damage.");

                    __instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.TestExplosion;
                    __instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1;
                }
            }
            else if (__instance.m_character.IsPlayer() && __instance.GetWeapon().m_shared.m_name.Contains("bow_fireTH"))
            {
                JObject balance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");
                
                __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.BowFireExplosionPrefab;
                __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_damage.m_fire = (float)balance["BowFire"]["effectVal"];
                __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1;
            }
            else if(__instance.m_character.IsPlayer() && __instance.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                var explosion = AssetHelper.BowFireExplosionPrefab;
                if(__instance.m_ammoItem != null &&
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile != null && 
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>() != null && 
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit == explosion)
                {
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = null;
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 0;
                }
                
            }
            if (__instance.m_character.GetSEMan().HaveStatusEffect("Throwing Weapon Velocity"))
            {
                if (__instance.GetWeapon().m_shared.m_name.Contains("_throwingaxe"))
                {
                    //Log.LogInfo(__instance.m_projectileVel);
                    __instance.m_projectileVel += __instance.m_projectileVel * (1 + (__instance.m_character.GetSEMan().GetStatusEffect("Throwing Weapon Velocity") as SE_ThrowingWeaponVelocity).GetVelocityBonus());
                }
                

            }
        }

        [HarmonyPatch(typeof(Attack), "FireProjectileBurst")]
        [HarmonyPostfix]
        public static void FireProjectileBurstPostfix(ref Attack __instance)
        {
            if (__instance.m_character.GetSEMan().HaveStatusEffect("Wyrdarrow"))
            {
                if (__instance.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow && __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit == AssetHelper.TestExplosion)
                {
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = null;
                    __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 0;
                }
                else if (__instance.GetWeapon().m_shared.m_name.Contains("spear") && __instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit == AssetHelper.TestExplosion)
                {
                    __instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = null;
                    __instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 0;
                }
                if (__instance.m_character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
                {
                    var effect = __instance.m_character.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter;
                    effect.ClearCounter();
                }
            }
        }
    }
}
