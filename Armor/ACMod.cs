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
            var wolftears = ScriptableObject.CreateInstance<SE_FullHPDamageBonus>();
            wolftears.SetDamageBonus((float)setBalance["setBonusVal"]);
            wolftears.SetActivationHP((float)setBalance["setActivationHP"]);
            wolftears.InitIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {
                if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                {
                    item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                    item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                    item.m_itemData.m_shared.m_setStatusEffect = wolftears;
                    item.m_itemData.m_shared.m_setSize = 3;
                    item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                    if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                        item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                }
                item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["berserker"]+ $"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var axeDamageBonus = ScriptableObject.CreateInstance<SE_OneHandDamageBonus>();
            var dodgeStamUse = ScriptableObject.CreateInstance<SE_DodgeStamUse>();
            var foodUse = ScriptableObject.CreateInstance<SE_FoodUsage>();
            var hpOnHit = ScriptableObject.CreateInstance<SE_HPOnHit>();
            

            //Configure Status Effects
            axeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description = helmet.m_itemData.m_shared.m_description + $"\nDamage with one handed weapons is increased by <color=cyan>{axeDamageBonus.getDamageBonus() * 100}%</color> when there is no item in the off hand.";

            dodgeStamUse.setDodgeStaminaUse((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description = chest.m_itemData.m_shared.m_description + $"\nDodge stamina cost is reduced by <color=cyan>{System.Math.Round((dodgeStamUse.getDodgeStaminaUse()) * 100)}%</color>.";

            hpOnHit.setHealAmount((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description = legs.m_itemData.m_shared.m_description + $"\n<color=cyan>Heal " + (hpOnHit.getHealAmount() * 100) + "%</color> of damage dealt as HP on hitting an enemy with a melee weapon.";

            cape.m_itemData.m_shared.m_description = cape.m_itemData.m_shared.m_description + $"\nMovement speed is increased by <color=cyan>"+(float)setBalance["capeEffectVal"]*100+"%</color>.";

            foodUse.SetFoodUsage((float)balance["loxEffectVal"]);
            loxCape.m_itemData.m_shared.m_description += $"\nFood fullness degrades <color=cyan>" + foodUse.GetFoodUsage() * 100 + "%</color> slower.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = axeDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = dodgeStamUse;
            legs.m_itemData.m_shared.m_equipStatusEffect = hpOnHit;
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
            var rangedDamageBonus = ScriptableObject.CreateInstance<SE_RangedDmgBonus>();
            var daggerSpearDamageBonus = ScriptableObject.CreateInstance<SE_DaggerSpearDmgBonus>();
            var ammoConsumption = ScriptableObject.CreateInstance<SE_AmmoConsumption>();
            var backstabBonus = ScriptableObject.CreateInstance<SE_BackstabBonus>();

            //configure status effects
            rangedDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            trollHood.m_itemData.m_shared.m_description = trollHood.m_itemData.m_shared.m_description + $"\nBow Damage is increased by <color=cyan>{System.Math.Round((rangedDamageBonus.getDamageBonus()) * 100)}%</color>.";
                       
            ammoConsumption.setAmmoConsumption((int)setBalance["chestEffectVal"]);
            trollChest.m_itemData.m_shared.m_description = trollChest.m_itemData.m_shared.m_description + $"\n<color=cyan>{ammoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";

            daggerSpearDamageBonus.setDamageBonus((float)setBalance["legsEffectVal"]);
            trollLegs.m_itemData.m_shared.m_description = trollLegs.m_itemData.m_shared.m_description + $"\nDagger Damage is increased by <color=cyan>{System.Math.Round((daggerSpearDamageBonus.getDamageBonus()) * 100)}%</color>.\nSpear Damage is increased by <color=cyan>{System.Math.Round(daggerSpearDamageBonus.getDamageBonus() * 100)}%</color>.";

            backstabBonus.setBackstabBonus((float)setBalance["capeEffectVal"]);
            trollCape.m_itemData.m_shared.m_description = trollCape.m_itemData.m_shared.m_description + $"\nBackstab Damage is increased by <color=cyan>{backstabBonus.getBackstabBonus()}x</color>.";


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
            var hpRegen = ScriptableObject.CreateInstance<SE_HPRegen>();
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
            var meleeDamageBonus = ScriptableObject.CreateInstance<SE_MeleeDamageBonus>();
            var blockStamUse = ScriptableObject.CreateInstance<SE_BlockStamUse>();
            var hpBonus = ScriptableObject.CreateInstance<SE_HealthIncrease>();

            //Configure Status Effects
            meleeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nMelee Damage is increased by <color=cyan>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";

            blockStamUse.setBlockStaminaUse((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nBase block stamina cost is reduced by <color=cyan>{System.Math.Round((blockStamUse.getBlockStaminaUse()) * 100)}%</color>.";

            hpBonus.setHealthBonus((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += $"\nHP is increased by <color=cyan>"+hpBonus.getHealthBonus()+"</color>.";

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
            var critChance = ScriptableObject.CreateInstance<SE_Wolftears>();
            critChance.SetDamageBonus((float)setBalance["setBonusVal"]);
            critChance.SetActivationHP((float)setBalance["setActivationHP"]);
            //critChance.SetIcon();

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
            var meleeDamageBonus = ScriptableObject.CreateInstance<SE_TwoHandedDmgBonus>();
            var extraStamina = ScriptableObject.CreateInstance<SE_ExtraStamina>();
            var stamRegen = ScriptableObject.CreateInstance<SE_StaminaRegen>();

            //Configure Status Effects
            meleeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nTwo-Handed weapons damage is increased by <color=cyan>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";

            extraStamina.SetStaminaBonus((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nStamina is increased by <color=cyan>{extraStamina.GetStaminaBonus()}</color> points.";

            stamRegen.SetRegenPercent((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += $"\nStamina regen is increased by <color=cyan>" + stamRegen.GetRegenPercent()*100 + "%</color>.";

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
            var sneakDamageBonus = ScriptableObject.CreateInstance<SE_AoECounter>();
            sneakDamageBonus.SetDamageBonus((float)setBalance["setBonusVal"]);
            sneakDamageBonus.SetActivationCount((int)setBalance["setActivationCount"]);
            sneakDamageBonus.SetAoESize((float)setBalance["setAoESize"]);
            sneakDamageBonus.SetIcon();

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
            var silverDamageBonus = ScriptableObject.CreateInstance<SE_SilverDamageBonus>();
            var ammoConsumption = ScriptableObject.CreateInstance<SE_AmmoConsumption>();
            var drawMoveSpeed = ScriptableObject.CreateInstance<SE_DrawMoveSpeed>();
            var spiritDamageBonus = ScriptableObject.CreateInstance<SE_SpiritDamageBonus>();

            //Configure Status Effects
            silverDamageBonus.SetDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nBows, daggers, and spears gain <color=cyan>{System.Math.Round((silverDamageBonus.GetDamageBonus()) * 100)}%</color> damage as spirit and frost damage.";

            ammoConsumption.setAmmoConsumption((int)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\n<color=cyan>{ammoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";

            drawMoveSpeed.SetDrawMoveSpeed((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += $"\nMove <color=cyan>" + drawMoveSpeed.GetDrawMoveSpeed() * 100 + "%</color> faster with a drawn bow.";

            spiritDamageBonus.SetDamageBonus((float)setBalance["capeEffectVal"]);
            cape.m_itemData.m_shared.m_description += $"\nAll weapons gain <color=cyan>{spiritDamageBonus.GetDamageBonus()}</color> spirit damage.";

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
            var thorns = ScriptableObject.CreateInstance<SE_Thorns>();
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
            var meleeDamageBonus = ScriptableObject.CreateInstance<SE_MeleeDamageBonus>();
            var blockPower = ScriptableObject.CreateInstance<SE_BlockPowerBonus>();
            var healOnBlock = ScriptableObject.CreateInstance<SE_HealOnBlock>();
            var attackStaminaUse = ScriptableObject.CreateInstance<SE_AttackStaminaUse>();

            //Configure Status Effects
            meleeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nMelee Damage is increased by <color=cyan>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";
            
            blockPower.SetBlockPower((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nBlock power increased by <color=cyan>{blockPower.GetBlockPower() * 100}%</color>.";

            healOnBlock.SetHealBonus((float)setBalance["legsEffectVal"]);
            legs.m_itemData.m_shared.m_description += "\nWhile using a tower shield, a successful block <color=cyan>heals</color> you." +
                "\nWhile using a normal shield, a successful parry <color=cyan>heals</color> you.";

            attackStaminaUse.SetStaminaUse((float)setBalance["capeEffectVal"]);
            cape.m_itemData.m_shared.m_description += $"\nReduce stamina use for melee weapons by <color=cyan>{attackStaminaUse.GetStaminaUse() * 100}%</color>.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = meleeDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = blockPower;
            legs.m_itemData.m_shared.m_equipStatusEffect = healOnBlock;
            cape.m_itemData.m_shared.m_equipStatusEffect = attackStaminaUse;
        }

    }
}
