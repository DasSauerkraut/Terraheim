using HarmonyLib;
using UnityEngine;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class PickableDropPatch
    {
        [HarmonyPatch(typeof(Pickable), nameof(Pickable.RPC_Pick))]
        public static void Prefix(Pickable __instance, long sender)
        {
            var player = Player.GetPlayer(sender);
            if (player == null)
                player = Player.m_localPlayer;
            if (player == null)
                return;
            if (player.GetSEMan().HaveStatusEffect("Harvest Yield Up"))
            {
                if (isWild(__instance.m_itemPrefab))
                    __instance.m_amount += 2;
                else if (isCrop(__instance.m_itemPrefab))
                    __instance.m_amount += 1;
            }
        }

        private static bool isWild(GameObject prefab)
        {
            var name = prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name;
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
            else if (name.Contains("cloudberries"))
                return true;
            else
                return false;
        }

        private static bool isCrop(GameObject prefab)
        {
            var name = prefab.GetComponent<ItemDrop>().m_itemData.m_shared.m_name;
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
            else if (name.Contains("onion"))
                return true;
            else if (name.Contains("puffs"))
                return true;
            else if (name.Contains("magecap"))
                return true;
            else
                return false;
        }
    }
}
