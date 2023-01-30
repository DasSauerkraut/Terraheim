using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_ChallengeMoveSpeed : StatusEffect
{
	private static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

	public void Awake()
	{
		m_name = "Challenge Move Speed";
		base.name = "Challenge Move Speed";
		m_tooltip = "";
		m_icon = null;
	}
}
