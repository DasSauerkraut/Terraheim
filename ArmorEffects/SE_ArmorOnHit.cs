using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_ArmorOnHit : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public float m_armorGainAmount = 0.02f;
        public float m_currentArmorBonus = 0f;
        public float m_maxArmorBonus = 0f;

        public void Awake()
        {
            m_name = "Brassflesh";
            base.name = "Brassflesh";
            m_tooltip = "";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            TTL = (float)balance["brassfleshTTL"];
            m_currentArmorBonus += m_armorGainAmount;
            m_name = base.name + " " + m_currentArmorBonus * 100 + "%";
            SetIcon();
            Log.LogMessage("Adding Brassflesh");
            base.Setup(character);
        }

        public void OnHit()
        {
            m_time = 0;
            m_currentArmorBonus += m_armorGainAmount;
            Log.LogInfo($"OnHit {m_currentArmorBonus}");
            if (m_currentArmorBonus > m_maxArmorBonus)
                m_currentArmorBonus = m_maxArmorBonus;
            m_name = base.name + " " + m_currentArmorBonus * 100 + "%";
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetBronze").m_itemData.GetIcon();
        }

        public void SetMaxArmor(float bonus)
        {
            m_maxArmorBonus = bonus;
            m_armorGainAmount = m_maxArmorBonus / 10;
            Log.LogInfo("setting max armor to " + bonus);
        }
        public float GetMaxArmor() { return m_maxArmorBonus; }

        public float GetCurrentDamageReduction() { return m_currentArmorBonus; }

    }
}
