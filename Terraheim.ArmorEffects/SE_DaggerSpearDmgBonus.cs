namespace Terraheim.ArmorEffects;

internal class SE_DaggerSpearDmgBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Dagger/Spear Damage Bonus";
		base.name = "Dagger/Spear Damage Bonus";
		m_tooltip = "Dagger/Spear Damage Increased by " + m_damageBonus * 10f + "%";
	}

	public void setDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Dagger/Spear Damage Increased by " + bonus * 10f + "%";
	}

	public float getDamageBonus()
	{
		return m_damageBonus;
	}
}
