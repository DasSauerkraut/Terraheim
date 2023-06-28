using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects;

internal class SE_Wolftears : StatusEffect
{
	public float m_damageBonus = 0.05f;

	public float m_activationHealth = 0f;

	public float m_damageIncrement = 0f;

	private static JObject balance = Terraheim.balance;

	public void Awake()
	{
		m_name = "Wolftears";
		base.name = "Wolftears";
		m_tooltip = "Gain +" + m_damageIncrement * 100f + "% damage for every 10% of HP missing, up to " + m_damageBonus * 100f + "% when below " + m_activationHealth * 100f + "% HP." + string.Format("\nEvery {0} seconds, when an attack would have killed you, survive at 1 hp.", (float)balance["wolftearOneHitTTL"]);
		m_icon = null;
	}

	public void SetIcon()
	{
		if (m_icon == null)
		{
			m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetIron").m_itemData.GetIcon();
		}
		int num = Mathf.RoundToInt((1f - m_character.GetHealthPercentage()) * 10f);
		if (m_character.GetHealthPercentage() <= m_activationHealth)
		{
			num = 10;
		}
		if (num > 0)
		{
			m_name = base.name + " " + num;
		}
		else
		{
			m_name = base.name;
		}
		if (num > 4 && !m_character.GetSEMan().HaveStatusEffect("WolftearsFX"))
		{
			Log.LogInfo("Adding status");
			m_character.GetSEMan().AddStatusEffect("WolftearsFX".GetStableHashCode(), false);
		}
		else if (num <= 4 && m_character.GetSEMan().HaveStatusEffect("WolftearsFX"))
		{
			Log.LogInfo("Remove status");
			m_character.GetSEMan().RemoveStatusEffect("WolftearsFX".GetStableHashCode());
		}
	}

	public void ClearIcon()
	{
		m_icon = null;
	}

	public override void UpdateStatusEffect(float dt)
	{
		base.UpdateStatusEffect(dt);
		SetIcon();
	}

	public void SetDamageBonus(float bonus)
	{
		m_damageBonus = bonus;
		m_damageIncrement = m_damageBonus / 10f;
		m_tooltip = "Gain +" + m_damageIncrement * 100f + "% damage for every 10% of HP missing, up to " + m_damageBonus * 100f + "% when below " + m_activationHealth * 100f + "% HP." + string.Format("\nEvery {0} seconds, when an attack would have killed you, survive at 1 hp.", (float)balance["wolftearOneHitTTL"]);
	}

	public float GetDamageBonus()
	{
		if (m_character.GetHealthPercentage() <= m_activationHealth)
		{
			return m_damageBonus;
		}
		if (m_character.GetHealthPercentage() < 0.9f)
		{
			return (float)Mathf.RoundToInt((1f - m_character.GetHealthPercentage()) * 10f) * m_damageIncrement;
		}
		return 0f;
	}

	public void SetActivationHP(float bonus)
	{
		m_activationHealth = bonus;
		m_tooltip = "Gain +" + m_damageIncrement * 100f + "% damage for every 10% of HP missing, up to " + m_damageBonus * 100f + "% when below " + m_activationHealth * 100f + "% HP." + string.Format("\nEvery {0} seconds, when an attack would have killed you, survive at 1 hp.", (float)balance["wolftearOneHitTTL"]);
	}

	public float GetActivationHP()
	{
		return m_activationHealth;
	}
}
