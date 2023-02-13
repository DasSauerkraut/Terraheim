using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_ArmorOnHit : StatusEffect
{
	private static JObject balance = Terraheim.balance;

	public float m_armorGainAmount = 0.02f;

	public float m_currentArmorBonus = 0f;

	public float m_maxArmorBonus = 0f;

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
		m_name = "Brassflesh";
		base.name = "Brassflesh";
		m_tooltip = "";
		m_icon = null;
	}

	public override void Setup(Character character)
	{
		TTL = (float)balance["brassfleshTTL"];
		m_currentArmorBonus += m_armorGainAmount;
		m_name = base.name + " " + m_currentArmorBonus * 100f + "%";
		SetIcon();
		Log.LogMessage("Adding Brassflesh");
		base.Setup(character);
	}

	public void OnHit()
	{
		m_time = 0f;
		m_currentArmorBonus += m_armorGainAmount;
		Log.LogInfo($"OnHit {m_currentArmorBonus}");
		if (m_currentArmorBonus > m_maxArmorBonus)
		{
			m_currentArmorBonus = m_maxArmorBonus;
		}
		m_name = base.name + " " + m_currentArmorBonus * 100f + "%";
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetBronze").m_itemData.GetIcon();
	}

	public void SetMaxArmor(float bonus)
	{
		m_maxArmorBonus = bonus;
		m_armorGainAmount = m_maxArmorBonus / 10f;
		Log.LogInfo("setting max armor to " + bonus);
	}

	public float GetMaxArmor()
	{
		return m_maxArmorBonus;
	}

	public float GetCurrentDamageReduction()
	{
		return m_currentArmorBonus;
	}
}
