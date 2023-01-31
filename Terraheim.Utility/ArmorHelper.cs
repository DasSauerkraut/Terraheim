using System.Collections.Generic;
using System.Linq;
using Jotunn.Entities;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using UnityEngine;

namespace Terraheim.Utility;

internal class ArmorHelper
{
	private readonly static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

	public static StatusEffect GetSetEffect(string name, JToken values)
	{
		switch (name)
		{
		case "battlefuror":
		{
			SE_FullHPDamageBonus sE_FullHPDamageBonus = ScriptableObject.CreateInstance<SE_FullHPDamageBonus>();
			sE_FullHPDamageBonus.SetDamageBonus((float)values["setBonusVal"]);
			sE_FullHPDamageBonus.SetActivationHP((float)values["setActivationHP"]);
			sE_FullHPDamageBonus.InitIcon();
			return sE_FullHPDamageBonus;
		}
		case "hpregen":
		{
			SE_HPRegen sE_HPRegen = ScriptableObject.CreateInstance<SE_HPRegen>();
			sE_HPRegen.setHealPercent((float)values["setBonusVal"]);
			sE_HPRegen.SetIcon();
			return sE_HPRegen;
		}
		case "wolftears":
		{
			SE_Wolftears sE_Wolftears = ScriptableObject.CreateInstance<SE_Wolftears>();
			sE_Wolftears.SetDamageBonus((float)values["setBonusVal"]);
			sE_Wolftears.SetActivationHP((float)values["setActivationHP"]);
			return sE_Wolftears;
		}
		case "wyrdarrow":
		{
			SE_AoECounter sE_AoECounter = ScriptableObject.CreateInstance<SE_AoECounter>();
			sE_AoECounter.SetDamageBonus((float)values["setBonusVal"]);
			sE_AoECounter.SetActivationCount((int)values["setActivationCount"]);
			sE_AoECounter.SetAoESize((float)values["setAoESize"]);
			sE_AoECounter.SetIcon();
			return sE_AoECounter;
		}
		case "thorns":
		{
			SE_Thorns sE_Thorns = ScriptableObject.CreateInstance<SE_Thorns>();
			sE_Thorns.SetReflectPercent((float)values["setBonusVal"]);
			sE_Thorns.SetIcon();
			return sE_Thorns;
		}
		case "deathmark":
		{
			SE_DeathMark sE_DeathMark = ScriptableObject.CreateInstance<SE_DeathMark>();
			sE_DeathMark.SetDamageBonus((float)values["setBonusVal"]);
			sE_DeathMark.SetThreshold((int)values["setBonusThreshold"]);
			sE_DeathMark.SetHitDuration((int)values["setBonusDuration"]);
			sE_DeathMark.SetIcon();
			return sE_DeathMark;
		}
		case "brassflesh":
		{
			SE_ArmorOnHitListener sE_ArmorOnHitListener = ScriptableObject.CreateInstance<SE_ArmorOnHitListener>();
			sE_ArmorOnHitListener.SetMaxArmor((float)values["setBonusVal"]);
			return sE_ArmorOnHitListener;
		}
		case "pinning":
		{
			SE_Pinning sE_Pinning = ScriptableObject.CreateInstance<SE_Pinning>();
			sE_Pinning.SetPinTTL((float)values["setBonusVal"]);
			sE_Pinning.SetPinCooldownTTL((float)values["setBonusCooldown"]);
			sE_Pinning.SetIcon();
			return sE_Pinning;
		}
		case "crit":
		{
			SE_CritChance sE_CritChance = ScriptableObject.CreateInstance<SE_CritChance>();
			sE_CritChance.SetCritChance((float)values["setBonusChance"]);
			sE_CritChance.SetCritBonus((float)values["setBonusVal"]);
			sE_CritChance.SetIcon();
			return sE_CritChance;
		}
		case "sprinter":
		{
			SE_ChallengeSprinter sE_ChallengeSprinter = ScriptableObject.CreateInstance<SE_ChallengeSprinter>();
			sE_ChallengeSprinter.SetTotalStamina((float)values["setBonusVal"]);
			sE_ChallengeSprinter.SetRegen((float)values["setRegenBonusVal"]);
			sE_ChallengeSprinter.SetIcon();
			return sE_ChallengeSprinter;
		}
		case "chosen":
		{
			SE_Chosen sE_Chosen = ScriptableObject.CreateInstance<SE_Chosen>();
			sE_Chosen.SetBoonTTLIncrease((int)values["setBoonVal"]);
			sE_Chosen.SetBaneTTLIncrease((int)values["setBaneVal"]);
			return sE_Chosen;
		}
		case "mercenary":
		{
			SE_Mercenary sE_Mercenary = ScriptableObject.CreateInstance<SE_Mercenary>();
			sE_Mercenary.SetCoinThreshold((int)values["setCoinThreshold"]);
			sE_Mercenary.SetCoinUse((int)values["setCoinUse"]);
			sE_Mercenary.SetMaxDamage((float)values["setMaxDmg"]);
			sE_Mercenary.SetIcon();
			return sE_Mercenary;
		}
		case "retaliation":
		{
			SE_Retaliation sE_Retaliation = ScriptableObject.CreateInstance<SE_Retaliation>();
            sE_Retaliation.SetAbsorb((float)values["setBonusVal"]);
            sE_Retaliation.SetIcon();
			return sE_Retaliation;
		}
		default:
			return null;
		}
	}

