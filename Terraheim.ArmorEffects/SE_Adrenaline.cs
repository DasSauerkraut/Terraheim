using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

public class SE_Adrenaline : StatusEffect
{
	private bool m_hasTriggered = false;

	private float m_moveSpeed = 0f;

	private float m_attackSpeed = 0f;

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
		m_name = "Adrenaline";
		base.name = "Adrenaline";
		m_tooltip = "Move speed increased by 15%, attack speed increased by 5%";
	}

	public override void Setup(Character character)
	{
		m_attackSpeed = (float)Terraheim.balance["boonSettings"]!["adrenaline"]!["attackspeed"];
		m_moveSpeed = (float)Terraheim.balance["boonSettings"]!["adrenaline"]!["movespeed"];
		m_icon = AssetHelper.SpriteChosenSlaaneshBoon;
		Log.LogWarning("Adding Adrenaline");
		base.Setup(character);
	}

	public override void UpdateStatusEffect(float dt)
	{
		if (m_time >= TTL - 0.1f && !m_hasTriggered)
		{
			SEMan sEMan = m_character.GetSEMan();
			if (sEMan.HaveStatusEffect("Chosen"))
			{
				(UtilityFunctions.GetStatusEffectFromName("Chosen", sEMan) as SE_Chosen).m_currentBoons.Remove(m_name);
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

	public float GetAttackSpeed()
	{
		return m_attackSpeed;
	}

	public override void ModifySpeed(float baseSpeed, ref float speed)
	{
		speed *= 1f + m_moveSpeed;
		base.ModifySpeed(baseSpeed, ref speed);
	}
}
