using ValheimLib;
using ValheimLib.ODB;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;
using UnityEngine;
using HarmonyLib;
using System.IO;

namespace Terraheim.Armor
{
    internal static class BarbarianArmor
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ObjectDBHelper.OnAfterInit += ModExistingArmor;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddNewSets;
        }

        internal static void Integrate()
        {
            //if (!Harmony.HasAnyPatches("GoldenJude_BarbarianArmor"))
            if (!File.Exists(Terraheim.ModPath + "/../barbarianArmor.dll" ))
            { 
                Log.LogWarning("Barbarian armor not found!");
                return;
            }
            Terraheim.hasBarbarianArmor = true;
            Log.LogInfo("Barbarian Armor Found!");
        }

        private static void ModExistingArmor()
        {
            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeHelmetJD");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeChestJD");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeLegsJD");
            var cape = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianCapeJD");

            var setBalance = balance["barbarian"];

            List<ItemDrop> armorList = new List<ItemDrop>() { helmet, chest, legs, cape };

            //Create Set effect
            var deathMark = ScriptableObject.CreateInstance<SE_DeathMark>();
            deathMark.SetDamageBonus((float)setBalance["setBonusVal"]);
            deathMark.SetThreshold((int)setBalance["setBonusThreshold"]);
            deathMark.SetHitDuration((int)setBalance["setBonusDuration"]);
            deathMark.SetIcon();

            //Adjust Stats
            foreach (ItemDrop item in armorList)
            {
                if (!item.m_itemData.m_shared.m_name.Contains("Cape"))
                {
                    item.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
                    item.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
                    item.m_itemData.m_shared.m_setStatusEffect = deathMark;
                    item.m_itemData.m_shared.m_setSize = 3;
                    item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                    if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                        item.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];
                }
                helmet.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"] + 1;
                item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["thrower"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
            }

            //Create Status Effects
            var throwingDamageBonus = ScriptableObject.CreateInstance<SE_ThrowingDamageBonus>();
            var throwVelocity = ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>();
            var damageVsLowHP = ScriptableObject.CreateInstance<SE_DamageVSLowHP>();
            var speedBnsOnKill = ScriptableObject.CreateInstance<SE_MoveSpeedOnKillListener>();

            //Configure Status Effects
            throwingDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.m_itemData.m_shared.m_description += $"\nDamage with throwing weapons is increased by <color=cyan>{throwingDamageBonus.getDamageBonus() * 100}%</color>.";

            throwVelocity.SetVelocityBonus((float)setBalance["chestEffectVal"]);
            chest.m_itemData.m_shared.m_description += $"\nThrowing weapons velocity is increased by <color=cyan>{System.Math.Round((throwVelocity.GetVelocityBonus()) * 100)}%</color>.";

            damageVsLowHP.SetDamageBonus((float)setBalance["legsEffectVal"]);
            damageVsLowHP.SetHealthThreshold((float)setBalance["legsEffectThreshold"]);
            legs.m_itemData.m_shared.m_description += $"\nDamage against enemies with less than <color=cyan>" + (damageVsLowHP.GetHealthThreshold() * 100) +
                "%</color> HP is increased by <color=cyan>" + damageVsLowHP.GetDamageBonus() * 100 + "%</color>.";

            speedBnsOnKill.SetSpeedBonus((float)setBalance["capeEffectVal"]);
            cape.m_itemData.m_shared.m_description += $"\nAfter killing an enemy, gain a stacking <color=cyan>+{speedBnsOnKill.GetSpeedBonus() * 100}%</color> movement speed bonus for {(float)balance["bloodrushTTL"]}" +
                $"seconds. Killing an enemy resets the countdown.";

            //Apply Status Effects
            helmet.m_itemData.m_shared.m_equipStatusEffect = throwingDamageBonus;
            chest.m_itemData.m_shared.m_equipStatusEffect = throwVelocity;
            legs.m_itemData.m_shared.m_equipStatusEffect = damageVsLowHP;
            cape.m_itemData.m_shared.m_equipStatusEffect = speedBnsOnKill;
        }

        private static void AddNewSets()
        {
            var setBalance = balance["barbarian"];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];

                //create new items
                var mockHelmet = Mock<ItemDrop>.Create("ArmorBarbarianBronzeHelmetJD");
                var mockChest = Mock<ItemDrop>.Create("ArmorBarbarianBronzeChestJD");
                var mockLegs = Mock<ItemDrop>.Create("ArmorBarbarianBronzeLegsJD");

                var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"ArmorBarbarianBronzeHelmetJDT{i}", false);
                var clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"ArmorBarbarianBronzeChestJDT{i}", false);
                var clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"ArmorBarbarianBronzeLegsJDT{i}", false);

                CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);
                CustomItem chest = new CustomItem(clonedChest, fixReference: true);
                CustomItem legs = new CustomItem(clonedLegs, fixReference: true);

                //name items
                helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_barbarian_t{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"$item_chest_barbarian_t{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"$item_legs_barbarian_t{i}";

                List<ItemDrop> armorList = new List<ItemDrop>() { helmet.ItemDrop, chest.ItemDrop, legs.ItemDrop };

                //Create Set effect
                var deathMark = ScriptableObject.CreateInstance<SE_DeathMark>();
                deathMark.SetDamageBonus((float)tierBalance["setBonusVal"]);
                deathMark.SetThreshold((int)tierBalance["setBonusThreshold"]);
                deathMark.SetHitDuration((int)tierBalance["setBonusDuration"]);
                deathMark.SetIcon();

                //Adjust Stats
                foreach (ItemDrop item in armorList)
                {
                    if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                    {
                        item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                        item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                        item.m_itemData.m_shared.m_setStatusEffect = deathMark;
                        item.m_itemData.m_shared.m_setSize = 3;
                        item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                        if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                            item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];
                    }
                    item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["berserker"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
                }

                //Create Status Effects
                var throwingDamageBonus = ScriptableObject.CreateInstance<SE_ThrowingDamageBonus>();
                var throwVelocity = ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>();
                var damageVsLowHP = ScriptableObject.CreateInstance<SE_DamageVSLowHP>();

                //Configure Status Effects
                throwingDamageBonus.setDamageBonus((float)tierBalance["headEffectVal"]);
                helmet.ItemDrop.m_itemData.m_shared.m_description += $"\nDamage with throwing weapons is increased by <color=cyan>{throwingDamageBonus.getDamageBonus() * 100}%</color>.";

                throwVelocity.SetVelocityBonus((float)tierBalance["chestEffectVal"]);
                chest.ItemDrop.m_itemData.m_shared.m_description += $"\nThrowing weapons velocity is increased by <color=cyan>{System.Math.Round((throwVelocity.GetVelocityBonus()) * 100)}%</color>.";

                damageVsLowHP.SetDamageBonus((float)tierBalance["legsEffectVal"]);
                damageVsLowHP.SetHealthThreshold((float)tierBalance["legsEffectThreshold"]);
                legs.ItemDrop.m_itemData.m_shared.m_description += $"\nDamage against enemies with less than <color=cyan>" + (damageVsLowHP.GetHealthThreshold() * 100) +
                    "%</color> HP is increased by <color=cyan>" + damageVsLowHP.GetDamageBonus() * 100 + "%</color>.";

                //Apply Status Effects
                helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = throwingDamageBonus;
                chest.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = throwVelocity;
                legs.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = damageVsLowHP;

                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_HelmetBarbarianT{i}";
                chestRecipe.name = $"Recipe_ArmorBarbarianChestT{i}";
                legsRecipe.name = $"Recipe_ArmorBarbarianLegsT{i}";

                //Recipe Requirement List
                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();

                //Add base armor to requirements
                if (i == 1)
                {
                    helmetList.Add(MockRequirement.Create("ArmorBarbarianBronzeHelmetJD", 1));
                    chestList.Add(MockRequirement.Create("ArmorBarbarianBronzeChestJD", 1));
                    legsList.Add(MockRequirement.Create("ArmorBarbarianBronzeLegsJD", 1));
                }

                //Get recipe reqs from json
                var recipeReqs = balance["upgradePath"][$"t{i}"];
                int index = 0;
                foreach (JObject item in recipeReqs["head"])
                {
                    helmetList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    helmetList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                index = 0;
                foreach (JObject item in recipeReqs["chest"])
                {
                    chestList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    chestList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                index = 0;
                foreach (JObject item in recipeReqs["legs"])
                {
                    legsList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    legsList[index].m_amountPerLevel = (int)item["perLevel"];
                    index++;
                }

                //Set crafting station
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
    }
}
