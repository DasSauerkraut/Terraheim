﻿
using HarmonyLib;

namespace Terraheim.ArmorEffects
{
    class SE_BlockPowerBonus : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Block Power Bonus";
            base.name = "Block Power Bonus";
            m_tooltip = "Block Power increased by " + m_bonus*100 + "%";
        }

        public void SetBlockPower(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Block Power increased by " + m_bonus * 100 + "%";
        }

        public float GetBlockPower() { return m_bonus; }
    }
}