using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_Bloated : StatusEffect
{
	private bool m_hasTriggered = false;

	public float m_regenAmount = 0f;

	public float TTL
	{
		get
		{
			return m_ttl;
		}
		set
		{
			m_ttl = value;
		}
	}

	public void Awake()
	{
		m_name = "Bloated";
		base.name = "Bloated";
		m_tooltip = "HP regen increased by 1.5x";
	}

	public override void Setup(Character character)
	{
		m_regenAmount = (float)Terraheim.balance["boonSettings"]!["bloated"]!["regen"];
		m_icon = AssetHelper.SpriteChosenNurgleBoon;
		Log.LogWarning("Adding Bloated");
		base.Setup(character);
	}

	public override void UpdateStatusEffect(float dt)
	{
		if (m_time >= TTL - 0.1f && !m_hasTriggered)
		{
			SEMan sEMan = m_character.GetSEMan();
			if (sEMan.HaveStatusEffect("Chosen"))
			{
				(sEMan.GetStatusEffect("Chosen") as SE_Chosen).m_currentBoons.Remove(m_name);
			}
		}
		base.UpdateStatusEffect(dt);
	}

	public void SetTTL(float newTTL)
	{
		TTL = newTTL;
		m_time = 0f;
	}

	public void IncreaseTTL(float increase)
	{
		TTL += increase;
		m_time -= increase;
	}

	public float GetRegen()
	{
		return m_regenAmount;
	}
}
