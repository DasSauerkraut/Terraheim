using BepInEx;
using UnityEngine;
using Jotunn.Entities;
using Jotunn.Managers;
using Terraheim.ArmorEffects;
using HarmonyLib;
using Terraheim.Utility;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Terraheim
{
    [BepInDependency(Jotunn.Main.ModGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("GoldenJude_BarbarianArmor", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    class Terraheim : BaseUnityPlugin
    {

        public const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "DasSauerkraut";
        private const string ModName = "Terraheim";
        private const string ModVer = "2.0.4";
        public static readonly string ModPath = Path.GetDirectoryName(typeof(Terraheim).Assembly.Location);

        public static bool hasBarbarianArmor = false;
        
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        private readonly Harmony harmony = new Harmony(ModGuid);

        internal static Terraheim Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Log.Init(Logger);

            TranslationUtils.LoadTranslations();
            SetupStatusEffects();

            harmony.PatchAll();

            AssetHelper.Init();
            AssetHelper.SetupVFX();

            hasBarbarianArmor = UtilityFunctions.CheckBarbarian();
            if ((bool)balance["armorChangesEnabled"])
            {
                Armor.ModExistingSets.Init();
                Armor.AddNewSets.Init();
            }
            else
            {
                Log.LogInfo("Terraheim armor changes disabled!");
            }
            

            Log.LogInfo("Patching complete");
        }

        private void SetupStatusEffects()
        {
            //Damage Bonuses
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

            //Stamina Modifiers
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DodgeStamUse>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlockStamUse>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ExtraStamina>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaminaRegen>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AttackStaminaUse>(), fixReference: true));

            //Misc. EFfects
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AmmoConsumption>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BackstabBonus>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HealthIncrease>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DrawMoveSpeed>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlockPowerBonus>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FoodUsage>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HealOnBlock>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WetImmunity>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_IncreaseHarvest>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SneakMovement>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MoveSpeedOnKill>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MoveSpeedOnKillListener>(), fixReference: true));

            //Set Bonuses
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPOnHit>(), fixReference: true)); //Leather
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Pinning>(), fixReference: true)); //Leather
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Pinned>(), fixReference: true)); //Leather
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_PinnedCooldown>(), fixReference: true)); //Leather
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPRegen>(), fixReference: true)); //BronzeOLD
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ArmorOnHitListener>(), fixReference: true)); //BronzeOLD
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ArmorOnHit>(), fixReference: true)); //BronzeOLD
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CritChance>(), fixReference: true)); //IronOLD
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonus>(), fixReference: true)); //Iron
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonusFX>(), fixReference: true)); //Iron 
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Wolftears>(), fixReference: true)); //Silver
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsFX>(), fixReference: true)); //Silver
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsProtectionExhausted>(), fixReference: true)); //Silver
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounter>(), fixReference: true)); //Silver
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterFX>(), fixReference: true)); //Silver
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterExhausted>(), fixReference: true)); //Silver
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Thorns>(), fixReference: true)); //Padded
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DeathMark>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MarkedForDeath>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MarkedForDeathFX>(), fixReference: true)); //Barbarian

            //Challenge Statuses
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeMoveSpeed>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeDodgeBonus>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeSprinter>(), fixReference: true));


            Log.LogInfo("Status Effects Injected");
        }

    }
}
