namespace Terraheim.ArmorEffects
{
    class SE_TreeDamageBonus : StatusEffect
    {
        public static float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Tree Damage Bonus";
            base.name = "Tree Damage Bonus";
            m_tooltip = "Tree Damage Increased by " + m_damageBonus * 10 + "%";
        }

        public void setDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Tree Damage Increased by " + bonus * 10 + "%";
        }

        public float getDamageBonus() { return m_damageBonus; }
    }
}