	public static StatusEffect GetArmorEffect(string name, JToken values, string location, ref string description)
	{
		switch (name)
		{
		case "onehanddmg":
		{
			SE_OneHandDamageBonus sE_OneHandDamageBonus = ScriptableObject.CreateInstance<SE_OneHandDamageBonus>();
			sE_OneHandDamageBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nDamage with one handed and fist weapons is increased by <color=cyan>{sE_OneHandDamageBonus.getDamageBonus() * 100f}%</color> when you do not have a shield equipped.";
			return sE_OneHandDamageBonus;
		}
		case "dodgestamuse":
		{
			SE_DodgeStamUse sE_DodgeStamUse = ScriptableObject.CreateInstance<SE_DodgeStamUse>();
			sE_DodgeStamUse.setDodgeStaminaUse((float)values[location + "EffectVal"]);
			description += $"\n\nDodge stamina cost is reduced by <color=cyan>{sE_DodgeStamUse.getDodgeStaminaUse() * 100f}%</color>.";
			return sE_DodgeStamUse;
		}
		case "lifesteal":
		{
			SE_HPOnHit sE_HPOnHit = ScriptableObject.CreateInstance<SE_HPOnHit>();
			sE_HPOnHit.setHealAmount((float)values[location + "EffectVal"]);
			description = description + "\n\n<color=cyan>Heal " + sE_HPOnHit.getHealAmount() * 100f + "%</color> of damage dealt as HP on hitting an enemy with a melee weapon.";
			return sE_HPOnHit;
		}
		case "rangeddmg":
		{
			SE_RangedDmgBonus sE_RangedDmgBonus = ScriptableObject.CreateInstance<SE_RangedDmgBonus>();
			sE_RangedDmgBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nBow Damage is increased by <color=cyan>{sE_RangedDmgBonus.getDamageBonus() * 100f}%</color>.";
			return sE_RangedDmgBonus;
		}
		case "daggerspeardmg":
		{
			SE_DaggerSpearDmgBonus sE_DaggerSpearDmgBonus = ScriptableObject.CreateInstance<SE_DaggerSpearDmgBonus>();
			sE_DaggerSpearDmgBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description = description + $"\n\nDagger Damage is increased by <color=cyan>{sE_DaggerSpearDmgBonus.getDamageBonus() * 100f}%</color>." + $"\nSpear Damage is increased by <color=cyan>{sE_DaggerSpearDmgBonus.getDamageBonus() * 100f}%</color>.";
			return sE_DaggerSpearDmgBonus;
		}
		case "ammoconsumption":
		{
			SE_AmmoConsumption sE_AmmoConsumption = ScriptableObject.CreateInstance<SE_AmmoConsumption>();
			sE_AmmoConsumption.setAmmoConsumption((int)values[location + "EffectVal"]);
			description += $"\n\n<color=cyan>{sE_AmmoConsumption.getAmmoConsumption()}%</color> chance to not consume ammo.";
			return sE_AmmoConsumption;
		}
		case "meleedmg":
		{
			SE_MeleeDamageBonus sE_MeleeDamageBonus = ScriptableObject.CreateInstance<SE_MeleeDamageBonus>();
			sE_MeleeDamageBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nMelee Damage is increased by <color=cyan>{sE_MeleeDamageBonus.getDamageBonus() * 100f}%</color>.";
			return sE_MeleeDamageBonus;
		}
		case "blockstam":
		{
			SE_BlockStamUse sE_BlockStamUse = ScriptableObject.CreateInstance<SE_BlockStamUse>();
			sE_BlockStamUse.setBlockStaminaUse((float)values[location + "EffectVal"]);
			description += $"\n\nBase block stamina cost is reduced by <color=cyan>{sE_BlockStamUse.getBlockStaminaUse() * 100f}%</color>.";
			return sE_BlockStamUse;
		}
		case "hpbonus":
		{
			SE_HealthIncrease sE_HealthIncrease = ScriptableObject.CreateInstance<SE_HealthIncrease>();
			sE_HealthIncrease.setHealthBonus((float)values[location + "EffectVal"]);
			description += $"\n\nMax HP is increased by <color=cyan>{sE_HealthIncrease.getHealthBonus() * 100f}%</color>.";
			return sE_HealthIncrease;
		}
		case "twohanddmg":
		{
			SE_TwoHandedDmgBonus sE_TwoHandedDmgBonus = ScriptableObject.CreateInstance<SE_TwoHandedDmgBonus>();
			sE_TwoHandedDmgBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nTwo-Handed weapons damage is increased by <color=cyan>{sE_TwoHandedDmgBonus.getDamageBonus() * 100f}%</color>.";
			return sE_TwoHandedDmgBonus;
		}
		case "staminabns":
		{
			SE_ExtraStamina sE_ExtraStamina = ScriptableObject.CreateInstance<SE_ExtraStamina>();
			sE_ExtraStamina.SetStaminaBonus((float)values[location + "EffectVal"]);
			description += $"\n\nStamina is increased by <color=cyan>{sE_ExtraStamina.GetStaminaBonus()}</color> points.";
			return sE_ExtraStamina;
		}
		case "staminaregen":
		{
			SE_StaminaRegen sE_StaminaRegen = ScriptableObject.CreateInstance<SE_StaminaRegen>();
			sE_StaminaRegen.SetRegenPercent((float)values[location + "EffectVal"]);
			description += $"\n\nStamina regen is increased by <color=cyan>{sE_StaminaRegen.GetRegenPercent() * 100f}%</color>.";
			return sE_StaminaRegen;
		}
		case "bowdaggerspearsilverdmg":
		{
			SE_SilverDamageBonus sE_SilverDamageBonus = ScriptableObject.CreateInstance<SE_SilverDamageBonus>();
			sE_SilverDamageBonus.SetDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nBows, daggers, and spears gain <color=cyan>{sE_SilverDamageBonus.GetDamageBonus() * 100f}%</color> damage as spirit and frost damage.";
			return sE_SilverDamageBonus;
		}
		case "drawmovespeed":
		{
			SE_DrawMoveSpeed sE_DrawMoveSpeed = ScriptableObject.CreateInstance<SE_DrawMoveSpeed>();
			sE_DrawMoveSpeed.SetDrawMoveSpeed((float)values[location + "EffectVal"]);
			description += $"\n\nMove <color=cyan>{sE_DrawMoveSpeed.GetDrawMoveSpeed() * 100f}%</color> faster with a drawn bow.";
			return sE_DrawMoveSpeed;
		}
		case "blockpower":
		{
			SE_BlockPowerBonus sE_BlockPowerBonus = ScriptableObject.CreateInstance<SE_BlockPowerBonus>();
			sE_BlockPowerBonus.SetBlockPower((float)values[location + "EffectVal"]);
			description += $"\n\nBlock power increased by <color=cyan>{sE_BlockPowerBonus.GetBlockPower() * 100f}%</color>.";
			return sE_BlockPowerBonus;
		}
		case "healonblock":
		{
			SE_HealOnBlock sE_HealOnBlock = ScriptableObject.CreateInstance<SE_HealOnBlock>();
			sE_HealOnBlock.SetHealBonus((float)values[location + "EffectVal"]);
			description += "\n\nWhile using a tower shield, a successful block <color=cyan>heals</color> you.\nWhile using a normal shield, a successful parry <color=cyan>heals</color> you.";
			return sE_HealOnBlock;
		}
		case "backstabdmg":
		{
			SE_BackstabBonus sE_BackstabBonus = ScriptableObject.CreateInstance<SE_BackstabBonus>();
			sE_BackstabBonus.setBackstabBonus((float)values[location + "EffectVal"]);
			description += $"\n\nSneak Attack Damage is increased by <color=cyan>{sE_BackstabBonus.getBackstabBonus()}x</color>.";
			return sE_BackstabBonus;
		}
		case "spiritdmg":
		{
			SE_SpiritDamageBonus sE_SpiritDamageBonus = ScriptableObject.CreateInstance<SE_SpiritDamageBonus>();
			sE_SpiritDamageBonus.SetDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nAll weapons gain <color=cyan>{sE_SpiritDamageBonus.GetDamageBonus()}</color> spirit damage.";
			return sE_SpiritDamageBonus;
		}
		case "fooduse":
		{
			SE_FoodUsage sE_FoodUsage = ScriptableObject.CreateInstance<SE_FoodUsage>();
			sE_FoodUsage.SetFoodUsage((float)values[location + "EffectVal"]);
			description += $"\n\nFood fullness degrades <color=cyan>{sE_FoodUsage.GetFoodUsage() * 100f}%</color> slower.";
			return sE_FoodUsage;
		}
		case "attackstamina":
		{
			SE_AttackStaminaUse sE_AttackStaminaUse = ScriptableObject.CreateInstance<SE_AttackStaminaUse>();
			sE_AttackStaminaUse.SetStaminaUse((float)values[location + "EffectVal"]);
			description += $"\n\nReduce stamina use for melee weapons by <color=cyan>{sE_AttackStaminaUse.GetStaminaUse() * 100f}%</color>.";
			return sE_AttackStaminaUse;
		}
		case "throwdmg":
		{
			SE_ThrowingDamageBonus sE_ThrowingDamageBonus = ScriptableObject.CreateInstance<SE_ThrowingDamageBonus>();
			sE_ThrowingDamageBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nDamage with throwing weapons is increased by <color=cyan>{sE_ThrowingDamageBonus.getDamageBonus() * 100f}%</color>.";
			return sE_ThrowingDamageBonus;
		}
		case "throwvel":
		{
			SE_ThrowingWeaponVelocity sE_ThrowingWeaponVelocity = ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>();
			sE_ThrowingWeaponVelocity.SetVelocityBonus((float)values[location + "EffectVal"]);
			description += $"\n\nThrowing weapons velocity is increased by <color=cyan>{sE_ThrowingWeaponVelocity.GetVelocityBonus() * 100f}%</color>.";
			return sE_ThrowingWeaponVelocity;
		}
		case "dmgvslowhp":
		{
			SE_DamageVSLowHP sE_DamageVSLowHP = ScriptableObject.CreateInstance<SE_DamageVSLowHP>();
			sE_DamageVSLowHP.SetDamageBonus((float)values[location + "EffectVal"]);
			sE_DamageVSLowHP.SetHealthThreshold((float)values[location + "EffectThreshold"]);
			description = description + "\n\nDamage against enemies with less than <color=cyan>" + sE_DamageVSLowHP.GetHealthThreshold() * 100f + "%</color> HP is increased by <color=cyan>" + sE_DamageVSLowHP.GetDamageBonus() * 100f + "%</color>.";
			return sE_DamageVSLowHP;
		}
		case "speedonkill":
		{
			SE_MoveSpeedOnKillListener sE_MoveSpeedOnKillListener = ScriptableObject.CreateInstance<SE_MoveSpeedOnKillListener>();
			sE_MoveSpeedOnKillListener.SetSpeedBonus((float)values[location + "EffectVal"]);
			description = description + string.Format("\n\nAfter killing an enemy, gain a stacking <color=cyan>+{0}%</color> movement speed bonus for {1}", sE_MoveSpeedOnKillListener.GetSpeedBonus() * 100f, (float)balance["bloodrushTTL"]) + " seconds. Killing an enemy resets the countdown.";
			return sE_MoveSpeedOnKillListener;
		}
		case "woodsman":
		{
			SE_TreeDamageBonus sE_TreeDamageBonus = ScriptableObject.CreateInstance<SE_TreeDamageBonus>();
			sE_TreeDamageBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"Weland's designs raises your effectiveness at woodcutting.\nDamage against trees is increased by <color=yellow>{sE_TreeDamageBonus.getDamageBonus() * 100f}%</color>.";
			return sE_TreeDamageBonus;
		}
		case "miner":
		{
			SE_MiningBonus sE_MiningBonus = ScriptableObject.CreateInstance<SE_MiningBonus>();
			sE_MiningBonus.setDamageBonus((float)values[location + "EffectVal"]);
			description += $"An artifact of Brokkr, crushing earth comes easily now.\nDamage against rocks and ores is increased by <color=yellow>{sE_MiningBonus.getDamageBonus() * 100f}%</color>.";
			return sE_MiningBonus;
		}
		case "waterproof":
		{
			SE_WetImmunity result3 = ScriptableObject.CreateInstance<SE_WetImmunity>();
			description += "A gift from Freyr.\nYou will not gain the <color=cyan>Wet</color> Ailment.";
			return result3;
		}
		case "farmer":
		{
			SE_IncreaseHarvest result2 = ScriptableObject.CreateInstance<SE_IncreaseHarvest>();
			description += "A blessing from Freyr.\nWhen you harvest wild plants, gain <color=cyan>2</color> more items from each harvest.\nWhen you harvest grown plants, gain <color=cyan>1</color> more item from each harvest.";
			return result2;
		}
		case "thief":
		{
			SE_SneakMovement sE_SneakMovement = ScriptableObject.CreateInstance<SE_SneakMovement>();
			sE_SneakMovement.SetBonus((float)values[location + "EffectVal"]);
			description += $"A blessing from Loki.\nMove <color=cyan>{sE_SneakMovement.GetBonus() * 100f}%</color> faster while using <color=cyan>{sE_SneakMovement.GetBonus() * 100f}%</color> less stamina while sneaking.";
			return sE_SneakMovement;
		}
        case "setbns":
        {
            SE_SetBonusIncrease sE_SetBonusIncrease = ScriptableObject.CreateInstance<SE_SetBonusIncrease>();
            description += $"A relic of Acca.\nThis belt counts as a piece of an armor set, allowing you to get the set bonus while wearing only two items.";
            return sE_SetBonusIncrease;
        }
        case "rangerweapondmg":
		{
			SE_RangerWeaponBonus sE_RangerWeaponBonus = ScriptableObject.CreateInstance<SE_RangerWeaponBonus>();
			sE_RangerWeaponBonus.SetDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nDamage with bows, daggers, and spears is increased by <color=cyan>{sE_RangerWeaponBonus.GetDamageBonus() * 100f}%</color>.";
			return sE_RangerWeaponBonus;
		}
		case "poisonvuln":
		{
			SE_PoisonVulnerable sE_PoisonVulnerable = ScriptableObject.CreateInstance<SE_PoisonVulnerable>();
			sE_PoisonVulnerable.SetDamageBonus((float)values[location + "EffectVal"]);
			description += $"\n\nStriking an enemy with a damage type it is vulnerable deals <color=cyan>{sE_PoisonVulnerable.GetDamageBonus() * 100f}%</color> of the damage dealt as poison damage.";
			return sE_PoisonVulnerable;
		}
		case "challengemvespd":
		{
			SE_ChallengeMoveSpeed result = ScriptableObject.CreateInstance<SE_ChallengeMoveSpeed>();
			description = description + string.Format("\n\nMovement speed is increased by <color=cyan>{0}%</color>.\n", (float)values[location + "EffectVal"] * 100f) + "Suffer <color=red>100%</color> more damage.";
			return result;
		}
		case "challengedodgecost":
		{
			SE_ChallengeDodgeBonus sE_ChallengeDodgeBonus = ScriptableObject.CreateInstance<SE_ChallengeDodgeBonus>();
			sE_ChallengeDodgeBonus.SetDodgeBonus((float)values[location + "EffectVal"]);
			description = description + $"\n\nStamina cost for dodging is reduced by <color=cyan>{sE_ChallengeDodgeBonus.GetDodgeBonus() * 100f}%</color>.\n" + "Food fullness degrades <color=red>2x</color> faster.";
			return sE_ChallengeDodgeBonus;
		}
		case "hyperarmor":
		{
			SE_Hyperarmor sE_Hyperarmor = ScriptableObject.CreateInstance<SE_Hyperarmor>();
			sE_Hyperarmor.SetArmor((float)values[location + "EffectVal"]);
			description += $"\n\nWhen hit during an attack, damage suffered is reduced by <color=cyan>{sE_Hyperarmor.GetArmor() * 100f}%</color>, Chosen Banes will not have their TTL increased and you will suffer no knockback or stagger.";
			return sE_Hyperarmor;
		}
		case "2hattackspeed":
		{
			SE_TwoHandAttackSpeed sE_TwoHandAttackSpeed = ScriptableObject.CreateInstance<SE_TwoHandAttackSpeed>();
			sE_TwoHandAttackSpeed.SetSpeed((float)values[location + "EffectVal"]);
			description += $"\n\nAttack speed for Two-Handed weapons is increased by <color=cyan>{sE_TwoHandAttackSpeed.GetSpeed() * 100f}%</color>.";
			return sE_TwoHandAttackSpeed;
		}
		case "parrybns":
		{
			SE_ParryBonus sE_ParryBonus = ScriptableObject.CreateInstance<SE_ParryBonus>();
			sE_ParryBonus.SetParryBonus((float)values[location + "EffectVal"]);
			description += $"\n\nParry Bonus is increased by <color=cyan>{sE_ParryBonus.GetParryBonus() * 100f}%</color>.";
			return sE_ParryBonus;
		}
		case "coindrop":
		{
			SE_CoinDrop sE_CoinDrop = ScriptableObject.CreateInstance<SE_CoinDrop>();
			sE_CoinDrop.SetChance((float)values[location + "EffectChance"]);
			sE_CoinDrop.SetCoinAmount((float)values[location + "EffectAmount"]);
			description += $"\n\n<color=cyan>{sE_CoinDrop.GetChance()}%</color> chance to drop <color=cyan>{sE_CoinDrop.GetCoinAmount()} coins</color> when striking an enemy.";
			return sE_CoinDrop;
		}
		case "restoreresources":
		{
			SE_RestoreResources sE_RestoreResources = ScriptableObject.CreateInstance<SE_RestoreResources>();
			sE_RestoreResources.SetChance((float)values[location + "EffectChance"]);
			sE_RestoreResources.SetStaminaAmount((int)values[location + "Stamina"]);
			description += $"\n\nOn hit, restore <color=cyan>{sE_RestoreResources.GetStaminaAmount()}</color> Stamina, {sE_RestoreResources.GetChance()}% chance to <color=cyan>refund ammo</color>.";
			return sE_RestoreResources;
		}
		case "staggercap":
		{
			SE_StaggerCapacity sE_StaggerCapacity = ScriptableObject.CreateInstance<SE_StaggerCapacity>();
			sE_StaggerCapacity.SetStaggerCap((float)values[location + "EffectVal"]);
			description += $"\n\nStagger Capacity increased by <color=cyan>{sE_StaggerCapacity.GetStaggerCap() * 100f}%</color>";
			return sE_StaggerCapacity;
		}
		case "staggerdmg":
		{
			SE_StaggerDamage sE_StaggerDamage = ScriptableObject.CreateInstance<SE_StaggerDamage>();
			sE_StaggerDamage.SetStaggerDmg((float)values[location + "EffectVal"]);
			sE_StaggerDamage.SetStaggerBns((float)values[location + "EffectDmg"]);
			description = description + $"\n\nDeal an addtional <color=cyan>{sE_StaggerDamage.GetStaggerDmg() * 100f}%</color> Stagger Damage.\n" + $"Deal an additional <color=cyan>{sE_StaggerDamage.GetStaggerBns() * 100f}%</color> damage to Staggered enemies.";
			return sE_StaggerDamage;
		}
		default:
			return null;
		}
	}

