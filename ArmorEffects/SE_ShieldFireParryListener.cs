using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects
{
    public class SE_ShieldFireParryListener : StatusEffect
    {
        private JObject wepBalance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");

        public void Awake()
        {
            m_name = "ShieldFireParryListener";
            base.name = "ShieldFireParryListener";
            m_tooltip = "";
            //m_damageReduction = (int)wepBalance["ShieldFire"]["effectVal"];
            //m_effectTTL = (int)wepBalance["ShieldFire"]["effectDur"];
            //m_effectCooldownTTL = (int)wepBalance["ShieldFire"]["effectCooldown"];
        }
    }
}
