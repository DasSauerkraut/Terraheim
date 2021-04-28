using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Terraheim.ArmorEffects;
using UnityEngine;
using ValheimLib;
using ValheimLib.ODB;
using static Terraheim.Utility.Data;
using System;

namespace Terraheim.Utility
{
    class ArmorHelper
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

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
                        description += $"\nHP is increased by <color=cyan>{effect.getHealthBonus()}</color>.";
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
                default:
                    return null;
            }
        }

        public static void ModArmorSet(string setName, ref ItemDrop helmet,ref ItemDrop chest, ref ItemDrop legs, JToken values, bool isNewSet, int i)
        {
            ArmorSet armor = ArmorSets[setName];
            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs };
            JToken tierBalance;
            if (isNewSet)
                tierBalance = values["upgrades"][$"t{i}"];
            else
                tierBalance = values;

            StatusEffect setEffect = GetSetEffect((string)values["setEffect"], tierBalance);

            foreach (ItemDrop item in armorList)
            {
                item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                if (setEffect != null)
                    item.m_itemData.m_shared.m_setStatusEffect = setEffect;
                else
                    Log.LogMessage($"{setName}: No set effect found for {(string)values["setEffect"]}");
                item.m_itemData.m_shared.m_setSize = 3;
                item.m_itemData.m_shared.m_setName = (string)values["name"];
                if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                    item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];

                item.m_itemData.m_shared.m_description = $"<i>{armor.ClassName}</i>\n{item.m_itemData.m_shared.m_description}";
            }

            helmet.m_itemData.m_shared.m_armor += armor.HelmetArmor;

            StatusEffect headStatus = GetArmorEffect((string)values["headEffect"], tierBalance, "head", ref helmet.m_itemData.m_shared.m_description);
            StatusEffect chestStatus = GetArmorEffect((string)values["chestEffect"], tierBalance, "chest", ref chest.m_itemData.m_shared.m_description);
            StatusEffect legStatus = GetArmorEffect((string)values["legsEffect"], tierBalance, "legs", ref legs.m_itemData.m_shared.m_description);

            helmet.m_itemData.m_shared.m_equipStatusEffect = headStatus;
            chest.m_itemData.m_shared.m_equipStatusEffect = chestStatus;
            legs.m_itemData.m_shared.m_equipStatusEffect = legStatus;
        }

        public static void AddArmorSet(string setName)
        {
            var setBalance = balance[setName];
            ArmorSet armor = ArmorSets[setName];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];

                //Create mocks for use in clones
                ItemDrop mockHelmet = Mock<ItemDrop>.Create(armor.HelmetID);
                ItemDrop mockChest = Mock<ItemDrop>.Create(armor.ChestID);
                ItemDrop mockLegs = Mock<ItemDrop>.Create(armor.LegsID);

                GameObject clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"{armor.HelmetID}T{i}", false);
                GameObject clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"{armor.ChestID}T{i}", false);
                GameObject clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"{armor.LegsID}T{i}", false);

                //Set ID so that previous armors still exist
                if(setName != "barbarian")
                {
                    string armorSetName = char.ToUpper(setName[0]) + setName.Substring(1);
                    clonedHelmet.name = $"{armor.HelmetID}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor";
                    clonedChest.name = $"{armor.ChestID}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor";
                    clonedLegs.name = $"{armor.LegsID}T{i}_Terraheim_AddNewSets_Add{armorSetName}Armor";
                }
                else
                {
                    clonedHelmet.name = $"{armor.HelmetID}T{i}_Terraheim_BarbarianArmor_AddNewSets";
                    clonedChest.name = $"{armor.ChestID}T{i}_Terraheim_BarbarianArmor_AddNewSets";
                    clonedLegs.name = $"{armor.LegsID}T{i}_Terraheim_BarbarianArmor_AddNewSets";
                }

                CustomItem helmet = new CustomItem(clonedHelmet, true);
                CustomItem chest = new CustomItem(clonedChest, true);
                CustomItem legs = new CustomItem(clonedLegs, true);

                helmet.ItemDrop.m_itemData.m_shared.m_name = $"{armor.HelmetName}{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"{armor.ChestName}{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"{armor.LegsName}{i}";

                ModArmorSet(setName, ref helmet.ItemDrop, ref chest.ItemDrop, ref legs.ItemDrop, setBalance, true, i);

                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_{armor.HelmetID}T{i}";
                chestRecipe.name = $"Recipe_{armor.ChestID}T{i}";
                legsRecipe.name = $"Recipe_{armor.LegsID}T{i}";

                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();

                //Add base armor to requirements
                int j = 0;
                if (i == (int)setBalance["upgrades"]["startingTier"])
                {
                    helmetList.Add(MockRequirement.Create(armor.HelmetID, 1, false));
                    chestList.Add(MockRequirement.Create(armor.ChestID, 1, false));
                    legsList.Add(MockRequirement.Create(armor.LegsID, 1, false));
                    j++;
                    helmetList[0].m_amountPerLevel = 0;
                    chestList[0].m_amountPerLevel = 0;
                    legsList[0].m_amountPerLevel = 0;
                }

                var recipeReqs = balance["upgradePath"][$"t{i}"];
                int index = 0 + j;
                foreach (JObject item in recipeReqs["head"])
                {
                    helmetList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    helmetList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                index = 0 + j;
                foreach (JObject item in recipeReqs["chest"])
                {
                    chestList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    chestList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                index = 0 + j;
                foreach (JObject item in recipeReqs["legs"])
                {
                    legsList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    legsList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                helmetRecipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);
                chestRecipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);
                legsRecipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);

                //Set crafting station level
                helmetRecipe.m_minStationLevel = (int)recipeReqs["minLevel"];
                chestRecipe.m_minStationLevel = (int)recipeReqs["minLevel"];
                legsRecipe.m_minStationLevel = (int)recipeReqs["minLevel"];

                //Assign reqs to recipe
                helmetRecipe.m_resources = helmetList.ToArray();
                chestRecipe.m_resources = chestList.ToArray();
                legsRecipe.m_resources = legsList.ToArray();

                //Add item to recipe
                helmetRecipe.m_item = helmet.ItemDrop;
                chestRecipe.m_item = chest.ItemDrop;
                legsRecipe.m_item = legs.ItemDrop;

                //Create the custome recipe
                CustomRecipe customHelmetRecipe = new CustomRecipe(helmetRecipe, fixReference: true, fixRequirementReferences: true);
                CustomRecipe customChestRecipe = new CustomRecipe(chestRecipe, fixReference: true, fixRequirementReferences: true);
                CustomRecipe customLegsRecipe = new CustomRecipe(legsRecipe, fixReference: true, fixRequirementReferences: true);

                ObjectDBHelper.Add(helmet);
                ObjectDBHelper.Add(chest);
                ObjectDBHelper.Add(legs);

                //Add recipes to DB

                ObjectDBHelper.Add(customHelmetRecipe);
                ObjectDBHelper.Add(customChestRecipe);
                ObjectDBHelper.Add(customLegsRecipe);
            }
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

                if(setName == "silver")
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
