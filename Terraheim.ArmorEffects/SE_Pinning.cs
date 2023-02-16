using Jotunn.Managers;

namespace Terraheim.ArmorEffects;

public class SE_Pinning : StatusEffect
{
	public float m_pinTTL = -1f;

	public float m_pinCooldownTTL = -1f;

	private bool m_setup = false;

	public void Awake()
	{
		m_name = "Pinning";
		name = "Pinning";
		UpdateTooltip();
	}

    public override void UpdateStatusEffect(float dt)
    {
		if (!m_setup)
		{
            try
            {
				if(m_pinTTL != -1f || m_pinCooldownTTL != -1f)
				{
                    m_character.m_nview.GetZDO().Set("hasPinning", true);
                    m_character.m_nview.GetZDO().Set("pinTTL", m_pinTTL);
                    m_character.m_nview.GetZDO().Set("pinCooldownTTL", m_pinCooldownTTL);
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
        m_character.m_nview.GetZDO().Set("hasPinning", false);
        base.Stop();
    }

    public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather").m_itemData.GetIcon();
	}

	public void UpdateTooltip()
	{
		m_tooltip = $"Hitting an enemy with a damage type it is vulnerable to Pins it for {m_pinTTL} seconds. " + "Pinned enemies are vulnerable to all damage types and have halved movement speed. After the effect wears off, " + $"{m_pinCooldownTTL} seconds must pass before the same enemy can be pinned again.";
	}

	public void SetPinTTL(float bonus)
	{
		m_pinTTL = bonus;
        UpdateTooltip();
	}

	public float GetPinTTL()
	{
		return m_pinTTL;
	}

	public void SetPinCooldownTTL(float bonus)
	{
		m_pinCooldownTTL = bonus;
        UpdateTooltip();
	}

	public float GetPinCooldownTTL()
	{
		return m_pinCooldownTTL;
	}
}
