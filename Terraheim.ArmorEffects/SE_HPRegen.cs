using Jotunn.Managers;

namespace Terraheim.ArmorEffects;

internal class SE_HPRegen : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "HP Regen";
		base.name = "HP Regen";
		m_tooltip = "Health Regen is increased by " + (m_bonus + 1f) + "x.";
		m_icon = null;
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetBronze").m_itemData.GetIcon();
	}

	public void setHealPercent(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = "Health Regen is increased by " + (m_bonus + 1f) + "x.";
	}

	public float getHealPercent()
	{
		return m_bonus;
	}
}
