
namespace Terraheim.ArmorEffects
{
    class SE_StaminaRegen : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Stamina Regen";
            base.name = "Stamina Regen";
            m_tooltip = "Stamina Regen is increased by " + m_bonus + "%.";
        }

        public void SetRegenPercent(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Stamina Regen is increased by " + m_bonus + "%.";
        }

        public float GetRegenPercent() { return m_bonus; }
    }
}