	public static void ModArmorSet(string setName, ref ItemDrop.ItemData helmet, ref ItemDrop.ItemData chest, ref ItemDrop.ItemData legs, JToken values, bool isNewSet, int i)
	{
        JToken armorSet = values["armorSet"];
		List<ItemDrop.ItemData> list = new List<ItemDrop.ItemData> { helmet, chest, legs };
		JToken jToken;
		if (isNewSet)
		{
			jToken = values["upgrades"]![$"t{i}"];
		}
		else
		{
			jToken = values;
			helmet.m_shared.m_name = armorSet["HelmetName"] + "0";
			chest.m_shared.m_name = armorSet["ChestName"] + "0";
			legs.m_shared.m_name = armorSet["LegsName"] + "0";
		}
        StatusEffect setEffect = GetSetEffect((string?)values["setEffect"], jToken);
		foreach (ItemDrop.ItemData item in list)
		{
			item.m_shared.m_armor = (float)jToken["baseArmor"];
			item.m_shared.m_armorPerLevel = (float)jToken["armorPerLevel"];
			if (setEffect != null)
			{
				item.m_shared.m_setStatusEffect = setEffect;
			}
			else
			{
				Log.LogWarning(setName + " - No set effect found for provided effect: " + (string?)values["setEffect"]);
			}
			item.m_shared.m_setSize = 3;
			item.m_shared.m_setName = (string?)values["name"];
			if (!item.m_shared.m_name.Contains("helmet"))
			{
				item.m_shared.m_movementModifier = (float)jToken["globalMoveMod"];
			}
			item.m_shared.m_description = "<i>" + armorSet["ClassName"] + "</i>\n" + item.m_shared.m_description;
		}
		helmet.m_shared.m_armor += (float)armorSet["HelmetArmor"];
		StatusEffect armorEffect = GetArmorEffect((string?)values["headEffect"], jToken, "head", ref helmet.m_shared.m_description);
		StatusEffect armorEffect2 = GetArmorEffect((string?)values["chestEffect"], jToken, "chest", ref chest.m_shared.m_description);
		StatusEffect armorEffect3 = GetArmorEffect((string?)values["legsEffect"], jToken, "legs", ref legs.m_shared.m_description);
		if ((string?)values["headEffect"] == "challengemvespd")
		{
			helmet.m_shared.m_movementModifier += (float)values["headEffectVal"];
		}
		if ((string?)values["chestEffect"] == "challengemvespd")
		{
			chest.m_shared.m_movementModifier += (float)values["chestEffectVal"];
		}
		if ((string?)values["legsEffect"] == "challengemvespd")
		{
			legs.m_shared.m_movementModifier += (float)values["legsEffectVal"];
		}
		if (armorEffect != null)
		{
			helmet.m_shared.m_equipStatusEffect = armorEffect;
			Log.LogWarning(helmet.m_shared.m_equipStatusEffect);
        }
		else
		{
			Log.LogWarning(setName + " Head - No status effect found for provided effect: " + (string?)values["headEffect"]);
		}
		if (armorEffect2 != null)
		{
			chest.m_shared.m_equipStatusEffect = armorEffect2;
		}
		else
		{
			Log.LogWarning(setName + " Chest - No status effect found for provided effect: " + (string?)values["chestEffect"]);
		}
		if (armorEffect3 != null)
		{
			legs.m_shared.m_equipStatusEffect = armorEffect3;
		}
		else
		{
			Log.LogWarning(setName + " Legs - No status effect found for provided effect: " + (string?)values["legsEffect"]);
		}
	}

