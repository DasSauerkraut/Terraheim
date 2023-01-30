namespace Terraheim.ArmorEffects;

internal class SE_ParryBonus : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Parry Bonus Increase";
		base.name = "Parry Bonus Increase";
		m_tooltip = "Parry Bonus increased by " + m_bonus * 100f + "%";
	}

	public void SetParryBonus(float bonus)
	{
		m_bonus = bonus;
	}

	public float GetParryBonus()
	{
		return m_bonus;
	}
}
