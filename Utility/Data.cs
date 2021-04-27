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

        public static Dictionary<string, ArmorSet> ArmorSets = new Dictionary<string, ArmorSet>
        {
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
                    HelmetName = "$item_helmet_drake",
                    ChestName = "$item_chest_wolf",
                    LegsName = "$item_legs_wolf",
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
            }
        };
    }
}
