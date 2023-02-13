using Jotunn.Entities;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using Terraheim.Utility;

namespace Terraheim.Armor;

internal static class ModExistingSets
{
	private static JObject balance = Terraheim.balance;

	private static bool itemsInstantiated = false;

	internal static void Init()
	{
        foreach (JToken armorSet in (IEnumerable<JToken>)(balance["armors"]))
		{
			if ((bool)armorSet["enabled"] && (string)armorSet["name"] != "beotes" )
            {
                Log.LogInfo("Modifying the " + armorSet["name"].ToString() + " set.");
                ItemManager.OnItemsRegistered += delegate() { ModArmor(armorSet); };
            }
        }
		ItemManager.OnItemsRegistered += ModCapes;
		ItemManager.OnItemsRegistered += ModWeapons;
	}

	internal static void RunWeapons()
	{
		ItemManager.OnItemsRegistered += ModWeapons;
	}

	private static void ModArmor(JToken armor)
	{
        itemsInstantiated = true;
		ItemDrop prefab = null;
		if((string)armor["armorSet"]["HelmetID"] != "n/a") { 
			prefab = PrefabManager.Cache.GetPrefab<ItemDrop>((string)armor["armorSet"]["HelmetID"]);
			Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
            recipe.m_craftingStation = Pieces.Reforger;
			if((int)armor["upgrades"]["startingTier"] < 2)
			{
                recipe.m_minStationLevel = 1;
            }
        }
        ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>((string)armor["armorSet"]["ChestID"]);
        ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>((string)armor["armorSet"]["LegsID"]);
        Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
        Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
        recipe2.m_craftingStation = Pieces.Reforger;
        recipe3.m_craftingStation = Pieces.Reforger;
        recipe2.m_minStationLevel = 1;
        recipe3.m_minStationLevel = 1;
        ArmorHelper.ModArmorSet((string)armor["name"], ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, armor, isNewSet: false, -1);
        ItemManager.OnItemsRegistered -= delegate() { ModArmor(armor); };
    }

	public static void ModJudes()
	{
		if (itemsInstantiated)
		{
			ModBarbarianArmor();
			ModJudesPlateArmor();
			ModJudesNomadArmor();
			itemsInstantiated = false;
		}
	}

