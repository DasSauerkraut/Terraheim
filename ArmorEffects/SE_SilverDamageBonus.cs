using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_SilverDamageBonus : StatusEffect
    {
        public static float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Silver Damage Bonus";
            base.name = "Silver Damage Bonus";
            m_tooltip = "Silver Damage Increased by " + m_damageBonus * 10 + "%";
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Silver Damage Increased by " + bonus * 10 + "%";
        }

        public float GetDamageBonus() { return m_damageBonus; }
    }
}
