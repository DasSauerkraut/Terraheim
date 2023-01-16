namespace Terraheim.ArmorEffects;

internal class SE_DodgeStamUse : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Dodge Stamina Use";
		base.name = "Dodge Stamina Use";
		m_tooltip = "Dodge Stamina Use decreased by " + m_bonus + "x";
	}

	public void setDodgeStaminaUse(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = "Dodge Stamina Use decreased by " + bonus + "x";
	}

	public float getDodgeStaminaUse()
	{
		return m_bonus;
	}
}
