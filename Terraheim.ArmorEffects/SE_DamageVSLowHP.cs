namespace Terraheim.ArmorEffects;

internal class SE_DamageVSLowHP : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public float m_healthThreshold = 0f;

	public void Awake()
	{
		m_name = "Damage Vs Low HP";
		base.name = "Damage Vs Low HP";
		m_tooltip = "Damage Vs Low HP Increased by " + m_damageBonus * 10f + "%";
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Damage Vs Low HP Increased by " + bonus * 10f + "%";
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}

	public void SetHealthThreshold(float bonus)
	{
		m_healthThreshold = bonus;
	}

	public float GetHealthThreshold()
	{
		return m_healthThreshold;
	}
}
