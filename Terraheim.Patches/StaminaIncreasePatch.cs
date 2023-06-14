using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(Player), nameof(Player.GetTotalFoodValue))]
    class StaminaIncreasePatches
    {
        public static void Postfix(Player __instance, ref float stamina)
        {
            if (__instance.GetSEMan().HaveStatusEffect("Extra Stamina"))
            {
                SE_ExtraStamina effect = UtilityFunctions.GetStatusEffectFromName("Extra Stamina", __instance.GetSEMan()) as SE_ExtraStamina;
                //Log.LogInfo($"Total Val Has Effect Stamina ${stamina}");

                stamina += effect.GetStaminaBonus();
                //Log.LogInfo($"Total Val Modded Stamina ${stamina} from effect ${effect.GetStaminaBonus()}");
            }

            if (__instance.GetSEMan().HaveStatusEffect("Sprinter"))
            {
                SE_ChallengeSprinter effect = UtilityFunctions.GetStatusEffectFromName("Sprinter", __instance.GetSEMan()) as SE_ChallengeSprinter;
                Log.LogInfo($"Total Val Has Effect Stamina ${stamina}");

                stamina *= effect.GetTotalStamina();
                Log.LogInfo($"Total Val Modded Stamina ${stamina} from effect ${effect.GetTotalStamina()}");
            }
        }
    }
}