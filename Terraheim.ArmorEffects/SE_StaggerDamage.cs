namespace Terraheim.ArmorEffects;

internal class SE_StaggerDamage : StatusEffect
{
	public float m_staggerdmg = 0.05f;

	public float m_staggerbns;

	public void Awake()
	{
		m_name = "Stagger Damage";
		base.name = "Stagger Damage";
		m_tooltip = $"Stagger damage increased by {m_staggerdmg * 100f}%, damage vs staggered enemies is increased by {m_staggerbns * 100f}%";
	}

	public void SetStaggerDmg(float bonus)
	{
		m_staggerdmg = bonus;
		m_tooltip = $"Stagger damage increased by {m_staggerdmg * 100f}%, damage vs staggered enemies is increased by {m_staggerbns * 100f}%";
	}

	public float GetStaggerDmg()
	{
		return m_staggerdmg;
	}

	public void SetStaggerBns(float bonus)
	{
		m_staggerbns = bonus;
		m_tooltip = $"Stagger damage increased by {m_staggerdmg * 100f}%, damage vs staggered enemies is increased by {m_staggerbns * 100f}%";
	}

	public float GetStaggerBns()
	{
		return m_staggerbns;
	}
}
