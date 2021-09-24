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
            m_tooltip = $"<color=cyan>{getAmmoConsumption()}%</color> chance to not consume ammo.";
        }

        public void setAmmoConsumption(int reduction)
        {
            //Log.LogInfo("Setting Reduction: " + reduction * 10 + "%");
            m_reduction = reduction;
            m_tooltip = $"<color=cyan>{getAmmoConsumption()}%</color> chance to not consume ammo.";
        }

        public float getAmmoConsumption() { return m_reduction; }
    }
}
