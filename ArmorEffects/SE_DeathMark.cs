using HarmonyLib;
using System.Threading;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_DeathMark : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public int m_threshold = 0;
        public int m_duration = 0;
        public bool m_lastHitThrowing = false;

        public void Awake()
        {
            m_name = "Death Mark";
            base.name = "Death Mark";
            m_tooltip = $"Hit an enemy {m_threshold} times with throwing weapons to Mark them for death. The next {(m_duration > 1 ? $"{m_duration} hits" : "hit")} against a Marked enemy deals {m_damageBonus}x damage.";
            m_icon = null;
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianBronzeHelmetJD").m_itemData.GetIcon();
        }

        public void SetLastHitThrowing(bool hit) { m_lastHitThrowing = hit; }
        public bool GetLastHitThrowing() { return m_lastHitThrowing; }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = $"Hit an enemy {m_threshold} times with throwing weapons to Mark them for death. The next {(m_duration > 1 ? $"{m_duration} hits" : "hit")} against a Marked enemy deals {m_damageBonus}x damage.";
        }

        public float GetDamageBonus() { return m_damageBonus; }

        public void SetHitDuration(int dur)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_duration = dur;
            m_tooltip = $"Hit an enemy {m_threshold} times with throwing weapons to Mark them for death. The next {(m_duration > 1 ? $"{m_duration} hits" : "hit")} against a Marked enemy deals {m_damageBonus}x damage.";
        }

        public int GetHitDuration() { return m_duration; }

        public void SetThreshold(int threshold)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_threshold = threshold;
            m_tooltip = $"Hit an enemy {m_threshold} times with throwing weapons to Mark them for death. The next {(m_duration > 1 ? $"{m_duration} hits" : "hit")} against a Marked enemy deals {m_damageBonus}x damage.";
        }

        public int GetThreshold() { return m_threshold; }
    }
}
