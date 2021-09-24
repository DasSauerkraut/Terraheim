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
        float m_speed;
        public void Awake()
        {
            m_name = "Challenge Move Speed";
            base.name = "Challenge Move Speed";
            m_tooltip = "";
            m_icon = null;
        }

        public void SetMoveSpeed(float bonus)
        {
            m_speed = bonus;
            m_tooltip = $"\n\nMovement speed is increased by <color=cyan>{m_speed * 100}%</color>.\n" +
                            $"Suffer <color=red>100%</color> more damage.";
        }
    }
}
