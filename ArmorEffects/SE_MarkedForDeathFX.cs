using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_MarkedForDeathFX : StatusEffect
    {

        public void Awake()
        {
            m_name = "Marked For Death FX";
            base.name = "Marked For Death FX";
            m_tooltip = "";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            m_startEffects = new EffectList();
            m_startEffects.m_effectPrefabs = new EffectList.EffectData[] { UtilityFunctions.VFXMarkedForDeath };
            base.Setup(character);
        }
    }
}
