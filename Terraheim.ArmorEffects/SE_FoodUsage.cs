namespace Terraheim.ArmorEffects;

internal class SE_FoodUsage : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Food Usage";
		base.name = "Food Usage";
		m_tooltip = "Food Usage decreased by " + m_bonus * 100f + "%";
	}

	public void SetFoodUsage(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = "Food Usage decreased by " + m_bonus * 100f + "%";
	}

	public float GetFoodUsage()
	{
		return m_bonus;
	}
}
