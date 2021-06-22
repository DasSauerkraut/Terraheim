using Jotunn.Managers;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;
using Jotunn.Entities;

namespace Terraheim.Armor
{
    internal static class ModExistingSets
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            if ((bool)Terraheim.balance["leather"]["enabled"])
                ItemManager.OnItemsRegistered += ModLeatherArmor;
            if ((bool)Terraheim.balance["trollLeather"]["enabled"])
                ItemManager.OnItemsRegistered += ModTrollArmor;
            if ((bool)Terraheim.balance["bronze"]["enabled"])
                ItemManager.OnItemsRegistered += ModBronzeArmor;
            if ((bool)Terraheim.balance["iron"]["enabled"])
                ItemManager.OnItemsRegistered += ModIronArmor;
            if ((bool)Terraheim.balance["silver"]["enabled"])
                ItemManager.OnItemsRegistered += ModSilverArmor;
            if ((bool)Terraheim.balance["padded"]["enabled"])
                ItemManager.OnItemsRegistered += ModPaddedArmor;
            if(Terraheim.hasBarbarianArmor)
                ItemManager.OnItemsRegistered += ModBarbarianArmor;
            if (Terraheim.hasChaosArmor)
                ItemManager.OnItemsRegistered += ModChaosArmor;
            ItemManager.OnItemsRegistered += ModCapes;
        }

        private static void ModLeatherArmor()
        {
            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherLegs");

            var setBalance = balance["leather"];

            var headRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;

            headRecipe.m_minStationLevel = 1;
            chestRecipe.m_minStationLevel = 1;
            legsRecipe.m_minStationLevel = 1;

            ArmorHelper.ModArmorSet("leather", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
            ItemManager.OnItemsRegistered -= ModLeatherArmor;
        }

        private static void ModTrollArmor()
        {
            var setBalance = balance["trollLeather"];

            var trollHood = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather");
            var trollChest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherChest");
            var trollLegs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherLegs");

            var headRecipe = ObjectDB.instance.GetRecipe(trollHood.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(trollChest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(trollLegs.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;

            ArmorHelper.ModArmorSet("trollLeather", ref trollHood.m_itemData, ref trollChest.m_itemData, ref trollLegs.m_itemData, setBalance, false, -1);
        }

        private static void ModBronzeArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetBronze");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");
            var setBalance = balance["bronze"];

            var headRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;

            ArmorHelper.ModArmorSet("bronze", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModIronArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetIron");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronLegs");
            var setBalance = balance["iron"];

            var headRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;

            ArmorHelper.ModArmorSet("iron", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModSilverArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfLegs");
            var setBalance = balance["silver"];

            var headRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;

            ArmorHelper.ModArmorSet("silver", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModPaddedArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedCuirass");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedGreaves");

            var headRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;

            var setBalance = balance["padded"];

            ArmorHelper.ModArmorSet("padded", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModBarbarianArmor()
        {
            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeHelmetJD");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeChestJD");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeLegsJD");
            var cape = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianCapeJD");

            var setBalance = balance["barbarian"];

            var headRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
            var chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
            var legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);
            var capeRecipe = ObjectDB.instance.GetRecipe(cape.m_itemData);

            headRecipe.m_craftingStation = Pieces.Reforger;
            chestRecipe.m_craftingStation = Pieces.Reforger;
            legsRecipe.m_craftingStation = Pieces.Reforger;
            capeRecipe.m_craftingStation = Pieces.Reforger;

            ArmorHelper.ModArmorSet("barbarian", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);

            cape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)balance["capes"]["barbarian"]["effect"], balance["capes"]["barbarian"], "cape", ref cape.m_itemData.m_shared.m_description);
        }

        private static void ModChaosArmor()
        {

            for (int i = 0; i < 3; i++)
            {
                ItemDrop helmet;
                ItemDrop chest;
                ItemDrop legs;
                if (i != 2)
                {
                    helmet = PrefabManager.Cache.GetPrefab<ItemDrop>($"T{i + 1}ChaosPlateHelm");
                    chest = PrefabManager.Cache.GetPrefab<ItemDrop>($"T{i + 1}ChaosPlateArmor");
                    legs = PrefabManager.Cache.GetPrefab<ItemDrop>($"T{i + 1}ChaosPlateLegs");
                }
                else
                {
                    helmet = PrefabManager.Cache.GetPrefab<ItemDrop>($"ChaosPlateHelm");
                    chest = PrefabManager.Cache.GetPrefab<ItemDrop>($"ChaosPlateArmorBody");
                    legs = PrefabManager.Cache.GetPrefab<ItemDrop>($"ChaosPlateLegs");
                }

                Recipe helmetRecipe = ObjectDB.instance.GetRecipe(helmet.m_itemData);
                Recipe chestRecipe = ObjectDB.instance.GetRecipe(chest.m_itemData);
                Recipe legsRecipe = ObjectDB.instance.GetRecipe(legs.m_itemData);

                var setBalance = balance[$"chaosT{i}"];

                UtilityFunctions.GetRecipe(ref helmetRecipe, setBalance["recipe"], "head", false);
                UtilityFunctions.GetRecipe(ref chestRecipe, setBalance["recipe"], "chest", false);
                UtilityFunctions.GetRecipe(ref legsRecipe, setBalance["recipe"], "legs", false);

                ArmorHelper.ModArmorSet($"chaosT{i}", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
            }
        }

        private static void ModCapes()
        {
            var deerCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeDeerHide");
            var trollCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeTrollHide");
            var wolfCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeWolf");
            var loxCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeLox");
            var linenCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeLinen");

            var capeBalance = balance["capes"];

            var deerRecipe = ObjectDB.instance.GetRecipe(deerCape.m_itemData);
            var trollRecipe = ObjectDB.instance.GetRecipe(trollCape.m_itemData);
            var wolfRecipe = ObjectDB.instance.GetRecipe(wolfCape.m_itemData);
            var loxRecipe = ObjectDB.instance.GetRecipe(loxCape.m_itemData);
            var linenRecipe = ObjectDB.instance.GetRecipe(linenCape.m_itemData);

            deerRecipe.m_craftingStation = Pieces.Reforger;
            trollRecipe.m_craftingStation = Pieces.Reforger;
            wolfRecipe.m_craftingStation = Pieces.Reforger;
            loxRecipe.m_craftingStation = Pieces.Reforger;
            linenRecipe.m_craftingStation = Pieces.Reforger;

            //deer
            deerCape.m_itemData.m_shared.m_description += $"\nMovement speed is increased by <color=cyan>{(float)capeBalance["leather"]["capeEffectVal"] * 100 }%</color>.";
            deerCape.m_itemData.m_shared.m_movementModifier = (float)capeBalance["leather"]["capeEffectVal"];
            deerRecipe.m_minStationLevel = 1;

            //troll
            trollCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["trollLeather"]["effect"], capeBalance["trollLeather"], "cape", ref trollCape.m_itemData.m_shared.m_description);

            wolfCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["wolf"]["effect"], capeBalance["wolf"], "cape", ref wolfCape.m_itemData.m_shared.m_description);

            loxCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["lox"]["effect"], capeBalance["lox"], "cape", ref loxCape.m_itemData.m_shared.m_description);

            linenCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["linen"]["effect"], capeBalance["linen"], "cape", ref linenCape.m_itemData.m_shared.m_description);
            ItemManager.OnItemsRegistered -= ModCapes;
        }

    }
}
