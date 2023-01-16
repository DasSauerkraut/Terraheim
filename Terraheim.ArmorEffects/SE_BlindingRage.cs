using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_BlindingRage : StatusEffect
{
	private bool m_hasTriggered = false;

	private float m_damageMod;

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
		m_name = "Blinding Rage";
		base.name = "Blinding Rage";
		m_tooltip = "Suffer 20% addtional damage from every source";
	}

	public override void Setup(Character character)
	{
		m_damageMod = (float)Terraheim.balance["baneSettings"]!["blindingRage"]!["damageIncrease"];
		m_icon = AssetHelper.SpriteChosenKhorneBane;
		Log.LogWarning("Adding Blinding Rage");
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

	public override void OnDamaged(HitData hit, Character attacker)
	{
		Log.LogInfo($"Damage: {hit.GetTotalDamage()}");
		hit.m_damage.m_blunt += hit.m_damage.m_blunt * m_damageMod;
		hit.m_damage.m_chop += hit.m_damage.m_chop * m_damageMod;
		hit.m_damage.m_damage += hit.m_damage.m_damage * m_damageMod;
		hit.m_damage.m_fire += hit.m_damage.m_fire * m_damageMod;
		hit.m_damage.m_frost += hit.m_damage.m_frost * m_damageMod;
		hit.m_damage.m_lightning += hit.m_damage.m_lightning * m_damageMod;
		hit.m_damage.m_pickaxe += hit.m_damage.m_pickaxe * m_damageMod;
		hit.m_damage.m_pierce += hit.m_damage.m_pierce * m_damageMod;
		hit.m_damage.m_poison += hit.m_damage.m_poison * m_damageMod;
		hit.m_damage.m_slash += hit.m_damage.m_slash * m_damageMod;
		hit.m_damage.m_spirit += hit.m_damage.m_spirit * m_damageMod;
		Log.LogInfo($"Mod Damage: {hit.GetTotalDamage()}");
		base.OnDamaged(hit, attacker);
	}
}
