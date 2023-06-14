﻿using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using System;
using UnityEngine;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(ItemDrop.ItemData))]
    class TooltipPatch
    {
        public void Awake()
        {
            Log.LogInfo("Tooltip Patching Complete");
        }

        [HarmonyPostfix]
        [HarmonyPatch("GetTooltip", new Type[]
            {
                typeof(ItemDrop.ItemData),
                typeof(int),
                typeof(bool)
            }
        )]
        public static void GetTooltipPostfix(ref string __result, ItemDrop.ItemData item, int qualityLevel, bool crafting)
        {
            if (item != null && Player.m_localPlayer != null && UtilityFunctions.HasTooltipEffect(Player.m_localPlayer.GetSEMan()))
            {
                if (item.m_shared.m_name.Contains("staff")){
                    UpdateMagicDamageTooltip(ref __result, item);
                    UpdateMagicCostToolTip(ref __result, item);
                }
                if (item.IsWeapon())
                {
                    UpdateBlockPowerTooltip(ref __result, item);
                    UpdateDamageTooltip(ref __result, item);
                    UpdateBackstabBonus(ref __result, item);
                }
                if (item.m_shared.m_name.Contains("shield"))
                {
                    UpdateBlockPowerTooltip(ref __result, item);
                    UpdateHealOnBlockTooltip(ref __result, item);
                }
                
            }
        }
        public static void UpdateMagicDamageTooltip(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            SEMan seman = localplayer.GetSEMan();
            float totalMultiplier = 0f;
            if(seman.HaveStatusEffect("Wyrd Damage"))
            {
                SE_MageDamage effect = UtilityFunctions.GetStatusEffectFromName("Wyrd Damage", localplayer.GetSEMan()) as SE_MageDamage;
                totalMultiplier += effect.GetMageDmg();
            }

            if (totalMultiplier > 0f)
            {
                var damages = item.m_shared.m_damages;
                if (damages.m_fire != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_fire * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_fire * max);
                    string toolString = $"$inventory_fire: <color=orange>{item.GetDamage().m_fire}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_fire + item.GetDamage().m_fire * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_fire + item.GetDamage().m_fire * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_lightning != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_lightning * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_lightning * max);
                    string toolString = $"$inventory_lightning: <color=orange>{item.GetDamage().m_lightning}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_lightning + item.GetDamage().m_lightning * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_lightning + item.GetDamage().m_lightning * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_poison != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_poison * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_poison * max);
                    string toolString = $"$inventory_poison: <color=orange>{item.GetDamage().m_poison}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_poison + item.GetDamage().m_poison * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_poison + item.GetDamage().m_poison * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_frost != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_frost * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_frost * max);
                    string toolString = $"$inventory_frost: <color=orange>{item.GetDamage().m_frost}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_frost + item.GetDamage().m_frost * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_frost + item.GetDamage().m_frost * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_spirit != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_spirit * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_spirit * max);
                    string toolString = $"$inventory_spirit: <color=orange>{item.GetDamage().m_spirit}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_spirit + item.GetDamage().m_spirit * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_spirit + item.GetDamage().m_spirit * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
            }
        }

        public static void UpdateMagicCostToolTip(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            if (localplayer == null)
                return;
            if (localplayer.GetSEMan().HaveStatusEffect("Wyrd Cost"))
            {
                SE_MageCost effect = UtilityFunctions.GetStatusEffectFromName("Wyrd Cost", localplayer.GetSEMan()) as SE_MageCost;
                string hpString = $"$item_healthuse: <color=orange>{item.m_shared.m_attack.m_attackHealthPercentage:#.0}%</color>";
                var index = tooltip.IndexOf(hpString);
                if (index > -1)
                {
                    var hpUse = item.m_shared.m_attack.m_attackHealthPercentage * (1f - effect.GetMageCst());
                    tooltip = tooltip.Insert(index + hpString.Length, $" <color=orange>|</color> <color=cyan>({hpUse:#.0}%)</color>");
                }

                string eitrString = $"$item_eitruse: <color=orange>{item.m_shared.m_attack.m_attackEitr}</color>";
                var index2 = tooltip.IndexOf(eitrString);
                if (index2 > -1)
                {
                    var eitrUse = item.m_shared.m_attack.m_attackEitr * (1f - effect.GetMageCst());
                    tooltip = tooltip.Insert(index2 + eitrString.Length, $" <color=orange>|</color> <color=cyan>({eitrUse:#.#})</color>");
                }
            }
        }

        public static void UpdateBlockPowerTooltip(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            if(localplayer.GetSEMan().HaveStatusEffect("Block Power Bonus"))
            {
                SE_BlockPowerBonus effect = UtilityFunctions.GetStatusEffectFromName("Block Power Bonus", localplayer.GetSEMan()) as SE_BlockPowerBonus;
                string blockPowerString = $"$item_blockpower: <color=orange>{item.m_shared.m_blockPower}</color> <color=yellow>({item.m_shared.m_blockPower})</color>";
                var index = tooltip.IndexOf(blockPowerString);
                if( index > -1)
                {
                    var blockBonus = item.m_shared.m_blockPower * (1+effect.GetBlockPower());
                    tooltip = tooltip.Insert(index + blockPowerString.Length, $" <color=orange>|</color> <color=cyan>({blockBonus:#.##})</color>");
                } else
                {
                    blockPowerString = $"$item_blockpower: <color=orange>{item.m_shared.m_blockPower}</color>";
                    index = tooltip.IndexOf(blockPowerString);
                    if (index > -1)
                    {
                        var blockBonus = item.m_shared.m_blockPower * (1 + effect.GetBlockPower());
                        tooltip = tooltip.Insert(index + blockPowerString.Length, $" <color=orange>|</color> <color=cyan>({blockBonus:#.##})</color>");
                    }
                }
            }
            if(localplayer.GetSEMan().HaveStatusEffect("Parry Bonus Increase"))
            {
                //Log.LogInfo(item.m_shared.m_timedBlockBonus);
                SE_ParryBonus effect = UtilityFunctions.GetStatusEffectFromName("Parry Bonus Increase", localplayer.GetSEMan()) as SE_ParryBonus;
                string blockPowerString = $"$item_parrybonus: <color=orange>{item.m_shared.m_timedBlockBonus}x</color>";
                var index = tooltip.IndexOf(blockPowerString);
                if (index > -1)
                {
                    var blockBonus = item.m_shared.m_timedBlockBonus * (1 + effect.GetParryBonus());
                    tooltip = tooltip.Insert(index + blockPowerString.Length, $" <color=orange>|</color> <color=cyan>{blockBonus:#.##}x</color>");
                }
            }
        }

        public static void UpdateDamageTooltip(ref string tooltip, ItemDrop.ItemData item)
        {

            Player localplayer = Player.m_localPlayer;
            SEMan seman = localplayer.GetSEMan();
            float totalMultiplier = 0f;
            float frostDamage = 0f;
            float spiritDamage = 0f;

            if (item == null || seman == null || localplayer == null || item.m_shared == null || item.m_shared.m_name == null )
                return;

            if (seman.HaveStatusEffect("One Hand Damage Bonus") && (item.m_shared.m_name.Contains("axe") || item.m_shared.m_name.Contains("battleaxe")))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("One Hand Damage Bonus", localplayer.GetSEMan()) as SE_OneHandDamageBonus;
                totalMultiplier += effect.getDamageBonus();
            }

            if (seman.HaveStatusEffect("Dagger/Spear Damage Bonus") && (item.m_shared.m_name.Contains("spear") || item.m_shared.m_name.Contains("knife")))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Dagger/Spear Damage Bonus", localplayer.GetSEMan()) as SE_DaggerSpearDmgBonus;
                totalMultiplier += effect.getDamageBonus();
            }

            if (seman.HaveStatusEffect("Melee Damage Bonus") && item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Melee Damage Bonus", localplayer.GetSEMan()) as SE_MeleeDamageBonus;
                totalMultiplier += effect.getDamageBonus();
            }

            if (seman.HaveStatusEffect("Ranged Damage Bonus") && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Ranged Damage Bonus", localplayer.GetSEMan()) as SE_RangedDmgBonus;
                totalMultiplier += effect.getDamageBonus();
            }

            if (seman.HaveStatusEffect("Two Handed Damage Bonus") && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Two Handed Damage Bonus", localplayer.GetSEMan()) as SE_TwoHandedDmgBonus;
                totalMultiplier += effect.getDamageBonus();
            }

            if (seman.HaveStatusEffect("Ranger Weapon Bonus") && (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || item.m_shared.m_name.Contains("spear") || item.m_shared.m_name.Contains("knife")))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Ranger Weapon Bonus", localplayer.GetSEMan()) as SE_RangerWeaponBonus;
                totalMultiplier += effect.GetDamageBonus();
            }

            if (seman.HaveStatusEffect("Throwing Damage Bonus") && item.m_shared.m_name.Contains("_throwingaxe") ||
                item.m_shared.m_name.Contains("_spear") ||
                item.m_shared.m_name.Contains("bomb"))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Throwing Damage Bonus", seman) as SE_ThrowingDamageBonus;
                if(effect != null)
                    totalMultiplier += effect.getDamageBonus();
            }
            if (seman.HaveStatusEffect("Wolftears"))
            {
                SE_Wolftears effect = UtilityFunctions.GetStatusEffectFromName("Wolftears", seman) as SE_Wolftears;
                totalMultiplier += effect.GetDamageBonus();
            }
            if (seman.HaveStatusEffect("Battle Furor"))
            {
                SE_FullHPDamageBonus effect = UtilityFunctions.GetStatusEffectFromName("Battle Furor", seman) as SE_FullHPDamageBonus;
                if (localplayer.GetHealthPercentage() >= effect.GetActivationHP())
                {
                    totalMultiplier += effect.GetDamageBonus();
                }
            }
            if (seman.HaveStatusEffect("Silver Damage Bonus") && (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || item.m_shared.m_name.Contains("spear") || item.m_shared.m_name.Contains("knife")))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Silver Damage Bonus", localplayer.GetSEMan()) as SE_SilverDamageBonus;
                spiritDamage += (item.GetDamage().GetTotalDamage() * effect.GetDamageBonus()) / 2;
                frostDamage += (item.GetDamage().GetTotalDamage() * effect.GetDamageBonus()) / 2;
            }
            if (seman.HaveStatusEffect("Spirit Damage Bonus"))
            {
                var effect = UtilityFunctions.GetStatusEffectFromName("Spirit Damage Bonus", localplayer.GetSEMan()) as SE_SpiritDamageBonus;
                spiritDamage += effect.GetDamageBonus();
            }
            if (seman.HaveStatusEffect("ShieldFireParryListener"))
            {
                float damageBns = 0f;
                if (item.m_shared.m_name.Contains("knife_fire"))
                    damageBns = 5f;
                else if (item.m_shared.m_name.Contains("spear_fire"))
                    damageBns = 15f;
                if(damageBns > 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);

                    int minRange = Mathf.RoundToInt(item.GetDamage().m_damage * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_damage * max);
                    string toolString = $"$inventory_damage: <color=orange>{item.GetDamage().m_damage}</color> <color=yellow>({minRange}-{maxRange}) </color>";

                    var dmgBonusMin = Mathf.RoundToInt((item.GetDamage().m_damage + damageBns) * min);
                    var dmgBonusMax = Mathf.RoundToInt((item.GetDamage().m_damage + damageBns) * max);

                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.##}-{dmgBonusMax:#.##})</color>");
                    }
                    else
                    {
                        string newString = $"$item_blockpower: <color=orange>{item.m_shared.m_blockPower}</color> <color=yellow>({item.m_shared.m_blockPower})</color>";
                        index = tooltip.IndexOf(newString);
                        if (index > -1)
                        {
                            tooltip = tooltip.Insert(index, $"<color=cyan>$inventory_damage: {item.GetDamage().m_damage + damageBns} ({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>\n");
                        }
                    }
                }
            }
            if (totalMultiplier > 0f)
            {
                var damages = item.m_shared.m_damages;
                if (damages.m_blunt != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_blunt * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_blunt * max);
                    string bluntString = $"$inventory_blunt: <color=orange>{item.GetDamage().m_blunt}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(bluntString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_blunt + item.GetDamage().m_blunt * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_blunt + item.GetDamage().m_blunt * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + bluntString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_slash != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_slash * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_slash * max);
                    string toolString = $"$inventory_slash: <color=orange>{item.GetDamage().m_slash}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_slash + item.GetDamage().m_slash * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_slash + item.GetDamage().m_slash * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_pierce != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_pierce * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_pierce * max);
                    string toolString = $"$inventory_pierce: <color=orange>{item.GetDamage().m_pierce}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_pierce + item.GetDamage().m_pierce * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_pierce + item.GetDamage().m_pierce * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_fire != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_fire * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_fire * max);
                    string toolString = $"$inventory_fire: <color=orange>{item.GetDamage().m_fire}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_fire + item.GetDamage().m_fire * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_fire + item.GetDamage().m_fire * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                
                if (damages.m_lightning != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_lightning * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_lightning * max);
                    string toolString = $"$inventory_lightning: <color=orange>{item.GetDamage().m_lightning}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_lightning + item.GetDamage().m_lightning * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_lightning + item.GetDamage().m_lightning * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
                if (damages.m_poison != 0f)
                {
                    localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                    int minRange = Mathf.RoundToInt(item.GetDamage().m_poison * min);
                    int maxRange = Mathf.RoundToInt(item.GetDamage().m_poison * max);
                    string toolString = $"$inventory_poison: <color=orange>{item.GetDamage().m_poison}</color> <color=yellow>({minRange}-{maxRange}) </color>";
                    var index = tooltip.IndexOf(toolString);
                    if (index > -1)
                    {
                        var dmgBonusMin = (item.GetDamage().m_poison + item.GetDamage().m_poison * totalMultiplier) * min;
                        var dmgBonusMax = (item.GetDamage().m_poison + item.GetDamage().m_poison * totalMultiplier) * max;
                        tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                    }
                }
            }
            if (spiritDamage > 0f)
            {
                localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);

                int minRange = Mathf.RoundToInt(item.GetDamage().m_spirit * min);
                int maxRange = Mathf.RoundToInt(item.GetDamage().m_spirit * max);
                string toolString = $"$inventory_spirit: <color=orange>{item.GetDamage().m_spirit}</color> <color=yellow>({minRange}-{maxRange}) </color>";

                var dmgBonusMin = Mathf.RoundToInt((item.GetDamage().m_spirit + spiritDamage) * min);
                var dmgBonusMax = Mathf.RoundToInt((item.GetDamage().m_spirit + spiritDamage) * max);

                var index = tooltip.IndexOf(toolString);
                if (index > -1)
                {
                    tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.##}-{dmgBonusMax:#.##})</color>");
                }
                else
                {
                    string newString = $"$item_blockpower: <color=orange>{item.m_shared.m_blockPower}</color> <color=yellow>({item.m_shared.m_blockPower})</color>";
                    index = tooltip.IndexOf(newString);
                    if (index > -1)
                    {
                        tooltip = tooltip.Insert(index, $"<color=cyan>$inventory_spirit: {item.GetDamage().m_spirit + spiritDamage} ({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>\n");
                    }
                }
            }
            if (frostDamage > 0f)
            {
                localplayer.GetSkills().GetRandomSkillRange(out var min, out var max, item.m_shared.m_skillType);
                int minRange = Mathf.RoundToInt(item.GetDamage().m_frost * min);
                int maxRange = Mathf.RoundToInt(item.GetDamage().m_frost * max);
                string toolString = $"$inventory_frost: <color=orange>{item.GetDamage().m_frost}</color> <color=yellow>({minRange}-{maxRange}) </color>";

                var dmgBonusMin = ((item.GetDamage().m_frost + frostDamage) * min);
                var dmgBonusMax = ((item.GetDamage().m_frost + frostDamage) * max);

                var index = tooltip.IndexOf(toolString);
                if (index > -1)
                {
                    tooltip = tooltip.Insert(index + toolString.Length, $"<color=orange>|</color> <color=cyan>({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>");
                }
                else
                {
                    string newString = $"$item_blockpower: <color=orange>{item.m_shared.m_blockPower}</color> <color=yellow>({item.m_shared.m_blockPower})</color>";
                    index = tooltip.IndexOf(newString);
                    if (index > -1)
                    {
                        tooltip = tooltip.Insert(index, $"<color=cyan>$inventory_frost: {item.GetDamage().m_frost + frostDamage} ({dmgBonusMin:#.#}-{dmgBonusMax:#.#})</color>\n");
                    }
                }
            }
        }

        public static void UpdateBackstabBonus(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            if (localplayer == null)
                return;
            if (localplayer.GetSEMan().HaveStatusEffect("Backstab Bonus"))
            {
                SE_BackstabBonus effect = UtilityFunctions.GetStatusEffectFromName("Backstab Bonus", localplayer.GetSEMan()) as SE_BackstabBonus;
                string backstabString = $"$item_backstab: <color=orange>{item.m_shared.m_backstabBonus}x</color>";
                var index = tooltip.IndexOf(backstabString);
                if (index > -1)
                {
                    var backstabBonus = item.m_shared.m_backstabBonus + effect.getBackstabBonus();
                    tooltip = tooltip.Insert(index + backstabString.Length, $" <color=orange>|</color> <color=cyan>({backstabBonus:#.#}x)</color>");
                }
            }
        }

        public static void UpdateHealOnBlockTooltip(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            if (localplayer == null)
                return;
            if (localplayer.GetSEMan().HaveStatusEffect("Heal On Block"))
            {
                SE_HealOnBlock effect = UtilityFunctions.GetStatusEffectFromName("Heal On Block", localplayer.GetSEMan()) as SE_HealOnBlock;

                if ((item.m_shared.m_name.Contains("tower") || item.m_shared.m_name.Contains("shield_serpentscale")) && !item.m_shared.m_description.Contains("Heal"))
                    item.m_shared.m_description += $"\n<color=cyan>Heal {item.GetBaseBlockPower() * effect.GetBlockHeal()} HP on a successful block.</color>";
                else if ((!item.m_shared.m_name.Contains("tower") || !item.m_shared.m_name.Contains("shield_serpentscale")) && !item.m_shared.m_description.Contains("successful parry"))
                    item.m_shared.m_description += $"\n<color=cyan>Heal {item.GetBaseBlockPower() * effect.GetBlockHeal() + (item.GetBaseBlockPower() * item.m_shared.m_timedBlockBonus * effect.GetBlockHeal())} HP on a successful parry.</color>";
            }
        }
    }
}
