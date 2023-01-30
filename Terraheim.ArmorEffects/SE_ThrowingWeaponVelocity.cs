namespace Terraheim.ArmorEffects;

internal class SE_ThrowingWeaponVelocity : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Throwing Weapon Velocity";
		base.name = "Throwing Weapon Velocity";
		m_tooltip = "Throwing Weapon Velocity Increased by " + m_damageBonus * 10f + "%";
	}

	public void SetVelocityBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Throwing Weapon Velocity Increased by " + bonus * 10f + "%";
	}

	public float GetVelocityBonus()
	{
		return m_damageBonus;
	}
}
