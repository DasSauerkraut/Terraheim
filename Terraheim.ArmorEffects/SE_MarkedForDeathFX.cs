using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_MarkedForDeathFX : StatusEffect
{
	private static JObject balance = Terraheim.balance;

	public void Awake()
	{
		m_name = "Marked For Death FX";
		base.name = "Marked For Death FX";
		m_tooltip = "";
		m_icon = null;
	}

	public override void Setup(Character character)
	{
		if ((bool)balance["enableMarkedForDeathFX"])
		{
			m_startEffects = new EffectList();
			m_startEffects.m_effectPrefabs = new EffectList.EffectData[1] { Data.VFXMarkedForDeath };
		}
		base.Setup(character);
	}
}
