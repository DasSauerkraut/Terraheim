using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_ThrowingWeaponVelocity : StatusEffect
    {
        public static float m_damageBonus = 0.05f;

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
            m_tooltip = "Throwing Weapon Velocity Increased by " + bonus * 10 + "%";
        }

        public float GetVelocityBonus() { return m_damageBonus; }
    }
}
