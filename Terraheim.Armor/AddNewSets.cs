using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Terraheim.Utility;

namespace Terraheim.Armor;

internal static class AddNewSets
{
	internal static void Init()
	{
		PrefabManager.OnVanillaPrefabsAvailable += AddArmorSets;
		//ItemManager.OnVanillaItemsAvailable += AddArmorSets;
		ItemManager.OnItemsRegistered += ModExistingRecipes;
	}

	private static void AddArmorSets()
	{
        foreach (JToken armorSet in (IEnumerable<JToken>)(Terraheim.balance["armors"]))
        {
            if ((bool)armorSet["enabled"])
            {
				Log.LogInfo("Modifying the " + armorSet["name"].ToString() + " set.");
                ArmorHelper.AddArmorSet((string)armorSet["name"], armorSet);
            }
        }
        
		ArmorHelper.AddBelt("woodsmanHelmet");
		ArmorHelper.AddBelt("minersBelt");
		ArmorHelper.AddBelt("waterproofBelt");
		ArmorHelper.AddBelt("farmerBelt");
		ArmorHelper.AddBelt("thiefBelt");
		ArmorHelper.AddBelt("setBelt");

        ArmorHelper.AddCape("CapeDeerHide", "leather");
		ArmorHelper.AddCape("CapeTrollHide", "trollLeather");
		ArmorHelper.AddCape("CapeWolf", "wolf");
		ArmorHelper.AddCape("CapeLox", "lox");
		ArmorHelper.AddCape("CapeLinen", "linen");
		if (Terraheim.hasBarbarianArmor)
		{
			ArmorHelper.AddCape("ArmorBarbarianCapeJD", "barbarian");
		}

		PrefabManager.OnVanillaPrefabsAvailable -= AddArmorSets;
	}

	private static void ModExistingRecipes()
	{
        foreach (JToken armorSet in (IEnumerable<JToken>)(Terraheim.balance["armors"]))
        {
            if ((bool)armorSet["enabled"])
            {
                ArmorHelper.AddTieredRecipes((string)armorSet["name"], armorSet);
            }
        }
        /*if ((bool)Terraheim.balance["leather"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("leather");
		}
		if ((bool)Terraheim.balance["trollLeather"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("trollLeather");
		}
		if ((bool)Terraheim.balance["bronze"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("bronze");
		}
		if ((bool)Terraheim.balance["iron"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("iron");
		}
		if ((bool)Terraheim.balance["silver"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("silver");
		}
		if ((bool)Terraheim.balance["padded"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("padded");
		}
		if (Terraheim.hasJudesEquipment)
		{
			ArmorHelper.AddTieredRecipes("barbarian");
			ArmorHelper.AddTieredRecipes("plate");
			ArmorHelper.AddTieredRecipes("nomad");
		}
		else if (Terraheim.hasBarbarianArmor)
		{
			ArmorHelper.AddTieredRecipes("barbarian");
		}
		if ((bool)Terraheim.balance["rags"]!["enabled"])
		{
			ArmorHelper.AddTieredRecipes("rags", hasHelmet: false);
		}
		ArmorHelper.AddTieredCape("CapeDeerHide");
		ArmorHelper.AddTieredCape("CapeTrollHide");
		ArmorHelper.AddTieredCape("CapeLinen");
		if (Terraheim.hasBarbarianArmor)
		{
			ArmorHelper.AddTieredCape("ArmorBarbarianCapeJD");
		}*/
		ItemManager.OnItemsRegistered -= ModExistingRecipes;
	}
}
