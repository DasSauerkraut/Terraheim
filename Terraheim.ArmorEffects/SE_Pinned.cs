using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

public class SE_Pinned : StatusEffect
{
	private static JObject balance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");

	public bool hasTriggered = false;

	public float m_cooldownTTL = 0f;

	public HitData.DamageModifiers m_baseMods;

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
		m_name = "Pinned";
		base.name = "Pinned";
		m_tooltip = "";
		m_icon = null;
	}

	public override void UpdateStatusEffect(float dt)
	{
		if (m_character.GetSEMan().HaveStatusEffect("Pinned Cooldown"))
		{
			m_character.GetSEMan().RemoveStatusEffect("Pinned");
		}
		if (m_time >= TTL - 0.05f && !hasTriggered)
		{
			m_character.GetSEMan().AddStatusEffect("Pinned Cooldown", false);
			SE_PinnedCooldown sE_PinnedCooldown = m_character.GetSEMan().GetStatusEffect("Pinned Cooldown") as SE_PinnedCooldown;
			sE_PinnedCooldown.SetPinCooldownTTL(m_cooldownTTL);
			sE_PinnedCooldown.SetBaseMods(m_baseMods);
			hasTriggered = true;
		}
		base.UpdateStatusEffect(dt);
	}

	public override void Setup(Character character)
	{
		TTL = 5f;
		Log.LogMessage("Hit " + character.m_name + " w/ vulnerable damage! Pinned!");
		List<HitData.DamageModPair> list = new List<HitData.DamageModPair>();
		m_baseMods = character.m_damageModifiers;
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Blunt,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Pierce,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Slash,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Fire,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Frost,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Lightning,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Poison,
			m_modifier = HitData.DamageModifier.Weak
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Spirit,
			m_modifier = HitData.DamageModifier.Weak
		});
		character.m_damageModifiers.Apply(list);
		m_startEffects = new EffectList();
		m_startEffects.m_effectPrefabs = new EffectList.EffectData[1] { Data.VFXPinned };
		base.Setup(character);
	}

	public override void ModifySpeed(float baseSpeed, ref float speed)
	{
		speed *= 0.5f;
		base.ModifySpeed(baseSpeed, ref speed);
	}

	public void SetPinTTL(float bonus)
	{
		TTL = bonus;
	}

	public void SetPinCooldownTTL(float bonus)
	{
		m_cooldownTTL = bonus;
	}

	public float GetPinCooldownTTL()
	{
		return m_cooldownTTL;
	}
}