	public static void ModArmorPiece(string setName, string location, ref ItemDrop.ItemData piece, JToken values, bool isNewSet, int i)
	{
		JToken armorSet = values["armorSet"];
		JToken jToken;
		if (isNewSet)
		{
            jToken = values["upgrades"]![$"t{i}"];
		}
		else
		{
			jToken = values;
			if (setName != "barbarian")
			{
				piece.m_shared.m_name = armorSet["HelmetName"] + "0";
			}
		}
        StatusEffect setEffect = GetSetEffect((string)values["setEffect"], jToken);
        piece.m_shared.m_armor = (float)jToken["baseArmor"];
		piece.m_shared.m_armorPerLevel = (float)jToken["armorPerLevel"];
		if (setEffect != null)
		{
			piece.m_shared.m_setStatusEffect = setEffect;
		}
		else
		{
			Log.LogWarning(setName + " - No set effect found for provided effect: " + (string?)values["setEffect"]);
		}
		piece.m_shared.m_setSize = ((setName != "rags") ? 3 : 2);
		if ((bool)balance["setsAreLockedByTier"])
		{
			piece.m_shared.m_setName = (string?)values["name"] + " T" + i;
		}
		else
		{
			piece.m_shared.m_setName = (string?)values["name"];
		}
		if (!piece.m_shared.m_name.Contains("helmet"))
		{
			piece.m_shared.m_movementModifier = (float)jToken["globalMoveMod"];
		}
		piece.m_shared.m_description = "<i>" + armorSet["ClassName"] + "</i>\n" + piece.m_shared.m_description;
		if (location == "head")
		{
			piece.m_shared.m_armor += (float)armorSet["HelmetArmor"];
		}
		StatusEffect armorEffect = GetArmorEffect((string?)values[location + "Effect"], jToken, location, ref piece.m_shared.m_description);
		if ((string?)values[location + "Effect"] == "challengemvespd")
		{
			piece.m_shared.m_movementModifier += (float)values[location + "EffectVal"];
		}
		if (armorEffect != null)
		{
			piece.m_shared.m_equipStatusEffect = armorEffect;
			return;
		}
		Log.LogWarning(setName + " " + location + " - No status effect found for provided effect: " + (string?)values[location + "Effect"]);
	}

