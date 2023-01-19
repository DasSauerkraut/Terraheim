using BepInEx;
using UnityEngine;
using Jotunn.Entities;
using Jotunn.Managers;
using Terraheim.ArmorEffects;
using HarmonyLib;
using Terraheim.Utility;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Terraheim.ArmorEffects.ChosenEffects;
using System.Reflection;
using BepInEx.Configuration;
using Jotunn.Configs;

namespace Terraheim
{
    [BepInDependency(Jotunn.Main.ModGuid, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("GoldenJude_BarbarianArmor", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("GoldenJude-Judes_Equipment", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("AeehyehssReeper-ChaosArmor", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("randyknapp.mods.auga", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(ModGuid, ModName, ModVer)]
    class Terraheim : BaseUnityPlugin
    {

        public const string ModGuid = AuthorName + "." + ModName;
        private const string AuthorName = "DasSauerkraut";
        private const string ModName = "Terraheim";
        private const string ModVer = "2.3.0";
        public static readonly string ModPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static bool hasBarbarianArmor = false;
        public static bool hasJudesEquipment = false;
        public static bool hasChaosArmor = false;
        public static bool hasAuga = false;
        public static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        private ConfigEntry<KeyCode> KenningConfig;
        public static ButtonConfig KenningButton;

        private readonly Harmony harmony = new Harmony(ModGuid);

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
            AddButtonConfig();

            hasAuga = Auga.API.IsLoaded();
            hasChaosArmor = UtilityFunctions.CheckChaos();
            hasJudesEquipment = UtilityFunctions.CheckJudes();
            hasBarbarianArmor = UtilityFunctions.CheckBarbarian();

            if (hasBarbarianArmor && hasJudesEquipment)
                Log.LogWarning("Both the Barbarian Armor and Judes Equipment are installed! Consider removing Barbarian Armor as it is included in Judes_Equipment!");
            if ((bool)balance["armorChangesEnabled"])
            {
                Armor.ModExistingSets.Init();
                Armor.AddNewSets.Init();
            }
            else
            {
                Armor.ModExistingSets.RunWeapons();
                Log.LogInfo("Terraheim armor changes disabled!");
            }

            if (hasAuga)
            {
                Log.LogInfo("Auga detected, modifying Auga tooltips for Terraheim.");
                Auga.API.ComplexTooltip_AddItemTooltipCreatedListener(AugaTooltipsForItemEffects);
            }
            Log.LogInfo("Patching complete");
        }

        public static void AugaTooltipsForItemEffects(GameObject complexTooltip, ItemDrop.ItemData item)
        {
            if(UtilityFunctions.IsArmor(item) && item.m_shared.m_equipStatusEffect != null)
            {
                Auga.API.ComplexTooltip_SetTopic(complexTooltip, item.m_shared.m_name);
                string className = "";
                
                foreach (KeyValuePair<string, Data.ArmorSet> armor in Data.ArmorSets)
                {
                    if (armor.Value.HelmetID == item.m_dropPrefab.name || armor.Value.ChestID == item.m_dropPrefab.name || armor.Value.LegsID == item.m_dropPrefab.name)
                    {
                        className = armor.Value.ClassName;
                        break;
                    }
                }
                if(className != "")
                    Auga.API.ComplexTooltip_SetSubtitle(complexTooltip, className);
                GameObject textbox = Auga.API.ComplexTooltip_AddTwoColumnTextBox(complexTooltip);
                Auga.API.TooltipTextBox_AddLine(textbox, "$item_effect_title", "", "$item_effect_equipped");
                Auga.API.TooltipTextBox_AddLine(textbox, "", item.m_shared.m_equipStatusEffect.m_tooltip);
            }
        }

        private void AddButtonConfig()
        {
            Config.SaveOnConfigSet = true;
            KenningConfig = Config.Bind("Client config", "Kenning Keybind", KeyCode.B,
                new ConfigDescription("Key to activate Ken Mode when using a set with the Kenning effect."));

            KenningButton = new ButtonConfig
            {
                Name = "KenningButton",
                Config = KenningConfig,
                HintToken = "$kenning_button"
            };

            InputManager.Instance.AddButton(ModGuid, KenningButton);
        }

        private void AddResources()
        {
            var salRecipe = ScriptableObject.CreateInstance<Recipe>();
            JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
            salRecipe.m_item = AssetHelper.ItemSalamanderFurPrefab.GetComponent<ItemDrop>();
            var itemReqs = new List<Piece.Requirement>();
            int index = 0;

            foreach (var item in balance["salamanderFur"]["crafting"]["items"])
            {
                itemReqs.Add(MockRequirement.Create((string)item["item"], (int)item["amount"]));
                itemReqs[index].m_amountPerLevel = (int)item["perLevel"];
                index++;
            }
            salRecipe.name = $"Recipe_{balance["salamanderFur"].Path}";
            salRecipe.m_resources = itemReqs.ToArray();
            salRecipe.m_craftingStation = Mock<CraftingStation>.Create((string)balance["salamanderFur"]["crafting"]["station"]);
            salRecipe.m_amount = (int)balance["salamanderFur"]["crafting"]["amountCrafted"];

            CustomRecipe SalamanderRecipe = new CustomRecipe(salRecipe, true, true);

            ItemManager.Instance.AddRecipe(SalamanderRecipe);

            SharedResources.SalamanderItem = new CustomItem(AssetHelper.ItemSalamanderFurPrefab, true);
            ItemManager.Instance.AddItem(SharedResources.SalamanderItem);
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
            //ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ShieldFireListener>(), fixReference: true));
            //ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ShieldFireParryListener>(), fixReference: true));
            //ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Afterburn>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AfterburnCooldown>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_CoinDrop>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RestoreResources>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaggerCapacity>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_StaggerDamage>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_AttackSpeed>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Counter>(), fixReference: true));

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
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Mercenary>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Retaliation>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_RetaliationTimer>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Kenning>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_KenningFX>(), fixReference: true)); //Barbarian
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_KenningCounter>(), fixReference: true)); //Barbarian

            //Chosen
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Chosen>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Bloodlust>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Bloated>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_HiddenKnowledge>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Adrenaline>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_BlindingRage>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Pestilence>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_MaddeningVisions>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_Withdrawals>(), fixReference: true));

            //Challenge Statuses
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeMoveSpeed>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeDodgeBonus>(), fixReference: true));
            ItemManager.Instance.AddStatusEffect(new CustomStatusEffect(ScriptableObject.CreateInstance<SE_ChallengeSprinter>(), fixReference: true));


            Log.LogInfo("Status Effects Injected");
        }
    }
}
