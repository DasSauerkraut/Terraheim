using Terraheim.Utility;
using ValheimLib;

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

        public override void Setup(Character character)
        {
            m_startEffects = new EffectList();
            m_startEffects.m_effectPrefabs = new EffectList.EffectData[] { UtilityFunctions.VFXDamageAtFullHp };
            base.Setup(character);
        }
    }
}
