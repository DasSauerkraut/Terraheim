using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_AoECounterExhausted : StatusEffect
{
	private static JObject balance = Terraheim.balance;

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
		m_name = "Wyrd Exhausted";
		base.name = "Wyrd Exhausted";
		m_tooltip = "";
	}

	public override void Setup(Character character)
	{
		SetIcon();
		TTL = (float)balance["wyrdExhaustedTTL"];
		Log.LogWarning("Adding Exhaustion");
		base.Setup(character);
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
	}
}
