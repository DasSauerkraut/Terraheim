using ValheimLib;
using ValheimLib.ODB;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Terraheim.Armor
{
    internal static class BarbarianArmor
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        internal static void Init()
        {
            ObjectDBHelper.OnAfterInit += ModExistingArmor;
            ObjectDBHelper.OnBeforeCustomItemsAdded += AddNewSets;
            ObjectDBHelper.OnAfterInit += AddTieredRecipes;
        }

        internal static void Integrate()
        {
            //if (!Harmony.HasAnyPatches("GoldenJude_BarbarianArmor"))
            if (!File.Exists(Terraheim.ModPath + "/../barbarianArmor.dll" ))
            { 
                Log.LogWarning("Barbarian armor not found!");
                return;
            }
            Terraheim.hasBarbarianArmor = true;
            Log.LogInfo("Barbarian Armor Found!");
        }

        private static void ModExistingArmor()
        {
            var helmet = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeHelmetJD");
            var chest = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeChestJD");
            var legs = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeLegsJD");
            var cape = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianCapeJD");

            var setBalance = balance["barbarian"];


            ArmorHelper.ModArmorSet("barbarian", ref helmet, ref chest, ref legs, setBalance, false, -1);

            cape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)balance["capes"]["barbarian"]["effect"], balance["capes"]["barbarian"], "cape", ref cape.m_itemData.m_shared.m_description);
        }

        private static void AddNewSets()
        {
            ArmorHelper.AddArmorSet("barbarian");
        }

        private static void AddTieredRecipes()
        {
            ArmorHelper.AddTieredRecipes("barbarian");
        }
    }
}
