using System.Collections;
using System.Collections.Generic;
using System.IO;
using BepInEx.Bootstrap;
using Jotunn.Entities;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Terraheim.Utility;

internal class UtilityFunctions
{
	public static JObject GetJsonFromFile(string filename)
	{
        string path = Path.Combine(Terraheim.ModPath, filename);
        try
        {
            string json = File.ReadAllText(path);
            return JObject.Parse(json);
        }
		catch (System.IO.FileNotFoundException)
		{
			File.WriteAllText(path, "{}");
            string json = File.ReadAllText(path);
            return JObject.Parse(json);
		}
	}

	public static JObject GetBalanceFile()
	{
		JObject baseBalance = GetJsonFromFile("balance.json");
		JObject userBalance = GetJsonFromFile("userBalance.json");
		baseBalance.Merge(userBalance);
		return baseBalance;
    }

	public static bool HasTooltipEffect(SEMan seman)
	{
		foreach (StatusEffect statusEffect in seman.GetStatusEffects())
		{
			switch (statusEffect.m_name)
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
			case "ShieldFireParryListener":
				return true;
			case "Wyrd Damage":
				return true;
			case "Wyrd Cost":
				return true;
			}
		}
		return false;
	}

	public static IEnumerator PlayDelayedAudio(AudioClip _clip, AudioSource _source, float _delay)
	{
		yield return new WaitForSeconds(_delay);
		_source.PlayOneShot(_clip);
	}

    public static bool IsArmor(ItemDrop.ItemData item)
    {
        ItemDrop.ItemData.ItemType itemType = item.m_shared.m_itemType;
        if (itemType == ItemDrop.ItemData.ItemType.Helmet ||
            itemType == ItemDrop.ItemData.ItemType.Shoulder ||
            itemType == ItemDrop.ItemData.ItemType.Chest ||
            itemType == ItemDrop.ItemData.ItemType.Legs ||
            itemType == ItemDrop.ItemData.ItemType.Utility)
            return true;
        return false;
    }

    public static bool IsBoss(string name)
	{
		return name switch
		{
			"$enemy_eikthyr" => true, 
			"$enemy_gdking" => true, 
			"$enemy_bonemass" => true, 
			"$enemy_dragon" => true, 
			"$enemy_goblinking" => true,
            "$enemy_seekerqueen" => true,
            _ => false, 
		};
	}

	public static bool CheckBarbarian()
	{
		if (File.Exists(Terraheim.ModPath + "/../barbarianArmor.dll"))
		{
			if (Terraheim.hasJudesEquipment)
			{
				Log.LogWarning("Barbarian armor found, but Judes Equipment is also installed. Please disable Barbarian Armor and only use Judes Equipment.");
			}
			else
			{
				Log.LogInfo("Barbarian armor found");
			}
			return true;
		}
		if (Terraheim.hasJudesEquipment)
		{
			return false;
		}
		Log.LogInfo("Barbarian armor not found! If you have not installed Barbarian Armor, you can safely ignore this");
		return false;
	}

	public static bool CheckChaos()
	{
		if (File.Exists(Terraheim.ModPath + "/../ChaosArmor.dll"))
		{
			Log.LogInfo("Chaos Armor Found!");
			return true;
		}
		if (File.Exists(Terraheim.ModPath + "/../AeehyehssReeper-ChaosArmor/ChaosArmor.dll"))
		{
			Log.LogInfo("Chaos Armor Found!");
			return true;
		}
		Log.LogInfo("Chaos armor not found! If you have not installed Chaos Armor, you can safely ignore this");
		return false;
	}

	public static bool CheckJudes()
	{
		if (File.Exists(Terraheim.ModPath + "/../JudesEquipment.dll"))
		{
			Log.LogInfo("Judes Equipment Found!");
			return true;
		}
		if (File.Exists(Terraheim.ModPath + "/../GoldenJude_JudesEquipment/JudesEquipment.dll"))
		{
			Log.LogInfo("Judes Equipment Found!");
			return true;
		}
		Log.LogInfo("Judes Equipment not found! If you have not installed Judes_Equipment, you can safely ignore this.");
		return false;
	}

	public static bool CheckBowPlugin()
	{
		if (Chainloader.PluginInfos.ContainsKey("blacks7ar.BowPlugin"))
		{
			Log.LogInfo("BowPlugin Found!");
			return true;
		}

		return false;
	}

	public static void GetRecipe(ref Recipe recipe, JToken json, string location, bool useName = true)
	{
		List<Piece.Requirement> list = new List<Piece.Requirement>();
		int num = 0;
		foreach (JToken item3 in (IEnumerable<JToken>)(json[location]!))
		{
			if ((string?)item3["item"] == "SalamanderFur")
			{
				Piece.Requirement item = new Piece.Requirement
				{
					m_resItem = SharedResources.SalamanderItem.ItemDrop,
					m_amount = (int)item3["amount"]
				};
				list.Add(item);
			}
			else
			{
				Piece.Requirement item2 = new Piece.Requirement
				{
					m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>((string?)item3["item"]),
					m_amount = (int)item3["amount"],
					m_amountPerLevel = (int)item3["perLevel"]
				};
				list.Add(item2);
			}
			num++;
		}
		if (useName)
		{
			recipe.name = "Recipe_" + json.Path;
		}
		recipe.m_resources = list.ToArray();
		recipe.m_minStationLevel = (int)json["minLevel"];
		if ((string?)json["station"] == "reforger")
		{
			recipe.m_craftingStation = Pieces.Reforger;
		}
		else
		{
			recipe.m_craftingStation = Mock<CraftingStation>.Create((string?)json["station"]);
		}
	}

	public static bool CheckIfVulnerable(Character __instance, HitData hit)
	{
		if ((__instance.GetDamageModifier(HitData.DamageType.Blunt) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Blunt) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_blunt > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Slash) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Slash) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_slash > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Pierce) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Pierce) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_pierce > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Fire) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Fire) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_fire > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Frost) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Frost) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_frost > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Lightning) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Lightning) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_lightning > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Spirit) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Spirit) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_spirit > 0f)
		{
			return true;
		}
		if ((__instance.GetDamageModifier(HitData.DamageType.Poison) == HitData.DamageModifier.Weak || __instance.GetDamageModifier(HitData.DamageType.Poison) == HitData.DamageModifier.VeryWeak) && hit.m_damage.m_poison > 0f)
		{
			return true;
		}
		return false;
	}

    public static StatusEffect GetStatusEffectFromName(string name, SEMan seman)
    {
        foreach (StatusEffect statusEffect in seman.GetStatusEffects())
        {
            if (statusEffect.name == name)
            {
                return statusEffect;
            }
        }
        return null;
    }
}
