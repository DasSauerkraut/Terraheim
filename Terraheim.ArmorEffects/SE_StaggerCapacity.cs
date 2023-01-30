namespace Terraheim.ArmorEffects;

internal class SE_StaggerCapacity : StatusEffect
{
	public float m_bonus = 0.05f;

	public void Awake()
	{
		m_name = "Stagger Capacity";
		base.name = "Stagger Capacity";
		m_tooltip = "Stagger Capacity increased by " + m_bonus * 100f + "%";
	}

	public void SetStaggerCap(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = "Stagger Capacity increased by " + m_bonus * 100f + "%";
	}

	public float GetStaggerCap()
	{
		return m_bonus;
	}
}
