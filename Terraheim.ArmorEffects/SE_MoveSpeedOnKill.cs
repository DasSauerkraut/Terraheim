using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_MoveSpeedOnKill : StatusEffect
{
	private static JObject balance = Terraheim.balance;

	public float m_speedBonusAmount = 0.15f;

	public float m_currentSpeedBonus = 0f;

	public float m_maxSpeedBonus = 0f;

	public float TTL
	{
		get
		{
			return m_ttl;
		}
		set
		{
			m_ttl = value;
		}
	}

	public void Awake()
	{
		m_name = "Bloodrush";
		base.name = "Bloodrush";
		m_tooltip = "";
		m_icon = null;
	}

	public override void Setup(Character character)
	{
		TTL = (float)balance["bloodrushTTL"];
		SetIcon();
		m_maxSpeedBonus = (float)balance["bloodrushMaxSpeed"];
		m_currentSpeedBonus = m_speedBonusAmount;
		Log.LogWarning("Adding Bloodrush");
		m_name = base.name + " " + m_currentSpeedBonus * 100f + "%";
		base.Setup(character);
	}

	public void OnKill()
	{
		m_time = 0f;
		m_currentSpeedBonus += m_speedBonusAmount;
		if (m_currentSpeedBonus > m_maxSpeedBonus)
		{
			m_currentSpeedBonus = m_maxSpeedBonus;
		}
		m_name = base.name + " " + m_currentSpeedBonus * 100f + "%";
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("CapeWolf").m_itemData.GetIcon();
	}

	public void SetSpeedBonus(float bonus)
	{
		m_speedBonusAmount = bonus;
		if (m_currentSpeedBonus < m_speedBonusAmount)
		{
			m_currentSpeedBonus = m_speedBonusAmount;
		}
	}

	public float GetSpeedBonus()
	{
		return m_speedBonusAmount;
	}

	public float GetCurrentSpeedBonus()
	{
		return m_currentSpeedBonus;
	}
}
