using HarmonyLib;
using System.Threading;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_StaggerCapacity : StatusEffect
    {
        public float m_bonus = 0.05f;

        public void Awake()
        {
            m_name = "Stagger Capacity";
            base.name = "Stagger Capacity";
            m_tooltip = "Stagger Capacity increased by " + m_bonus * 100 + "%";
        }

        public void SetStaggerCap(float bonus) { 
            m_bonus = bonus;
            m_tooltip = $"\n\nStagger Capacity is increased by <color=cyan>{GetStaggerCap() * 100}%</color>";
        }
        
        public float GetStaggerCap() { return m_bonus; }
    }
}
