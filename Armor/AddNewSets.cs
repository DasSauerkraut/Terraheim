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
            if ((bool)Terraheim.balance["leather"]["enabled"])
                ArmorHelper.AddArmorSet("leather");
            //ArmorHelper.AddArmorSet("rags");
            if ((bool)Terraheim.balance["rags"]["enabled"])
            { 
                ArmorHelper.AddArmorPiece("rags", "chest");
                ArmorHelper.AddArmorPiece("rags", "legs");
            }

            if ((bool)Terraheim.balance["trollLeather"]["enabled"])
                ArmorHelper.AddArmorSet("trollLeather");

            if ((bool)Terraheim.balance["bronze"]["enabled"])
                ArmorHelper.AddArmorSet("bronze");

            if ((bool)Terraheim.balance["iron"]["enabled"])
                ArmorHelper.AddArmorSet("iron");

            if ((bool)Terraheim.balance["silver"]["enabled"])
                ArmorHelper.AddArmorSet("silver");

            if ((bool)Terraheim.balance["padded"]["enabled"])
                ArmorHelper.AddArmorSet("padded");
            if (Terraheim.hasBarbarianArmor)
                ArmorHelper.AddArmorSet("barbarian");
            /*if (Terraheim.hasChaosArmor)
                ArmorHelper.AddArmorSet("chaos");*/

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
            if ((bool)Terraheim.balance["leather"]["enabled"])
                ArmorHelper.AddTieredRecipes("leather");
            //ArmorHelper.AddTieredRecipes("rags");
            if ((bool)Terraheim.balance["trollLeather"]["enabled"])
                ArmorHelper.AddTieredRecipes("trollLeather");
            if ((bool)Terraheim.balance["bronze"]["enabled"])
                ArmorHelper.AddTieredRecipes("bronze");
            if ((bool)Terraheim.balance["iron"]["enabled"])
                ArmorHelper.AddTieredRecipes("iron");
            if ((bool)Terraheim.balance["silver"]["enabled"])
                ArmorHelper.AddTieredRecipes("silver");
            if ((bool)Terraheim.balance["padded"]["enabled"])
                ArmorHelper.AddTieredRecipes("padded");
            if (Terraheim.hasBarbarianArmor)
                ArmorHelper.AddTieredRecipes("barbarian");
            /*if (Terraheim.hasChaosArmor)
                ArmorHelper.AddTieredRecipes("chaos");*/

            ArmorHelper.AddTieredCape("CapeDeerHide");
            ArmorHelper.AddTieredCape("CapeTrollHide");
            ArmorHelper.AddTieredCape("CapeLinen");
            if (Terraheim.hasBarbarianArmor)
                ArmorHelper.AddTieredCape("ArmorBarbarianCapeJD");
        }
    }
}