	public static void AddArmorPiece(string setName, string location, JToken armor)
	{
		JToken armorSet = armor["armorSet"];
		for (int i = (int)armor["upgrades"]!["startingTier"]; i <= 7; i++)
		{
			JToken jToken2 = armor["upgrades"]![$"t{i}"];
			string text = "";
			string arg = "";
			switch (location)
			{
			case "head":
				text = (string)armorSet["HelmetID"];
				arg = (string)armorSet["HelmetName"];
				break;
			case "chest":
				text = (string)armorSet["ChestID"];
				arg = (string)armorSet["ChestName"];
				break;
			case "legs":
				text = (string)armorSet["LegsID"];
				arg = (string)armorSet["LegsName"];
				break;
			}
			GameObject gameObject = PrefabManager.Instance.CreateClonedPrefab($"{text}T{i}", text);
			if (setName != "barbarian")
			{
				string arg2 = char.ToUpper(setName[0]) + setName.Substring(1);
				gameObject.name = $"{text}T{i}_Terraheim_AddNewSets_Add{arg2}Armor";
			}
			else
			{
				gameObject.name = $"{text}T{i}_Terraheim_BarbarianArmor_AddNewSets";
			}
			CustomItem customItem = new CustomItem(gameObject, fixReference: true);
			customItem.ItemDrop.m_itemData.m_shared.m_name = $"{arg}{i}";
			ModArmorPiece(setName, location, ref customItem.ItemDrop.m_itemData, armor, isNewSet: true, i);
			Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
			recipe.name = $"Recipe_{text}T{i}";
			List<Piece.Requirement> list = new List<Piece.Requirement>();
			int num = 0;
			if (i == (int)armor["upgrades"]!["startingTier"])
			{
				list.Add(MockRequirement.Create(text, 1, recover: false));
				num++;
				list[0].m_amountPerLevel = 0;
			}
			JToken jToken3 = balance["upgradePath"]![$"t{i}"];
			int num2 = num;
			foreach (JObject item2 in (IEnumerable<JToken>)(jToken3[location]!))
			{
				if ((string?)item2["item"] == "SalamanderFur")
				{
					Piece.Requirement item = new Piece.Requirement
					{
						m_resItem = SharedResources.SalamanderItem.ItemDrop,
						m_amount = (int)item2["amount"]
					};
					list.Add(item);
				}
				else
				{
					list.Add(MockRequirement.Create((string?)item2["item"], (int)item2["amount"]));
				}
				list[num2].m_amountPerLevel = (int)item2["perLevel"];
				num2++;
			}
			recipe.m_craftingStation = Mock<CraftingStation>.Create((string?)jToken3["station"]);
			recipe.m_minStationLevel = (int)jToken3["minLevel"];
			recipe.m_resources = list.ToArray();
			recipe.m_item = customItem.ItemDrop;
			CustomRecipe customRecipe = new CustomRecipe(recipe, fixReference: true, fixRequirementReferences: true);
			ItemManager.Instance.AddItem(customItem);
			ItemManager.Instance.AddRecipe(customRecipe);
		}
	}

