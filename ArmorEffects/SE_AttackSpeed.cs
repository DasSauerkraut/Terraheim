namespace Terraheim.ArmorEffects
{
    public class SE_AttackSpeed : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Attack Speed";
            base.name = "Attack Speed";

        }

        public void SetSpeed(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"\n\nWeapon attack speed is increased by <color=cyan>{GetSpeed() * 100}%</color>.";
        }

        public float GetSpeed() { return m_bonus; }
    }
}
