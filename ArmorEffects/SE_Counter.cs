using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_Counter : StatusEffect
    {
        public float m_armor = 0f;

        public void Awake()
        {
            m_name = "Counter";
            base.name = "Counter";
        }

        public void SetBonus(float bonus)
        {
            m_armor = bonus;
            m_tooltip = $"\n\nWhen you hit an enemy that is mid-attack, deal <color=cyan>{GetBonus() * 100}%</color> more damage.";
        }

        public float GetBonus() { return m_armor; }
    }
}
