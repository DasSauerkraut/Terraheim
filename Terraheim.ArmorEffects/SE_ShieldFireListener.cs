using System;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

public class SE_ShieldFireListener : StatusEffect
{
	private JObject wepBalance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");

	private int m_killThreshold;

	private int m_currentKills = 0;

	private float m_killTotalHp = 0f;

	public void Awake()
	{
		m_name = "Svalinn";
		base.name = "Svalinn";
		m_tooltip = "";
		m_killThreshold = (int)wepBalance["ShieldFireTower"]!["effectVal"];
	}

	public override void Setup(Character character)
	{
		m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("ShieldFireTowerTH").m_itemData.GetIcon();
		base.Setup(character);
	}

	public float OnKill(float killHP)
	{
		m_currentKills++;
		m_killTotalHp += killHP;
		m_name = $"Svalinn\nKills: {m_currentKills} / {m_killThreshold} | HP {Math.Pow(m_killTotalHp / (float)m_killThreshold, 0.74074071645736694):#.#}";
		if (m_currentKills >= m_killThreshold)
		{
			m_currentKills = 0;
			float num = m_killTotalHp / (float)m_killThreshold;
			float num2 = (float)Math.Pow(num, 0.74074071645736694);
			Log.LogInfo($"Kill exceeded threshold, Aoe should heal {num2}, with {num2 / 4f} HP per tick.");
			m_killTotalHp = 0f;
			m_name = "Svalinn";
			return num2;
		}
		return -1f;
	}

	public override void OnDamaged(HitData hit, Character attacker)
	{
		if (!(attacker == null) && !(attacker as Humanoid == null) && (attacker as Humanoid).GetCurrentWeapon() != null)
		{
			if (IsRangedAttack((attacker as Humanoid).GetCurrentWeapon()))
			{
				float num = 1f - (float)wepBalance["ShieldFireTower"]!["projProtection"];
				hit.m_damage.m_blunt *= num;
				hit.m_damage.m_chop *= num;
				hit.m_damage.m_damage *= num;
				hit.m_damage.m_fire *= num;
				hit.m_damage.m_frost *= num;
				hit.m_damage.m_lightning *= num;
				hit.m_damage.m_pickaxe *= num;
				hit.m_damage.m_pierce *= num;
				hit.m_damage.m_poison *= num;
				hit.m_damage.m_slash *= num;
				hit.m_damage.m_spirit *= num;
			}
			base.OnDamaged(hit, attacker);
		}
	}

	private bool IsRangedAttack(ItemDrop.ItemData weapon)
	{
		if (weapon == null || weapon.m_shared == null || weapon.m_dropPrefab == null)
		{
			return false;
		}
		if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
		{
			return true;
		}
		switch (weapon.m_shared.m_name)
		{
		case "cold ball":
			return true;
		case "dragon breath":
			return true;
		case "fireballattack":
			return true;
		default:
			if (weapon.m_dropPrefab.name == "GoblinSpear")
			{
				return true;
			}
			return false;
		}
	}
}
