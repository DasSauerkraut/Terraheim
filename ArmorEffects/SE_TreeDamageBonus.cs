﻿namespace Terraheim.ArmorEffects
{
    class SE_TreeDamageBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;

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
            m_tooltip = $"Damage against trees is increased by <color=yellow>{getDamageBonus() * 100}%</color>.";
        }

        public float getDamageBonus() { return m_damageBonus; }
    }
}
