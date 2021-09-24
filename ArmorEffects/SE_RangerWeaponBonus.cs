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
            m_tooltip = $"\n\nDamage with bows, daggers, and spears is increased by <color=cyan>{GetDamageBonus() * 100}%</color>.";
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = $"\n\nDamage with bows, daggers, and spears is increased by <color=cyan>{GetDamageBonus() * 100}%</color>.";
        }

        public float GetDamageBonus() { return m_damageBonus; }
    }
}
