using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.ArmorEffects
{
    class SE_ChallengeMoveSpeed : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        public void Awake()
        {
            m_name = "Challenge Move Speed";
            base.name = "Challenge Move Speed";
            m_tooltip = "";
            m_icon = null;
        }
    }
}
