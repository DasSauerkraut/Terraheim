namespace Terraheim.ArmorEffects;

internal class SE_Hyperarmor : StatusEffect
{
	public float m_armor = 0f;

	public void Awake()
	{
		m_name = "Hyperarmor";
		base.name = "Hyperarmor";
	}

	public void SetArmor(float bonus)
	{
		m_armor = bonus;
	}

	public float GetArmor()
	{
		return m_armor;
	}
}
