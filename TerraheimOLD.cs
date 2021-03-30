/*using BepInEx;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Terraheim
{
    [BepInPlugin("org.bepinex.plugins.terraheim", "Terraheim", "1.0.0.0")]
    [BepInProcess("valheim.exe")]

    public class Terraheim : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("dassauerkraut.Terraheim");

        void Awake()
        {
            UnityEngine.Debug.Log("Pre Harmony Patch");
            harmony.PatchAll();
        }
        [HarmonyPatch]
        class PatchArmor
        {
            [HarmonyPatch(typeof(ObjectDB), "UpdateItemHashes")]
            static void Postfix()
            {
                Debug.Log("Starting AC Patch...");
                ItemDrop ragChest = new ItemDrop();
                foreach (GameObject item in ObjectDB.instance.m_items)
                {
                    ItemDrop component = item.GetComponent<ItemDrop>(); 
                    Debug.Log(component.m_itemData.m_shared.m_name);
                    if (component.m_itemData.m_shared.m_name.Contains("trollleather"))
                    {
                        Debug.Log("Found it!");
                        ragChest = component;
                        ragChest.m_itemData.m_shared.m_armor = 20;
                        ragChest.m_itemData.m_shared.m_name = "Unreasonably Strong " + ragChest.m_itemData.m_shared.m_name;
                        Debug.Log("Fiddlin with effects");
                        Debug.Log("Movespeed Up");
                        ragChest.m_itemData.m_shared.m_movementModifier = 0.2f;
                        ragChest.m_itemData.m_shared.m_setSize = 3;

                        ragChest.m_itemData.m_shared.m_equipStatusEffect = ObjectDB.instance.GetStatusEffect("Burning");
                    }
                }
                

            }
        }
    }
}*/
