using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_RangerWeaponBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;

        public void Awake()
        {
            m_name = "Ranger Weapon Bonus";
            base.name = "Ranger Weapon Bonus";
            m_tooltip = "Ranger Weapon Increased by " + m_damageBonus * 10 + "%";
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "Ranger Weapon Increased by " + bonus * 10 + "%";
        }

        public float GetDamageBonus() { return m_damageBonus; }
    }
}
