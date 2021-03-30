
using HarmonyLib;

namespace Terraheim.ArmorEffects
{
    class SE_AttackStaminaUse : StatusEffect
    {
        public static float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Attack Stamina Use";
            base.name = "Attack Stamina Use";
            m_tooltip = "Attack Stamina Use decreased by " + m_bonus + "x";
        }

        public void SetStaminaUse(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Attack Stamina Use decreased by " + bonus + "x";
        }

        public float GetStaminaUse() { return m_bonus; }
    }
}