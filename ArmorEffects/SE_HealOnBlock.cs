using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_HealOnBlock : StatusEffect
    {
        public float m_blockHeal = 0.05f;
        public float m_parryHeal = 0.05f;

        public void Awake()
        {
            m_name = "Heal On Block";
            base.name = "Heal On Block";
            //m_tooltip = "Axe Damage Increased by " + m_damageBonus * 10 + "%";
        }

        public void SetHealBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_blockHeal = bonus;
            m_parryHeal = bonus * 1.75f;
            //m_tooltip = "Axe Damage Increased by " + bonus * 10 + "%";
        }

        public float GetBlockHeal() { return m_blockHeal; }
        public float GetParryHeal() { return m_parryHeal; }
    }
}
