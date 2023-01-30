using Jotunn.Managers;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_Retaliation : StatusEffect
{
	public float m_absorb;

	public float m_stored = 0f;

	public void Awake()
	{
		m_name = "Retaliation";
		base.name = "Retaliation";
		m_tooltip = $"Store {m_absorb * 100f}% of damage taken while blocking. When staggered, release the stored damage, harming all within 2m. Stored damage dissapates over time.";
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetCarapace").m_itemData.GetIcon();
	}

	public void SetAbsorb(float bonus)
	{
		m_absorb = bonus;
		m_tooltip = $"Store {m_absorb * 100f}% of damage taken while blocking. When staggered, release the stored damage, harming all within 2m. Stored damage dissapates over time.";
	}

	public float GetAbsorb()
	{
		return m_absorb;
	}

	public float GetStored()
	{
		return m_stored;
	}

	public void ResetStored()
	{
		m_stored = 0f;
	}

	public override void OnDamaged(HitData hit, Character attacker)
	{
		if (m_character.IsBlocking() && !m_character.GetSEMan().HaveStatusEffect("Retaliation Cooldown"))
		{
			m_stored += hit.GetTotalBlockableDamage() * m_absorb;
			m_name = base.name + $"\n{m_stored:#.##}";
		}
		base.OnDamaged(hit, attacker);
	}

	public override void UpdateStatusEffect(float dt)
	{
		if (m_stored > 0f)
		{
			m_stored -= 0.005f;
			m_name = base.name + $"\n{m_stored:#.##}";
		}
		if (m_stored < 0f || m_character.GetSEMan().HaveStatusEffect("Retaliation Cooldown"))
		{
			m_stored = 0f;
			m_name = base.name;
		}
		base.UpdateStatusEffect(dt);
	}
}
