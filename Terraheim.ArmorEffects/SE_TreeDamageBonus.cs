namespace Terraheim.ArmorEffects;

internal class SE_TreeDamageBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Tree Damage Bonus";
		base.name = "Tree Damage Bonus";
		m_tooltip = "Tree Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Tree Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
