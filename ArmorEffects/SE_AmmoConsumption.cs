using HarmonyLib;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_AmmoConsumption : StatusEffect
    {
        public int m_reduction = 0;

        public void Awake()
        {
            m_name = "Ammo Consumption";
            base.name = "Ammo Consumption";
            m_tooltip = "Ammo Consumption Reduced by " + m_reduction * 10 + "%";
        }

        public void setAmmoConsumption(int reduction)
        {
            //Log.LogInfo("Setting Reduction: " + reduction * 10 + "%");
            m_reduction = reduction;
            m_tooltip = "Ammo Consumption Reduced by " + m_reduction * 10 + "%";
        }

        public float getAmmoConsumption() { return m_reduction; }
    }
}
