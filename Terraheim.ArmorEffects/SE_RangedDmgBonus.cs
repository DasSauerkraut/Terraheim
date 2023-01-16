namespace Terraheim.ArmorEffects;

internal class SE_RangedDmgBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Ranged Damage Bonus";
		base.name = "Ranged Damage Bonus";
		m_tooltip = "Ranged Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Ranged Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
