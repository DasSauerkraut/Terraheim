using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_TwoHandedDmgBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Two Handed Damage Bonus";
            base.name = "Two Handed Damage Bonus";
            m_tooltip = $"Two-Handed weapons damage is increased by <color=cyan>{getDamageBonus() * 100}%</color>.";
        }

        public void setDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = $"Two-Handed weapons damage is increased by <color=cyan>{getDamageBonus() * 100}%</color>.";
        }

        public float getDamageBonus() { return m_damageBonus; }
    }
}
