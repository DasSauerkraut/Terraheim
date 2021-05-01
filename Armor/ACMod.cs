using Jotunn.Managers;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Armor
{
    internal static class ACMod
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ItemManager.OnItemsRegistered += ModLeatherArmor;
            ItemManager.OnItemsRegistered += ModTrollArmor;
            ItemManager.OnItemsRegistered += ModBronzeArmor;
            ItemManager.OnItemsRegistered += ModIronArmor;
            ItemManager.OnItemsRegistered += ModSilverArmor;
            ItemManager.OnItemsRegistered += ModPaddedArmor;
            ItemManager.OnItemsRegistered += ModCapes;
        }

        private static void ModLeatherArmor()
        {
            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherLegs");

            var setBalance = balance["leather"];

            ArmorHelper.ModArmorSet("leather", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModTrollArmor()
        {
            var setBalance = balance["trollLeather"];

            var trollHood = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather");
            var trollChest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherChest");
            var trollLegs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherLegs");

            ArmorHelper.ModArmorSet("trollLeather", ref trollHood.m_itemData, ref trollChest.m_itemData, ref trollLegs.m_itemData, setBalance, false, -1);
        }

        private static void ModBronzeArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetBronze");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");
            var setBalance = balance["bronze"];

            ArmorHelper.ModArmorSet("bronze", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModIronArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetIron");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronLegs");
            var setBalance = balance["iron"];

            ArmorHelper.ModArmorSet("iron", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModSilverArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfChest");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfLegs");
            var setBalance = balance["silver"];

            ArmorHelper.ModArmorSet("silver", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModPaddedArmor()
        {

            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedCuirass");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedGreaves");

            var setBalance = balance["padded"];

            ArmorHelper.ModArmorSet("padded", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);
        }

        private static void ModCapes()
        {
            var deerCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeDeerHide");
            var trollCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeTrollHide");
            var wolfCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeWolf");
            var loxCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeLox");
            var linenCape = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeLinen");

            var capeBalance = balance["capes"];
            //deer
            deerCape.m_itemData.m_shared.m_description += $"\nMovement speed is increased by <color=cyan>{(float)capeBalance["leather"]["capeEffectVal"] * 100 }%</color>.";
            deerCape.m_itemData.m_shared.m_movementModifier = (float)capeBalance["leather"]["capeEffectVal"];

            //troll
            trollCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["trollLeather"]["effect"], capeBalance["trollLeather"], "cape", ref trollCape.m_itemData.m_shared.m_description);

            wolfCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["wolf"]["effect"], capeBalance["wolf"], "cape", ref wolfCape.m_itemData.m_shared.m_description);

            loxCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["lox"]["effect"], capeBalance["lox"], "cape", ref loxCape.m_itemData.m_shared.m_description);

            linenCape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)capeBalance["linen"]["effect"], capeBalance["linen"], "cape", ref linenCape.m_itemData.m_shared.m_description);

        }

    }
}
