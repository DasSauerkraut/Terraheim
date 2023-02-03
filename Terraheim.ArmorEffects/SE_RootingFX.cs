using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_RootingFX : StatusEffect
{
	public void Awake()
	{
		m_name = "RootingFX";
		base.name = "RootingFX";
		m_tooltip = "";
	}

	public override void Setup(Character character)
	{
		m_startEffects = new EffectList();
		m_startEffects.m_effectPrefabs = new EffectList.EffectData[1] { Data.VFXRooting };
		base.Setup(character);
	}

	public override void UpdateStatusEffect(float dt)
	{
		base.UpdateStatusEffect(dt);
		if (!m_character.GetSEMan().HaveStatusEffect("Rooting"))
		{
			m_character.GetSEMan().RemoveStatusEffect("RootingFX");
		}
	}
}
