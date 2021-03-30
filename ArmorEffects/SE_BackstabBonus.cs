
namespace Terraheim.ArmorEffects
{
    class SE_BackstabBonus : StatusEffect
    {
        public static float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Backstab Bonus";
            base.name = "Backstab Bonus";
            m_tooltip = "Backstab Bonus Increased by " + m_bonus + "x";
        }

        public void setBackstabBonus(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Backstab Bonus Increased by " + bonus + "x";
        }

        public float getBackstabBonus() { return m_bonus; }
    }
}
