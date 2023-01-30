namespace Terraheim.ArmorEffects;

internal class SE_BlockStamUse : StatusEffect
{
	public float m_bonus = 0f;

	public float m_baseConsumption = 0f;

	public void Awake()
	{
		m_name = "Block Stamina Use";
		base.name = "Block Stamina Use";
		m_tooltip = "Block Stamina Use decreased by " + m_bonus + "x";
	}

	public void setBlockStaminaUse(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = "Block Stamina Use decreased by " + bonus + "x";
	}

	public float getBlockStaminaUse()
	{
		return m_bonus;
	}
}
