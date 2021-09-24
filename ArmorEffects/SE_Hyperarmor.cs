using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_Hyperarmor : StatusEffect
    {
        public float m_armor = 0f;

        public void Awake()
        {
            m_name = "Hyperarmor";
            base.name = "Hyperarmor";

        }

        public void SetArmor(float bonus)
        {
            m_armor = bonus;
            m_tooltip = $"\n\nWhen hit during an attack, damage suffered is reduced by <color=cyan>{GetArmor() * 100}%</color>, Chosen Banes will not have their TTL increased and you will suffer no knockback or stagger.";
        }

        public float GetArmor() { return m_armor; }
    }
}
