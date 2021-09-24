
namespace Terraheim.ArmorEffects
{
    class SE_StaminaRegen : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Stamina Regen";
            base.name = "Stamina Regen";
            m_tooltip = $"Stamina regen is increased by <color=cyan>{GetRegenPercent() * 100}%</color>.";
        }

        public void SetRegenPercent(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"Stamina regen is increased by <color=cyan>{GetRegenPercent() * 100}%</color>.";
        }

        public float GetRegenPercent() { return m_bonus; }
    }
}
