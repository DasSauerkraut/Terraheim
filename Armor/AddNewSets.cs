using ValheimLib.ODB;
using Terraheim.Utility;

namespace Terraheim.Armor
{
    internal static class AddNewSets
    {
        internal static void Init()
        {
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddArmorSets;
            ObjectDBHelper.OnAfterInit += ModExistingRecipes;
        }
        private static void AddArmorSets()
        {
            ArmorHelper.AddArmorSet("leather");
            ArmorHelper.AddArmorSet("trollLeather");
            ArmorHelper.AddArmorSet("bronze");
            ArmorHelper.AddArmorSet("iron");
            ArmorHelper.AddArmorSet("silver");
        }

        private static void ModExistingRecipes()
        {
            ArmorHelper.AddTieredRecipes("leather");
            ArmorHelper.AddTieredRecipes("trollLeather");
            ArmorHelper.AddTieredRecipes("bronze");
            ArmorHelper.AddTieredRecipes("iron");
            ArmorHelper.AddTieredRecipes("silver");
        }
    }
}
