using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_WolftearsProtectionExhausted : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Tear Protection Exhausted";
            base.name = "Tear Protection Exhausted";
            m_tooltip = "";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            SetIcon();
            TTL = (float)balance["wolftearOneHitTTL"];
            Log.LogInfo("Adding Wolftear Exhaustion");
            base.Setup(character);
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetIron").m_itemData.GetIcon();
        }
    }
}
