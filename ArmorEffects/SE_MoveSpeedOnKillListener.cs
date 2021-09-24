using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_MoveSpeedOnKillListener : StatusEffect
    {
        public float m_speedBonus = 0.05f;
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

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
            m_tooltip = $"\n\nAfter killing an enemy, gain a stacking <color=cyan>+{GetSpeedBonus() * 100}%</color> movement speed bonus for {(float)balance["bloodrushTTL"]}" +
                        $" seconds. Killing an enemy resets the countdown.";
        }

        public float GetSpeedBonus() { return m_speedBonus; }
    }
}
