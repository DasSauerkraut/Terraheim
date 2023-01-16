using Jotunn.Managers;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_Mercenary : StatusEffect
{
	private float m_maxDamage;

	private float m_damagePerCoin;

	private float m_currentDamage;

	private float m_coinThreshold;

	private int m_coinUse;

	private float lastdt = -1f;

	private float m_clock = 0f;

	public void Awake()
	{
		m_name = "Mercenary";
		base.name = "Mercenary";
		SetTooltip();
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>(Data.ArmorSets["nomad"].HelmetID).m_itemData.GetIcon();
	}

	public void SetMaxDamage(float val)
	{
		m_maxDamage = val;
		m_damagePerCoin = m_maxDamage / m_coinThreshold;
		SetTooltip();
	}

	public float GetCurrentDamage()
	{
		return m_currentDamage;
	}

	public void SetCoinThreshold(int coin)
	{
		m_coinThreshold = coin;
		SetTooltip();
	}

	public void SetCoinUse(int coin)
	{
		m_coinUse = coin;
		SetTooltip();
	}

	private void SetTooltip()
	{
		m_tooltip = $"Gain {m_damagePerCoin * 100f}% damage per coin in your inventory, up to a {m_maxDamage * 100f}% boost when at or above {m_coinThreshold} coins. " + $"Whenever you attack, {m_coinUse} coin is consumed.";
	}

	public override void UpdateStatusEffect(float dt)
	{
		m_clock += dt;
		if (m_clock - lastdt > 0.75f || lastdt == -1f)
		{
			lastdt = m_clock;
			int num = (m_character as Player).GetInventory().CountItems("$item_coins");
			m_currentDamage = m_damagePerCoin * (float)num;
			if (num > 0)
			{
				m_name = $"{base.name}\n{m_currentDamage * 100f:#.##}%";
			}
			else
			{
				m_name = base.name;
				m_currentDamage = 0f;
			}
		}
		if (m_clock > 10000f)
		{
			m_clock = 0f;
			lastdt = -1f;
		}
		base.UpdateStatusEffect(dt);
	}
}