	private static void ModBarbarianArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeHelmetJD");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeChestJD");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeLegsJD");
		ItemDrop prefab4 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianCapeJD");
		JToken values = balance["barbarian"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		Recipe recipe4 = ObjectDB.instance.GetRecipe(prefab4.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		recipe4.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("barbarian", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		prefab4.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string?)balance["capes"]!["barbarian"]!["effect"], balance["capes"]!["barbarian"], "cape", ref prefab4.m_itemData.m_shared.m_description);
		ItemManager.OnItemsRegistered -= ModBarbarianArmor;
	}

	private static void ModJudesPlateArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPlateIronHelmetJD");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPlateIronChestJD");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPlateIronLegsJD");
		JToken values = balance["plate"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("plate", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
	}

	private static void ModJudesNomadArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBlackmetalgarbHelmet");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBlackmetalgarbChest");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBlackmetalgarbLegs");
		JToken values = balance["nomad"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("nomad", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
	}

	private static void ModChaosArmor()
	{
		for (int i = 0; i < 3; i++)
		{
			ItemDrop prefab;
			ItemDrop prefab2;
			ItemDrop prefab3;
			if (i != 2)
			{
				prefab = PrefabManager.Cache.GetPrefab<ItemDrop>($"T{i + 1}ChaosPlateHelm");
				prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>($"T{i + 1}ChaosPlateArmor");
				prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>($"T{i + 1}ChaosPlateLegs");
			}
			else
			{
				prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("ChaosPlateHelm");
				prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ChaosPlateArmorBody");
				prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ChaosPlateLegs");
			}
			prefab.m_itemData.m_shared.m_maxQuality = 4;
			prefab2.m_itemData.m_shared.m_maxQuality = 4;
			prefab3.m_itemData.m_shared.m_maxQuality = 4;
			Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
			Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
			Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
			JToken jToken = balance[$"chaosT{i}"];
			UtilityFunctions.GetRecipe(ref recipe, jToken["recipe"], "head", useName: false);
			UtilityFunctions.GetRecipe(ref recipe2, jToken["recipe"], "chest", useName: false);
			UtilityFunctions.GetRecipe(ref recipe3, jToken["recipe"], "legs", useName: false);
			ArmorHelper.ModArmorSet($"chaosT{i}", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, jToken, isNewSet: false, -1);
		}
		ItemManager.OnItemsRegistered -= ModChaosArmor;
	}

	private static void ModCapes()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeDeerHide");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeTrollHide");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeWolf");
		ItemDrop prefab4 = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeLox");
		ItemDrop prefab5 = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeLinen");
		JToken jToken = balance["capes"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		Recipe recipe4 = ObjectDB.instance.GetRecipe(prefab4.m_itemData);
		Recipe recipe5 = ObjectDB.instance.GetRecipe(prefab5.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		recipe4.m_craftingStation = Pieces.Reforger;
		recipe5.m_craftingStation = Pieces.Reforger;
		prefab.m_itemData.m_shared.m_description += string.Format("\nMovement speed is increased by <color=cyan>{0}%</color>.", (float)jToken["leather"]!["capeEffectVal"] * 100f);
		prefab.m_itemData.m_shared.m_movementModifier = (float)jToken["leather"]!["capeEffectVal"];
		recipe.m_minStationLevel = 1;
		prefab2.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string?)jToken["trollLeather"]!["effect"], jToken["trollLeather"], "cape", ref prefab2.m_itemData.m_shared.m_description);
		prefab3.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string?)jToken["wolf"]!["effect"], jToken["wolf"], "cape", ref prefab3.m_itemData.m_shared.m_description);
		prefab4.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string?)jToken["lox"]!["effect"], jToken["lox"], "cape", ref prefab4.m_itemData.m_shared.m_description);
		prefab5.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string?)jToken["linen"]!["effect"], jToken["linen"], "cape", ref prefab5.m_itemData.m_shared.m_description);
		ItemManager.OnItemsRegistered -= ModCapes;
	}

	private static void ModWeapons()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("KnifeSilverTH");
		prefab.m_itemData.m_shared.m_name = "Obsidian Dagger";
		prefab.m_itemData.m_shared.m_description = "Mountain glass, sharp as could be. It has a certain affinity with the Chosen armor.";
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("BowBlackmetalTH");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("BowFireTH");
		ItemDrop prefab4 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArrowGreatFireTH");
		prefab2.m_itemData.m_shared.m_ammoType = "$ammo_arrows";
		prefab2.m_itemData.m_shared.m_attack.m_bowDraw = true;
		prefab2.m_itemData.m_shared.m_attack.m_drawDurationMin = 1.5f;
		prefab2.m_itemData.m_shared.m_attack.m_drawStaminaDrain = 12f;
		prefab2.m_itemData.m_shared.m_attack.m_drawAnimationState = "bow_aim";
		prefab3.m_itemData.m_shared.m_ammoType = "$ammo_arrows";
		prefab3.m_itemData.m_shared.m_attack.m_bowDraw = true;
		prefab3.m_itemData.m_shared.m_attack.m_drawDurationMin = 0.5f;
		prefab3.m_itemData.m_shared.m_attack.m_drawStaminaDrain = 14f;
		prefab3.m_itemData.m_shared.m_attack.m_drawAnimationState = "bow_aim";
		prefab4.m_itemData.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Ammo;
		prefab4.m_itemData.m_shared.m_ammoType = "$ammo_arrows";
		ItemManager.OnItemsRegistered -= ModWeapons;
	}
}
