using HarmonyLib;
using Jotunn.Managers;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class GetAttackDrawPercentagePatch
    {
        public static Dictionary<string, float> baseDrawSpeeds = new Dictionary<string, float>();

        [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.GetAttackDrawPercentage))]
        static void Prefix(Humanoid __instance)
        {
            ItemDrop.ItemData currentWeapon = __instance.GetCurrentWeapon();

            if (__instance.GetSEMan().HaveStatusEffect("Bow Draw Speed") && currentWeapon != null && currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                SE_BowDrawSpeed sE_BowDrawSpeed = UtilityFunctions.GetStatusEffectFromName("Bow Draw Speed", __instance.GetSEMan()) as SE_BowDrawSpeed;
                float baseDrawSpeed = 0f;

                if(baseDrawSpeeds.ContainsKey(currentWeapon.m_shared.m_name))
                {
                    baseDrawSpeed = baseDrawSpeeds.GetValueSafe(currentWeapon.m_shared.m_name);
                } 
                else
                {
                    ItemDrop itemDrop = PrefabManager.Cache.GetPrefab<ItemDrop>(currentWeapon.m_dropPrefab.name);
                    if (itemDrop == null)
                    {
                        Log.LogMessage("Terraheim (AttackPatch Start) | Weapon is null, grabbing directly");
                        itemDrop = ObjectDB.instance.GetItemPrefab(currentWeapon.m_dropPrefab.name).GetComponent<ItemDrop>();
                    }
                    baseDrawSpeed = itemDrop.m_itemData.m_shared.m_attack.m_drawDurationMin;
                    baseDrawSpeeds.Add(currentWeapon.m_shared.m_name, itemDrop.m_itemData.m_shared.m_attack.m_drawDurationMin);
                }

                currentWeapon.m_shared.m_attack.m_drawDurationMin = baseDrawSpeed * (1 - sE_BowDrawSpeed.GetSpeed());
            }
            else if (currentWeapon != null && currentWeapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow && currentWeapon.m_shared?.m_attack?.m_drawDurationMin != null && baseDrawSpeeds.ContainsKey(currentWeapon.m_shared.m_name) && currentWeapon.m_shared.m_attack.m_drawDurationMin != baseDrawSpeeds.GetValueSafe(currentWeapon.m_shared.m_name))
            {
                currentWeapon.m_shared.m_attack.m_drawDurationMin = baseDrawSpeeds.GetValueSafe(currentWeapon.m_shared.m_name);
            }
        }
    }
}
