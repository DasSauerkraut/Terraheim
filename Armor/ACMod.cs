using ValheimLib;
using ValheimLib.ODB;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Terraheim;
using Newtonsoft.Json.Linq;
using System.Linq;
using UnityEngine;

namespace Terraheim.Armor
{
    internal static class ACMod
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ObjectDBHelper.OnAfterInit += ModLeatherArmor;
            ObjectDBHelper.OnAfterInit += ModTrollArmor;
            ObjectDBHelper.OnAfterInit += ModBronzeArmor;
            ObjectDBHelper.OnAfterInit += ModIronArmor;
            ObjectDBHelper.OnAfterInit += ModSilverArmor;
            ObjectDBHelper.OnAfterInit += ModPaddedArmor;
            ObjectDBHelper.OnAfterInit += ModCapes;
        }

        private static void ModLeatherArmor()
        {
            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetLeather");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorLeatherChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorLeatherLegs");

            var setBalance = balance["leather"];

            ArmorHelper.ModArmorSet("leather", ref helmet, ref chest, ref legs, setBalance, false, -1);
        }

        private static void ModTrollArmor()
        {
            var setBalance = balance["trollLeather"];

            var trollHood = Prefab.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather");
            var trollChest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherChest");
            var trollLegs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherLegs");

            ArmorHelper.ModArmorSet("trollLeather", ref trollHood, ref trollChest, ref trollLegs, setBalance, false, -1);
        }

        private static void ModBronzeArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetBronze");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBronzeChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");
            var setBalance = balance["bronze"];

            ArmorHelper.ModArmorSet("bronze", ref helmet, ref chest, ref legs, setBalance, false, -1);
        }

        private static void ModIronArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetIron");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorIronChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorIronLegs");
            var setBalance = balance["iron"];

            ArmorHelper.ModArmorSet("iron", ref helmet, ref chest, ref legs, setBalance, false, -1);
        }

        private static void ModSilverArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetDrake");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorWolfChest");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorWolfLegs");
            var setBalance = balance["silver"];

            ArmorHelper.ModArmorSet("silver", ref helmet, ref chest, ref legs, setBalance, false, -1);
        }

        private static void ModPaddedArmor()
        {

            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("HelmetPadded");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorPaddedCuirass");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorPaddedGreaves");

            var setBalance = balance["padded"];

            ArmorHelper.ModArmorSet("padded", ref helmet, ref chest, ref legs, setBalance, false, -1);
        }

        private static void ModCapes()
        {
            var deerCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeDeerHide");
            var trollCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeTrollHide");
            var wolfCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeWolf");
            var loxCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeLox");
            var linenCape = Prefab.Cache.GetPrefab<ItemDrop>("CapeLinen");

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
