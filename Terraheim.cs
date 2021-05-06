using BepInEx;
using UnityEngine;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using HarmonyLib;
using Terraheim.Utility;
using System.IO;

namespace Terraheim
{
    [BepInDependency("ValheimModdingTeam.ValheimLib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("GoldenJude_BarbarianArmor", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInProcess("valheim.exe")]

    class Terraheim : BaseUnityPlugin
    {

        public const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "DasSauerkraut";
        private const string ModName = "Terraheim";
        private const string ModVer = "1.7.4";
        public static readonly string ModPath = Path.GetDirectoryName(typeof(Terraheim).Assembly.Location);

        public static bool hasBarbarianArmor = false;

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

            Armor.ACMod.Init();
            Armor.AddNewSets.Init();
            Armor.AddCirculets.Init();

            Armor.BarbarianArmor.Integrate();
            if (hasBarbarianArmor)
                Armor.BarbarianArmor.Init();

            Log.LogInfo("Patching complete");
        }

        private void SetupStatusEffects()
        {
            //Damage Bonuses
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RangedDmgBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DaggerSpearDmgBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_OneHandDamageBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MeleeDamageBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_TwoHandedDmgBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SilverDamageBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SpiritDamageBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_TreeDamageBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MiningBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ThrowingDamageBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DamageVSLowHP>(), fixReference: true));

            //Stamina Modifiers
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DodgeStamUse>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlockStamUse>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ExtraStamina>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaminaRegen>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AttackStaminaUse>(), fixReference: true));

            //Misc. EFfects
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AmmoConsumption>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BackstabBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HealthIncrease>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DrawMoveSpeed>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlockPowerBonus>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FoodUsage>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HealOnBlock>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WetImmunity>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_IncreaseHarvest>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SneakMovement>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ThrowingWeaponVelocity>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MoveSpeedOnKill>(), fixReference: true));
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MoveSpeedOnKillListener>(), fixReference: true));

            //Set Bonuses
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPOnHit>(), fixReference: true)); //Leather
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPRegen>(), fixReference: true)); //Bronze
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CritChance>(), fixReference: true)); //IronOLD
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonus>(), fixReference: true)); //Iron
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonusFX>(), fixReference: true)); //Iron 
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Wolftears>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsFX>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsProtectionExhausted>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounter>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterFX>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterExhausted>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Thorns>(), fixReference: true)); //Padded
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_DeathMark>(), fixReference: true)); //Barbarian
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MarkedForDeath>(), fixReference: true)); //Barbarian
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MarkedForDeathFX>(), fixReference: true)); //Barbarian




            Log.LogInfo("Status Effects Injected");
        }

        

    }
}
