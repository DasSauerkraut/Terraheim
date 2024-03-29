﻿using Terraheim.Utility;

namespace Terraheim.ArmorEffects
{
    class SE_FullHPDamageBonusFX : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public float m_activationHealth = 0f;

        public void Awake()
        {
            m_name = "Battle FurorFX";
            base.name = "Battle FurorFX";
            m_tooltip = "When above " + m_activationHealth *100 +"% health, all damage is increased by " + m_damageBonus * 100 + "%.";
            m_icon = null;
        }

        public override void UpdateStatusEffect(float dt)
        {
            base.UpdateStatusEffect(dt);
            if (!m_character.GetSEMan().HaveStatusEffect("Battle Furor"))
            {
                m_character.GetSEMan().RemoveStatusEffect("Battle FurorFX");
            }
        }

        public override void Setup(Character character)
        {
            m_startEffects = new EffectList();
            m_startEffects.m_effectPrefabs = new EffectList.EffectData[] { Data.VFXDamageAtFullHp };
            base.Setup(character);
        }
    }
}
