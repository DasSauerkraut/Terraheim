namespace Terraheim.ArmorEffects;

internal class SE_CoinDrop : StatusEffect
{
	public float m_chance = 0f;

	public float m_bonus = 0f;

    public bool m_setup = false;

	public void Awake()
	{
		m_name = "Coin Drop";
		base.name = "Coin Drop";
		m_tooltip = m_chance + "% chance to drop " + m_bonus + " coins.";
	}

    public override void UpdateStatusEffect(float dt)
    {
        if (!m_setup)
        {
            try
            {
                if (m_chance != 0f)
                {
                    m_character.m_nview.GetZDO().Set("hasCoinDrop", true);
                    m_character.m_nview.GetZDO().Set("coinDropChance", m_chance);
                    m_character.m_nview.GetZDO().Set("coinDropBonus", m_bonus);
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
        m_character.m_nview.GetZDO().Set("hasCoinDrop", false);
        base.Stop();
    }

    public void SetChance(float bonus)
	{
		m_chance = bonus * 100f;
		m_tooltip = m_chance + "% chance to drop " + m_bonus + " coins.";
	}

	public float GetChance()
	{
		return m_chance;
	}

	public void SetCoinAmount(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = m_chance + "% chance to drop " + m_bonus + " coins.";
	}

	public float GetCoinAmount()
	{
		return m_bonus;
	}
}
