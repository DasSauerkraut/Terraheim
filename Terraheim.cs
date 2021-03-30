using BepInEx;
using UnityEngine;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using HarmonyLib;

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
        private const string ModVer = "1.1.0";

        private readonly Harmony harmony = new Harmony(ModGuid);

        internal static Terraheim Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            Log.Init(Logger);
            SetupStatusEffects();
            harmony.PatchAll();
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
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AxeDamageBonus>(), fixReference: true));
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

            //Set Bonuses
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPOnHit>(), fixReference: true)); //Leather
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HPRegen>(), fixReference: true)); //Bronze
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CritChance>(), fixReference: true)); //Iron
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_SneakDamageBonus>(), fixReference: true)); //Silver
            ObjectDBHelper.Add(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Thorns>(), fixReference: true)); //Padded


            Log.LogInfo("Status Effects Injected");
        }

    }
}
