using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_ChallengeMoveSpeed : StatusEffect
{
	public void Awake()
	{
		m_name = "Challenge Move Speed";
		base.name = "Challenge Move Speed";
		m_tooltip = "";
		m_icon = null;
	}
}
