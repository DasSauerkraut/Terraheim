
namespace Terraheim.ArmorEffects
{
    class SE_SneakMovement : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Sneak Movement";
            base.name = "Sneak Movement";
            m_tooltip = $"Move <color=cyan>{GetBonus() * 100}%</color> faster while using <color=cyan>{GetBonus() * 100}%</color> less stamina while sneaking.";
        }

        public void SetBonus(float bonus) { 
            m_bonus = bonus;
            m_tooltip = $"Move <color=cyan>{GetBonus() * 100}%</color> faster while using <color=cyan>{GetBonus() * 100}%</color> less stamina while sneaking.";
        }
        public float GetBonus() { return m_bonus; }

    }
}
