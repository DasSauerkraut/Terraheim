namespace Terraheim.ArmorEffects;

internal class SE_MeleeDamageBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Melee Damage Bonus";
		base.name = "Melee Damage Bonus";
		m_tooltip = "Melee Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Melee Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
