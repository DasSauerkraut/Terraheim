using HarmonyLib;
using Terraheim.ArmorEffects;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(Player), "GetTotalFoodValue")]
    class StaminaIncreasePatches
    {
        public static void Postfix(Player __instance, ref float stamina)
        {
            if (__instance.GetSEMan().HaveStatusEffect("Extra Stamina"))
            {
                SE_ExtraStamina effect = __instance.GetSEMan().GetStatusEffect("Extra Stamina") as SE_ExtraStamina;
                //Log.LogInfo($"Total Val Has Effect Stamina ${stamina}");

                stamina += effect.GetStaminaBonus();
                //Log.LogInfo($"Total Val Modded Stamina ${stamina} from effect ${effect.GetStaminaBonus()}");
            }
        }
    }
}