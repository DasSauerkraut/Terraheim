using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_SpiritDamageBonus : StatusEffect
    {
        public static float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Spirit Damage Bonus";
            base.name = "Spirit Damage Bonus";
            m_tooltip = "Spirit Damage Increased by " + m_damageBonus;
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Spirit Damage Increased by " + m_damageBonus;
        }

        public float GetDamageBonus() { return m_damageBonus; }
    }
}
