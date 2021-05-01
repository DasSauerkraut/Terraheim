using Jotunn.Managers;
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
            ItemManager.OnItemsRegistered += ModExistingArmor;
            On.ObjectDB.CopyOtherDB += AddNewSets;
            ItemManager.OnItemsRegistered += AddTieredRecipes;
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
            var helmet = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeHelmetJD");
            var chest = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeChestJD");
            var legs = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeLegsJD");
            var cape = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBarbarianCapeJD");

            var setBalance = balance["barbarian"];


            ArmorHelper.ModArmorSet("barbarian", ref helmet.m_itemData, ref chest.m_itemData, ref legs.m_itemData, setBalance, false, -1);

            cape.m_itemData.m_shared.m_equipStatusEffect = ArmorHelper.GetArmorEffect((string)balance["capes"]["barbarian"]["effect"], balance["capes"]["barbarian"], "cape", ref cape.m_itemData.m_shared.m_description);
        }

        private static void AddNewSets(On.ObjectDB.orig_CopyOtherDB orig, ObjectDB self, ObjectDB other)
        {
            ArmorHelper.AddArmorSet("barbarian");
        }

        private static void AddTieredRecipes()
        {
            ArmorHelper.AddTieredRecipes("barbarian");
        }
    }
}
