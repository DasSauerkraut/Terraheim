using HarmonyLib;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class HealPatch
    {
        [HarmonyPatch(typeof(Character), "Heal")]
        static void Postfix(Character __instance, ref SEMan ___m_seman)
        {
            if (___m_seman.HaveStatusEffect("Battle Furor"))
            {
                SE_FullHPDamageBonus effect = ___m_seman.GetStatusEffect("Battle Furor") as SE_FullHPDamageBonus;
                if (__instance.GetHealthPercentage() < effect.GetActivationHP() && effect.m_icon != null)
                {
                    effect.ClearIcon();
                }
                else if (__instance.GetHealthPercentage() >= effect.GetActivationHP() && effect.m_icon == null)
                {
                    effect.SetIcon();
                }
            }
        }
    }
    
}
