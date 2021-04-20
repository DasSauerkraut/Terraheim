using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_AoECounterExhausted : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Wyrd Exhausted";
            base.name = "Wyrd Exhausted";
            m_tooltip = "";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            SetIcon();
            TTL = (float)balance["wyrdExhaustedTTL"];
            Log.LogWarning("Adding Exhaustion");
            base.Setup(character);
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
        }
    }
}