	public static void AddCape(string setName, string balanceId)
	{
		JToken jToken = balance["capes"];
		for (int i = 0; i < 2; i++)
		{
			if ((setName.Contains("Wolf") || setName.Contains("Lox")) && i == 0)
			{
				continue;
			}
			GameObject itemPrefab = PrefabManager.Instance.CreateClonedPrefab($"{setName}T{i}TH", setName);
			CustomItem customItem = new CustomItem(itemPrefab, fixReference: true);
			customItem.ItemDrop.m_itemData.m_shared.m_name = $"{customItem.ItemDrop.m_itemData.m_shared.m_name}T{i}";
			if (setName.Contains("Deer"))
			{
				customItem.ItemDrop.m_itemData.m_shared.m_description += string.Format("\nMovement speed is increased by <color=cyan>{0}%</color>.", (float)balance["capes"]!["leather"]!["capeEffectVal"] * 100f);
				customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = (float)balance["capes"]!["leather"]!["capeEffectVal"];
			}
			else
			{
				customItem.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = GetArmorEffect((string?)balance["capes"]![balanceId]!["effect"], balance["capes"]![balanceId], "cape", ref customItem.ItemDrop.m_itemData.m_shared.m_description);
			}
			if (i == 0)
			{
				HitData.DamageModPair damageModPair = default(HitData.DamageModPair);
				damageModPair.m_type = HitData.DamageType.Frost;
				damageModPair.m_modifier = HitData.DamageModifier.Resistant;
				HitData.DamageModPair item = damageModPair;
				customItem.ItemDrop.m_itemData.m_shared.m_damageModifiers.Add(item);
			}
			else
			{
				HitData.DamageModPair damageModPair = default(HitData.DamageModPair);
				damageModPair.m_type = HitData.DamageType.Fire;
				damageModPair.m_modifier = HitData.DamageModifier.Resistant;
				HitData.DamageModPair item2 = damageModPair;
				customItem.ItemDrop.m_itemData.m_shared.m_damageModifiers.Add(item2);
				if (!setName.Contains("Wolf") && !setName.Contains("Lox"))
				{
					damageModPair = default(HitData.DamageModPair);
					damageModPair.m_type = HitData.DamageType.Frost;
					damageModPair.m_modifier = HitData.DamageModifier.Resistant;
					HitData.DamageModPair item3 = damageModPair;
					customItem.ItemDrop.m_itemData.m_shared.m_damageModifiers.Add(item3);
				}
			}
			Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
			recipe.name = $"Recipe_{setName}T{i}";
			List<Piece.Requirement> list = new List<Piece.Requirement>();
			int num = 0;
			if (i == 0 || setName.Contains("Wolf") || setName.Contains("Lox"))
			{
				list.Add(MockRequirement.Create(setName, 1, recover: false));
				num++;
				list[0].m_amountPerLevel = 0;
			}
			JToken jToken2 = jToken["upgrades"]![$"t{i}"];
			int num2 = num;
			foreach (JObject item5 in (IEnumerable<JToken>)(jToken2["resources"]!))
			{
				if ((string?)item5["item"] == "SalamanderFur")
				{
					Piece.Requirement item4 = new Piece.Requirement
					{
						m_resItem = SharedResources.SalamanderItem.ItemDrop,
						m_amount = (int)item5["amount"]
					};
					list.Add(item4);
				}
				else
				{
					list.Add(MockRequirement.Create((string?)item5["item"], (int)item5["amount"]));
				}
				list[num2].m_amountPerLevel = (int)item5["perLevel"];
				num2++;
			}
			recipe.m_craftingStation = Mock<CraftingStation>.Create((string?)jToken2["station"]);
			recipe.m_minStationLevel = (int)jToken2["minLevel"];
			recipe.m_resources = list.ToArray();
			recipe.m_item = customItem.ItemDrop;
			CustomRecipe customRecipe = new CustomRecipe(recipe, fixReference: true, fixRequirementReferences: true);
			ItemManager.Instance.AddItem(customItem);
			ItemManager.Instance.AddRecipe(customRecipe);
		}
	}

