using Jotunn.Managers;

namespace Terraheim.ArmorEffects;

internal class SE_DeathMark : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public int m_threshold = 0;

	public int m_duration = 0;

	public bool m_lastHitThrowing = false;

	public void Awake()
	{
		m_name = "Death Mark";
		base.name = "Death Mark";
		m_tooltip = string.Format("Hit an enemy {0} times with melee weapons to Mark them for death. The next {1} against a Marked enemy deals {2}x damage.", m_threshold, (m_duration > 1) ? $"{m_duration} hits" : "hit", m_damageBonus);
		m_icon = null;
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetFenring").m_itemData.GetIcon();
	}

	public void SetLastHitThrowing(bool hit)
	{
		m_lastHitThrowing = hit;
	}

	public bool GetLastHitThrowing()
	{
		return m_lastHitThrowing;
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
        m_tooltip = string.Format("Hit an enemy {0} times with melee weapons to Mark them for death. The next {1} against a Marked enemy deals {2}x damage.", m_threshold, (m_duration > 1) ? $"{m_duration} hits" : "hit", m_damageBonus);
    }

    public float GetDamageBonus()
	{
		return m_damageBonus;
	}

	public void SetHitDuration(int dur)
	{
		m_duration = dur;
        m_tooltip = string.Format("Hit an enemy {0} times with melee weapons to Mark them for death. The next {1} against a Marked enemy deals {2}x damage.", m_threshold, (m_duration > 1) ? $"{m_duration} hits" : "hit", m_damageBonus);
    }

    public int GetHitDuration()
	{
		return m_duration;
	}

	public void SetThreshold(int threshold)
	{
		m_threshold = threshold;
        m_tooltip = string.Format("Hit an enemy {0} times with melee weapons to Mark them for death. The next {1} against a Marked enemy deals {2}x damage.", m_threshold, (m_duration > 1) ? $"{m_duration} hits" : "hit", m_damageBonus);
    }

    public int GetThreshold()
	{
		return m_threshold;
	}
}
