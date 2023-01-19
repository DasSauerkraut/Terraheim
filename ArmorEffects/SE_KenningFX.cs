using Terraheim.Utility;
using UnityEngine;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.ArmorEffects
{
    class SE_KenningFX : StatusEffect
    {

        private float m_damageReduction; 
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "KenningFX";
            base.name = "KenningFX";
            m_tooltip = "";
        }

        public override void Setup(Character character)
        {
            m_startEffects = new EffectList();
            m_startEffects.m_effectPrefabs = new EffectList.EffectData[] { Data.VFXKenning };
            if (Terraheim.hasJudesEquipment)
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>(Data.ArmorSets["wanderer"].HelmetID).m_itemData.GetIcon();
            else
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_time >= TTL - 0.1f || ZInput.GetButtonDown("Hide"))
            {
                //Object.Instantiate(AssetHelper.KenningActivate, m_character.GetCenterPoint(), Quaternion.identity);
                m_character.GetSEMan().RemoveStatusEffect("KenningFX");
            }
            base.UpdateStatusEffect(dt);
            if (!m_character.GetSEMan().HaveStatusEffect("Kenning"))
            {
                m_character.GetSEMan().RemoveStatusEffect("KenningFX");
            }
        }

        public void SetTTL(float newTTL)
        {
            TTL = newTTL;
            m_time = 0;
        }

        public void SetDamageReduction(float bonus) { m_damageReduction = bonus; }

        public override void OnDamaged(HitData hit, Character attacker)
        {
            hit.m_damage.m_blunt *= 1 - m_damageReduction;
            hit.m_damage.m_chop *= 1 - m_damageReduction;
            hit.m_damage.m_damage *= 1 - m_damageReduction;
            hit.m_damage.m_fire *= 1 - m_damageReduction;
            hit.m_damage.m_frost *= 1 - m_damageReduction;
            hit.m_damage.m_lightning *= 1 - m_damageReduction;
            hit.m_damage.m_pickaxe *= 1 - m_damageReduction;
            hit.m_damage.m_pierce *= 1 - m_damageReduction;
            hit.m_damage.m_poison *= 1 - m_damageReduction;
            hit.m_damage.m_slash *= 1 - m_damageReduction;
            hit.m_damage.m_spirit *= 1 - m_damageReduction;
            base.OnDamaged(hit, attacker);
        }
    }
}
