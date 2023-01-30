using Jotunn.Managers;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects;

internal class SE_Thorns : StatusEffect
{
	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Thorns";
		base.name = "Thorns";
		m_tooltip = "Reflect " + m_bonus * 100f + "% of incoming damage back at your attacker.";
		m_icon = null;
	}

	public void SetIcon()
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded").m_itemData.GetIcon();
	}

	public void SetReflectPercent(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = "Reflect " + m_bonus * 100f + "% of incoming damage back at your attacker.";
	}

	public float GetReflectPercent()
	{
		return m_bonus;
	}

	public override void OnDamaged(HitData hit, Character attacker)
	{
		if (!m_character.IsBlocking() && attacker != null && attacker.m_seman != null)
		{
			Log.LogMessage($"Damage dealt: {hit.GetTotalDamage()}, Thorns {m_bonus * 100f}%");
			HitData hitData = new HitData();
			hitData.m_damage.Add(hit.m_damage);
			if (hitData.GetTotalDamage() > 1000f)
			{
				hitData.m_damage.m_blunt *= 0.075f;
				hitData.m_damage.m_chop *= 0.075f;
				hitData.m_damage.m_damage *= 0.075f;
				hitData.m_damage.m_fire *= 0.075f;
				hitData.m_damage.m_frost *= 0.075f;
				hitData.m_damage.m_lightning *= 0.075f;
				hitData.m_damage.m_pickaxe *= 0.075f;
				hitData.m_damage.m_pierce *= 0.075f;
				hitData.m_damage.m_poison *= 0.075f;
				hitData.m_damage.m_slash *= 0.075f;
				hitData.m_damage.m_spirit *= 0.075f;
				Log.LogMessage($"Greater than 1000, Reducing to 7.5% -> {hitData.GetTotalDamage()}");
			}
			hitData.m_damage.m_blunt *= m_bonus;
			hitData.m_damage.m_chop *= m_bonus;
			hitData.m_damage.m_damage *= m_bonus;
			hitData.m_damage.m_fire *= m_bonus;
			hitData.m_damage.m_frost *= m_bonus;
			hitData.m_damage.m_lightning *= m_bonus;
			hitData.m_damage.m_pickaxe *= m_bonus;
			hitData.m_damage.m_pierce *= m_bonus;
			hitData.m_damage.m_poison *= m_bonus;
			hitData.m_damage.m_slash *= m_bonus;
			hitData.m_damage.m_spirit *= m_bonus;
			hitData.m_staggerMultiplier = 0f;
			Log.LogMessage($"Reflected Damage: {hitData.m_damage.GetTotalDamage()}");
			if (attacker.GetHealth() <= hitData.GetTotalDamage() && attacker.GetHealthPercentage() >= (float)Terraheim.balance["thornsKillThreshold"])
			{
				float num = attacker.GetHealth() - 1f;
				hitData.m_damage.m_blunt = num * (hitData.m_damage.m_blunt / hitData.GetTotalDamage());
				hitData.m_damage.m_chop = num * (hitData.m_damage.m_chop / hitData.GetTotalDamage());
				hitData.m_damage.m_damage = num * (hitData.m_damage.m_damage / hitData.GetTotalDamage());
				hitData.m_damage.m_fire = num * (hitData.m_damage.m_fire / hitData.GetTotalDamage());
				hitData.m_damage.m_frost = num * (hitData.m_damage.m_frost / hitData.GetTotalDamage());
				hitData.m_damage.m_lightning = num * (hitData.m_damage.m_lightning / hitData.GetTotalDamage());
				hitData.m_damage.m_pickaxe = num * (hitData.m_damage.m_pickaxe / hitData.GetTotalDamage());
				hitData.m_damage.m_pierce = num * (hitData.m_damage.m_pierce / hitData.GetTotalDamage());
				hitData.m_damage.m_poison = num * (hitData.m_damage.m_poison / hitData.GetTotalDamage());
				hitData.m_damage.m_slash = num * (hitData.m_damage.m_slash / hitData.GetTotalDamage());
				hitData.m_damage.m_spirit = num * (hitData.m_damage.m_spirit / hitData.GetTotalDamage());
			}
			attacker.ApplyDamage(hitData, showDamageText: true, triggerEffects: false);
			GameObject gameObject = Object.Instantiate(AssetHelper.FXThorns, attacker.GetCenterPoint(), Quaternion.identity);
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem particleSystem in array)
			{
				particleSystem.Play();
			}
			base.OnDamaged(hit, attacker);
		}
	}
}
