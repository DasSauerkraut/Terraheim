using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_MoveSpeedOnKillListener : StatusEffect
    {
        public float m_speedBonus = 0.05f;

        public void Awake()
        {
            m_name = "Bloodrush Listener";
            base.name = "Bloodrush Listener";
            m_tooltip = "Bloodrush " + m_speedBonus * 10 + "%";
        }

        public void SetSpeedBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_speedBonus = bonus;
            m_tooltip = "Bloodrush " + bonus * 10 + "%";
        }

        public float GetSpeedBonus() { return m_speedBonus; }
    }
}
