using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_Bloodlust : StatusEffect
{
	private bool m_hasTriggered = false;

	public float m_damageBonus = 0f;

	public float m_thresholdPercent = 0f;

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
		m_name = "Bloodlust";
		base.name = "Bloodlust";
		m_tooltip = "20% additional damage vs enemies with more than 90% HP";
	}

	public override void Setup(Character character)
	{
		m_damageBonus = (float)Terraheim.balance["boonSettings"]!["bloodlust"]!["damage"];
		m_thresholdPercent = (float)Terraheim.balance["boonSettings"]!["bloodlust"]!["threshold"];
		m_icon = AssetHelper.SpriteChosenKhorneBoon;
		Log.LogWarning("Adding Bloodlust");
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

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}

	public float GetThreshold()
	{
		return m_thresholdPercent;
	}
}
