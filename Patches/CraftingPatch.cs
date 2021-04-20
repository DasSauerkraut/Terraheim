using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class CraftingPatch
    {
        static string m_crafterNameHolder;

        public void Awake()
        {
            Log.LogInfo("Draw Move Speed Patching Complete");
        }

        //public static string GetName() { return m_crafterNameHolder; }

        [HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
        public static void Prefix(InventoryGui __instance, Player player)
        {
            Log.LogWarning("Doin crafting");
            if (__instance.m_craftRecipe == null)
            {
                return;
            }

            Log.LogWarning("Doin crafting");

            var terraheimItems = Array.FindLast(__instance.m_craftRecipe.m_resources, resource => resource.m_resItem.name.Contains("_Terraheim_AddNewSets_") ||
            resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet ||
            resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest ||
            resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs);
            Log.LogMessage("Contains item: " + player.GetInventory().ContainsItem(terraheimItems.m_resItem.m_itemData));
            if (terraheimItems != null) {
                var inventoryItem = player.GetInventory().GetAllItems().Find(item => item.m_shared.m_name == terraheimItems.m_resItem.m_itemData.m_shared.m_name);
                if (inventoryItem != null)
                {
                    m_crafterNameHolder = inventoryItem.m_crafterName;
                    //m_crafterNameHolder.Replace(inventoryItem.m_shared.m_name, __instance.m_craftRecipe.m_item.m_itemData.m_shared.m_name);
                }
                Log.LogMessage("From Do Craftin " + m_crafterNameHolder);
            }
            else if (m_crafterNameHolder != null)
            {
                m_crafterNameHolder = null;
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Inventory), "AddItem", new Type[]
            {
                typeof(string),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(long),
                typeof(string),
            }
        )]
        public static void AddItemPostfix(ref ItemDrop.ItemData __result)
        {
            if (m_crafterNameHolder != null)
            {
                __result.m_crafterName = m_crafterNameHolder;
                m_crafterNameHolder = null;
            }
            Log.LogWarning("From AddItem" + __result.m_crafterName);
        }
        /*static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {

            var foundPlayerCall = false;
            int startIndex = -1;
            int endIndex = -1;

            var codes = new List<CodeInstruction>(instructions);

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldarg_1)
                {   
                    if(i+1 < codes.Count)
                        Log.LogWarning(codes[i] + " " + codes[i + 1]);

                    if (i+1 < codes.Count && codes[i+1].opcode == OpCodes.Callvirt)
                    {
                        if (foundPlayerCall)
                        {
                            //endIndex = i;
                            break;
                        }
                        else
                        {
                            startIndex = i + 1;

                            var strOperand = codes[startIndex].operand as string;
                            if(strOperand == "GetPlayerName")
                            {
                                foundPlayerCall = true;
                                endIndex = i;
                                Log.LogWarning(endIndex);
                                Log.LogWarning(startIndex);
                                break;
                            }
                        }
                    }
                }
            }
            if (startIndex > -1 && endIndex > -1)
            {
                //change shit
                var code = new CodeInstruction(OpCodes.Ldfld, m_crafterNameHolder);
                codes[endIndex] = code;
                codes.RemoveRange(startIndex, 1);
            }

            return codes.AsEnumerable();
        }*/
    }
}
    
