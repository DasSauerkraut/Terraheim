using HarmonyLib;
using System.Collections.Generic;
using Terraheim.ArmorEffects;
using ValheimLib;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class FoodUsagePatch
    {
        public void Awake()
        {
            Log.LogInfo("Food Patching Complete");
        }

        static Dictionary<string,float> foodDuration = new Dictionary<string, float>();

        [HarmonyPatch(typeof(Player), "UpdateFood")]
        static void Prefix(ref Player __instance)
        {
            foreach (Player.Food food in __instance.m_foods)
            {
                if (foodDuration.ContainsKey(food.m_name))
                {
                    if (__instance.GetSEMan().HaveStatusEffect("Food Usage"))
                    {
                        if(food.m_item.m_shared.m_foodBurnTime == foodDuration[food.m_name])
                        {
                            SE_FoodUsage effect = __instance.GetSEMan().GetStatusEffect("Food Usage") as SE_FoodUsage;
                            //Log.LogMessage("Dictionary Contains Food " + food.m_name + " base burn " + food.m_item.m_shared.m_foodBurnTime + " Dict Usage " + foodDuration[food.m_name]);
                            food.m_item.m_shared.m_foodBurnTime = foodDuration[food.m_name] * (1 + effect.GetFoodUsage());
                            //Log.LogMessage("mod burn " + food.m_item.m_shared.m_foodBurnTime);
                        }
                    }
                    else if (food.m_item.m_shared.m_foodBurnTime != foodDuration[food.m_name]) food.m_item.m_shared.m_foodBurnTime = foodDuration[food.m_name];
                }
                else
                {
                     //Log.LogMessage("Dictionary Misses Food " + food.m_name + " base burn " + food.m_item.m_shared.m_foodBurnTime);
                     foodDuration.Add(food.m_name, food.m_item.m_shared.m_foodBurnTime);
                     if (__instance.GetSEMan().HaveStatusEffect("Food Usage"))
                     {
                         SE_FoodUsage effect = __instance.GetSEMan().GetStatusEffect("Food Usage") as SE_FoodUsage;
                         //Log.LogMessage("Dictionary Contains Food " + food.m_name + " base burn " + food.m_item.m_shared.m_foodBurnTime + " Dict Usage " + foodDuration[food.m_name]);
                         food.m_item.m_shared.m_foodBurnTime = foodDuration[food.m_name] * (1 + effect.GetFoodUsage());
                         //Log.LogMessage("mod burn " + food.m_item.m_shared.m_foodBurnTime);
                     }
                     else if (food.m_item.m_shared.m_foodBurnTime != foodDuration[food.m_name]) food.m_item.m_shared.m_foodBurnTime = foodDuration[food.m_name];
                     //Log.LogMessage("Dictionary Contains Food " + food.m_name + " base burn " + food.m_item.m_shared.m_foodBurnTime + " Dict Usage " + foodDuration[food.m_name]);
                }
                    
            }
        }
        
    }
}
