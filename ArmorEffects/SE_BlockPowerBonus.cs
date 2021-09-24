
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
            m_tooltip = $"\n\nBlock power increased by <color=cyan>{GetBlockPower() * 100}%</color>.";
        }

        public void SetBlockPower(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"\n\nBlock power increased by <color=cyan>{GetBlockPower() * 100}%</color>.";
        }

        public float GetBlockPower() { return m_bonus; }
    }
}