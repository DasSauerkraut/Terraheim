﻿using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Terraheim.ArmorEffects;
using UnityEngine;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using static Terraheim.Utility.Data;
using System;

namespace Terraheim.Utility
{
    class ArmorHelper
    {
        static readonly JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        public static StatusEffect GetSetEffect(string name, JToken values)
        {
            switch (name)
            {
                case "battlefuror":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_FullHPDamageBonus>();
                        effect.SetDamageBonus((float)values["setBonusVal"]);
                        effect.SetActivationHP((float)values["setActivationHP"]);
                        effect.InitIcon();
                        return effect;
                    }
                case "hpregen":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_HPRegen>();
                        effect.setHealPercent((float)values["setBonusVal"]);
                        effect.SetIcon();
                        return effect;
                    }
                case "wolftears":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_Wolftears>();
                        effect.SetDamageBonus((float)values["setBonusVal"]);
                        effect.SetActivationHP((float)values["setActivationHP"]);
                        return effect;
                    }
                case "wyrdarrow":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_AoECounter>();
                        effect.SetDamageBonus((float)values["setBonusVal"]);
                        effect.SetActivationCount((int)values["setActivationCount"]);
                        effect.SetAoESize((float)values["setAoESize"]);
                        effect.SetIcon();
                        return effect;
                    }
                case "thorns":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_Thorns>();
                        effect.SetReflectPercent((float)values["setBonusVal"]);
                        effect.SetIcon();
                        return effect;
                    }
                case "deathmark":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_DeathMark>();
                        effect.SetDamageBonus((float)values["setBonusVal"]);
                        effect.SetThreshold((int)values["setBonusThreshold"]);
                        effect.SetHitDuration((int)values["setBonusDuration"]);
                        effect.SetIcon();
                        return effect;
                    }
                case "brassflesh":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ArmorOnHitListener>();
                        effect.SetMaxArmor((float)values["setBonusVal"]);
                        return effect;
                    }
                case "pinning":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_Pinning>();
                        effect.SetPinTTL((float)values["setBonusVal"]);
                        effect.SetPinCooldownTTL((float)values["setBonusCooldown"]);
                        effect.SetIcon();
                        return effect;
                    }
                case "crit":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_CritChance>();
                        effect.SetCritChance((float)values["setBonusChance"]);
                        effect.SetCritBonus((float)values["setBonusVal"]);
                        effect.SetIcon();
                        return effect;
                    }
                case "sprinter":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ChallengeSprinter>();
                        effect.SetTotalStamina((float)values["setBonusVal"]);
                        effect.SetRegen((float)values["setRegenBonusVal"]);
                        effect.SetIcon();
                        return effect;
                    }
                default:
                    return null;
            }
        }

        public static StatusEffect GetArmorEffect(string name, JToken values, string location, ref string description)
        {
            switch (name)
            {
                case "onehanddmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_OneHandDamageBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nDamage with one handed weapons is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color> when there is no item in the off hand.";
                        return effect;
                    }
                case "dodgestamuse":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_DodgeStamUse>();
                        effect.setDodgeStaminaUse((float)values[$"{location}EffectVal"]);
                        description += $"\nDodge stamina cost is reduced by <color=cyan>{effect.getDodgeStaminaUse() * 100}%</color>.";
                        return effect;
                    }
                case "lifesteal":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_HPOnHit>();
                        effect.setHealAmount((float)values[$"{location}EffectVal"]);
                        description += $"\n<color=cyan>Heal " + (effect.getHealAmount() * 100) + "%</color> of damage dealt as HP on hitting an enemy with a melee weapon.";
                        return effect;
                    }
                case "rangeddmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_RangedDmgBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nBow Damage is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "daggerspeardmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_DaggerSpearDmgBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nDagger Damage is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color>." +
                            $"\nSpear Damage is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "ammoconsumption":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_AmmoConsumption>();
                        effect.setAmmoConsumption((int)values[$"{location}EffectVal"]);
                        description += $"\n<color=cyan>{effect.getAmmoConsumption()}%</color> chance to not consume ammo.";
                        return effect;
                    }
                case "meleedmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_MeleeDamageBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nMelee Damage is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "blockstam":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_BlockStamUse>();
                        effect.setBlockStaminaUse((float)values[$"{location}EffectVal"]);
                        description += $"\nBase block stamina cost is reduced by <color=cyan>{effect.getBlockStaminaUse() * 100}%</color>.";
                        return effect;
                    }
                case "hpbonus":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_HealthIncrease>();
                        effect.setHealthBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nMax HP is increased by <color=cyan>{effect.getHealthBonus()*100}%</color>.";
                        return effect;
                    }
                case "twohanddmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_TwoHandedDmgBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nTwo-Handed weapons damage is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "staminabns":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ExtraStamina>();
                        effect.SetStaminaBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nStamina is increased by <color=cyan>{effect.GetStaminaBonus()}</color> points.";
                        return effect;
                    }
                case "staminaregen":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_StaminaRegen>();
                        effect.SetRegenPercent((float)values[$"{location}EffectVal"]);
                        description += $"\nStamina regen is increased by <color=cyan>{effect.GetRegenPercent() * 100}%</color>.";
                        return effect;
                    }
                case "bowdaggerspearsilverdmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SilverDamageBonus>();
                        effect.SetDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nBows, daggers, and spears gain <color=cyan>{effect.GetDamageBonus() * 100}%</color> damage as spirit and frost damage.";
                        return effect;
                    }
                case "drawmovespeed":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_DrawMoveSpeed>();
                        effect.SetDrawMoveSpeed((float)values[$"{location}EffectVal"]);
                        description += $"\nMove <color=cyan>{effect.GetDrawMoveSpeed() * 100}%</color> faster with a drawn bow.";
                        return effect;
                    }
                case "blockpower":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_BlockPowerBonus>();
                        effect.SetBlockPower((float)values[$"{location}EffectVal"]);
                        description += $"\nBlock power increased by <color=cyan>{effect.GetBlockPower() * 100}%</color>.";
                        return effect;
                    }
                case "healonblock":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_HealOnBlock>();
                        effect.SetHealBonus((float)values[$"{location}EffectVal"]);
                        description += "\nWhile using a tower shield, a successful block <color=cyan>heals</color> you." +
                            "\nWhile using a normal shield, a successful parry <color=cyan>heals</color> you.";
                        return effect;
                    }
                case "backstabdmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_BackstabBonus>();
                        effect.setBackstabBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nSneak Attack Damage is increased by <color=cyan>{effect.getBackstabBonus()}x</color>.";
                        return effect;
                    }
                case "spiritdmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SpiritDamageBonus>();
                        effect.SetDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nAll weapons gain <color=cyan>{effect.GetDamageBonus()}</color> spirit damage.";
                        return effect;
                    }
                case "fooduse":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_FoodUsage>();
                        effect.SetFoodUsage((float)values[$"{location}EffectVal"]);
                        description += $"\nFood fullness degrades <color=cyan>{effect.GetFoodUsage() * 100 }%</color> slower.";
                        return effect;
                    }
                case "attackstamina":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_AttackStaminaUse>();
                        effect.SetStaminaUse((float)values[$"{location}EffectVal"]);
                        description += $"\nReduce stamina use for melee weapons by <color=cyan>{effect.GetStaminaUse() * 100}%</color>.";
                        return effect;
                    }
                case "throwdmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ThrowingDamageBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nDamage with throwing weapons is increased by <color=cyan>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "throwvel":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>();
                        effect.SetVelocityBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nThrowing weapons velocity is increased by <color=cyan>{effect.GetVelocityBonus() * 100}%</color>.";
                        return effect;
                    }
                case "dmgvslowhp":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_DamageVSLowHP>();
                        effect.SetDamageBonus((float)values[$"{location}EffectVal"]);
                        effect.SetHealthThreshold((float)values[$"{location}EffectThreshold"]);
                        description += $"\nDamage against enemies with less than <color=cyan>" + (effect.GetHealthThreshold() * 100) +
                            "%</color> HP is increased by <color=cyan>" + effect.GetDamageBonus() * 100 + "%</color>.";
                        return effect;
                    }
                case "speedonkill":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_MoveSpeedOnKillListener>();
                        effect.SetSpeedBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nAfter killing an enemy, gain a stacking <color=cyan>+{effect.GetSpeedBonus() * 100}%</color> movement speed bonus for {(float)balance["bloodrushTTL"]}" +
                        $" seconds. Killing an enemy resets the countdown.";
                        return effect;
                    }
                case "woodsman":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_TreeDamageBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"Weland's designs raises your effectiveness at woodcutting.\nDamage against trees is increased by <color=yellow>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "miner":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_MiningBonus>();
                        effect.setDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"An artifact of Brokkr, crushing earth comes easily now.\nDamage against rocks and ores is increased by <color=yellow>{effect.getDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "waterproof":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_WetImmunity>();
                        description += $"A gift from Freyr.\nYou will not gain the <color=cyan>Wet</color> Ailment.";
                        return effect;
                    }
                case "farmer":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_IncreaseHarvest>();
                        description += $"A blessing from Freyr.\nWhen you harvest wild plants, gain <color=cyan>2</color> more items from each harvest.\nWhen you harvest grown plants, gain <color=cyan>1</color> more item from each harvest.";
                        return effect;
                    }
                case "thief":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_SneakMovement>();
                        effect.SetBonus((float)values[$"{location}EffectVal"]);
                        description += $"A blessing from Loki.\nMove <color=cyan>{effect.GetBonus() * 100}%</color> faster while using <color=cyan>{effect.GetBonus() * 100}%</color> less stamina while sneaking.";
                        return effect;
                    }
                case "rangerweapondmg":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_RangerWeaponBonus>();
                        effect.SetDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nDamage with bows, daggers, and spears is increased by <color=cyan>{effect.GetDamageBonus() * 100}%</color>.";
                        return effect;
                    }
                case "poisonvuln":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_PoisonVulnerable>();
                        effect.SetDamageBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nStriking an enemy with a damage type it is vulnerable deals <color=cyan>{effect.GetDamageBonus() * 100}%</color> of the damage dealt as poison damage.";
                        return effect;
                    }
                case "challengemvespd":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ChallengeMoveSpeed>();
                        description += $"\nMovement speed is increased by <color=cyan>{(float)values[$"{location}EffectVal"] * 100}%</color>.\n" +
                            $"Suffer <color=red>100%</color> more damage.";
                        return effect;
                    }
                case "challengedodgecost":
                    {
                        var effect = ScriptableObject.CreateInstance<SE_ChallengeDodgeBonus>();
                        effect.SetDodgeBonus((float)values[$"{location}EffectVal"]);
                        description += $"\nStamina cost for dodging is reduced by <color=cyan>{effect.GetDodgeBonus() * 100}%</color>.\n" +
                            $"Food fullness degrades <color=red>2x</color> faster.";
                        return effect;
                    }
                default:
                    return null;
            }
        }

        public static void ModArmorSet(string setName, ref ItemDrop.ItemData helmet,ref ItemDrop.ItemData chest, ref ItemDrop.ItemData legs, JToken values, bool isNewSet, int i)
        {
            ArmorSet armor = ArmorSets[setName];
            List<ItemDrop.ItemData> armorList = new List<ItemDrop.ItemData>() { helmet, chest, legs };
            JToken tierBalance;
            if (isNewSet)
                tierBalance = values["upgrades"][$"t{i}"];
            else
            {
                tierBalance = values;
                if(setName != "barbarian")
                {
                    helmet.m_shared.m_name = $"{armor.HelmetName}0";
                    chest.m_shared.m_name = $"{armor.ChestName}0";
                    legs.m_shared.m_name = $"{armor.LegsName}0";
                }
            }

            StatusEffect setEffect = GetSetEffect((string)values["setEffect"], tierBalance);

            foreach (ItemDrop.ItemData item in armorList)
            {
                item.m_shared.m_armor = (float)tierBalance["baseArmor"];
                item.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                if (setEffect != null)
                    item.m_shared.m_setStatusEffect = setEffect;
                else
                    Log.LogWarning($"{setName} - No set effect found for provided effect: {(string)values["setEffect"]}");
                item.m_shared.m_setSize = 3;
                item.m_shared.m_setName = (string)values["name"];
                if (!item.m_shared.m_name.Contains("helmet"))
                    item.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];

                item.m_shared.m_description = $"<i>{armor.ClassName}</i>\n{item.m_shared.m_description}";
            }

            helmet.m_shared.m_armor += armor.HelmetArmor;

            StatusEffect headStatus = GetArmorEffect((string)values["headEffect"], tierBalance, "head", ref helmet.m_shared.m_description);
            StatusEffect chestStatus = GetArmorEffect((string)values["chestEffect"], tierBalance, "chest", ref chest.m_shared.m_description);
            StatusEffect legStatus = GetArmorEffect((string)values["legsEffect"], tierBalance, "legs", ref legs.m_shared.m_description);

            if ((string)values["headEffect"] == "challengemvespd")
                helmet.m_shared.m_movementModifier += (float)values[$"headEffectVal"];
            if ((string)values["chestEffect"] == "challengemvespd")
                chest.m_shared.m_movementModifier += (float)values[$"chestEffectVal"];
            if ((string)values["legsEffect"] == "challengemvespd")
                legs.m_shared.m_movementModifier += (float)values[$"legsEffectVal"];

            if (headStatus != null)
                helmet.m_shared.m_equipStatusEffect = headStatus;
            else
                Log.LogWarning($"{setName} Head - No status effect found for provided effect: {(string)values["headEffect"]}");

            if (chestStatus != null)
                chest.m_shared.m_equipStatusEffect = chestStatus;
            else
                Log.LogWarning($"{setName} Chest - No status effect found for provided effect: {(string)values["chestEffect"]}");

            if (legStatus != null)
                legs.m_shared.m_equipStatusEffect = legStatus;
            else
                Log.LogWarning($"{setName} Legs - No status effect found for provided effect: {(string)values["legsEffect"]}");

        }

        public static void ModArmorPiece(string setName, string location, ref ItemDrop.ItemData piece, JToken values, bool isNewSet, int i)
        {
            ArmorSet armor = ArmorSets[setName];
            JToken tierBalance;
            if (isNewSet)
                tierBalance = values["upgrades"][$"t{i}"];
            else
            {
                tierBalance = values;
                if (setName != "barbarian")
                {
                    piece.m_shared.m_name = $"{armor.HelmetName}0";
                }
            }

            StatusEffect setEffect = GetSetEffect((string)values["setEffect"], tierBalance);

            piece.m_shared.m_armor = (float)tierBalance["baseArmor"];
            piece.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
            if (setEffect != null)
                piece.m_shared.m_setStatusEffect = setEffect;
            else
                Log.LogWarning($"{setName} - No set effect found for provided effect: {(string)values["setEffect"]}");
            piece.m_shared.m_setSize = (setName != "rags" ? 3 : 2);
            piece.m_shared.m_setName = (string)values["name"];
            if (!piece.m_shared.m_name.Contains("helmet"))
                piece.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];

            piece.m_shared.m_description = $"<i>{armor.ClassName}</i>\n{piece.m_shared.m_description}";

            if(location == "head")
                piece.m_shared.m_armor += armor.HelmetArmor;

            StatusEffect status = GetArmorEffect((string)values[$"{location}Effect"], tierBalance, location, ref piece.m_shared.m_description);

            if ((string)values[$"{location}Effect"] == "challengemvespd")
                piece.m_shared.m_movementModifier += (float)values[$"{location}EffectVal"];

            if (status != null)
                piece.m_shared.m_equipStatusEffect = status;
            else
                Log.LogWarning($"{setName} {location} - No status effect found for provided effect: {(string)values[$"{location}Effect"]}");
        }

        public static void AddArmorPiece(string setName, string location)
        {
            JToken setBalance = balance[setName];
            ArmorSet armor = ArmorSets[setName];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                // JToken tierBalance = setBalance["upgrades"][$"t{i}"];
                string id = "";
                string name = "";

                switch (location)
                {
                    case "head":
                        id = armor.HelmetID;
                        name = armor.HelmetName;
                        break;
                    case "chest":
                        id = armor.ChestID;
                        name = armor.ChestName;
                        break;
                    case "legs":
                        id = armor.LegsID;
                        name = armor.LegsName;
                        break;
                    default:
                        break;
                }

                //Create mocks for use in clones

                GameObject clonedPiece = PrefabManager.Instance.CreateClonedPrefab($"{id}T{i}", id);

                //Set ID so that previous armors still exist
                if (setName != "barbarian")
                {
                    string armorSetName = char.ToUpper(setName[0]) + setName.Substring(1);
                    clonedPiece.name = $"{id}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor";
                }
                else
                {
                    clonedPiece.name = $"{id}T{i}_Terraheim_BarbarianArmor_AddNewSets";
                }

                CustomItem piece = new CustomItem(clonedPiece, true);

                piece.ItemDrop.m_itemData.m_shared.m_name = $"{name}{i}";

                ModArmorPiece(setName, location, ref piece.ItemDrop.m_itemData, setBalance, true, i);

                //Recipes
                Recipe recipe = ScriptableObject.CreateInstance<Recipe>();

                recipe.name = $"Recipe_{id}T{i}";

                List<Piece.Requirement> recipeList = new List<Piece.Requirement>();

                //Add base armor to requirements
                int j = 0;
                if (i == (int)setBalance["upgrades"]["startingTier"])
                {
                    recipeList.Add(MockRequirement.Create(id, 1, false));
                    j++;
                    recipeList[0].m_amountPerLevel = 0;
                }

                JToken recipeReqs = balance["upgradePath"][$"t{i}"];
                int index = 0 + j;
                foreach (JObject item in recipeReqs[location].Cast<JObject>())
                {
                    recipeList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    recipeList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                recipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);

                //Set crafting station level
                recipe.m_minStationLevel = (int)recipeReqs["minLevel"];

                //Assign reqs to recipe
                recipe.m_resources = recipeList.ToArray();

                //Add item to recipe
                recipe.m_item = piece.ItemDrop;

                //Create the custome recipe
                CustomRecipe customPieceRecipe = new CustomRecipe(recipe, fixReference: true, fixRequirementReferences: true);

                ItemManager.Instance.AddItem(piece);

                //Add recipes to DB
                ItemManager.Instance.AddRecipe(customPieceRecipe);
            }
        }

        public static void AddArmorSet(string setName)
        {
            AddArmorPiece(setName, "head");
            AddArmorPiece(setName, "chest");
            AddArmorPiece(setName, "legs");
        }

        public static void AddBelt(string name)
        {
            var setBalance = balance[name];
            UtilityBelt beltData = UtilityBelts[name];

            GameObject clonedBelt = PrefabManager.Instance.CreateClonedPrefab(beltData.BaseID, beltData.CloneID);

            //Set ID so that previous armors still exist
            clonedBelt.name = beltData.FinalID;
            
            CustomItem belt = new CustomItem(clonedBelt, true);

            belt.ItemDrop.m_itemData.m_shared.m_name = $"{beltData.Name}";

            //GET EFFECT
            StatusEffect status = GetArmorEffect((string)setBalance["effect"], setBalance, "head", ref belt.ItemDrop.m_itemData.m_shared.m_description);
            if (status != null)
                belt.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = status;
            else
                Log.LogWarning($"{name} - No status effect found for provided effect: {(string)setBalance["effect"]}");

            //Recipes
            Recipe recipe = ScriptableObject.CreateInstance<Recipe>();

            recipe.name = $"Recipe_{beltData.BaseID}";

            List<Piece.Requirement> helmetList = new List<Piece.Requirement>();

            var recipeReqs = setBalance["crafting"];
            int index = 0;
            foreach (JObject item in recipeReqs["items"].Cast<JObject>())
            {
                helmetList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                helmetList[index].m_amountPerLevel = (int)item["perLevel"];
                index++;
            }


            recipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);

            //Set crafting station level
            recipe.m_minStationLevel = (int)recipeReqs["minLevel"];

            //Assign reqs to recipe
            recipe.m_resources = helmetList.ToArray();

            //Add item to recipe
            recipe.m_item = belt.ItemDrop;

            //Create the custome recipe
            CustomRecipe customRecipe = new CustomRecipe(recipe, fixReference: true, fixRequirementReferences: true);

            ItemManager.Instance.AddItem(belt);

            //Add recipes to DB
            ItemManager.Instance.AddRecipe(customRecipe);
        }

        public static void AddTieredRecipes(string setName)
        {
            ArmorSet armor = ArmorSets[setName];
            string armorSetName = char.ToUpper(setName[0]) + setName.Substring(1);
            for (int i = (int)balance[setName]["upgrades"]["startingTier"] + 1; i <= 5; i++)
            {
                Recipe helmetRecipe;
                Recipe chestRecipe;
                Recipe legsRecipe;
                if (setName != "barbarian")
                {
                    helmetRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armor.HelmetID}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor").GetComponent<ItemDrop>().m_itemData);
                    chestRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armor.ChestID}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor").GetComponent<ItemDrop>().m_itemData);
                    legsRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armor.LegsID}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor").GetComponent<ItemDrop>().m_itemData);
                }
                else
                {
                    helmetRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armor.HelmetID}T{i}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>().m_itemData);
                    chestRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armor.ChestID}T{i}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>().m_itemData);
                    legsRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armor.LegsID}T{i}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>().m_itemData);
                }

                List<Piece.Requirement> helmetList = helmetRecipe.m_resources.ToList();
                List<Piece.Requirement> chestList = chestRecipe.m_resources.ToList();
                List<Piece.Requirement> legsList = legsRecipe.m_resources.ToList();

                if (setName == "silver")
                {
                    helmetList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab("HelmetDrake").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });

                    chestList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab("ArmorWolfChest").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    }); ;

                    legsList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab("ArmorWolfLegs").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });
                } 
                else if (setName == "barbarian")
                {
                    helmetList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab($"{armor.HelmetID}T{i-1}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });

                    chestList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab($"{armor.ChestID}T{i-1}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });

                    legsList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab($"{armor.LegsID}T{i-1}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });
                }
                else
                {
                    helmetList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab($"{armor.HelmetID}T{i - 1}_Terraheim_AddNewSets_Add{armorSetName}Armor").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });

                    chestList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab($"{armor.ChestID}T{i - 1}_Terraheim_AddNewSets_Add{armorSetName}Armor").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });

                    legsList.Add(new Piece.Requirement()
                    {
                        m_resItem = ObjectDB.instance.GetItemPrefab($"{armor.LegsID}T{i - 1}_Terraheim_AddNewSets_Add{armorSetName}Armor").GetComponent<ItemDrop>(),
                        m_amount = 1,
                        m_amountPerLevel = 0,
                        m_recover = false
                    });
                }

                helmetRecipe.m_resources = helmetList.ToArray();
                chestRecipe.m_resources = chestList.ToArray();
                legsRecipe.m_resources = legsList.ToArray();
            }
        }
    }
}
