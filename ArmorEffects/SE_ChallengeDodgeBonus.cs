using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.ArmorEffects
{
    class SE_ChallengeDodgeBonus : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        public float m_bonus = 0f;
        public void Awake()
        {
            m_name = "Challenge Dodge Bonus";
            base.name = "Challenge Dodge Bonus";
            m_tooltip = "";
            m_icon = null;
        }

        public void SetDodgeBonus(float bonus) { m_bonus = bonus; }
        public float GetDodgeBonus() { return m_bonus; }
    }
}
