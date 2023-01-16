using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_Withdrawals : StatusEffect
{
	private bool m_hasTriggered = false;

	private float m_foodEffect;

	private float m_meadEffect;

	private float m_positiveMeadEffect;

	private const int m_meadbaseTTL = 120;

	private const int m_positiveMeadTTL = 600;

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
		m_name = "Withdrawals";
		base.name = "Withdrawals";
		m_tooltip = "Mead cooldowns last 2x longer, Food and other Meads last half as long";
	}

	public override void Setup(Character character)
	{
		m_foodEffect = (float)Terraheim.balance["baneSettings"]!["withdrawals"]!["foodDegradeRate"];
		m_meadEffect = (float)Terraheim.balance["baneSettings"]!["withdrawals"]!["meadCooldown"];
		m_positiveMeadEffect = (float)Terraheim.balance["baneSettings"]!["withdrawals"]!["meadDuration"];
		m_icon = AssetHelper.SpriteChosenSlaaneshBane;
		Log.LogWarning("Adding Withdrawals");
		base.Setup(character);
	}

	public override void UpdateStatusEffect(float dt)
	{
		SEMan sEMan = m_character.GetSEMan();
		foreach (StatusEffect statusEffect in m_character.GetSEMan().GetStatusEffects())
		{
			if (statusEffect.m_name.Contains("mead_hp") || statusEffect.m_name.Contains("mead_stamina"))
			{
				if (statusEffect.m_ttl == 120f)
				{
					statusEffect.m_time -= m_ttl * m_meadEffect;
					if (statusEffect.m_time < 0f)
					{
						statusEffect.m_time = 0f;
					}
					statusEffect.m_ttl *= m_meadEffect;
				}
			}
			else if ((statusEffect.m_name.Contains("barleywine") || statusEffect.m_name.Contains("mead_frostres") || statusEffect.m_name.Contains("mead_poisonres")) && statusEffect.m_ttl == 600f)
			{
				statusEffect.m_ttl *= m_positiveMeadEffect;
			}
		}
		if ((sEMan.HaveStatusEffect("$item_mead_stamina_medium") || sEMan.HaveStatusEffect("$item_mead_stamina_minor") || sEMan.HaveStatusEffect("$item_mead_hp_minor") || sEMan.HaveStatusEffect("$item_mead_hp_medium")) && m_time >= TTL - 0.1f && !m_hasTriggered && sEMan.HaveStatusEffect("Chosen"))
		{
			(sEMan.GetStatusEffect("Chosen") as SE_Chosen).m_currentBanes.Remove(m_name);
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

	public float GetMeadEffect()
	{
		return m_meadEffect;
	}

	public float GetFoodEffect()
	{
		return m_foodEffect;
	}
}
