using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    public class SE_TwoHandAttackSpeed : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Two Hand Attack Speed";
            base.name = "Two Hand Attack Speed";

        }

        public void SetSpeed(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = $"\n\nAttack speed for Two-Handed weapons is increased by <color=cyan>{GetSpeed() * 100}%</color>.";
        }

        public float GetSpeed() { return m_bonus; }
    }
}
