using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.Armor;

internal static class ModExistingSets
{
	private static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

	private static bool itemsInstantiated = false;

	internal static void Init()
	{
		if ((bool)Terraheim.balance["leather"]!["enabled"])
		{
			ItemManager.OnItemsRegistered += ModLeatherArmor;
		}
		if ((bool)Terraheim.balance["trollLeather"]!["enabled"])
		{
			ItemManager.OnItemsRegistered += ModTrollArmor;
		}
		if ((bool)Terraheim.balance["bronze"]!["enabled"])
		{
			ItemManager.OnItemsRegistered += ModBronzeArmor;
		}
		if ((bool)Terraheim.balance["iron"]!["enabled"])
		{
			ItemManager.OnItemsRegistered += ModIronArmor;
		}
		if ((bool)Terraheim.balance["silver"]!["enabled"])
		{
			ItemManager.OnItemsRegistered += ModSilverArmor;
		}
		if ((bool)Terraheim.balance["padded"]!["enabled"])
		{
			ItemManager.OnItemsRegistered += ModPaddedArmor;
		}
		if (Terraheim.hasBarbarianArmor)
		{
			ItemManager.OnItemsRegistered += ModBarbarianArmor;
		}
		if (Terraheim.hasChaosArmor)
		{
			ItemManager.OnItemsRegistered += ModChaosArmor;
		}
		ItemManager.OnItemsRegistered += ModCapes;
		ItemManager.OnItemsRegistered += ModWeapons;
	}

	internal static void RunWeapons()
	{
		ItemManager.OnItemsRegistered += ModWeapons;
	}

	private static void ModLeatherArmor()
	{
		itemsInstantiated = true;
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherChest");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherLegs");
		JToken values = balance["leather"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		recipe.m_minStationLevel = 1;
		recipe2.m_minStationLevel = 1;
		recipe3.m_minStationLevel = 1;
		ArmorHelper.ModArmorSet("leather", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		ItemManager.OnItemsRegistered -= ModLeatherArmor;
	}

	private static void ModTrollArmor()
	{
		JToken values = balance["trollLeather"];
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherChest");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherLegs");
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("trollLeather", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		ItemManager.OnItemsRegistered -= ModTrollArmor;
	}

	private static void ModBronzeArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetBronze");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeChest");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");
		JToken values = balance["bronze"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("bronze", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		ItemManager.OnItemsRegistered -= ModBronzeArmor;
	}

	private static void ModIronArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetIron");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronChest");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronLegs");
		JToken values = balance["iron"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("iron", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		ItemManager.OnItemsRegistered -= ModIronArmor;
	}

	private static void ModSilverArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfChest");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfLegs");
		JToken values = balance["silver"];
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		ArmorHelper.ModArmorSet("silver", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		ItemManager.OnItemsRegistered -= ModSilverArmor;
	}

	private static void ModPaddedArmor()
	{
		ItemDrop prefab = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded");
		ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedCuirass");
		ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedGreaves");
		Recipe recipe = ObjectDB.instance.GetRecipe(prefab.m_itemData);
		Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
		Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
		recipe.m_craftingStation = Pieces.Reforger;
		recipe2.m_craftingStation = Pieces.Reforger;
		recipe3.m_craftingStation = Pieces.Reforger;
		JToken values = balance["padded"];
		ArmorHelper.ModArmorSet("padded", ref prefab.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, isNewSet: false, -1);
		ItemManager.OnItemsRegistered -= ModPaddedArmor;
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
