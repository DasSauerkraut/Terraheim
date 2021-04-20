using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_MoveSpeedOnKill : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public float m_speedBonusAmount = 0.10f;
        public float m_currentSpeedBonus = 0f;
        public float m_maxSpeedBonus = 0f;

        public void Awake()
        {
            m_name = "Bloodrush";
            base.name = "Bloodrush";
            m_tooltip = "";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            TTL = (float)balance["bloodrushTTL"];
            SetIcon();
            m_maxSpeedBonus = (float)balance["bloodrushMaxSpeed"];
            m_currentSpeedBonus = m_speedBonusAmount;
            Log.LogWarning("Adding Bloodrush");
            m_name = base.name + " " + (m_currentSpeedBonus * 100) + "%";
            base.Setup(character);
        }

        public void OnKill()
        {
            m_time = 0;
            m_currentSpeedBonus += m_speedBonusAmount;
            if (m_currentSpeedBonus > m_maxSpeedBonus)
                m_currentSpeedBonus = m_maxSpeedBonus;
            m_name = base.name + " " + (m_currentSpeedBonus * 100) + "%";
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("ArmorBarbarianCapeJD").m_itemData.GetIcon();
        }

        public void SetSpeedBonus(float bonus) 
        { 
            m_speedBonusAmount = bonus;
            if (m_currentSpeedBonus < m_speedBonusAmount)
                m_currentSpeedBonus = m_speedBonusAmount;
        }
        public float GetSpeedBonus() { return m_speedBonusAmount; }

        public float GetCurrentSpeedBonus() { return m_currentSpeedBonus; }

    }
}
