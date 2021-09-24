
using HarmonyLib;

namespace Terraheim.ArmorEffects
{
    class SE_DodgeStamUse : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Dodge Stamina Use";
            base.name = "Dodge Stamina Use";
            m_tooltip = $"Dodge stamina cost is reduced by <color=cyan>{getDodgeStaminaUse() * 100}%</color>.";
        }

        public void setDodgeStaminaUse(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"Dodge stamina cost is reduced by <color=cyan>{getDodgeStaminaUse() * 100}%</color>.";
        }

        public float getDodgeStaminaUse() { return m_bonus; }
    }
}