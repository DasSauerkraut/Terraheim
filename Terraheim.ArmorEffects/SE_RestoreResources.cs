namespace Terraheim.ArmorEffects;

internal class SE_RestoreResources : StatusEffect
{
	public float m_chance = 0f;

	public float m_stamina = 0f;

    public bool m_setup = false;

	public void Awake()
	{
		m_name = "Restore Resources";
		base.name = "Restore Resources";
		m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
	}

    public override void UpdateStatusEffect(float dt)
    {
        if (!m_setup)
        {
            try
            {
                if (m_chance != 0f)
                {
                    m_character.m_nview.GetZDO().Set("hasRestoreResources", true);
                    m_character.m_nview.GetZDO().Set("restoreResourcesChance", m_chance);
                    m_character.m_nview.GetZDO().Set("restoreResourcesStamina", m_stamina);
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
        m_character.m_nview.GetZDO().Set("hasRestoreResources", false);
        base.Stop();
    }

    public void SetChance(float bonus)
	{
		m_chance = bonus * 100f;
		m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
	}

	public float GetChance()
	{
		return m_chance;
	}

	public void SetStaminaAmount(float bonus)
	{
		m_stamina = bonus;
		m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
	}

	public float GetStaminaAmount()
	{
		return m_stamina;
	}
}
