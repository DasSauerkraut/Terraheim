using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_Afterburn : StatusEffect
    {
        private JObject wepBalance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");
        private float m_damageReduction;
        private bool m_hasTriggered = false;

        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "AfterburnFire";
            base.name = "AfterburnFire";
            m_tooltip = "";
            m_damageReduction = (float)wepBalance["ShieldFire"]["effectVal"];
            m_ttl = (int)wepBalance["ShieldFire"]["effectDur"];
        }

        public override void Setup(Character character)
        {
            Log.LogInfo("Afterburn Added");
            m_startEffects = new EffectList
            {
                m_effectPrefabs = new EffectList.EffectData[] { Data.VFXAfterburn }
            };
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_character.GetSEMan().HaveStatusEffect("Afterburn Cooldown"))
                m_character.GetSEMan().RemoveStatusEffect("AfterburnFire", true);
            if (m_time >= TTL - 0.05f && !m_hasTriggered)
            {
                SE_AfterburnCooldown effect = ScriptableObject.CreateInstance<SE_AfterburnCooldown>();
                effect.TTL = (int)wepBalance["ShieldFire"]["effectCooldown"];
                m_character.GetSEMan().AddStatusEffect(effect);
                m_hasTriggered = true;
            }
            base.UpdateStatusEffect(dt);
        }
    }
}
