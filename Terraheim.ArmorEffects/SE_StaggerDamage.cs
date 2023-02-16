namespace Terraheim.ArmorEffects;

internal class SE_StaggerDamage : StatusEffect
{
	public float m_staggerdmg = -0f;

	public float m_staggerbns;

    private bool m_setup = false;

    public void Awake()
	{
		m_name = "Stagger Damage";
		base.name = "Stagger Damage";
		m_tooltip = $"Stagger damage increased by {m_staggerdmg * 100f}%, damage vs staggered enemies is increased by {m_staggerbns * 100f}%";
	}

    public override void UpdateStatusEffect(float dt)
    {
        if (!m_setup)
        {
            try
            {
                if (m_staggerdmg != -1f)
                {
                    m_character.m_nview.GetZDO().Set("hasStaggerDamage", true);
                    m_character.m_nview.GetZDO().Set("staggerDamageBonus", m_staggerbns);
                    m_setup = true;
                }
            }
            catch
            {
                Log.LogInfo("Not ready yet?");
            }
        }
        base.UpdateStatusEffect(dt);
    }

    public override void Stop()
    {
        m_character.m_nview.GetZDO().Set("hasStaggerDamage", false);
        base.Stop();
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
