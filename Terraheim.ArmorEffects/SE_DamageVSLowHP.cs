namespace Terraheim.ArmorEffects;

internal class SE_DamageVSLowHP : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public float m_healthThreshold = -1f;

    private bool m_setup = false;

	public void Awake()
	{
		m_name = "Damage Vs Low HP";
		base.name = "Damage Vs Low HP";
		m_tooltip = "Damage Vs Low HP Increased by " + m_damageBonus * 10f + "%";
	}

    public override void UpdateStatusEffect(float dt)
    {
        if (!m_setup)
        {
            try
            {
                if (m_healthThreshold != -1f)
                {
                    m_character.m_nview.GetZDO().Set("hasDamageVsLowHp", true);
                    m_character.m_nview.GetZDO().Set("damageVsLowHpThreshold", m_healthThreshold);
                    m_character.m_nview.GetZDO().Set("damageVsLowHpDamage", m_damageBonus);
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
        m_character.m_nview.GetZDO().Set("hasDamageVsLowHp", false);
        base.Stop();
    }

    public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_tooltip = "Damage Vs Low HP Increased by " + bonus * 10f + "%";
	}

	public float GetDamageBonus()
	{
		return m_damageBonus;
	}

	public void SetHealthThreshold(float bonus)
	{
		m_healthThreshold = bonus;
	}

	public float GetHealthThreshold()
	{
		return m_healthThreshold;
	}
}
