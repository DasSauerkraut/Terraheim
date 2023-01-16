namespace Terraheim.ArmorEffects;

internal class SE_RangerWeaponBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Ranger Weapon Bonus";
		base.name = "Ranger Weapon Bonus";
		m_tooltip = "Ranger Weapon Increased by " + m_damageBonus * 10f + "%";
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Ranger Weapon Increased by " + bonus * 10f + "%";
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}
}
