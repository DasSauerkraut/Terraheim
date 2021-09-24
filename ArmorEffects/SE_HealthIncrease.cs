using HarmonyLib;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    public class SE_HealthIncrease : StatusEffect
    {
        public float m_healthBonus = 10f;

        public void Awake()
        {
            m_name = "Health Increase";
            base.name = "Health Increase";
            m_tooltip = $"Max HP is increased by <color=cyan>{getHealthBonus() * 100}%</color>.";
        }
               
        public void setHealthBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus);
            m_healthBonus = bonus;
            m_tooltip = $"Max HP is increased by <color=cyan>{getHealthBonus() * 100}%</color>.";
        }

        public float getHealthBonus() { return m_healthBonus; }
    }
}
