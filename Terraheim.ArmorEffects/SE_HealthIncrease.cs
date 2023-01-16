namespace Terraheim.ArmorEffects;

public class SE_HealthIncrease : StatusEffect
{
	public float m_healthBonus = 10f;

	public void Awake()
	{
		m_name = "Health Increase";
		base.name = "Health Increase";
		m_tooltip = "Health Increased by " + m_healthBonus;
	}

	public void setHealthBonus(float bonus)
	{
		m_healthBonus = bonus;
		m_tooltip = "Health Increased by " + bonus;
	}

	public float getHealthBonus()
	{
		return m_healthBonus;
	}
}
