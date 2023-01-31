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
		ItemManager.OnItemsRegistered += ModExistingRecipes;
	}

	private static void AddArmorSets()
	{
        foreach (JToken armorSet in (IEnumerable<JToken>)(Terraheim.balance["armors"]))
        {
            if ((bool)armorSet["enabled"])
            {
				Log.LogInfo("Adding tiers for the " + armorSet["name"].ToString() + " set.");
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
		ItemManager.OnItemsRegistered -= ModExistingRecipes;
	}
}
