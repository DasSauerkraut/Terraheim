using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_ArmorOnHitListener : StatusEffect
    {
        public float m_maxArmor = 0f;
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        public void Awake()
        {
            m_name = "Brassflesh Listener";
            base.name = "Brassflesh Listener";
            m_tooltip = $"Hitting an enemy grants {(m_maxArmor / 10) * 100}% damage reduction for {(int)balance["brassfleshTTL"]} seconds, stacking up to {m_maxArmor * 100}%. Striking an enemy resets the countdown. The damage reduction applies before armor bonuses.";
        }

        public void SetMaxArmor(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_maxArmor = bonus;
            m_tooltip = $"Hitting an enemy grants {(m_maxArmor / 10)*100}% damage reduction for {(int)balance["brassfleshTTL"]} seconds, stacking up to {m_maxArmor * 100}%. Striking an enemy resets the countdown. The damage reduction applies before armor bonuses.";
        }

        public float GetMaxArmor() { return m_maxArmor; }
    }
}
