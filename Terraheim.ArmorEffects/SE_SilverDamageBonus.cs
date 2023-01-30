namespace Terraheim.ArmorEffects;

internal class SE_SilverDamageBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Silver Damage Bonus";
		base.name = "Silver Damage Bonus";
		m_tooltip = "Silver Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Silver Damage Increased by " + bonus * 10f + "%";
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}
}
