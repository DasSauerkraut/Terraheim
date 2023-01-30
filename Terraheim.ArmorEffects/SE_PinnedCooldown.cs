using System.Collections.Generic;

namespace Terraheim.ArmorEffects;

internal class SE_PinnedCooldown : StatusEffect
{
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
		m_name = "Pinned Cooldown";
		name = "Pinned Cooldown";
		m_tooltip = "";
		m_icon = null;
	}

	public override void Setup(Character character)
	{
		TTL = 5f;
		Log.LogMessage("Cooldown started " + character.m_name + "!");
		base.Setup(character);
	}

	public void SetPinCooldownTTL(float bonus)
	{
		TTL = bonus;
	}

	public void SetBaseMods(HitData.DamageModifiers baseMods)
	{
		List<HitData.DamageModPair> list = new List<HitData.DamageModPair>();
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Blunt,
			m_modifier = baseMods.m_blunt
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Pierce,
			m_modifier = baseMods.m_pierce
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Slash,
			m_modifier = baseMods.m_slash
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Fire,
			m_modifier = baseMods.m_fire
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Frost,
			m_modifier = baseMods.m_frost
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Lightning,
			m_modifier = baseMods.m_lightning
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Poison,
			m_modifier = baseMods.m_poison
		});
		list.Add(new HitData.DamageModPair
		{
			m_type = HitData.DamageType.Spirit,
			m_modifier = baseMods.m_spirit
		});
		m_character.m_damageModifiers.Apply(list);
	}
}
