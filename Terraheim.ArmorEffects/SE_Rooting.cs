using Jotunn.Managers;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects;

internal class SE_Rooting : StatusEffect
{
	public float m_damageBonus = 20f;

	public int m_activationCount = 0;

	public int m_count = 0;

	public int m_maxCount = 12;

	public float m_aoeSize = 4f;

	public float m_rootedDuration = 0f;

    public float m_effectCooldown = 0f;

	public GameObject m_lastProjectile;

	public void Awake()
	{
		m_name = "Rooting";
		name = "Rooting";
		SetTooltip();
	}

	public void SetTooltip()
	{
        m_tooltip = "Hitting an enemy " + m_activationCount + " times charges you. The next bowshot or special attack with a dagger or spear will Root all enemies within " + m_aoeSize + "m. Rooted enemies are immobile for " + m_rootedDuration + " seconds and suffer " + m_damageBonus + " poison damage. After 3 seconds, a Rooted enemy will emit a burst that will Root enemies within " + m_aoeSize +  "m.";
    }

    public void IncreaseCounter()
	{
		if (m_character.GetSEMan().HaveStatusEffect("Roots Tapped"))
		{
			return;
		}
		m_count++;
		if (m_count >= m_maxCount)
		{
			m_count = m_maxCount;
		}
		if (m_count >= m_activationCount && !m_character.GetSEMan().HaveStatusEffect("RootingFX"))
		{
			Log.LogInfo("Adding status");
			m_character.GetSEMan().AddStatusEffect("RootingFX".GetStableHashCode(), false);
		}
		m_name = base.name + " " + m_count + " / " + m_activationCount;
	}

	public void ClearCounter()
	{
		Log.LogMessage("Clearing Counter");
		m_count -= m_activationCount;
		if (m_count < m_activationCount && m_character.GetSEMan().HaveStatusEffect("RootingFX"))
		{
			m_character.GetSEMan().RemoveStatusEffect("RootingFX".GetStableHashCode());
			m_character.GetSEMan().AddStatusEffect("Roots Tapped".GetStableHashCode(), false);
		}
		if (m_count < 1)
		{
			m_name = base.name;
			return;
		}
		m_name = base.name + " " + m_count + " / " + m_activationCount;
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetRoot").m_itemData.GetIcon();
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
        SetTooltip();
    }

    public float GetDamageBonus()
	{
		return m_damageBonus;
	}

    public void SetRootedDuration(float bonus)
    {
        m_rootedDuration = bonus;
        SetTooltip();
    }

    public float GetRootedDuration()
    {
        return m_rootedDuration;
    }

    public void SetActivationCount(int count)
	{
		m_activationCount = count;
        SetTooltip();
    }

    public float GetActivationCount()
	{
		return m_activationCount;
	}

	public void SetAoESize(float aoe)
	{
		m_aoeSize = aoe;
        SetTooltip();
    }

    public float GetAoESize()
	{
		return m_aoeSize;
	}

	public void SetLastProjectile(GameObject proj)
	{
		m_lastProjectile = proj;
	}

	public GameObject GetLastProjectile()
	{
		return m_lastProjectile;
	}
}
