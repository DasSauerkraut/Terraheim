using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_AxeDamageBonus : StatusEffect
    {
        public static float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Axe Damage Bonus";
            base.name = "Axe Damage Bonus";
            m_tooltip = "Axe Damage Increased by " + m_damageBonus * 10 + "%";
        }

        public void setDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Axe Damage Increased by " + bonus * 10 + "%";
        }

        public float getDamageBonus() { return m_damageBonus; }
    }
}
