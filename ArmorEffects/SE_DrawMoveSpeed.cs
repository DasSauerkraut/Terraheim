
using HarmonyLib;

namespace Terraheim.ArmorEffects
{
    class SE_DrawMoveSpeed : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Draw Move Speed";
            base.name = "Draw Move Speed";
            m_tooltip = "Draw Move Speed increased by " + m_bonus + "x";
        }

        public void SetDrawMoveSpeed(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Draw Move Speed increased by " + m_bonus + "x";
        }

        public float GetDrawMoveSpeed() { return m_bonus; }
    }
}