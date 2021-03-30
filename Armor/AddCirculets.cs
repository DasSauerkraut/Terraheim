using ValheimLib;
using ValheimLib.ODB;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Terraheim.Armor
{
    internal static class AddCirculets
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddWoodsmanCirculet;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddMinersCirculet;
        }

        private static void AddWoodsmanCirculet()
        {
            var setBalance = balance["woodsmanHelmet"];
            //create new items
            var mockHelmet = Mock<ItemDrop>.Create("BeltStrength");

            var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"HelmetWoodsman", false);
            CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);

            //name items
            helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_helmet_woodsman";

            //Adjust Stats
            //helmet.ItemDrop.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
            //helmet.ItemDrop.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
            //helmet.ItemDrop.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];

            //Create Status Effects
            var axeDamageBonus = Prefab.Cache.GetPrefab<SE_TreeDamageBonus>("Tree Damage Bonus");

            //Configure Status Effects
            axeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.ItemDrop.m_itemData.m_shared.m_description = $"Weland's designs raises your effectiveness at woodcutting.\nDamage against trees is increased by <color=yellow>{axeDamageBonus.getDamageBonus() * 100}%</color>.";

            //Apply Status Effects
            helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = axeDamageBonus;

            //Recipes
            Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();

            helmetRecipe.name = $"Recipe_HelmetWoodsman";

            //Recipe Requirement List
            List<Piece.Requirement> helmetList = new List<Piece.Requirement>();

            //Get recipe reqs from json
            var recipeReqs = setBalance["crafting"];
            int index = 0;
            foreach (JObject item in recipeReqs["items"])
            {
                helmetList.Add(MockRequirement.Create((string)item["item"],(int)item["amount"]));
                helmetList[index].m_amountPerLevel = (int)item["perLevel"];
                index++;
            }

            //Set crafting station
            helmetRecipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);

            //Set crafting station level
            helmetRecipe.m_minStationLevel = (int)recipeReqs["minLevel"];

            //Assign reqs to recipe
            helmetRecipe.m_resources = helmetList.ToArray();

            //Add item to recipe
            helmetRecipe.m_item = helmet.ItemDrop;

            //Create the custome recipe
            CustomRecipe customHelmetRecipe = new CustomRecipe(helmetRecipe, fixReference: true, fixRequirementReferences: true);

            ObjectDBHelper.Add(helmet);

            //Add recipes to DB
            ObjectDBHelper.Add(customHelmetRecipe);
        }

        private static void AddMinersCirculet()
        {
            var setBalance = balance["minersBelt"];
            //create new items
            var mockHelmet = Mock<ItemDrop>.Create("BeltStrength");

            var clonedHelmet = Prefab.GetRealPrefabFromMock<ItemDrop>(mockHelmet).gameObject.InstantiateClone($"BeltMiner", false);
            CustomItem helmet = new CustomItem(clonedHelmet, fixReference: true);

            //name items
            helmet.ItemDrop.m_itemData.m_shared.m_name = $"$item_belt_miner";

            //Adjust Stats
            //helmet.ItemDrop.m_itemData.m_shared.m_armor = (float)setBalance["baseArmor"];
            //helmet.ItemDrop.m_itemData.m_shared.m_armorPerLevel = (float)setBalance["armorPerLevel"];
            //helmet.ItemDrop.m_itemData.m_shared.m_movementModifier = (float)setBalance["globalMoveMod"];

            //Create Status Effects
            var axeDamageBonus = Prefab.Cache.GetPrefab<SE_MiningBonus>("Mining Bonus");

            //Configure Status Effects
            axeDamageBonus.setDamageBonus((float)setBalance["headEffectVal"]);
            helmet.ItemDrop.m_itemData.m_shared.m_description = $"An artifact of Brokkr, crushing earth comes easily now.\nDamage against rocks and ores is increased by <color=yellow>{axeDamageBonus.getDamageBonus() * 100}%</color>.";

            //Apply Status Effects
            helmet.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = axeDamageBonus;

            //Recipes
            Recipe helmetRecipe = ScriptableObject.CreateInstance<Recipe>();

            helmetRecipe.name = "Recipe_BeltMiner";

            //Recipe Requirement List
            List<Piece.Requirement> helmetList = new List<Piece.Requirement>();

            //Get recipe reqs from json
            var recipeReqs = setBalance["crafting"];
            int index = 0;
            foreach (JObject item in recipeReqs["items"])
            {
                helmetList.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                helmetList[index].m_amountPerLevel = (int)item["perLevel"];
                index++;
            }

            //Set crafting station
            helmetRecipe.m_craftingStation = Mock<CraftingStation>.Create((string)recipeReqs["station"]);

            //Set crafting station level
            helmetRecipe.m_minStationLevel = (int)recipeReqs["minLevel"];

            //Assign reqs to recipe
            helmetRecipe.m_resources = helmetList.ToArray();

            //Add item to recipe
            helmetRecipe.m_item = helmet.ItemDrop;

            //Create the custome recipe
            CustomRecipe customHelmetRecipe = new CustomRecipe(helmetRecipe, fixReference: true, fixRequirementReferences: true);

            ObjectDBHelper.Add(helmet);

            //Add recipes to DB
            ObjectDBHelper.Add(customHelmetRecipe);
        }
    }
}
