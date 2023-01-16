namespace Terraheim.ArmorEffects;

internal class SE_TwoHandedDmgBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Two Handed Damage Bonus";
		base.name = "Two Handed Damage Bonus";
		m_tooltip = "Two Handed Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Two Handed Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
