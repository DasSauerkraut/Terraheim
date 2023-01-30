namespace Terraheim.ArmorEffects;

internal class SE_ThrowingDamageBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Throwing Damage Bonus";
		base.name = "Throwing Damage Bonus";
		m_tooltip = "Throwing Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Throwing Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
