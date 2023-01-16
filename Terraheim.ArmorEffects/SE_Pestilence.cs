using System;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects;

internal class SE_Pestilence : StatusEffect
{
	private bool m_hasTriggered = false;

	private float m_damagePercent;

	private float m_percentActivate;

	private int m_checkInterval;

	private float m_checkClock = 0f;

	private System.Random m_dice = new System.Random();

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
		m_name = "Pestilence";
		base.name = "Pestilence";
		m_tooltip = "70% chance to suffer 14% of max hp as poison damage every 15 seconds";
	}

	public override void Setup(Character character)
	{
		m_damagePercent = (float)Terraheim.balance["baneSettings"]!["pestilence"]!["damagePercent"];
		m_percentActivate = (float)Terraheim.balance["baneSettings"]!["pestilence"]!["chance"];
		m_checkInterval = (int)Terraheim.balance["baneSettings"]!["pestilence"]!["timeToCheck"];
		m_icon = AssetHelper.SpriteChosenNurgleBane;
		Log.LogWarning("Adding Pestilence");
		base.Setup(character);
	}

	public override void UpdateStatusEffect(float dt)
	{
		if (m_time >= TTL - 0.1f && !m_hasTriggered)
		{
			SEMan sEMan = m_character.GetSEMan();
			if (sEMan.HaveStatusEffect("Chosen"))
			{
				(sEMan.GetStatusEffect("Chosen") as SE_Chosen).m_currentBanes.Remove(m_name);
			}
		}
		if (m_time >= m_checkClock)
		{
			m_checkClock += m_checkInterval;
			int num = m_dice.Next(1, 101);
			if ((float)num <= m_percentActivate * 100f)
			{
				AssetHelper.PestilenceExplosion.GetComponent<Aoe>().m_damage.m_poison = m_character.GetMaxHealth() * m_damagePercent;
				UnityEngine.Object.Instantiate(AssetHelper.PestilenceExplosion, m_character.GetCenterPoint(), Quaternion.identity);
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
}
