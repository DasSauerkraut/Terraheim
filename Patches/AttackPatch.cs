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
    class AttackPatch
    {
        public void Awake()
        {
            Log.LogInfo("Attack Patching Complete");
        }

        [HarmonyPatch(typeof(Attack), "Start")]
        public static void Prefix(Humanoid character, ref ItemDrop.ItemData weapon)
        {
            //Log.LogWarning("Attack Start");
            if (character.m_unarmedWeapon && weapon == character?.m_unarmedWeapon.m_itemData)
            {
                //check for all damage bonus
                return;
            }
            //Log.LogWarning("Weapon: " + weapon.m_shared.m_name + " " + weapon.m_dropPrefab.name);
            
            //Get base weapon
            var baseWeapon = Prefab.Cache.GetPrefab<ItemDrop>(weapon.m_dropPrefab.name);

            //set all damages to default values to prevent forever increasing damages
            weapon.m_shared.m_backstabBonus = baseWeapon.m_itemData.m_shared.m_backstabBonus;
            weapon.m_shared.m_damages.m_frost = baseWeapon.m_itemData.m_shared.m_damages.m_frost;
            weapon.m_shared.m_damages.m_spirit = baseWeapon.m_itemData.m_shared.m_damages.m_spirit;
            weapon.m_shared.m_attack.m_damageMultiplier = baseWeapon.m_itemData.m_shared.m_attack.m_damageMultiplier;

            //Bow Damage Effect
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
            if (weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
            {
                if (character.GetSEMan().HaveStatusEffect("Melee Damage Bonus"))
                {
                    SE_MeleeDamageBonus effect = character.GetSEMan().GetStatusEffect("Melee Damage Bonus") as SE_MeleeDamageBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();
                    //Log.LogMessage("weapon  damage " + weapon.m_shared.m_attack.m_damageMultiplier);
                }
            }

            //Dagger/Spear Damage Effect
            if ((weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife")))
            {
                if(character.GetSEMan().HaveStatusEffect("Dagger/Spear Damage Bonus"))
                {
                    SE_DaggerSpearDmgBonus effect = character.GetSEMan().GetStatusEffect("Dagger/Spear Damage Bonus") as SE_DaggerSpearDmgBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();
                }
            }

            //Axe Damage Effect
            if (weapon.m_shared.m_name.Contains("axe") || weapon.m_shared.m_name.Contains("battleaxe"))
            {
                if (character.GetSEMan().HaveStatusEffect("Axe Damage Bonus"))
                {
                    SE_AxeDamageBonus effect = character.GetSEMan().GetStatusEffect("Axe Damage Bonus") as SE_AxeDamageBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();

                }
            }

            //Two Handed Damage Effect
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                if (character.GetSEMan().HaveStatusEffect("Two Handed Damage Bonus"))
                {
                    SE_TwoHandedDmgBonus effect = character.GetSEMan().GetStatusEffect("Two Handed Damage Bonus") as SE_TwoHandedDmgBonus;
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.getDamageBonus();

                }
            }

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
                        //ZSFX sfx = new ZSFX();                        
                    }
                }
            }

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
        
            //Backstab Bonus Effect
            if(character.GetSEMan().HaveStatusEffect("Backstab Bonus"))
            {
                SE_BackstabBonus effect = character.GetSEMan().GetStatusEffect("Backstab Bonus") as SE_BackstabBonus;
                weapon.m_shared.m_backstabBonus = baseWeapon.m_itemData.m_shared.m_backstabBonus + effect.getBackstabBonus();
            }
            
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

            //Add Spirit damage to all weapons
            if (character.GetSEMan().HaveStatusEffect("Spirit Damage Bonus"))
            {
                SE_SpiritDamageBonus effect = character.GetSEMan().GetStatusEffect("Spirit Damage Bonus") as SE_SpiritDamageBonus;
                weapon.m_shared.m_damages.m_spirit += effect.GetDamageBonus();
                //Log.LogMessage("weapon spirit damage " + weapon.m_shared.m_damages.m_spirit);
            }

            //Red Tearstone Ring
            if(character.GetSEMan().HaveStatusEffect("Wolftears"))
            {
                Log.LogWarning("Has Effect");
                SE_SneakDamageBonus effect = character.GetSEMan().GetStatusEffect("Wolftears") as SE_SneakDamageBonus;
                if(character.GetHealthPercentage() <= effect.GetActivationHP())
                {
                    Log.LogWarning("Active");
                    weapon.m_shared.m_attack.m_damageMultiplier += effect.GetDamageBonus();
                }
            }
            /*
            if(character.GetSEMan().HaveStatusEffect("Sneak Damage Bonus"))
            {
                System.Random rand = new System.Random();
                SE_SneakDamageBonus effect = character.GetSEMan().GetStatusEffect("Sneak Damage Bonus") as SE_SneakDamageBonus;

                int roll = rand.Next(1, 100);
                if (roll <= effect.GetAOEChance())
                {
                    if(weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                    {
                        Log.LogWarning("Bow AOE");

                        weapon.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_aoe = effect.GetDamageBonus();
                    } else if (weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife"))
                    {
                        Log.LogWarning("Do AOE");
                        weapon.m_shared.m_attack.DoAreaAttack();
                    }
                }
            }*/
        }

        [HarmonyPatch(typeof(Attack), "GetStaminaUsage")]
        [HarmonyPrefix]
        public static void GetStaminaUsagePrefix(ref Attack __instance)
        {
            //Log.LogMessage("Has Stamina Effect base " + __instance.m_weapon.m_dropPrefab.name);
            if (__instance.m_character.m_unarmedWeapon && __instance.m_weapon == __instance.m_character.m_unarmedWeapon.m_itemData)
            {
                //check for all damage bonus
                return;
            }
            var weapon = Prefab.Cache.GetPrefab<ItemDrop>(__instance.m_weapon.m_dropPrefab.name);
            __instance.m_attackStamina = weapon.m_itemData.m_shared.m_attack.m_attackStamina;
            if (__instance.m_character.GetSEMan().HaveStatusEffect("Attack Stamina Use"))
            {
                //JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
                //float baseStaminaUse = (float)balance["baseDodgeStaminaUse"];
                SE_AttackStaminaUse effect = __instance.m_character.GetSEMan().GetStatusEffect("Attack Stamina Use") as SE_AttackStaminaUse;               
                Log.LogMessage("Base Stamina " + weapon.m_itemData.m_shared.m_attack.m_attackStamina + " * " + (1f - effect.GetStaminaUse()));
                __instance.m_attackStamina = weapon.m_itemData.m_shared.m_attack.m_attackStamina * (1f-effect.GetStaminaUse());
                Log.LogMessage("modded " + __instance.m_attackStamina);
            }
        }
    }
}
