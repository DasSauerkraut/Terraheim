using System;
using HarmonyLib;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class CraftingPatch
{
	private static string m_crafterNameHolder;

	public void Awake()
	{
		Log.LogInfo("Draw Move Speed Patching Complete");
	}

	[HarmonyPatch(typeof(InventoryGui), "DoCrafting")]
	public static void Prefix(InventoryGui __instance, Player player)
	{
		if (__instance.m_craftRecipe == null || __instance.m_craftRecipe.m_resources == null || __instance.m_craftRecipe.m_resources[0].m_resItem == null)
		{
			return;
		}
		int terraheimItems = Array.FindIndex(__instance.m_craftRecipe.m_resources, (Piece.Requirement resource) => resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet || resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest || resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs || resource.m_resItem.m_itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder);
		if (terraheimItems != -1)
		{
			ItemDrop.ItemData itemData = player.GetInventory().GetAllItems().Find((ItemDrop.ItemData item) => item.m_shared.m_name == __instance.m_craftRecipe.m_resources[terraheimItems].m_resItem.m_itemData.m_shared.m_name);
			if (itemData != null)
			{
				m_crafterNameHolder = itemData.m_crafterName;
			}
			Log.LogMessage("Crafter Info " + m_crafterNameHolder);
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
		typeof(string)
	})]
	public static void AddItemPostfix(ref ItemDrop.ItemData __result)
	{
		if (m_crafterNameHolder != null)
		{
			__result.m_crafterName = m_crafterNameHolder;
			m_crafterNameHolder = null;
		}
		Log.LogInfo("Crafter Name from Add Item Patch" + __result.m_crafterName);
	}
}
