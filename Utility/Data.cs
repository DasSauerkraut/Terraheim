using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraheim.Utility
{
    class Data
    {
        public static EffectList.EffectData VFXRedTearstone
        {
            get; set;
        }

        public static EffectList.EffectData VFXDamageAtFullHp
        {
            get; set;
        }

        public static EffectList.EffectData VFXAoECharged
        {
            get; set;
        }

        public static EffectList.EffectData VFXMarkedForDeath
        {
            get; set;
        }

        public static EffectList.EffectData VFXPinned
        {
            get; set;
        }

        public static EffectList.EffectData VFXAfterburn
        {
            get; set;
        }

        public class ArmorSet
        {
            public string HelmetID { get; set; }
            public string ChestID { get; set; }
            public string LegsID { get; set; }
            public string HelmetName { get; set; }
            public string ChestName { get; set; }
            public string LegsName { get; set; }
            public string ClassName { get; set; }
            public int HelmetArmor { get; set; }
        }

        public class UtilityBelt
        {
            public string BaseID { get; set; }
            public string FinalID { get; set; }
            public string CloneID { get; set; }
            public string Name { get; set; }
        }

        public static Dictionary<string, ArmorSet> ArmorSets = new Dictionary<string, ArmorSet>
        {
            {"rags", new ArmorSet
                {
                    HelmetID = "n/a",
                    ChestID = "ArmorRagsChest",
                    LegsID = "ArmorRagsLegs",
                    HelmetName = "n/a",
                    ChestName = "$item_chest_rags_t",
                    LegsName = "$item_legs_rags_t",
                    ClassName = "$class_challenge",
                    HelmetArmor = 0
                }
            },
            {"leather", new ArmorSet
                {
                    HelmetID = "HelmetLeather",
                    ChestID = "ArmorLeatherChest",
                    LegsID = "ArmorLeatherLegs",
                    HelmetName = "$item_helmet_leather_t",
                    ChestName = "$item_chest_leather_t",
                    LegsName = "$item_legs_leather_t",
                    ClassName = "$class_berserker",
                    HelmetArmor = 0
                }
            },
            {"trollLeather", new ArmorSet
                {
                    HelmetID = "HelmetTrollLeather",
                    ChestID = "ArmorTrollLeatherChest",
                    LegsID = "ArmorTrollLeatherLegs",
                    HelmetName = "$item_helmet_trollleather_t",
                    ChestName = "$item_chest_trollleather_t",
                    LegsName = "$item_legs_trollleather_t",
                    ClassName = "$class_ranger",
                    HelmetArmor = 0
                }
            },
            {"bronze", new ArmorSet
                {
                    HelmetID = "HelmetBronze",
                    ChestID = "ArmorBronzeChest",
                    LegsID = "ArmorBronzeLegs",
                    HelmetName = "$item_helmet_bronze_t",
                    ChestName = "$item_chest_bronze_t",
                    LegsName = "$item_legs_bronze_t",
                    ClassName = "$class_tank",
                    HelmetArmor = 0


                }
            },
            {"iron", new ArmorSet
                {
                    HelmetID = "HelmetIron",
                    ChestID = "ArmorIronChest",
                    LegsID = "ArmorIronLegs",
                    HelmetName = "$item_helmet_iron_t",
                    ChestName = "$item_chest_iron_t",
                    LegsName = "$item_legs_iron_t",
                    ClassName = "$class_berserker",
                    HelmetArmor = 2
                }
            },
            {"silver", new ArmorSet
                {
                    HelmetID = "HelmetDrake",
                    ChestID = "ArmorWolfChest",
                    LegsID = "ArmorWolfLegs",
                    HelmetName = "$item_helmet_drake_t",
                    ChestName = "$item_chest_wolf_t",
                    LegsName = "$item_legs_wolf_t",
                    ClassName = "$class_ranger",
                    HelmetArmor = 2
                }
            },
            {"padded", new ArmorSet
                {
                    HelmetID = "HelmetPadded",
                    ChestID = "ArmorPaddedCuirass",
                    LegsID = "ArmorPaddedGreaves",
                    HelmetName = "$item_helmet_padded_t",
                    ChestName = "$item_chest_padded_t",
                    LegsName = "$item_legs_padded_t",
                    ClassName = "$class_tank",
                    HelmetArmor = 2
                }
            },
            {"barbarian", new ArmorSet
                {
                    HelmetID = "ArmorBarbarianBronzeHelmetJD",
                    ChestID = "ArmorBarbarianBronzeChestJD",
                    LegsID = "ArmorBarbarianBronzeLegsJD",
                    HelmetName = "$item_helmet_barbarian_t",
                    ChestName = "$item_chest_barbarian_t",
                    LegsName = "$item_legs_barbarian_t",
                    ClassName = "$class_thrower",
                    HelmetArmor = 1
                }
            },
            {"plate", new ArmorSet
                {
                    HelmetID = "ArmorPlateIronHelmetJD",
                    ChestID = "ArmorPlateIronChestJD",
                    LegsID = "ArmorPlateIronLegsJD",
                    HelmetName = "$item_helmet_plate_t",
                    ChestName = "$item_chest_plate_t",
                    LegsName = "$item_legs_plate_t",
                    ClassName = "$class_tank",
                    HelmetArmor = 1
                }
            },
            {"nomad", new ArmorSet
                {
                    HelmetID = "ArmorBlackmetalgarbHelmet",
                    ChestID = "ArmorBlackmetalgarbChest",
                    LegsID = "ArmorBlackmetalgarbLegs",
                    HelmetName = "$item_helmet_nomad_t",
                    ChestName = "$item_chest_nomad_t",
                    LegsName = "$item_legs_nomad_t",
                    ClassName = "$class_ranger",
                    HelmetArmor = 1
                }
            },
            {"chaosT0", new ArmorSet
                {
                    HelmetID = "T1ChaosPlateArmor",
                    ChestID = "T1ChaosPlateHelm",
                    LegsID = "T1ChaosPlateLegs",
                    HelmetName = "$item_helmet_chaos_t",
                    ChestName = "$item_chest_chaos_t",
                    LegsName = "$item_legs_chaos_t",
                    ClassName = "$class_berserker",
                    HelmetArmor = 0
                }
            },
            {"chaosT1", new ArmorSet
                {
                    HelmetID = "T2ChaosPlateArmor",
                    ChestID = "T2ChaosPlateHelm",
                    LegsID = "T2ChaosPlateLegs",
                    HelmetName = "$item_helmet_chaos_t1",
                    ChestName = "$item_chest_chaos_t1",
                    LegsName = "$item_legs_chaos_t1",
                    ClassName = "$class_berserker",
                    HelmetArmor = 0
                }
            },
            {"chaosT2", new ArmorSet
                {
                    HelmetID = "ChaosPlateHelm",
                    ChestID = "ChaosPlateArmorBody",
                    LegsID = "ChaosPlateLegs",
                    HelmetName = "$item_helmet_chaos_t2",
                    ChestName = "$item_chest_chaos_t2",
                    LegsName = "$item_legs_chaos_t2",
                    ClassName = "$class_berserker",
                    HelmetArmor = 0
                }
            }
        };

        public static readonly Dictionary<string, UtilityBelt> UtilityBelts = new Dictionary<string, UtilityBelt>()
        {
            {"woodsmanHelmet", new UtilityBelt
                {
                    BaseID = "HelmetWoodsman",
                    CloneID = "BeltStrength",
                    FinalID = "HelmetWoodsman_Terraheim_AddCirculets_AddWoodsmanCirculet",
                    Name = "$item_helmet_woodsman"
                }
            },
            {"minersBelt", new UtilityBelt
                {
                    BaseID = "BeltMiner",
                    CloneID = "BeltStrength",
                    FinalID = "BeltMiner_Terraheim_AddCirculets_AddMinersCirculet",
                    Name = "$item_belt_miner"
                }
            },
            {"waterproofBelt", new UtilityBelt
                {
                    BaseID = "BeltWaterproof",
                    CloneID = "BeltStrength",
                    FinalID = "BeltWaterproof_Terraheim_AddCirculets_AddWaterproofBelt",
                    Name = "$item_belt_waterproof"
                }
            },
            {"farmerBelt", new UtilityBelt
                {
                    BaseID = "BeltFarmer",
                    CloneID = "BeltStrength",
                    FinalID = "BeltFarmer_Terraheim_AddCirculets_AddFarmerBelt",
                    Name = "$item_belt_farmer"
                }
            },
            {"thiefBelt", new UtilityBelt
                {
                    BaseID = "BeltThief",
                    CloneID = "BeltStrength",
                    FinalID = "BeltThief_Terraheim_AddCirculets_AddThiefBelt",
                    Name = "$item_belt_thief"
                }
            },
        };
    }
}
