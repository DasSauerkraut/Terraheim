namespace Terraheim.ArmorEffects;

public class SE_AttackSpeed : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Attack Speed";
		name = "Attack Speed";
	}

	public void SetSpeed(float bonus)
	{
		m_bonus = bonus;
	}

	public float GetSpeed()
	{
		return m_bonus;
	}
}
