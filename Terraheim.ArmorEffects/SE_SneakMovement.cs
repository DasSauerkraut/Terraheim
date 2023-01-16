namespace Terraheim.ArmorEffects;

internal class SE_SneakMovement : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Sneak Movement";
		base.name = "Sneak Movement";
		m_tooltip = "Move faster and use less stamina while sneaking.";
	}

	public void SetBonus(float bonus)
	{
		m_bonus = bonus;
	}

	public float GetBonus()
	{
		return m_bonus;
	}
}
