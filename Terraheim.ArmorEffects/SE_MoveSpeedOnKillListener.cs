namespace Terraheim.ArmorEffects;

internal class SE_MoveSpeedOnKillListener : StatusEffect
{
	public float m_speedBonus = 0.05f;

	public void Awake()
	{
		m_name = "Bloodrush Listener";
		base.name = "Bloodrush Listener";
		m_tooltip = "Bloodrush " + m_speedBonus * 10f + "%";
	}

	public void SetSpeedBonus(float bonus)
	{
		m_speedBonus = bonus;
		m_tooltip = "Bloodrush " + bonus * 10f + "%";
	}

	public float GetSpeedBonus()
	{
		return m_speedBonus;
	}
}
