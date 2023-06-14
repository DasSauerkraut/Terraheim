using Terraheim.ArmorEffects;
using HarmonyLib;
using Terraheim.Utility;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(SEMan), nameof(SEMan.ModifyStaminaRegen))]
    public static class StaminaRegenPatch
    {
        public static void Postfix(SEMan __instance, ref float staminaMultiplier)
        {
            if (__instance.HaveStatusEffect("Stamina Regen"))
            {
                SE_StaminaRegen effect = UtilityFunctions.GetStatusEffectFromName("Stamina Regen", __instance) as SE_StaminaRegen;
                staminaMultiplier += effect.GetRegenPercent();
            }
            if (__instance.HaveStatusEffect("Sprinter"))
            {
                SE_ChallengeSprinter effect = UtilityFunctions.GetStatusEffectFromName("Sprinter", __instance) as SE_ChallengeSprinter;
                staminaMultiplier += effect.GetRegen();
            }
        }
    }
}
