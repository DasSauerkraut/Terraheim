namespace Terraheim.ArmorEffects;

internal class SE_HealOnBlock : StatusEffect
{
	public float m_blockHeal = 0.05f;

	public float m_parryHeal = 0.05f;

	public void Awake()
	{
		m_name = "Heal On Block";
		base.name = "Heal On Block";
	}

	public void SetHealBonus(float bonus)
	{
		m_blockHeal = bonus;
		m_parryHeal = bonus * 1.75f;
	}

	public float GetBlockHeal()
	{
		return m_blockHeal;
	}

	public float GetParryHeal()
	{
		return m_parryHeal;
	}
}
