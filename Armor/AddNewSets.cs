using Jotunn.Managers;
using Terraheim.Utility;

namespace Terraheim.Armor
{
    internal static class AddNewSets
    {
        internal static void Init()
        {
            ItemManager.OnVanillaItemsAvailable += AddArmorSets;
            ItemManager.OnItemsRegistered += ModExistingRecipes;
        }
        private static void AddArmorSets()
        {

            ArmorHelper.AddArmorSet("leather");
            //ArmorHelper.AddArmorSet("rags");
            ArmorHelper.AddArmorPiece("rags", "chest");
            ArmorHelper.AddArmorPiece("rags", "legs");
            ArmorHelper.AddArmorSet("trollLeather");
            ArmorHelper.AddArmorSet("bronze");
            ArmorHelper.AddArmorSet("iron");
            ArmorHelper.AddArmorSet("silver");
            if(Terraheim.hasBarbarianArmor)
                ArmorHelper.AddArmorSet("barbarian");
            if (Terraheim.hasChaosArmor)
                ArmorHelper.AddArmorSet("chaos");

            ArmorHelper.AddBelt("woodsmanHelmet");
            ArmorHelper.AddBelt("minersBelt");
            ArmorHelper.AddBelt("waterproofBelt");
            ArmorHelper.AddBelt("farmerBelt");
            ArmorHelper.AddBelt("thiefBelt");

            ArmorHelper.AddCape("CapeDeerHide", "leather");
            ArmorHelper.AddCape("CapeTrollHide", "trollLeather");
            ArmorHelper.AddCape("CapeWolf", "wolf");
            ArmorHelper.AddCape("CapeLox", "lox");
            ArmorHelper.AddCape("CapeLinen", "linen");
            if (Terraheim.hasBarbarianArmor)
                ArmorHelper.AddCape("ArmorBarbarianCapeJD", "barbarian");
        }

        private static void ModExistingRecipes()
        {
            ArmorHelper.AddTieredRecipes("leather");
            //ArmorHelper.AddTieredRecipes("rags");
            ArmorHelper.AddTieredRecipes("trollLeather");
            ArmorHelper.AddTieredRecipes("bronze");
            ArmorHelper.AddTieredRecipes("iron");
            ArmorHelper.AddTieredRecipes("silver");
            if (Terraheim.hasBarbarianArmor)
                ArmorHelper.AddTieredRecipes("barbarian");
            if (Terraheim.hasChaosArmor)
                ArmorHelper.AddTieredRecipes("chaos");

            ArmorHelper.AddTieredCape("CapeDeerHide");
            ArmorHelper.AddTieredCape("CapeTrollHide");
            ArmorHelper.AddTieredCape("CapeLinen");
            if (Terraheim.hasBarbarianArmor)
                ArmorHelper.AddTieredCape("ArmorBarbarianCapeJD");
        }
    }
}
