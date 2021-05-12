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

            ArmorHelper.AddBelt("woodsmanHelmet");
            ArmorHelper.AddBelt("minersBelt");
            ArmorHelper.AddBelt("waterproofBelt");
            ArmorHelper.AddBelt("farmerBelt");
            ArmorHelper.AddBelt("thiefBelt");
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
        }
    }
}
