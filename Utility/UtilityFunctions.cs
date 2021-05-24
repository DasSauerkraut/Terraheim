using System.IO;
using Newtonsoft.Json.Linq;

using BepInEx;


namespace Terraheim.Utility
{
    class UtilityFunctions
    {
        public static JObject GetJsonFromFile(string filename)
        {
            var filePath = Path.Combine(Terraheim.ModPath, filename);
            //Log.LogWarning(filePath);
            string rawText = File.ReadAllText(filePath);
            //Log.LogWarning(rawText);
            return JObject.Parse(rawText);
        }

        public static bool HasTooltipEffect(SEMan seman)
        {
            foreach( var effect in seman.GetStatusEffects())
            {
                switch (effect.m_name)
                {
                    case "One Hand Damage Bonus":
                        return true;
                    case "Block Power Bonus":
                        return true;
                    case "Dagger/Spear Damage Bonus":
                        return true;
                    case "Melee Damage Bonus":
                        return true;
                    case "Ranged Damage Bonus":
                        return true;
                    case "Silver Damage Bonus":
                        return true;
                    case "Spirit Damage Bonus":
                        return true;
                    case "Two Handed Damage Bonus":
                        return true;
                    case "Backstab Bonus":
                        return true;
                    case "Throwing Damage Bonus":
                        return true;
                    case "Ranger Weapon Bonus":
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }
        
        public static bool CheckBarbarian()
        {
            if (!File.Exists(Terraheim.ModPath + "/../barbarianArmor.dll"))
            {
                Log.LogWarning("Barbarian armor not found!");
                return false;
            }
            Log.LogInfo("Barbarian Armor Found!");
            return true;
        }

        public static bool CheckChaos()
        {
            if (File.Exists(Terraheim.ModPath + "/../ChaosArmor.dll"))
            {
                Log.LogInfo("Chaos Armor Found!");
                return true;
            }
            else if (File.Exists(Terraheim.ModPath + "/../AeehyehssReeper-ChaosArmor/ChaosArmor.dll"))
            {
                Log.LogInfo("Chaos Armor Found!");
                return true;
            }
            Log.LogWarning("Chaos armor not found!");
            return false;
        }

        public static bool CheckIfVulnerable(Character __instance, HitData hit)
        {
            if ((__instance.GetDamageModifier(HitData.DamageType.Blunt) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Blunt) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_blunt > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Slash) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Slash) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_slash > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Pierce) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Pierce) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_pierce > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Fire) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Fire) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_fire > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Frost) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Frost) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_frost > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Lightning) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Lightning) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_lightning > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Spirit) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Spirit) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_spirit > 0)
            {
                return true;
            }
            else if ((__instance.GetDamageModifier(HitData.DamageType.Poison) == HitData.DamageModifier.Weak ||
                __instance.GetDamageModifier(HitData.DamageType.Poison) == HitData.DamageModifier.VeryWeak) &&
                hit.m_damage.m_poison > 0)
            {
                return true;
            }
            return false;
        }
    }
}
