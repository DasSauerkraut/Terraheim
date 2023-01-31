using System;
using HarmonyLib;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using UnityEngine;
using TerraheimItems;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class AttackPatch
{
	public void Awake()
	{
		Log.LogInfo("Attack Patching Complete");
	}

	[HarmonyPatch(typeof(Attack), nameof(Attack.Start))]
	public static void Prefix(ref Attack __instance, Humanoid character, ref ItemDrop.ItemData weapon)
	{	
		if (((bool)character.m_unarmedWeapon && weapon == character?.m_unarmedWeapon.m_itemData) || weapon.m_dropPrefab == null || weapon.m_dropPrefab.name == null)
		{
			return;
		}
		ItemDrop itemDrop = PrefabManager.Cache.GetPrefab<ItemDrop>(weapon.m_dropPrefab.name);
		if (itemDrop == null)
		{
			Log.LogMessage("Terraheim (AttackPatch Start) | Weapon is null, grabbing directly");
			itemDrop = ObjectDB.instance.GetItemPrefab(weapon.m_dropPrefab.name).GetComponent<ItemDrop>();
		}
		weapon.m_shared.m_backstabBonus = itemDrop.m_itemData.m_shared.m_backstabBonus;
		weapon.m_shared.m_damages.m_frost = itemDrop.m_itemData.m_shared.m_damages.m_frost;
		weapon.m_shared.m_damages.m_spirit = itemDrop.m_itemData.m_shared.m_damages.m_spirit;
		weapon.m_shared.m_damages.m_damage = itemDrop.m_itemData.m_shared.m_damages.m_damage;
		weapon.m_shared.m_attack.m_damageMultiplier = itemDrop.m_itemData.m_shared.m_attack.m_damageMultiplier;
		if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
		{
			if ((bool)character.GetSEMan().GetStatusEffect("Life Steal"))
			{
				SE_HPOnHit sE_HPOnHit = character.GetSEMan().GetStatusEffect("Life Steal") as SE_HPOnHit;
				sE_HPOnHit.setLastHitMelee(melee: false);
			}
			if (character.GetSEMan().HaveStatusEffect("Ranged Damage Bonus"))
			{
				SE_RangedDmgBonus sE_RangedDmgBonus = character.GetSEMan().GetStatusEffect("Ranged Damage Bonus") as SE_RangedDmgBonus;
				weapon.m_shared.m_attack.m_damageMultiplier += sE_RangedDmgBonus.getDamageBonus();
			}
		}
		else if ((bool)character.GetSEMan().GetStatusEffect("Life Steal"))
		{
			SE_HPOnHit sE_HPOnHit2 = character.GetSEMan().GetStatusEffect("Life Steal") as SE_HPOnHit;
			sE_HPOnHit2.setLastHitMelee(melee: true);
		}
		if (weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow && character.GetSEMan().HaveStatusEffect("Melee Damage Bonus"))
		{
			SE_MeleeDamageBonus sE_MeleeDamageBonus = character.GetSEMan().GetStatusEffect("Melee Damage Bonus") as SE_MeleeDamageBonus;
			weapon.m_shared.m_attack.m_damageMultiplier += sE_MeleeDamageBonus.getDamageBonus();
		}
		if (weapon.m_shared.m_name.Contains("_throwingaxe") || (weapon.m_shared.m_name.Contains("_spear") && __instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation) || weapon.m_shared.m_name.Contains("bomb"))
		{
			if (character.GetSEMan().HaveStatusEffect("Throwing Damage Bonus"))
			{
				SE_ThrowingDamageBonus sE_ThrowingDamageBonus = character.GetSEMan().GetStatusEffect("Throwing Damage Bonus") as SE_ThrowingDamageBonus;
				weapon.m_shared.m_attack.m_damageMultiplier += sE_ThrowingDamageBonus.getDamageBonus();
			}
			if (character.GetSEMan().HaveStatusEffect("Death Mark"))
			{
				(character.GetSEMan().GetStatusEffect("Death Mark") as SE_DeathMark).SetLastHitThrowing(hit: true);
			}
		}
		else if (character.GetSEMan().HaveStatusEffect("Death Mark"))
		{
			(character.GetSEMan().GetStatusEffect("Death Mark") as SE_DeathMark).SetLastHitThrowing(hit: false);
		}
		if ((weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife")) && character.GetSEMan().HaveStatusEffect("Dagger/Spear Damage Bonus"))
		{
			SE_DaggerSpearDmgBonus sE_DaggerSpearDmgBonus = character.GetSEMan().GetStatusEffect("Dagger/Spear Damage Bonus") as SE_DaggerSpearDmgBonus;
			weapon.m_shared.m_attack.m_damageMultiplier += sE_DaggerSpearDmgBonus.getDamageBonus();
		}
		if ((weapon.m_shared.m_itemType != ItemDrop.ItemData.ItemType.TwoHandedWeapon || weapon.m_shared.m_name.Contains("fistweapon")) && (character.GetLeftItem() == null || character.GetLeftItem().m_shared.m_itemType != ItemDrop.ItemData.ItemType.Shield) && character.GetSEMan().HaveStatusEffect("One Hand Damage Bonus"))
		{
			Log.LogInfo("Damage is buffed");
			SE_OneHandDamageBonus sE_OneHandDamageBonus = character.GetSEMan().GetStatusEffect("One Hand Damage Bonus") as SE_OneHandDamageBonus;
			weapon.m_shared.m_attack.m_damageMultiplier += sE_OneHandDamageBonus.getDamageBonus();
		}
		if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon && character.GetSEMan().HaveStatusEffect("Two Handed Damage Bonus"))
		{
			SE_TwoHandedDmgBonus sE_TwoHandedDmgBonus = character.GetSEMan().GetStatusEffect("Two Handed Damage Bonus") as SE_TwoHandedDmgBonus;
			weapon.m_shared.m_attack.m_damageMultiplier += sE_TwoHandedDmgBonus.getDamageBonus();
		}
		if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon && character.GetSEMan().HaveStatusEffect("Crit Chance"))
		{
			SE_CritChance sE_CritChance = character.GetSEMan().GetStatusEffect("Crit Chance") as SE_CritChance;
			System.Random random = new System.Random();
			int num = random.Next(1, 100);
			if ((float)num <= sE_CritChance.GetCritChance())
			{
				Log.LogWarning("Crit!");
				weapon.m_shared.m_attack.m_damageMultiplier *= sE_CritChance.GetCritBonus();
			}
		}
		if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow && character.GetSEMan().HaveStatusEffect("Ammo Consumption"))
		{
			System.Random random2 = new System.Random();
			SE_AmmoConsumption sE_AmmoConsumption = character.GetSEMan().GetStatusEffect("Ammo Consumption") as SE_AmmoConsumption;
			int num2 = random2.Next(1, 100);
			if ((float)num2 <= sE_AmmoConsumption.getAmmoConsumption())
			{
				if (string.IsNullOrEmpty(weapon.m_shared.m_ammoType))
				{
					return;
				}
				bool flag = true;
				ItemDrop.ItemData itemData = character.GetAmmoItem();
				if (itemData != null && (!character.GetInventory().ContainsItem(itemData) || itemData.m_shared.m_ammoType != weapon.m_shared.m_ammoType))
				{
					itemData = null;
				}
				if (itemData == null)
				{
					itemData = character.GetInventory().GetAmmoItem(weapon.m_shared.m_ammoType);
				}
				if (itemData == null)
				{
					return;
				}
				if (itemData.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
				{
					flag = character.CanConsumeItem(itemData);
				}
				if (flag)
				{
					itemData.m_stack++;
				}
			}
		}
		if (character.GetSEMan().HaveStatusEffect("Backstab Bonus"))
		{
			SE_BackstabBonus sE_BackstabBonus = character.GetSEMan().GetStatusEffect("Backstab Bonus") as SE_BackstabBonus;
			weapon.m_shared.m_backstabBonus = itemDrop.m_itemData.m_shared.m_backstabBonus + sE_BackstabBonus.getBackstabBonus();
		}
		weapon.m_shared.m_damages.m_spirit = itemDrop.m_itemData.m_shared.m_damages.m_spirit;
		if ((weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife")) && character.GetSEMan().HaveStatusEffect("Silver Damage Bonus"))
		{
			SE_SilverDamageBonus sE_SilverDamageBonus = character.GetSEMan().GetStatusEffect("Silver Damage Bonus") as SE_SilverDamageBonus;
			float totalDamage = weapon.GetDamage().GetTotalDamage();
			float num3 = totalDamage * sE_SilverDamageBonus.GetDamageBonus() / 2f;
			weapon.m_shared.m_damages.m_frost += num3;
			weapon.m_shared.m_damages.m_spirit += num3;
		}
		if ((weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || weapon.m_shared.m_name.Contains("spear") || weapon.m_shared.m_name.Contains("knife")) && character.GetSEMan().HaveStatusEffect("Ranger Weapon Bonus"))
		{
			weapon.m_shared.m_attack.m_damageMultiplier += (character.GetSEMan().GetStatusEffect("Ranger Weapon Bonus") as SE_RangerWeaponBonus).GetDamageBonus();
		}
		if (character.GetSEMan().HaveStatusEffect("Spirit Damage Bonus"))
		{
			SE_SpiritDamageBonus sE_SpiritDamageBonus = character.GetSEMan().GetStatusEffect("Spirit Damage Bonus") as SE_SpiritDamageBonus;
			weapon.m_shared.m_damages.m_spirit += sE_SpiritDamageBonus.GetDamageBonus();
		}
		if (character.GetSEMan().HaveStatusEffect("Wolftears"))
		{
			SE_Wolftears sE_Wolftears = character.GetSEMan().GetStatusEffect("Wolftears") as SE_Wolftears;
			weapon.m_shared.m_attack.m_damageMultiplier += sE_Wolftears.GetDamageBonus();
		}
		if (character.GetSEMan().HaveStatusEffect("Battle Furor"))
		{
			SE_FullHPDamageBonus sE_FullHPDamageBonus = character.GetSEMan().GetStatusEffect("Battle Furor") as SE_FullHPDamageBonus;
			if (character.GetHealthPercentage() >= sE_FullHPDamageBonus.GetActivationHP())
			{
				weapon.m_shared.m_attack.m_damageMultiplier += sE_FullHPDamageBonus.GetDamageBonus();
			}
		}
		if (character.GetSEMan().HaveStatusEffect("Chosen") && weapon.m_shared.m_name.Contains("Obsidian Dagger") && __instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation)
		{
			(character.GetSEMan().GetStatusEffect("Chosen") as SE_Chosen).OnKnifeUse();
		}
		if (character.GetSEMan().HaveStatusEffect("ShieldFireParryListener"))
		{
			if (weapon.m_shared.m_name.Contains("knife_fire"))
			{
				weapon.m_shared.m_damages.m_damage += 5f;
			}
			else if (weapon.m_shared.m_name.Contains("spear_fire"))
			{
				weapon.m_shared.m_damages.m_damage += 15f;
			}
		}
		if (character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
		{
			Log.LogInfo("Wyrd Active");
			if (weapon.m_shared.m_name.Contains("_knife") && __instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation)
			{
				SE_AoECounter sE_AoECounter = character.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter;
				float num4 = weapon.m_shared.m_damages.GetTotalDamage() * sE_AoECounter.GetDamageBonus() / 2f;
				AssetHelper.TestProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_radius = sE_AoECounter.GetAoESize();
				AssetHelper.TestProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_damage.m_frost = num4;
				AssetHelper.TestProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_damage.m_spirit = num4;
				__instance.m_attackProjectile = AssetHelper.TestProjectile;
				__instance.m_attackType = Attack.AttackType.Projectile;
			}
		}
		if (character.GetSEMan().HaveStatusEffect("Mercenary") && (__instance.m_attackAnimation == weapon.m_shared.m_secondaryAttack.m_attackAnimation || __instance.m_attackAnimation == weapon.m_shared.m_attack.m_attackAnimation))
		{
			SE_Mercenary sE_Mercenary = character.GetSEMan().GetStatusEffect("Mercenary") as SE_Mercenary;
			if (sE_Mercenary.GetCurrentDamage() > 0f && __instance.m_attackStamina < (character as Player).GetStamina())
			{
				weapon.m_shared.m_attack.m_damageMultiplier += sE_Mercenary.GetCurrentDamage();
				character.GetInventory().RemoveItem("$item_coins", 1);
				Log.LogWarning(character.GetInventory().CountItems("$item_coins"));
			}
		}
		if (character.GetSEMan().HaveStatusEffect("Stagger Damage"))
		{
			weapon.m_shared.m_attack.m_staggerMultiplier += (character.GetSEMan().GetStatusEffect("Stagger Damage") as SE_StaggerDamage).GetStaggerDmg();
		}
	}

	[HarmonyPatch(typeof(Attack), nameof(Attack.GetAttackStamina))]
	[HarmonyPrefix]
	public static void GetStaminaUsagePrefix(ref Attack __instance, ref Humanoid ___m_character, ref ItemDrop.ItemData ___m_weapon, ref ItemDrop.ItemData ___m_ammoItem)
	{
		if ((!___m_character.m_unarmedWeapon || ___m_weapon != ___m_character.m_unarmedWeapon.m_itemData) && !(___m_weapon.m_dropPrefab == null))
		{
			ItemDrop itemDrop = PrefabManager.Cache.GetPrefab<ItemDrop>(___m_weapon.m_dropPrefab.name);
			if (itemDrop == null)
			{
				Log.LogMessage("Terraheim (AttackPatch GetStaminaUsage) | Weapon is null, grabbing directly");
				itemDrop = ObjectDB.instance.GetItemPrefab(___m_weapon.m_dropPrefab.name).GetComponent<ItemDrop>();
			}
			__instance.m_attackStamina = itemDrop.m_itemData.m_shared.m_attack.m_attackStamina;
			if (___m_character.GetSEMan().HaveStatusEffect("Attack Stamina Use"))
			{
				SE_AttackStaminaUse sE_AttackStaminaUse = ___m_character.GetSEMan().GetStatusEffect("Attack Stamina Use") as SE_AttackStaminaUse;
				Log.LogMessage("Base Stamina " + itemDrop.m_itemData.m_shared.m_attack.m_attackStamina + " * " + (1f - sE_AttackStaminaUse.GetStaminaUse()));
				__instance.m_attackStamina = itemDrop.m_itemData.m_shared.m_attack.m_attackStamina * (1f - sE_AttackStaminaUse.GetStaminaUse());
				Log.LogMessage("modded " + __instance.m_attackStamina);
			}
		}
	}

	[HarmonyPatch(typeof(Attack), nameof(Attack.FireProjectileBurst))]
	[HarmonyPrefix]
	public static void FireProjectileBurstPrefix(ref Attack __instance)
	{
		if (__instance.GetWeapon().m_shared.m_name.Contains("throwingaxe_"))
		{
			Log.LogInfo(__instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_stayAfterHitStatic);
			Log.LogInfo(__instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_hideOnHit);
			__instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile.GetComponent<Projectile>().m_hideOnHit = __instance.GetWeapon().m_shared.m_secondaryAttack.m_attackProjectile;
		}
		if (__instance.m_character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
		{
			SE_AoECounter sE_AoECounter = __instance.m_character.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter;
			AssetHelper.TestExplosion.GetComponent<Aoe>().m_radius = sE_AoECounter.GetAoESize();
			if (__instance.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
			{
				float num = (__instance.GetWeapon().GetDamage().GetTotalDamage() + __instance.m_ammoItem.m_shared.m_damages.GetTotalDamage() * sE_AoECounter.GetDamageBonus()) / 2f;
				if (__instance.GetWeapon().m_shared.m_name.Contains("bow_fireTH"))
				{
					AssetHelper.FlamebowWyrdExplosion.GetComponent<Aoe>().m_damage.m_spirit = num;
					AssetHelper.FlamebowWyrdExplosion.GetComponent<Aoe>().m_damage.m_frost = num;
					Log.LogInfo("Terraheim | Aoe deals " + num + " frost and " + num + " spirit damage.");
					__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.FlamebowWyrdExplosion;
					__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1f;
				}
				else
				{
					AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_spirit = num;
					AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_frost = num;
					Log.LogInfo("Terraheim | Aoe deals " + num + " frost and " + num + " spirit damage.");
					__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.TestExplosion;
					__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1f;
				}
			}
			else if (__instance.GetWeapon().m_shared.m_name.Contains("spear"))
			{
				float num2 = __instance.GetWeapon().GetDamage().GetTotalDamage() * sE_AoECounter.GetDamageBonus() / 2f;
				AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_spirit = num2;
				AssetHelper.TestExplosion.GetComponent<Aoe>().m_damage.m_frost = num2;
				Log.LogInfo("Terraheim | Aoe deals " + num2 + " frost and " + num2 + " spirit damage.");
				__instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.TestExplosion;
				__instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1f;
			}
		}
		else if (__instance.m_character.IsPlayer() && __instance.GetWeapon().m_shared.m_name.Contains("bow_fireTH"))
		{
			JObject jsonFromFile = UtilityFunctions.GetJsonFromFile("weaponBalance.json");
			__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = AssetHelper.BowFireExplosionPrefab;
			__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit.GetComponent<Aoe>().m_damage.m_fire = (float)jsonFromFile["BowFire"]!["effectVal"];
			__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 1f;
		}
		else if (__instance.m_character.IsPlayer() && __instance.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
		{
			GameObject bowFireExplosionPrefab = AssetHelper.BowFireExplosionPrefab;
			if (__instance.m_ammoItem != null && __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile != null && __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>() != null && __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit == bowFireExplosionPrefab)
			{
				__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = null;
				__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 0f;
			}
		}
		if (__instance.m_character.GetSEMan().HaveStatusEffect("Throwing Weapon Velocity") && __instance.GetWeapon().m_shared.m_name.Contains("_throwingaxe"))
		{
			__instance.m_projectileVel += __instance.m_projectileVel * (1f + (__instance.m_character.GetSEMan().GetStatusEffect("Throwing Weapon Velocity") as SE_ThrowingWeaponVelocity).GetVelocityBonus());
		}
	}

	[HarmonyPatch(typeof(Attack), nameof(Attack.FireProjectileBurst))]
	[HarmonyPostfix]
	public static void FireProjectileBurstPostfix(ref Attack __instance)
	{
		if (__instance.m_character.GetSEMan().HaveStatusEffect("Wyrdarrow"))
		{
			if (__instance.GetWeapon().m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow && __instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit == AssetHelper.TestExplosion)
			{
				__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = null;
				__instance.m_ammoItem.m_shared.m_attack.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 0f;
			}
			else if (__instance.GetWeapon().m_shared.m_name.Contains("spear") && __instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit == AssetHelper.TestExplosion)
			{
				__instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHit = null;
				__instance.m_attackProjectile.GetComponent<Projectile>().m_spawnOnHitChance = 0f;
			}
			if (__instance.m_character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
			{
				SE_AoECounter sE_AoECounter = __instance.m_character.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter;
				sE_AoECounter.ClearCounter();
			}
		}
	}
}
