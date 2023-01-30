namespace Terraheim.ArmorEffects;

internal class SE_MiningBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Mining Bonus";
		base.name = "Mining Bonus";
		m_tooltip = "Mining Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Mining Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