	public static void AddArmorSet(string setName, JToken armorSet)
	{
		if ((string)armorSet["armorSet"]["HelmetID"] != "n/a") AddArmorPiece(setName, "head", armorSet);
		AddArmorPiece(setName, "chest", armorSet);
		AddArmorPiece(setName, "legs", armorSet);
	}

	public static void AddBelt(string name)
	{
		JToken jToken = balance[name];
		Data.UtilityBelt utilityBelt = Data.UtilityBelts[name];
		GameObject gameObject = PrefabManager.Instance.CreateClonedPrefab(utilityBelt.BaseID, utilityBelt.CloneID);
		gameObject.name = utilityBelt.FinalID;
		CustomItem customItem = new CustomItem(gameObject, fixReference: true);
		customItem.ItemDrop.m_itemData.m_shared.m_name = utilityBelt.Name ?? "";
		customItem.ItemDrop.m_itemData.m_shared.m_description = "";
		StatusEffect armorEffect = GetArmorEffect((string?)jToken["effect"], jToken, "head", ref customItem.ItemDrop.m_itemData.m_shared.m_description);
		if (armorEffect != null)
		{
			customItem.ItemDrop.m_itemData.m_shared.m_equipStatusEffect = armorEffect;
		}
		else
		{
			Log.LogWarning(name + " - No status effect found for provided effect: " + (string?)jToken["effect"]);
		}
		Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
		recipe.name = "Recipe_" + utilityBelt.BaseID;
		List<Piece.Requirement> list = new List<Piece.Requirement>();
		JToken jToken2 = jToken["crafting"];
		int num = 0;
		foreach (JObject item in (IEnumerable<JToken>)(jToken2["items"]!))
		{
			list.Add(MockRequirement.Create((string?)item["item"], (int)item["amount"]));
			list[num].m_amountPerLevel = (int)item["perLevel"];
			num++;
		}
		recipe.m_craftingStation = Mock<CraftingStation>.Create((string?)jToken2["station"]);
		recipe.m_minStationLevel = (int)jToken2["minLevel"];
		recipe.m_resources = list.ToArray();
		recipe.m_item = customItem.ItemDrop;
		CustomRecipe customRecipe = new CustomRecipe(recipe, fixReference: true, fixRequirementReferences: true);
		ItemManager.Instance.AddItem(customItem);
		ItemManager.Instance.AddRecipe(customRecipe);
	}

