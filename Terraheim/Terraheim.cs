using System.Collections.Generic;
using System.IO;
using System.Reflection;
using BepInEx;
using HarmonyLib;
using Jotunn.Entities;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using Terraheim.Armor;
using Terraheim.ArmorEffects;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim;

[BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency("GoldenJude_BarbarianArmor", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("GoldenJude_JudesEquipment", BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("AeehyehssReeper-ChaosArmor", BepInDependency.DependencyFlags.SoftDependency)]
[BepInPlugin(ModGuid, ModName, ModVer)]
internal class Terraheim : BaseUnityPlugin
{
	public const string ModGuid = "DasSauerkraut.Terraheim";

	private const string AuthorName = "DasSauerkraut";

	private const string ModName = "Terraheim";

	private const string ModVer = "2.2.3";

	public static readonly string ModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

	public static bool hasBarbarianArmor = false;

	public static bool hasJudesEquipment = false;

	public static bool hasChaosArmor = false;

	public static bool hasBowPlugin = false;

	public static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

	private readonly Harmony harmony = new Harmony("DasSauerkraut.Terraheim");

	internal static Terraheim Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		TranslationUtils.LoadTranslations();
		SetupStatusEffects();
		harmony.PatchAll();
		AssetHelper.Init();
		AssetHelper.SetupVFX();
		Pieces.Init();
		AddResources();
		hasChaosArmor = UtilityFunctions.CheckChaos();
		hasJudesEquipment = UtilityFunctions.CheckJudes();
		hasBarbarianArmor = UtilityFunctions.CheckBarbarian();
		hasBowPlugin = UtilityFunctions.CheckBowPlugin();
		if (hasBarbarianArmor && hasJudesEquipment)
		{
			Log.LogWarning("Both the Barbarian Armor and Judes Equipment are installed! Consider removing Barbarian Armor as it is included in Judes_Equipment!");
		}
		if ((bool)balance["armorChangesEnabled"])
		{
			ModExistingSets.Init();
			AddNewSets.Init();
		}
		else
		{
			ModExistingSets.RunWeapons();
			Log.LogInfo("Terraheim armor changes disabled!");
		}
		Log.LogInfo("Patching complete");
	}

	private void AddResources()
	{
		Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
		JObject jsonFromFile = UtilityFunctions.GetJsonFromFile("balance.json");
		recipe.m_item = AssetHelper.ItemSalamanderFurPrefab.GetComponent<ItemDrop>();
		List<Piece.Requirement> list = new List<Piece.Requirement>();
		int num = 0;
		foreach (JToken item in (IEnumerable<JToken>)(jsonFromFile["salamanderFur"]!["crafting"]!["items"]!))
		{
			list.Add(MockRequirement.Create((string?)item["item"], (int)item["amount"]));
			list[num].m_amountPerLevel = (int)item["perLevel"];
			num++;
		}
		recipe.name = "Recipe_" + jsonFromFile["salamanderFur"]!.Path;
		recipe.m_resources = list.ToArray();
		recipe.m_craftingStation = Mock<CraftingStation>.Create((string?)jsonFromFile["salamanderFur"]!["crafting"]!["station"]);
		recipe.m_amount = (int)jsonFromFile["salamanderFur"]!["crafting"]!["amountCrafted"];
		CustomRecipe customRecipe = new CustomRecipe(recipe, fixReference: true, fixRequirementReferences: true);
		ItemManager.Instance.AddRecipe(customRecipe);
		SharedResources.SalamanderItem = new CustomItem(AssetHelper.ItemSalamanderFurPrefab, fixReference: true);
		ItemManager.Instance.AddItem(SharedResources.SalamanderItem);
	}

	private void SetupStatusEffects()
	{
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RangedDmgBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DaggerSpearDmgBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_OneHandDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MeleeDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_TwoHandedDmgBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SilverDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SpiritDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_TreeDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MiningBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ThrowingDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DamageVSLowHP>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RangerWeaponBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_PoisonVulnerable>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DodgeStamUse>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlockStamUse>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ExtraStamina>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaminaRegen>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AttackStaminaUse>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AmmoConsumption>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BackstabBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HealthIncrease>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DrawMoveSpeed>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ParryBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlockPowerBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FoodUsage>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HealOnBlock>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WetImmunity>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_IncreaseHarvest>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SneakMovement>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MoveSpeedOnKill>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MoveSpeedOnKillListener>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Hyperarmor>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_TwoHandAttackSpeed>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ShieldFireHeal>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AfterburnCooldown>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CoinDrop>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RestoreResources>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaggerCapacity>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaggerDamage>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPOnHit>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Pinning>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Pinned>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_PinnedCooldown>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPRegen>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ArmorOnHitListener>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ArmorOnHit>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CritChance>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonusFX>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Wolftears>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsFX>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsProtectionExhausted>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounter>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterFX>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterExhausted>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Thorns>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DeathMark>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MarkedForDeath>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MarkedForDeathFX>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Mercenary>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Retaliation>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RetaliationTimer>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Chosen>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Bloodlust>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Bloated>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HiddenKnowledge>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Adrenaline>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlindingRage>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Pestilence>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MaddeningVisions>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Withdrawals>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeMoveSpeed>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeDodgeBonus>(), fixReference: true));
		ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeSprinter>(), fixReference: true));
		Log.LogInfo("Status Effects Injected");
	}
}
