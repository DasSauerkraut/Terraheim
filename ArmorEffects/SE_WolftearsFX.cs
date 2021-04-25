using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_WolftearsFX : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public float m_activationHealth = 0f;

        public void Awake()
        {
            m_name = "WolftearsFX";
            base.name = "WolftearsFX";
            m_tooltip = "Gain +2.5% damage for every 10% of HP missing, up to " + m_damageBonus * 100 + "% when below " + m_activationHealth + "% HP.";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            m_startEffects = new EffectList();
            m_startEffects.m_effectPrefabs = new EffectList.EffectData[] { UtilityFunctions.VFXRedTearstone };
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            base.UpdateStatusEffect(dt);
            if (!m_character.GetSEMan().HaveStatusEffect("Wolftears"))
            {
                m_character.GetSEMan().RemoveStatusEffect("WolftearsFX");
            }
        }
    }
}
