using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_AoECounterFX : StatusEffect
{
	public void Awake()
	{
		m_name = "WyrdarrowFX";
		base.name = "WyrdarrowFX";
		m_tooltip = "";
	}

	public override void Setup(Character character)
	{
		m_startEffects = new EffectList();
		m_startEffects.m_effectPrefabs = new EffectList.EffectData[1] { Data.VFXAoECharged };
		base.Setup(character);
	}

	public override void UpdateStatusEffect(float dt)
	{
		base.UpdateStatusEffect(dt);
		if (!m_character.GetSEMan().HaveStatusEffect("Wyrdarrow"))
		{
			m_character.GetSEMan().RemoveStatusEffect("WyrdarrowFX");
		}
	}
}
