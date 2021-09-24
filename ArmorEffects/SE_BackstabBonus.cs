
namespace Terraheim.ArmorEffects
{
    class SE_BackstabBonus : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Backstab Bonus";
            base.name = "Backstab Bonus";
            m_tooltip = $"\n\nSneak Attack Damage is increased by <color=cyan>{getBackstabBonus()}x</color>.";
        }

        public void setBackstabBonus(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"\n\nSneak Attack Damage is increased by <color=cyan>{getBackstabBonus()}x</color>.";
        }

        public float getBackstabBonus() { return m_bonus; }
    }
}
