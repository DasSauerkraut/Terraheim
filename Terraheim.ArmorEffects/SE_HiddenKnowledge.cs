using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_HiddenKnowledge : StatusEffect
{
	private bool m_hasTriggered = false;

	public float m_modifier = 0f;

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
		m_name = "Hidden Knowledge";
		base.name = "Hidden Knowledge";
		m_tooltip = "Skill gain increased by 50%";
	}

	public override void Setup(Character character)
	{
		m_modifier = (float)Terraheim.balance["boonSettings"]!["knowledge"]!["increase"];
		m_icon = AssetHelper.SpriteChosenTzeentchBoon;
		Log.LogWarning("Adding Hidden Knowledge");
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

	public override void ModifyRaiseSkill(Skills.SkillType skill, ref float value)
	{
		value *= 1f + m_modifier;
		base.ModifyRaiseSkill(skill, ref value);
	}
}