	public static void AddTieredRecipes(string setName, JToken armor)
	{
		JToken armorSet = armor["armorSet"];
		bool hasHelmet = (string)armorSet["HelmetID"] != "n/a";
		string arg = char.ToUpper(setName[0]) + setName.Substring(1);
		for (int i = (int)armor!["upgrades"]!["startingTier"] + 1; i <= 7; i++)
		{
			Recipe recipe = null;
			Recipe recipe2;
			Recipe recipe3;
			if (setName != "barbarian")
			{
				if (hasHelmet)
				{
					recipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armorSet["HelmetID"]}T{i}_Terraheim_AddNewSets_Add{arg}Armor").GetComponent<ItemDrop>().m_itemData);
				}
				recipe2 = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armorSet["ChestID"]}T{i}_Terraheim_AddNewSets_Add{arg}Armor").GetComponent<ItemDrop>().m_itemData);
				recipe3 = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armorSet["LegsID"]}T{i}_Terraheim_AddNewSets_Add{arg}Armor").GetComponent<ItemDrop>().m_itemData);
			}

            else
			{
				if (hasHelmet)
				{
					recipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armorSet["HelmetID"]}T{i}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>().m_itemData);
				}
				recipe2 = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armorSet["ChestID"]}T{i}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>().m_itemData);
				recipe3 = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{armorSet["LegsID"]}T{i}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>().m_itemData);
			}
			List<Piece.Requirement> list = null;
			if (hasHelmet)
			{
				list = recipe.m_resources.ToList();
			}
			List<Piece.Requirement> list2 = recipe2.m_resources.ToList();
			List<Piece.Requirement> list3 = recipe3.m_resources.ToList();
			if (setName == "s222ilver")
			{
				if (hasHelmet)
				{
					list.Add(new Piece.Requirement
					{
						m_resItem = ObjectDB.instance.GetItemPrefab("HelmetDrake").GetComponent<ItemDrop>(),
						m_amount = 1,
						m_amountPerLevel = 0,
						m_recover = false
					});
				}
				list2.Add(new Piece.Requirement
				{
					m_resItem = ObjectDB.instance.GetItemPrefab("ArmorWolfChest").GetComponent<ItemDrop>(),
					m_amount = 1,
					m_amountPerLevel = 0,
					m_recover = false
				});
				list3.Add(new Piece.Requirement
				{
					m_resItem = ObjectDB.instance.GetItemPrefab("ArmorWolfLegs").GetComponent<ItemDrop>(),
					m_amount = 1,
					m_amountPerLevel = 0,
					m_recover = false
				});
			}
			else if (setName == "barbarian")
			{
				if (hasHelmet)
				{
					list.Add(new Piece.Requirement
					{
						m_resItem = ObjectDB.instance.GetItemPrefab($"{armorSet["HelmetID"]}T{i - 1}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>(),
						m_amount = 1,
						m_amountPerLevel = 0,
						m_recover = false
					});
				}
				list2.Add(new Piece.Requirement
				{
					m_resItem = ObjectDB.instance.GetItemPrefab($"{armorSet["ChestID"]}T{i - 1}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>(),
					m_amount = 1,
					m_amountPerLevel = 0,
					m_recover = false
				});
				list3.Add(new Piece.Requirement
				{
					m_resItem = ObjectDB.instance.GetItemPrefab($"{armorSet["LegsID"]}T{i - 1}_Terraheim_BarbarianArmor_AddNewSets").GetComponent<ItemDrop>(),
					m_amount = 1,
					m_amountPerLevel = 0,
					m_recover = false
				});
			}
			else
			{
				if (hasHelmet)
				{
					list.Add(new Piece.Requirement
					{
						m_resItem = ObjectDB.instance.GetItemPrefab($"{armorSet["HelmetID"]}T{i - 1}_Terraheim_AddNewSets_Add{arg}Armor").GetComponent<ItemDrop>(),
						m_amount = 1,
						m_amountPerLevel = 0,
						m_recover = false
					});
				}
				list2.Add(new Piece.Requirement
				{
					m_resItem = ObjectDB.instance.GetItemPrefab($"{armorSet["ChestID"]}T{i - 1}_Terraheim_AddNewSets_Add{arg}Armor").GetComponent<ItemDrop>(),
					m_amount = 1,
					m_amountPerLevel = 0,
					m_recover = false
				});
				list3.Add(new Piece.Requirement
				{
					m_resItem = ObjectDB.instance.GetItemPrefab($"{armorSet["LegsID"]}T{i - 1}_Terraheim_AddNewSets_Add{arg}Armor").GetComponent<ItemDrop>(),
					m_amount = 1,
					m_amountPerLevel = 0,
					m_recover = false
				});
			}
			if (hasHelmet)
			{
				recipe.m_resources = list.ToArray();
			}
			recipe2.m_resources = list2.ToArray();
			recipe3.m_resources = list3.ToArray();
		}
	}

	public static void AddTieredCape(string setName)
	{
		Recipe recipe = ObjectDB.instance.GetRecipe(ObjectDB.instance.GetItemPrefab($"{setName}T{1}TH").GetComponent<ItemDrop>().m_itemData);
		List<Piece.Requirement> list = recipe.m_resources.ToList();
		list.Add(new Piece.Requirement
		{
			m_resItem = ObjectDB.instance.GetItemPrefab($"{setName}T{0}TH").GetComponent<ItemDrop>(),
			m_amount = 1,
			m_amountPerLevel = 0,
			m_recover = false
		});
		recipe.m_resources = list.ToArray();
	}
}
