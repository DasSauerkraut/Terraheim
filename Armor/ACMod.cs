using ValheimLib;
using ValheimLib.ODB;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Terraheim;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace Terraheim.Armor
{
    internal static class ACMod
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ObjectDBHelper.OnAfterInit += ModLeatherArmor;
            ObjectDBHelper.OnAfterInit += ModTrollArmor;
            ObjectDBHelper.OnAfterInit += ModBronzeArmor;
            ObjectDBHelper.OnAfterInit += ModIronArmor;
            ObjectDBHelper.OnAfterInit += ModSilverArmor;
            ObjectDBHelper.OnAfterInit += ModPaddedArmor;
        }

        private static void ModLeatherArmor()
        {
            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetLeather");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorLeatherChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorLeatherLegs");
            var cape = Prefab.Cache.GetPrefab<ItemDrop>("CapeDeerHide");
            var loxCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeLox");

            var setBalance = balance["leather"];

            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs, cape, loxCape };

            //Create Set effect
            var hpOnHit = Prefab.Cache.GetPrefab<SE_HPOnHit>("Life Steal");
            hpOnHit.setHealAmount((float)setBalance["setBonusVal"]);
            hpOnHit.SetIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {
                if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                {
                    item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                    item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                    item.m_itemData.m_shared.m_setStatusEffect = hpOnHit;
                    item.m_itemData.m_shared.m_setSize = 3;
                    item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                    if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                        item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                }
                item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["berserker"]+ $"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var axeDamageBonus = Prefab.Cache.GetPrefab<SE_AxeDamageBonus>("Axe Damage Bonus");
            var dodgeStamUse = Prefab.Cache.GetPrefab<SE_DodgeStamUse>("Dodge Stamina Use");
            var foodUse = Prefab.Cache.GetPrefab<SE_FoodUsage>("Food Usage");

            //Configure Status Effects
            axeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description = helmet.m_itemData.m_shared.m_description + $"\nAxe Damage is increased by <color=yellow>{System.Math.Round((axeDamageBonus.getDamageBonus()) * 100)}%</color>.";

            dodgeStamUse.setDodgeStaminaUse((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description = chest.m_itemData.m_shared.m_description + $"\nDodge stamina cost is reduced by <color=yellow>{System.Math.Round((dodgeStamUse.getDodgeStaminaUse()) * 100)}%</color>.";

            legs.m_itemData.m_shared.m_description = legs.m_itemData.m_shared.m_description + $"\nMovement speed is increased by <color=yellow>"+(float)setBalance["legsEffectVal"]*100+"%</color>.";
            cape.m_itemData.m_shared.m_description = cape.m_itemData.m_shared.m_description + $"\nMovement speed is increased by <color=yellow>"+(float)setBalance["capeEffectVal"]*100+"%</color>.";

            foodUse.SetFoodUsage((float)balance["loxEffectVal"]);
            loxCape.m_itemData.m_shared.m_description += $"\nFood fullness degrades <color=yellow>" + foodUse.GetFoodUsage() * 100 + "%</color> slower.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = axeDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = dodgeStamUse;
            legs.m_itemData.m_shared.m_movementModifier = (float)setBalance["legsEffectVal"];
            cape.m_itemData.m_shared.m_movementModifier = (float)setBalance["capeEffectVal"];
            loxCape.m_itemData.m_shared.m_equipStatusEffect = foodUse;
        }

        private static void ModTrollArmor()
        {
            var setBalance = balance["trollLeather"];

            var trollHood = Prefab.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather");
            var trollChest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherChest");
            var trollLegs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherLegs");
            var trollCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeTrollHide");
            List<ItemDrop> trollList = new List<ItemDrop>() { trollHood, trollChest, trollLegs, trollCape};

            //Adjust Stats
            foreach(ItemDrop item in trollList){
                if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                {
                    item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"]; ;
                    item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"]; ;
                } 
                else
                {
                    item.m_itemData.m_shared.m_setStatusEffect = null;
                    item.m_itemData.m_shared.m_setName = null;
                }
                if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                    item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                item.m_itemData.m_shared.m_setSize = 3;
                item.m_itemData.m_shared.m_description = $"<i>"+(string)balance["classes"]["ranger"]+$"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //create status effects
            var rangedDamageBonus = Prefab.Cache.GetPrefab<SE_RangedDmgBonus>("Ranged Damage Bonus");
            var daggerSpearDamageBonus = Prefab.Cache.GetPrefab<SE_DaggerSpearDmgBonus>("Dagger/Spear Damage Bonus");
            var ammoConsumption = Prefab.Cache.GetPrefab<SE_AmmoConsumption>("Ammo Consumption");
            var backstabBonus = Prefab.Cache.GetPrefab<SE_BackstabBonus>("Backstab Bonus");

            //configure status effects
            rangedDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            trollHood.m_itemData.m_shared.m_description = trollHood.m_itemData.m_shared.m_description + $"\nBow Damage is increased by <color=yellow>{System.Math.Round((rangedDamageBonus.getDamageBonus()) * 100)}%</color>.";
                       
            ammoConsumption.setAmmoConsumption((int)setBalance["chestEffectVal"]);
            trollChest.m_itemData.m_shared.m_description = trollChest.m_itemData.m_shared.m_description + $"\n<color=yellow>{ammoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";

            daggerSpearDamageBonus.setDamageBonus((float)setBalance["legsEffectVal"]);
            trollLegs.m_itemData.m_shared.m_description = trollLegs.m_itemData.m_shared.m_description + $"\nDagger Damage is increased by <color=yellow>{System.Math.Round((daggerSpearDamageBonus.getDamageBonus()) * 100)}%</color>.\nSpear Damage is increased by <color=yellow>{System.Math.Round(daggerSpearDamageBonus.getDamageBonus() * 100)}%</color>.";

            backstabBonus.setBackstabBonus((float)setBalance["capeEffectVal"]);
            trollCape.m_itemData.m_shared.m_description = trollCape.m_itemData.m_shared.m_description + $"\nBackstab Damage is increased by <color=yellow>{backstabBonus.getBackstabBonus()}x</color>.";


            //Assign Effects
            trollHood.m_itemData.m_shared.m_equipStatusEffect = rangedDamageBonus;
            trollChest.m_itemData.m_shared.m_equipStatusEffect = ammoConsumption;
            trollLegs.m_itemData.m_shared.m_equipStatusEffect = daggerSpearDamageBonus;
            trollCape.m_itemData.m_shared.m_equipStatusEffect = backstabBonus;
        }

        private static void ModBronzeArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetBronze");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBronzeChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");

            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs };
            
            var setBalance = balance["bronze"];

            //Create Set effect
            var hpRegen = Prefab.Cache.GetPrefab<SE_HPRegen>("HP Regen");
            hpRegen.setHealPercent((float)setBalance["setBonusVal"]);
            hpRegen.SetIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {

                item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                item.m_itemData.m_shared.m_setStatusEffect = hpRegen;
                item.m_itemData.m_shared.m_setSize = 3;
                item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                    item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];

                item.m_itemData.m_shared.m_description = $"<i>"+balance["classes"]["tank"]+$"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var meleeDamageBonus = Prefab.Cache.GetPrefab<SE_MeleeDamageBonus>("Melee Damage Bonus");
            var blockStamUse = Prefab.Cache.GetPrefab<SE_BlockStamUse>("Block Stamina Use");
            var hpBonus = Prefab.Cache.GetPrefab<SE_HealthIncrease>("Health Increase");

            //Configure Status Effects
            meleeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nMelee Damage is increased by <color=yellow>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";

            blockStamUse.setBlockStaminaUse((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nBase block stamina cost is reduced by <color=yellow>{System.Math.Round((blockStamUse.getBlockStaminaUse()) * 100)}%</color>.";

            hpBonus.setHealthBonus((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += $"\nHP is increased by <color=yellow>"+hpBonus.getHealthBonus()+"</color>.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = meleeDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = blockStamUse;
            legs.m_itemData.m_shared.m_equipStatusEffect = hpBonus;

        }

        private static void ModIronArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetIron");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorIronChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorIronLegs");

            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs };

            var setBalance = balance["iron"];

            //Create Set effect
            var critChance = Prefab.Cache.GetPrefab<SE_CritChance>("Crit Chance");
            critChance.SetCritChance((float)setBalance["setBonusVal"]);
            critChance.SetCritBonus((float)setBalance["setCritBonus"]);
            critChance.SetIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {
                item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                item.m_itemData.m_shared.m_setStatusEffect = critChance;
                item.m_itemData.m_shared.m_setSize = 3;
                item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                if(!item.m_itemData.m_shared.m_name.Contains("helmet"))
                    item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                item.m_itemData.m_shared.m_description = $"<i>" + balance["classes"]["berserker"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var meleeDamageBonus = Prefab.Cache.GetPrefab<SE_TwoHandedDmgBonus>("Two Handed Damage Bonus");
            var extraStamina = Prefab.Cache.GetPrefab<SE_ExtraStamina>("Extra Stamina");
            var stamRegen = Prefab.Cache.GetPrefab<SE_StaminaRegen>("Stamina Regen");

            //Configure Status Effects
            meleeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nTwo-Handed weapons damage is increased by <color=yellow>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";

            extraStamina.SetStaminaBonus((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nStamina is increased by <color=yellow>{extraStamina.GetStaminaBonus()}</color> points.";

            stamRegen.SetRegenPercent((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += $"\nStamina regen is increased by <color=yellow>" + stamRegen.GetRegenPercent()*100 + "%</color>.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = meleeDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = extraStamina;
            legs.m_itemData.m_shared.m_equipStatusEffect = stamRegen;

        }

        private static void ModSilverArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetDrake");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorWolfChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorWolfLegs");
            var cape = Prefab.Cache.GetPrefab<ItemDrop>("CapeWolf");

            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs, cape };

            var setBalance = balance["silver"];

            //Create Set effect
            var sneakDamageBonus = ScriptableObject.CreateInstance<SE_SneakDamageBonus>();
            sneakDamageBonus.SetDamageBonus((float)setBalance["setBonusVal"]);
            sneakDamageBonus.SetActivationHP((float)setBalance["setActivationHP"]);
            //sneakDamageBonus.SetIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {
                if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                {
                    item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                    item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                    item.m_itemData.m_shared.m_setStatusEffect = sneakDamageBonus;
                    item.m_itemData.m_shared.m_setSize = 3;
                    item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                    if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                        item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                }                
                item.m_itemData.m_shared.m_description = $"<i>" + balance["classes"]["ranger"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var silverDamageBonus = Prefab.Cache.GetPrefab<SE_SilverDamageBonus> ("Silver Damage Bonus");
            var ammoConsumption = Prefab.Cache.GetPrefab<SE_AmmoConsumption>("Ammo Consumption");
            var drawMoveSpeed = Prefab.Cache.GetPrefab<SE_DrawMoveSpeed>("Draw Move Speed");
            var spiritDamageBonus = Prefab.Cache.GetPrefab<SE_SpiritDamageBonus>("Spirit Damage Bonus");

            //Configure Status Effects
            silverDamageBonus.SetDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nBows, daggers, and spears gain <color=yellow>{System.Math.Round((silverDamageBonus.GetDamageBonus()) * 100)}%</color> damage as spirit and frost damage.";

            ammoConsumption.setAmmoConsumption((int)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\n<color=yellow>{ammoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";

            drawMoveSpeed.SetDrawMoveSpeed((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += $"\nMove <color=yellow>" + drawMoveSpeed.GetDrawMoveSpeed() * 100 + "%</color> faster with a drawn bow.";

            spiritDamageBonus.SetDamageBonus((float)setBalance["capeEffectVal"]);
            cape.m_itemData.m_shared.m_description += $"\nAll weapons gain <color=yellow>{spiritDamageBonus.GetDamageBonus()}</color> spirit damage.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = silverDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = ammoConsumption;
            legs.m_itemData.m_shared.m_equipStatusEffect = drawMoveSpeed;
            cape.m_itemData.m_shared.m_equipStatusEffect = spiritDamageBonus;
        }

        private static void ModPaddedArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetPadded");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorPaddedCuirass");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorPaddedGreaves");
            var cape = Prefab.Cache.GetPrefab<ItemDrop>("CapeLinen");

            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs, cape };
            var setBalance = balance["padded"];

            //Create Set effect
            var thorns = Prefab.Cache.GetPrefab<SE_Thorns>("Thorns");
            thorns.SetReflectPercent((float)setBalance["setBonusVal"]);
            thorns.SetIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {
                if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                {
                    item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                    item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                    item.m_itemData.m_shared.m_setStatusEffect = thorns;
                    item.m_itemData.m_shared.m_setSize = 3;
                    item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                    if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                        item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                }
                item.m_itemData.m_shared.m_description = $"<i>" + balance["classes"]["tank"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var meleeDamageBonus = Prefab.Cache.GetPrefab<SE_MeleeDamageBonus>("Melee Damage Bonus");
            var blockPower = Prefab.Cache.GetPrefab<SE_BlockPowerBonus>("Block Power Bonus");
            var staminaRegen = Prefab.Cache.GetPrefab<SE_StaminaRegen>("Stamina Regen");
            var attackStaminaUse = Prefab.Cache.GetPrefab<SE_AttackStaminaUse>("Attack Stamina Use");

            //Configure Status Effects
            meleeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nMelee Damage is increased by <color=yellow>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";
            
            blockPower.SetBlockPower((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nBlock power increased by <color=yellow>{blockPower.GetBlockPower() * 100}%</color>.";

            staminaRegen.SetRegenPercent((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += "\nStamina regen is increased by <color=yellow>" + staminaRegen.GetRegenPercent() * 100 + "%</color>.";

            attackStaminaUse.SetStaminaUse((float)setBalance["capeEffectVal"]);
            cape.m_itemData.m_shared.m_description += $"\nReduce stamina use for melee weapons by <color=yellow>{attackStaminaUse.GetStaminaUse() * 100}%</color>.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = meleeDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = blockPower;
            legs.m_itemData.m_shared.m_equipStatusEffect = staminaRegen;
            cape.m_itemData.m_shared.m_equipStatusEffect = attackStaminaUse;
        }

    }
}
