using HarmonyLib;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_Pinning : StatusEffect
    {
        public float m_pinTTL = 0f;
        public float m_pinCooldownTTL = 0f;

        public void Awake()
        {
            m_name = "Pinning";
            base.name = "Pinning";
            UpdateTooltip();
        }

        public void SetIcon()
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather").m_itemData.GetIcon();
        }

        public void UpdateTooltip()
        {
            m_tooltip = $"Hitting an enemy with a damage type it is vulnerable to Pins it for {m_pinTTL} seconds. " +
                $"Pinned enemies are vulnerable to all damage types and have halved movement speed. After the effect wears off, " +
                $"{m_pinCooldownTTL} seconds must pass before the same enemy can be pinned again.";
        }

        public void SetPinTTL(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_pinTTL = bonus;
            UpdateTooltip();
        }

        public float GetPinTTL() { return m_pinTTL; }

        public void SetPinCooldownTTL(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_pinCooldownTTL = bonus;
            UpdateTooltip();
        }

        public float GetPinCooldownTTL() { return m_pinCooldownTTL; }
    }
}
