using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

public class SE_ShieldFireParryListener : StatusEffect
{
	private JObject wepBalance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");

	public void Awake()
	{
		m_name = "ShieldFireParryListener";
		base.name = "ShieldFireParryListener";
		m_tooltip = "";
	}
}
