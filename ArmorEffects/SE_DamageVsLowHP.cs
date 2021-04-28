using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_DamageVSLowHP : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public float m_healthThreshold = 0f;

        public void Awake()
        {
            m_name = "Damage Vs Low HP";
            base.name = "Damage Vs Low HP";
            m_tooltip = "Damage Vs Low HP Increased by " + m_damageBonus * 10 + "%";
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Damage Vs Low HP Increased by " + bonus * 10 + "%";
        }

        public float GetDamageBonus() { return m_damageBonus; }

        public void SetHealthThreshold(float bonus) { m_healthThreshold = bonus; }
        public float GetHealthThreshold() { return m_healthThreshold; }
    }
}
