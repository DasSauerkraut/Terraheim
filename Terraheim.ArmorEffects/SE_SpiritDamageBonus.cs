namespace Terraheim.ArmorEffects;

internal class SE_SpiritDamageBonus : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public void Awake()
	{
		m_name = "Spirit Damage Bonus";
		base.name = "Spirit Damage Bonus";
		m_tooltip = "Spirit Damage Increased by " + m_damageBonus;
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Spirit Damage Increased by " + m_damageBonus;
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}
}
