using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;
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
            if (UtilityFunctions.HasTooltipEffect(Player.m_localPlayer.GetSEMan()))
            {
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
        public static void UpdateBlockPowerTooltip(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            if(localplayer.GetSEMan().HaveStatusEffect("Block Power Bonus"))
            {
                SE_BlockPowerBonus effect = localplayer.GetSEMan().GetStatusEffect("Block Power Bonus") as SE_BlockPowerBonus;
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
        }

        public static void UpdateDamageTooltip(ref string tooltip, ItemDrop.ItemData item)
        {
            Player localplayer = Player.m_localPlayer;
            SEMan seman = localplayer.GetSEMan();
            float totalMultiplier = 0f;
            float frostDamage = 0f;
            float spiritDamage = 0f;
            if (seman.HaveStatusEffect("One Hand Damage Bonus") && (item.m_shared.m_name.Contains("axe") || item.m_shared.m_name.Contains("battleaxe")))
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("One Hand Damage Bonus") as SE_OneHandDamageBonus;
                totalMultiplier += effect.getDamageBonus();
            }
            if (seman.HaveStatusEffect("Dagger/Spear Damage Bonus") && (item.m_shared.m_name.Contains("spear") || item.m_shared.m_name.Contains("knife")))
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("Dagger/Spear Damage Bonus") as SE_DaggerSpearDmgBonus;
                totalMultiplier += effect.getDamageBonus();
            }
            if (seman.HaveStatusEffect("Melee Damage Bonus") && item.m_shared.m_itemType != ItemDrop.ItemData.ItemType.Bow)
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("Melee Damage Bonus") as SE_MeleeDamageBonus;
                totalMultiplier += effect.getDamageBonus();
            }
            if (seman.HaveStatusEffect("Ranged Damage Bonus") && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("Ranged Damage Bonus") as SE_RangedDmgBonus;
                totalMultiplier += effect.getDamageBonus();
            }
            if (seman.HaveStatusEffect("Two Handed Damage Bonus") && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("Two Handed Damage Bonus") as SE_TwoHandedDmgBonus;
                totalMultiplier += effect.getDamageBonus();
            }
            if (seman.HaveStatusEffect("Wolftears"))
            {
                SE_Wolftears effect = seman.GetStatusEffect("Wolftears") as SE_Wolftears;
                totalMultiplier += effect.GetDamageBonus();
            }
            if (seman.HaveStatusEffect("Battle Furor"))
            {
                SE_FullHPDamageBonus effect = seman.GetStatusEffect("Battle Furor") as SE_FullHPDamageBonus;
                if (localplayer.GetHealthPercentage() >= effect.GetActivationHP())
                {
                    totalMultiplier += effect.GetDamageBonus();
                }
            }
            if (seman.HaveStatusEffect("Silver Damage Bonus") && (item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow || item.m_shared.m_name.Contains("spear") || item.m_shared.m_name.Contains("knife")))
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("Silver Damage Bonus") as SE_SilverDamageBonus;
                spiritDamage += (item.GetDamage().GetTotalDamage() * effect.GetDamageBonus()) / 2;
                frostDamage += (item.GetDamage().GetTotalDamage() * effect.GetDamageBonus()) / 2;
            }
            if (seman.HaveStatusEffect("Spirit Damage Bonus"))
            {
                var effect = localplayer.GetSEMan().GetStatusEffect("Spirit Damage Bonus") as SE_SpiritDamageBonus;
                spiritDamage += effect.GetDamageBonus();
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
            if (localplayer.GetSEMan().HaveStatusEffect("Backstab Bonus"))
            {
                SE_BackstabBonus effect = localplayer.GetSEMan().GetStatusEffect("Backstab Bonus") as SE_BackstabBonus;
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
            if (localplayer.GetSEMan().HaveStatusEffect("Heal On Block"))
            {
                SE_HealOnBlock effect = localplayer.GetSEMan().GetStatusEffect("Heal On Block") as SE_HealOnBlock;

                if ((item.m_shared.m_name.Contains("tower") || item.m_shared.m_name.Contains("shield_serpentscale")) && !item.m_shared.m_description.Contains("Heal"))
                    item.m_shared.m_description += $"\n<color=cyan>Heal {item.GetBaseBlockPower() * effect.GetBlockHeal()} HP on a successful block.</color>";
                else if ((!item.m_shared.m_name.Contains("tower") || !item.m_shared.m_name.Contains("shield_serpentscale")) && !item.m_shared.m_description.Contains("successful parry"))
                    item.m_shared.m_description += $"\n<color=cyan>Heal {item.GetBaseBlockPower() * effect.GetBlockHeal() + (item.GetBaseBlockPower() * item.m_shared.m_timedBlockBonus * effect.GetBlockHeal())} HP on a successful parry.</color>";
            }
        }
    }
}
