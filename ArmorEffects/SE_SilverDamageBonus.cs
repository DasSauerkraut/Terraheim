using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_SilverDamageBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Silver Damage Bonus";
            base.name = "Silver Damage Bonus";
            m_tooltip = $"\n\nBows, daggers, and spears gain <color=cyan>{GetDamageBonus() * 100}%</color> damage as spirit and frost damage.";
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = $"\n\nBows, daggers, and spears gain <color=cyan>{GetDamageBonus() * 100}%</color> damage as spirit and frost damage.";
        }

        public float GetDamageBonus() { return m_damageBonus; }
    }
}
