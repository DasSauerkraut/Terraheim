namespace Terraheim.ArmorEffects;

internal class SE_RestoreResources : StatusEffect
{
	public float m_chance = 0f;

	public float m_stamina = 0f;

	public void Awake()
	{
		m_name = "Restore Resources";
		base.name = "Restore Resources";
		m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
	}

	public void SetChance(float bonus)
	{
		m_chance = bonus * 100f;
		m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
	}

	public float GetChance()
	{
		return m_chance;
	}

	public void SetStaminaAmount(float bonus)
	{
		m_stamina = bonus;
		m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
	}

	public float GetStaminaAmount()
	{
		return m_stamina;
	}
}
