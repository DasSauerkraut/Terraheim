using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_PoisonVulnerable : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Poison Vulnerable";
            base.name = "Poison Vulnerable";
            m_tooltip = "Poison Vulnerable " + m_damageBonus * 10 + "%";
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Poison Vulnerable " + bonus * 10 + "%";
        }

        public float GetDamageBonus() { return m_damageBonus; }
    }
}
