using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_OneHandDamageBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "One Hand Damage Bonus";
            base.name = "One Hand Damage Bonus";
            m_tooltip = $"Damage with one handed weapons is increased by <color=cyan>{getDamageBonus() * 100}%</color> when there is no item in the off hand.";
        }

        public void setDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = $"Damage with one handed weapons is increased by <color=cyan>{getDamageBonus() * 100}%</color> when there is no item in the off hand.";
        }

        public float getDamageBonus() { return m_damageBonus; }
    }
}
