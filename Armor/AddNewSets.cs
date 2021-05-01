using Jotunn.Managers;
using Terraheim.Utility;

namespace Terraheim.Armor
{
    internal static class AddNewSets
    {
        internal static void Init()
        {
            On.ObjectDB.CopyOtherDB += AddArmorSets;
            ItemManager.OnItemsRegistered += ModExistingRecipes;
        }
        private static void AddArmorSets(On.ObjectDB.orig_CopyOtherDB orig, ObjectDB self, ObjectDB other)
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
