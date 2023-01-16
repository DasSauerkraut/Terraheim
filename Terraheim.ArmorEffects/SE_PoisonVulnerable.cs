namespace Terraheim.ArmorEffects;

internal class SE_PoisonVulnerable : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Poison Vulnerable";
		base.name = "Poison Vulnerable";
		m_tooltip = "Poison Vulnerable " + m_damageBonus * 10f + "%";
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Poison Vulnerable " + bonus * 10f + "%";
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}
}
