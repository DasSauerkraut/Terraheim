namespace Terraheim.ArmorEffects;

internal class SE_PoisonVulnerable : StatusEffect
{
	public float m_damageBonus = -1f;
    private bool m_setup = false;

	public void Awake()
	{
		m_name = "Poison Vulnerable";
		base.name = "Poison Vulnerable";
		m_tooltip = "Poison Vulnerable " + m_damageBonus * 10f + "%";
	}

    public override void UpdateStatusEffect(float dt)
    {
        if (!m_setup)
        {
            try
            {
                if (m_damageBonus != -1f)
                {
                    m_character.m_nview.GetZDO().Set("hasPoisonVulnerable", true);
                    m_character.m_nview.GetZDO().Set("poisonVulnerableDamageBonus", m_damageBonus);
                    Log.LogInfo("Added ZDO");
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
        m_character.m_nview.GetZDO().Set("hasPoisonVulnerable", false);
        base.Stop();
    }

    public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Poison Vulnerable " + bonus * 100f + "%";
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}
}
