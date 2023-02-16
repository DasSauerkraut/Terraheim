namespace Terraheim.ArmorEffects;

internal class SE_IncreaseHarvest : StatusEffect
{
    private bool m_setup = false;

	public void Awake()
	{
		m_name = "Harvest Yield Up";
		base.name = "Harvest Yield Up";
		m_tooltip = "More items from crops and forageables.";
	}

    public override void UpdateStatusEffect(float dt)
    {
        if (!m_setup)
        {
            try
            {
                m_character.m_nview.GetZDO().Set("hasHarvestYieldUp", true);
                Log.LogInfo("Added ZDO");
                m_setup = true;
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
        m_character.m_nview.GetZDO().Set("hasHarvestYieldUp", false);
        base.Stop();
    }
}
