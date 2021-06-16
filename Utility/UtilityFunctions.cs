using System.IO;
using Newtonsoft.Json.Linq;
using Jotunn.Entities;
using System.Collections.Generic;
using Jotunn.Managers;
using Jotunn;

namespace Terraheim.Utility
{
    class UtilityFunctions
    {
        public static JObject GetJsonFromFile(string filename)
        {
            //Logger.LogWarning("AAA");
            //Logger.LogInfo(1);
            //Logger.LogInfo(Terraheim.ModPath);
            var filePath = Path.Combine(Terraheim.ModPath, filename);
            //Logger.LogWarning(filePath);
            string rawText = File.ReadAllText(filePath);
            //Logger.LogWarning(rawText);
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
                    case "Parry Bonus Increase":
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }

        public static bool IsBoss(string name)
        {
            switch (name)
            {
                case "$enemy_eikthyr":
                    return true;
                case "$enemy_gdking":
                    return true;
                case "$enemy_bonemass":
                    return true;
                case "$enemy_dragon":
                    return true;
                case "$enemy_goblinking":
                    return true;
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

        public static void GetRecipe(ref Recipe recipe, JToken json, string location, bool useName = true)
        {
            var itemReqs = new List<Piece.Requirement>();
            int index = 0;
            foreach (var item in json[location])
            {
                //Log.LogInfo((string)item["item"]);
                if ((string)item["item"] == "SalamanderFur")
                {
                    var fur = new Piece.Requirement
                    {
                        m_resItem = SharedResources.SalamanderItem.ItemDrop,
                        m_amount = (int)item["amount"],
                    };
                    itemReqs.Add(fur);
                }
                else
                {
                    var req = new Piece.Requirement
                    {
                        m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>((string)item["item"]),
                        m_amount = (int)item["amount"],
                        m_amountPerLevel = (int)item["perLevel"]
                    };
                    //itemReqs.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                    //itemReqs[index].m_amountPerLevel = (int)item["perLevel"];
                    itemReqs.Add(req);
                }
                index++;
            }
            if (useName)
                recipe.name = $"Recipe_{json.Path}";
            //Log.LogInfo("reqs obj " + itemReqs[0].m_resItem.m_itemData.m_shared.m_name);
            recipe.m_resources = itemReqs.ToArray();
            //Log.LogInfo("res obj " + recipe.m_resources[0].m_resItem.m_itemData.m_shared.m_name);

            recipe.m_minStationLevel = (int)json["minLevel"];
            if ((string)json["station"] == "reforger")
                recipe.m_craftingStation = Pieces.Reforger;
            else
                recipe.m_craftingStation = Mock<CraftingStation>.Create((string)json["station"]);
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
