using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_RootingExhausted : StatusEffect
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
		m_name = "Roots Tapped";
		base.name = "Roots Tapped";
		m_tooltip = "";
	}

	public override void Setup(Character character)
	{
		SetIcon();
		TTL = (float)balance["rootingExhaustedTTL"];
		Log.LogWarning("Adding Exhaustion");
		base.Setup(character);
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetRoot").m_itemData.GetIcon();
	}
}
