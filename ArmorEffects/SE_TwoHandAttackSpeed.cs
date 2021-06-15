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
        }

        public float GetSpeed() { return m_bonus; }
    }
}
