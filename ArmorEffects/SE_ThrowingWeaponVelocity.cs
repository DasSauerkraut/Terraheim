using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_ThrowingWeaponVelocity : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Throwing Weapon Velocity";
            base.name = "Throwing Weapon Velocity";
            m_tooltip = "Throwing Weapon Velocity Increased by " + m_damageBonus * 10 + "%";
        }

        public void SetVelocityBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = $"\n\nThrowing weapons velocity is increased by <color=cyan>{GetVelocityBonus() * 100}%</color>.";
        }

        public float GetVelocityBonus() { return m_damageBonus; }
    }
}
