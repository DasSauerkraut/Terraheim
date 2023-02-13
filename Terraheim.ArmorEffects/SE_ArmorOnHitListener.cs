using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

internal class SE_ArmorOnHitListener : StatusEffect
{
	public float m_maxArmor = 0f;

	private static JObject balance = Terraheim.balance;

	public void Awake()
	{
		m_name = "Brassflesh Listener";
		base.name = "Brassflesh Listener";
		m_tooltip = string.Format("Hitting an enemy grants {0}% damage reduction for {1} seconds, stacking up to {2}%. Striking an enemy resets the countdown. The damage reduction applies before armor bonuses.", m_maxArmor / 10f * 100f, (int)balance["brassfleshTTL"], m_maxArmor * 100f);
	}

	public void SetMaxArmor(float bonus)
	{
		m_maxArmor = bonus;
		m_tooltip = string.Format("Hitting an enemy grants {0}% damage reduction for {1} seconds, stacking up to {2}%. Striking an enemy resets the countdown. The damage reduction applies before armor bonuses.", m_maxArmor / 10f * 100f, (int)balance["brassfleshTTL"], m_maxArmor * 100f);
	}

	public float GetMaxArmor()
	{
		return m_maxArmor;
	}
}
