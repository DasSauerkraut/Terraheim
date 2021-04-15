using BepInEx;
using UnityEngine;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using HarmonyLib;
using Terraheim.Utility;

namespace Terraheim
{
    [BepInPlugin(ModGuid, ModName, ModVer)]
    [BepInProcess("valheim.exe")]
    [BepInDependency("ValheimModdingTeam.ValheimLib", BepInDependency.DependencyFlags.HardDependency)]
    class Terraheim : BaseUnityPlugin
    {

        public const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "DasSauerkraut";
        private const string ModName = "Terraheim";
        private const string ModVer = "1.4.0";

        private readonly Harmony harmony = new Harmony(ModGuid);

        internal static Terraheim Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Log.Init(Logger);
            SetupStatusEffects();
            harmony.PatchAll();
            Utility.AssetHelper.Init();
            SetupVFX();
            Armor.ACMod.Init();
            Armor.AddNewSets.Init();
            Armor.AddCirculets.Init();

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

            //Set Bonuses
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPOnHit>(), fixReference: true)); //Leather
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPRegen>(), fixReference: true)); //Bronze
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CritChance>(), fixReference: true)); //IronOLD
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonus>(), fixReference: true)); //Iron
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_FullHPDamageBonusFX>(), fixReference: true)); //Iron 
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Wolftears>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_WolftearsFX>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounter>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AoECounterFX>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Thorns>(), fixReference: true)); //Padded


            Log.LogInfo("Status Effects Injected");
        }

        private void SetupVFX()
        {
            UtilityFunctions.VFXRedTearstone = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXRedTearstone,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = false,
                m_inheritParentScale = false,
                m_randomRotation = false,
                m_scale = true
            };

            UtilityFunctions.VFXDamageAtFullHp = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXDamageAtFullHp,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = false,
                m_inheritParentScale = false,
                m_randomRotation = false,
                m_scale = true
            };

            UtilityFunctions.VFXAoECharged = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXAoECharged,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = true,
                m_inheritParentScale = false,
                m_randomRotation = false,
                m_scale = true
            };
        }

    }
}
