using ValheimLib;
using ValheimLib.ODB;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.Linq;

namespace Terraheim.Armor
{
    internal static class AddNewSets
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddLeatherArmor;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddTrollLeatherArmor;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddBronzeArmor;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddIronArmor;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddSilverArmor;
            ObjectDBHelper.OnAfterInit += ModExistingRecipes;
        }

        private static void AddLeatherArmor()
        {
            var setBalance = balance["leather"];
            for(int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];

                //create new items
                var mockHelmet = Mock<ItemDrop>.Create("HelmetLeather");
                var mockChest = Mock<ItemDrop>.Create("ArmorLeatherChest");
                var mockLegs = Mock<ItemDrop>.Create("ArmorLeatherLegs");

                var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"HelmetLeatherT{i}", false);
                var clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"ArmorLeatherChestT{i}", false);
                var clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"ArmorLeatherLegsT{i}", false);

                CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);
                CustomItem chest = new CustomItem(clonedChest, fixReference: true);
                CustomItem legs = new CustomItem(clonedLegs, fixReference: true);

                //name items
                helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_leather_t{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"$item_chest_leather_t{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"$item_legs_leather_t{i}";

                List<ItemDrop> armorList = new List<ItemDrop>() { helmet.ItemDrop, chest.ItemDrop, legs.ItemDrop };

                //Create Set effect
                var hpOnHit = ScriptableObject.CreateInstance<SE_HPOnHit>();
                hpOnHit.setHealAmount((float)tierBalance["setBonusVal"]);
                hpOnHit.SetIcon();

                //Adjust Stats
                foreach (ItemDrop item in armorList)
                {
                    if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                    {
                        item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                        item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                        item.m_itemData.m_shared.m_setStatusEffect = hpOnHit;
                        item.m_itemData.m_shared.m_setSize = 3;
                        item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                        if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                            item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];
                    }
                    item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["berserker"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
                }

                //Create Status Effects
                var axeDamageBonus = Prefab.Cache.GetPrefab<SE_AxeDamageBonus>("Axe Damage Bonus");
                var dodgeStamUse = Prefab.Cache.GetPrefab<SE_DodgeStamUse>("Dodge Stamina Use");

                //Configure Status Effects
                axeDamageBonus.setDamageBonus((float)tierBalance["headEffectVal"]);
                helmet.ItemDrop.m_itemData.m_shared.m_description += $"\nAxe Damage is increased by <color=yellow>{axeDamageBonus.getDamageBonus() * 100}%</color>.";

                dodgeStamUse.setDodgeStaminaUse((float)tierBalance["chestEffectVal"]);
                chest.ItemDrop.m_itemData.m_shared.m_description += $"\nDodge stamina cost is reduced by <color=yellow>{dodgeStamUse.getDodgeStaminaUse() * 100}%</color>.";

                legs.ItemDrop.m_itemData.m_shared.m_description += $"\nMovement speed is increased by <color=yellow>" + (float)tierBalance["legsEffectVal"] * 100 + "%</color>.";

                //Apply Status Effects
                helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = axeDamageBonus;
                chest.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = dodgeStamUse;
                legs.ItemDrop.m_itemData.m_shared.m_movementModifier = (float)setBalance["legsEffectVal"];
                
                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_HelmetLeatherT{i}";
                chestRecipe.name = $"Recipe_ArmorLeatherChestT{i}";
                legsRecipe.name = $"Recipe_ArmorLeatherLegsT{i}";

                //Recipe Requirement List
                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();
                
                //Add base armor to requirements
                if (i == 1)
                {
                    helmetList.Add(MockRequirement.Create("HelmetLeather", 1));
                    chestList.Add(MockRequirement.Create("ArmorLeatherChest", 1));
                    legsList.Add(MockRequirement.Create("ArmorLeatherLegs", 1));
                }

                //Get recipe reqs from json
                var recipeReqs = balance["upgradePath"][$"t{i}"];
                int index = 0;
                foreach (JObject item in recipeReqs["head"])
                {
                    helmetList.Add(MockRequirement.Create((string)item["item"],(int)item["amount"]));
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

        private static void AddTrollLeatherArmor()
        {
            var setBalance = balance["trollLeather"];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];
                //create new items
                var mockHelmet = Mock<ItemDrop>.Create("HelmetTrollLeather");
                var mockChest = Mock<ItemDrop>.Create("ArmorTrollLeatherChest");
                var mockLegs = Mock<ItemDrop>.Create("ArmorTrollLeatherLegs");

                var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"HelmetTrollLeatherT{i}", false);
                var clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"ArmorTrollLeatherChestT{i}", false);
                var clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"ArmorTrollLeatherLegsT{i}", false);

                CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);
                CustomItem chest = new CustomItem(clonedChest, fixReference: true);
                CustomItem legs = new CustomItem(clonedLegs, fixReference: true);

                //name items
                helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_trollleather_t{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"$item_chest_trollleather_t{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"$item_legs_trollleather_t{i}";

                List<ItemDrop> armorList = new List<ItemDrop>() { helmet.ItemDrop, chest.ItemDrop, legs.ItemDrop };

                //Adjust Stats
                foreach (ItemDrop item in armorList)
                {
                    if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                    {
                        item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                        item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                        item.m_itemData.m_shared.m_setSize = 3;
                        item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                        if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                            item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];
                    }
                    item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["ranger"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
                }

                //Create Status Effects
                var rangedDamageBonus = Prefab.Cache.GetPrefab<SE_RangedDmgBonus>("Ranged Damage Bonus");
                var daggerSpearDamageBonus = Prefab.Cache.GetPrefab<SE_DaggerSpearDmgBonus>("Dagger/Spear Damage Bonus");
                var ammoConsumption = Prefab.Cache.GetPrefab<SE_AmmoConsumption>("Ammo Consumption");

                //Configure Status Effects
                rangedDamageBonus.setDamageBonus((float)tierBalance["headEffectVal"]);
                helmet.ItemDrop.m_itemData.m_shared.m_description = helmet.ItemDrop.m_itemData.m_shared.m_description + $"\nBow Damage is increased by <color=yellow>{System.Math.Round((rangedDamageBonus.getDamageBonus()) * 100)}%</color>.";

                ammoConsumption.setAmmoConsumption((int)tierBalance["chestEffectVal"]);
                chest.ItemDrop.m_itemData.m_shared.m_description = chest.ItemDrop.m_itemData.m_shared.m_description + $"\n<color=yellow>{ammoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";

                daggerSpearDamageBonus.setDamageBonus((float)tierBalance["legsEffectVal"]);
                legs.ItemDrop.m_itemData.m_shared.m_description = legs.ItemDrop.m_itemData.m_shared.m_description + $"\nDagger Damage is increased by <color=yellow>{System.Math.Round((daggerSpearDamageBonus.getDamageBonus()) * 100)}%</color>.\nSpear Damage is increased by <color=yellow>{System.Math.Round(daggerSpearDamageBonus.getDamageBonus() * 100)}%</color>.";

                //Apply Status Effects
                helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = rangedDamageBonus;
                chest.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = ammoConsumption;
                legs.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = daggerSpearDamageBonus;

                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_HelmetTrollLeatherT{i}";
                chestRecipe.name = $"Recipe_ArmorTrollLeatherChestT{i}";
                legsRecipe.name = $"Recipe_ArmorTrollLeatherLegsT{i}";

                //Recipe Requirement List
                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();

                //Add previous armor to requirements
                if (i == (int)setBalance["upgrades"]["startingTier"])
                {
                    helmetList.Add(MockRequirement.Create("HelmetTrollLeather", 1));
                    chestList.Add(MockRequirement.Create("ArmorTrollLeatherChest", 1));
                    legsList.Add(MockRequirement.Create("ArmorTrollLeatherLegs", 1));
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

        private static void AddBronzeArmor()
        {
            var setBalance = balance["bronze"];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];
                //create new items
                var mockHelmet = Mock<ItemDrop>.Create("HelmetBronze");
                var mockChest = Mock<ItemDrop>.Create("ArmorBronzeChest");
                var mockLegs = Mock<ItemDrop>.Create("ArmorBronzeLegs");

                var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"HelmetBronzeT{i}", false);
                var clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"ArmorBronzeChestT{i}", false);
                var clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"ArmorBronzeLegsT{i}", false);

                CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);
                CustomItem chest = new CustomItem(clonedChest, fixReference: true);
                CustomItem legs = new CustomItem(clonedLegs, fixReference: true);

                //name items
                helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_bronze_t{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"$item_chest_bronze_t{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"$item_legs_bronze_t{i}";

                List<ItemDrop> armorList = new List<ItemDrop>() { helmet.ItemDrop, chest.ItemDrop, legs.ItemDrop };

                //Create Set effect
                var hpRegen = ScriptableObject.CreateInstance<SE_HPRegen>();
                hpRegen.setHealPercent((float)tierBalance["setBonusVal"]);
                hpRegen.SetIcon();

                //Adjust Stats
                foreach (ItemDrop item in armorList)
                {
                    if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                    {
                        item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                        item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                        item.m_itemData.m_shared.m_setStatusEffect = hpRegen;
                        item.m_itemData.m_shared.m_setSize = 3;
                        item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                        if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                            item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];
                    }
                    item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["tank"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
                }

                //Create Status Effects
                var meleeDamageBonus = Prefab.Cache.GetPrefab<SE_MeleeDamageBonus>("Melee Damage Bonus");
                var blockStamUse = Prefab.Cache.GetPrefab<SE_BlockStamUse>("Block Stamina Use");
                var hpBonus = Prefab.Cache.GetPrefab<SE_HealthIncrease>("Health Increase");

                //Configure Status Effects
                meleeDamageBonus.setDamageBonus((float)tierBalance["headEffectVal"]);
                helmet.ItemDrop.m_itemData.m_shared.m_description += $"\nMelee Damage is increased by <color=yellow>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";

                blockStamUse.setBlockStaminaUse((float)tierBalance["chestEffectVal"]);
                chest.ItemDrop.m_itemData.m_shared.m_description += $"\nBase block stamina cost is reduced by <color=yellow>{System.Math.Round((blockStamUse.getBlockStaminaUse()) * 100)}%</color>.";

                hpBonus.setHealthBonus((float)tierBalance["legsEffectVal"]);
                legs.ItemDrop.m_itemData.m_shared.m_description += $"\nHP is increased by <color=yellow>" + hpBonus.getHealthBonus() + "</color>.";

                //Apply Status Effects
                helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = meleeDamageBonus;
                chest.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = blockStamUse;
                legs.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = hpBonus;

                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_HelmetBronzeT{i}";
                chestRecipe.name = $"Recipe_ArmorBronzeChestT{i}";
                legsRecipe.name = $"Recipe_ArmorBronzeLegsT{i}";

                //Recipe Requirement List
                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();

                //Add previous armor to requirements
                if (i == (int)setBalance["upgrades"]["startingTier"])
                {
                    helmetList.Add(MockRequirement.Create("HelmetBronze", 1));
                    chestList.Add(MockRequirement.Create("ArmorBronzeChest", 1));
                    legsList.Add(MockRequirement.Create("ArmorBronzeLegs", 1));
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

        private static void AddIronArmor()
        {
            var setBalance = balance["iron"];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];
                //create new items
                var mockHelmet = Mock<ItemDrop>.Create("HelmetIron");
                var mockChest = Mock<ItemDrop>.Create("ArmorIronChest");
                var mockLegs = Mock<ItemDrop>.Create("ArmorIronLegs");

                var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"HelmetIronT{i}", false);
                var clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"ArmorIronChestT{i}", false);
                var clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"ArmorIronLegsT{i}", false);

                CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);
                CustomItem chest = new CustomItem(clonedChest, fixReference: true);
                CustomItem legs = new CustomItem(clonedLegs, fixReference: true);

                //name items
                helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_iron_t{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"$item_chest_iron_t{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"$item_legs_iron_t{i}";

                List<ItemDrop> armorList = new List<ItemDrop>() { helmet.ItemDrop, chest.ItemDrop, legs.ItemDrop };

                //Create Set effect
                var critChance = ScriptableObject.CreateInstance<SE_CritChance>();
                critChance.SetCritChance((float)tierBalance["setBonusVal"]);
                critChance.SetCritBonus((float)tierBalance["setCritBonus"]);
                critChance.SetIcon();

                //Adjust Stats
                foreach (ItemDrop item in armorList)
                {
                    if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                    {
                        item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                        item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                        item.m_itemData.m_shared.m_setStatusEffect = critChance;
                        item.m_itemData.m_shared.m_setSize = 3;
                        item.m_itemData.m_shared.m_setName = (string)setBalance["name"];
                        if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                            item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];
                    }
                    item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["berserker"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
                }

                //Create Status Effects
                var meleeDamageBonus = Prefab.Cache.GetPrefab<SE_TwoHandedDmgBonus>("Two Handed Damage Bonus");
                var extraStamina = Prefab.Cache.GetPrefab<SE_ExtraStamina>("Extra Stamina");
                var stamRegen = Prefab.Cache.GetPrefab<SE_StaminaRegen>("Stamina Regen");

                //Configure Status Effects
                meleeDamageBonus.setDamageBonus((float)tierBalance["headEffectVal"]);
                helmet.ItemDrop.m_itemData.m_shared.m_description += $"\nTwo-Handed weapons damage is increased by <color=yellow>{System.Math.Round((meleeDamageBonus.getDamageBonus()) * 100)}%</color>.";

                extraStamina.SetStaminaBonus((float)tierBalance["chestEffectVal"]);
                chest.ItemDrop.m_itemData.m_shared.m_description += $"\nStamina is increased by <color=yellow>{extraStamina.GetStaminaBonus()}</color> points.";

                stamRegen.SetRegenPercent((float)tierBalance["legsEffectVal"]);
                legs.ItemDrop.m_itemData.m_shared.m_description += $"\nStamina regen is increased by <color=yellow>" + stamRegen.GetRegenPercent() * 100 + "%</color>.";

                //Apply Status Effects
                helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = meleeDamageBonus;
                chest.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = extraStamina;
                legs.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = stamRegen;

                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_HelmetIronT{i}";
                chestRecipe.name = $"Recipe_ArmorIronChestT{i}";
                legsRecipe.name = $"Recipe_ArmorIronLegsT{i}";

                //Recipe Requirement List
                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();

                //Add previous armor to requirements
                if (i == (int)setBalance["upgrades"]["startingTier"])
                {
                    helmetList.Add(MockRequirement.Create("HelmetIron", 1));
                    chestList.Add(MockRequirement.Create("ArmorIronChest", 1));
                    legsList.Add(MockRequirement.Create("ArmorIronLegs", 1));
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

        private static void AddSilverArmor()
        {
            var setBalance = balance["silver"];
            for (int i = (int)setBalance["upgrades"]["startingTier"]; i <= 5; i++)
            {
                var tierBalance = setBalance["upgrades"][$"t{i}"];
                //create new items
                var mockHelmet = Mock<ItemDrop>.Create("HelmetDrake");
                var mockChest = Mock<ItemDrop>.Create("ArmorWolfChest");
                var mockLegs = Mock<ItemDrop>.Create("ArmorWolfLegs");

                var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"HelmetDrakeT{i}", false);
                var clonedChest = Prefab.GetRealPrefabFromMock<ItemDrop>(mockChest).gameObject.InstantiateClone($"ArmorWolfChestT{i}", false);
                var clonedLegs = Prefab.GetRealPrefabFromMock<ItemDrop>(mockLegs).gameObject.InstantiateClone($"ArmorWolfLegsT{i}", false);

                CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);
                CustomItem chest = new CustomItem(clonedChest, fixReference: true);
                CustomItem legs = new CustomItem(clonedLegs, fixReference: true);

                //name items
                helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_drake_t{i}";
                chest.ItemDrop.m_itemData.m_shared.m_name = $"$item_chest_wolf_t{i}";
                legs.ItemDrop.m_itemData.m_shared.m_name = $"$item_legs_wolf_t{i}";

                List<ItemDrop> armorList = new List<ItemDrop>() { helmet.ItemDrop, chest.ItemDrop, legs.ItemDrop };

                //Create Set effect
                var sneakDamageBonus = ScriptableObject.CreateInstance<SE_SneakDamageBonus>();
                sneakDamageBonus.SetDamageBonus((float)tierBalance["setBonusVal"]);
                sneakDamageBonus.SetActivationHP((float)tierBalance["setActivationHP"]);
                //sneakDamageBonus.SetIcon();

                //Adjust Stats
                foreach (ItemDrop item in armorList)
                {
                    if (!item.m_itemData.m_shared.m_name.Contains("cape"))
                    {
                        item.m_itemData.m_shared.m_armor = (float)tierBalance["baseArmor"];
                        item.m_itemData.m_shared.m_armorPerLevel = (float)tierBalance["armorPerLevel"];
                        item.m_itemData.m_shared.m_setStatusEffect = sneakDamageBonus;
                        item.m_itemData.m_shared.m_setSize = 3;
                        item.m_itemData.m_shared.m_setName = (string)setBalance["name"] + "T" + i;
                        if (!item.m_itemData.m_shared.m_name.Contains("helmet"))
                            item.m_itemData.m_shared.m_movementModifier = (float)tierBalance["globalMoveMod"];
                    }
                    item.m_itemData.m_shared.m_description = "<i>" + (string)balance["classes"]["ranger"] + $"</i>\n{item.m_itemData.m_shared.m_description}";
                }

                //Create Status Effects
                var silverDamageBonus = Prefab.Cache.GetPrefab<SE_SilverDamageBonus>("Silver Damage Bonus");
                var ammoConsumption = Prefab.Cache.GetPrefab<SE_AmmoConsumption>("Ammo Consumption");
                var drawMoveSpeed = Prefab.Cache.GetPrefab<SE_DrawMoveSpeed>("Draw Move Speed");

                silverDamageBonus.SetDamageBonus((float)tierBalance["headEffectVal"]);
                helmet.ItemDrop.m_itemData.m_shared.m_description += $"\nBows, daggers, and spears gain <color=yellow>{System.Math.Round((silverDamageBonus.GetDamageBonus()) * 100)}%</color> damage as spirit and frost damage.";

                ammoConsumption.setAmmoConsumption((int)tierBalance["chestEffectVal"]);
                chest.ItemDrop.m_itemData.m_shared.m_description += $"\n<color=yellow>{ammoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";

                drawMoveSpeed.SetDrawMoveSpeed((float)tierBalance["legsEffectVal"]);
                legs.ItemDrop.m_itemData.m_shared.m_description += $"\nMove <color=yellow>" + drawMoveSpeed.GetDrawMoveSpeed() * 100 + "%</color> faster with a drawn bow.";

                //Apply Status Effects
                helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = silverDamageBonus;
                chest.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = ammoConsumption;
                legs.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = drawMoveSpeed;

                //Recipes
                Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe chestRecipe = ScriptableObject.CreateInstance<Recipe>();
                Recipe legsRecipe = ScriptableObject.CreateInstance<Recipe>();

                helmetRecipe.name = $"Recipe_HelmetDrakeT{i}";
                chestRecipe.name = $"Recipe_ArmorWolfChestT{i}";
                legsRecipe.name = $"Recipe_ArmorWolfLegsT{i}";

                //Recipe Requirement List
                List<Piece.Requirement> helmetList = new List<Piece.Requirement>();
                List<Piece.Requirement> chestList = new List<Piece.Requirement>();
                List<Piece.Requirement> legsList = new List<Piece.Requirement>();

                //Add previous armor to requirements
                helmetList.Add(MockRequirement.Create("HelmetDrake", 1));
                chestList.Add(MockRequirement.Create("ArmorWolfChest", 1));
                legsList.Add(MockRequirement.Create("ArmorWolfLegs", 1));

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

        private static void ModExistingRecipes()
        {
            //Leather Loop
            for (int i = (int)balance["leather"]["upgrades"]["startingTier"] + 1; i <= 5; i++)
            {
                Recipe helmetRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"HelmetLeatherT{i}_Terraheim_AddNewSets_AddLeatherArmor").GetComponent<ItemDrop>().m_itemData);
                Recipe chestRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorLeatherChestT{i}_Terraheim_AddNewSets_AddLeatherArmor").GetComponent<ItemDrop>().m_itemData);
                Recipe legsRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorLeatherLegsT{i}_Terraheim_AddNewSets_AddLeatherArmor").GetComponent<ItemDrop>().m_itemData);

                List<Piece.Requirement> helmetList = helmetRecipe.m_resources.ToList();
                List<Piece.Requirement> chestList = chestRecipe.m_resources.ToList();
                List<Piece.Requirement> legsList = legsRecipe.m_resources.ToList();

                helmetList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"HelmetLeatherT{i-1}_Terraheim_AddNewSets_AddLeatherArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                chestList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorLeatherChestT{i-1}_Terraheim_AddNewSets_AddLeatherArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                legsList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorLeatherLegsT{i-1}_Terraheim_AddNewSets_AddLeatherArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                helmetRecipe.m_resources = helmetList.ToArray();
                chestRecipe.m_resources = chestList.ToArray();
                legsRecipe.m_resources = legsList.ToArray();

            }

            //Troll Leather
            for (int i = (int)balance["trollLeather"]["upgrades"]["startingTier"]+1; i <= 5; i++)
            {
                //Log.LogWarning(1);

                Recipe helmetRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"HelmetTrollLeatherT{i}_Terraheim_AddNewSets_AddTrollLeatherArmor").GetComponent<ItemDrop>().m_itemData);
                //Log.LogWarning(2);
                Recipe chestRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorTrollLeatherChestT{i}_Terraheim_AddNewSets_AddTrollLeatherArmor").GetComponent<ItemDrop>().m_itemData);
                //Log.LogWarning(3);
                Recipe legsRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorTrollLeatherLegsT{i}_Terraheim_AddNewSets_AddTrollLeatherArmor").GetComponent<ItemDrop>().m_itemData);

                List<Piece.Requirement> helmetList = helmetRecipe.m_resources.ToList();
                List<Piece.Requirement> chestList = chestRecipe.m_resources.ToList();
                List<Piece.Requirement> legsList = legsRecipe.m_resources.ToList();

                //Log.LogWarning(4);
                helmetList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"HelmetTrollLeatherT{i - 1}_Terraheim_AddNewSets_AddTrollLeatherArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                //Log.LogWarning(5);
                chestList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorTrollLeatherChestT{i - 1}_Terraheim_AddNewSets_AddTrollLeatherArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                //Log.LogWarning(6);
                legsList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorTrollLeatherLegsT{i - 1}_Terraheim_AddNewSets_AddTrollLeatherArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                helmetRecipe.m_resources = helmetList.ToArray();
                chestRecipe.m_resources = chestList.ToArray();
                legsRecipe.m_resources = legsList.ToArray();

            }

            //Bronze
            for (int i = (int)balance["bronze"]["upgrades"]["startingTier"]+1; i <= 5; i++)
            {
                Recipe helmetRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"HelmetBronzeT{i}_Terraheim_AddNewSets_AddBronzeArmor").GetComponent<ItemDrop>().m_itemData);
                Recipe chestRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorBronzeChestT{i}_Terraheim_AddNewSets_AddBronzeArmor").GetComponent<ItemDrop>().m_itemData);
                Recipe legsRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorBronzeLegsT{i}_Terraheim_AddNewSets_AddBronzeArmor").GetComponent<ItemDrop>().m_itemData);

                List<Piece.Requirement> helmetList = helmetRecipe.m_resources.ToList();
                List<Piece.Requirement> chestList = chestRecipe.m_resources.ToList();
                List<Piece.Requirement> legsList = legsRecipe.m_resources.ToList();

                helmetList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"HelmetBronzeT{i - 1}_Terraheim_AddNewSets_AddBronzeArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                chestList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorBronzeChestT{i - 1}_Terraheim_AddNewSets_AddBronzeArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                legsList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorBronzeLegsT{i - 1}_Terraheim_AddNewSets_AddBronzeArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                helmetRecipe.m_resources = helmetList.ToArray();
                chestRecipe.m_resources = chestList.ToArray();
                legsRecipe.m_resources = legsList.ToArray();

            }
            //Iron
            for (int i = (int)balance["iron"]["upgrades"]["startingTier"]+1; i <= 5; i++)
            {
                Recipe helmetRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"HelmetIronT{i}_Terraheim_AddNewSets_AddIronArmor").GetComponent<ItemDrop>().m_itemData);
                Recipe chestRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorIronChestT{i}_Terraheim_AddNewSets_AddIronArmor").GetComponent<ItemDrop>().m_itemData);
                Recipe legsRecipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"ArmorIronLegsT{i}_Terraheim_AddNewSets_AddIronArmor").GetComponent<ItemDrop>().m_itemData);

                List<Piece.Requirement> helmetList = helmetRecipe.m_resources.ToList();
                List<Piece.Requirement> chestList = chestRecipe.m_resources.ToList();
                List<Piece.Requirement> legsList = legsRecipe.m_resources.ToList();

                helmetList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"HelmetIronT{i - 1}_Terraheim_AddNewSets_AddIronArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                chestList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorIronChestT{i - 1}_Terraheim_AddNewSets_AddIronArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                legsList.Add(new Piece.Requirement()
                {
                    m_resItem = ObjectDB.instance.GetItemPrefab($"ArmorIronLegsT{i - 1}_Terraheim_AddNewSets_AddIronArmor").GetComponent<ItemDrop>(),
                    m_amount = 1,
                    m_amountPerLevel = 0
                });

                helmetRecipe.m_resources = helmetList.ToArray();
                chestRecipe.m_resources = chestList.ToArray();
                legsRecipe.m_resources = legsList.ToArray();    

            }

        }
    }
}
