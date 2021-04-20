using HarmonyLib;
using UnityEngine;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class PickableDropPatch
    {
        [HarmonyPatch(typeof(Pickable), nameof(Pickable.Drop))]
        public static void Prefix(GameObject prefab, ref int stack)
        {
            var player = Player.m_localPlayer;
            if (player.GetSEMan().HaveStatusEffect("Harvest Yield Up"))
            {
                if (isWild(prefab))
                {
                    stack += 2;
                }
                else if (isCrop(prefab))
                {
                    stack += 1;
                }
            }
        }

        private static bool isWild(GameObject prefab)
        {
            var name = prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name;
            Log.LogWarning(name);
            if (name.Contains("dandelion"))
                return true;
            else if (name.Contains("thistle"))
                return true;
            else if (name.Contains("mushroom"))
                return true;
            else if (name.Contains("raspberries"))
                return true;
            else if (name.Contains("blueberries"))
                return true;
            else if (name.Contains("cloudberry"))
                return true;
            else
                return false;
            /*switch (prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name)
            {
                case ("Dandelion"):
                    return true;
                case ("Mushroom"):
                    return true;
                case ("MushroomYellow"):
                    return true;
                case ("Thistle"):
                    return true;
                case ("Raspberry"):
                    return true;
                case ("Blueberries"):
                    return true;
                case ("Cloudberry"):
                    return true;
                default:
                    break;
            }*/
        }

        private static bool isCrop(GameObject prefab)
        {
            var name = prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name;
            Log.LogWarning(name);
            if (name.Contains("barley"))
                return true;
            else if (name.Contains("flax"))
                return true;
            else if (name.Contains("carrot"))
                return true;
            else if (name.Contains("turnip"))
                return true;
            else if (name.Contains("honey"))
                return true;
            else
                return false;
            /*
            switch (prefab.GetComponent<ItemDrop>().m_itemData.m_dropPrefab.name)
            {
                case ("Barley"):
                    return true;
                case ("Flax"):
                    return true;
                case ("Carrot"):
                    return true;
                case ("Turnip"):
                    return true;
                case ("Honey"):
                    return true;
                default:
                    break;
            }
            return false;*/
        }
    }
}
