
using HarmonyLib;

namespace Terraheim.ArmorEffects
{
    class SE_BlockStamUse : StatusEffect
    {
        public float m_bonus = 0f;
        public float m_baseConsumption = 0f;

        public void Awake()
        {
            m_name = "Block Stamina Use";
            base.name = "Block Stamina Use";
            m_tooltip = $"Base block stamina cost is reduced by <color=cyan>{getBlockStaminaUse() * 100}%</color>.";
        }

        public void setBlockStaminaUse(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"Base block stamina cost is reduced by <color=cyan>{getBlockStaminaUse() * 100}%</color>.";
        }

        public float getBlockStaminaUse() { return m_bonus; }
    }
}