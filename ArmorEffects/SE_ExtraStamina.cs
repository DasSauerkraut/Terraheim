
namespace Terraheim.ArmorEffects
{
    class SE_ExtraStamina : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Extra Stamina";
            base.name = "Extra Stamina";
            m_tooltip = $"Stamina is increased by <color=cyan>{GetStaminaBonus()}</color> points.";
        }

        public void SetStaminaBonus(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"Stamina is increased by <color=cyan>{GetStaminaBonus()}</color> points.";
        }

        public float GetStaminaBonus() { return m_bonus; }
    }
}
