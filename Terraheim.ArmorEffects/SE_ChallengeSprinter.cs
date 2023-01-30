using Jotunn.Managers;

namespace Terraheim.ArmorEffects;

internal class SE_ChallengeSprinter : StatusEffect
{
	public float m_totalStamina = 0f;

	public float m_regen = 0f;

	public void Awake()
	{
		m_name = "Sprinter";
		base.name = "Sprinter";
		SetTooltip();
	}

	private void SetTooltip()
	{
		m_tooltip = $"Stamina regeneration is increased by {m_regen * 100f} %.\nMaximum stamina is reduced by {m_totalStamina * 100f}%.";
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorRagsChest").m_itemData.GetIcon();
	}

	public void SetTotalStamina(float bonus)
	{
		m_totalStamina = bonus;
		SetTooltip();
	}

	public float GetTotalStamina()
	{
		return m_totalStamina;
	}

	public void SetRegen(float bonus)
	{
		m_regen = bonus;
		SetTooltip();
	}

	public float GetRegen()
	{
		return m_regen;
	}
}
