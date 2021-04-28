namespace Terraheim.ArmorEffects
{
    class SE_MiningBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Mining Bonus";
            base.name = "Mining Bonus";
            m_tooltip = "Mining Damage Increased by " + m_damageBonus * 10 + "%";
        }

        public void setDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Mining Damage Increased by " + bonus * 10 + "%";
        }

        public float getDamageBonus() { return m_damageBonus; }
    }
}
