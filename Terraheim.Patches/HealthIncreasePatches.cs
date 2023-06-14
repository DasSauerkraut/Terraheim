using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
    class HealthIncreasePatches
    {

        public static void Postfix(Player __instance, ref float hp, ref float stamina)
        {
            //Log.LogInfo("Total Val Increasing HP");
            if(__instance.GetSEMan().HaveStatusEffect("Health Increase"))
            {
                //Log.LogInfo($"Total Val Has Effect HP${hp}");
                SE_HealthIncrease effect = UtilityFunctions.GetStatusEffectFromName("Health Increase", __instance.GetSEMan()) as SE_HealthIncrease;
                hp += hp * effect.getHealthBonus();
                //Log.LogInfo($"Total Val Modded HP${hp} from effect ${effect.getHealthBonus()}");
            }
        }
    }

    [HarmonyPatch(typeof(Player), nameof(Player.GetBaseFoodHP))]
    public class HealthIncreasePatches_BaseFoodHP
    {

        public static void Postfix(Player __instance, ref float __result)
        {
            if (__instance.GetSEMan().HaveStatusEffect("Health Increase"))
            {
                //Log.LogMessage($"Base Food HP Has Effect HP${__result}");
                SE_HealthIncrease effect = UtilityFunctions.GetStatusEffectFromName("Health Increase", __instance.GetSEMan()) as SE_HealthIncrease;
                __result += __result * effect.getHealthBonus();
                //Log.LogMessage($"Base Food HP Modded HP${__result}, from effect ${effect.getHealthBonus()}");
            }
        }
    }
}
