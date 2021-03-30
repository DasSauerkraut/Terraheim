using Terraheim.ArmorEffects;
using HarmonyLib;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(SEMan), "ModifyStaminaRegen")]
    public static class StaminaRegenPatch
    {
        public static void Postfix(SEMan __instance, ref float staminaMultiplier)
        {
            if (__instance.HaveStatusEffect("Stamina Regen"))
            {
                SE_StaminaRegen effect = __instance.GetStatusEffect("Stamina Regen") as SE_StaminaRegen;
                staminaMultiplier += effect.GetRegenPercent();
            }

        }
    }
}
