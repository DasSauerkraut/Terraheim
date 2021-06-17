using HarmonyLib;
using Terraheim.ArmorEffects;

namespace Terraheim.Patches
{
    [HarmonyPatch(typeof(SEMan), "ModifyHealthRegen")]
    class HPRegenPatch
    {
        public static void Postfix(SEMan __instance, ref float regenMultiplier)
        {
            //Log.LogInfo("Total Val Increasing HP");
            if ( __instance.HaveStatusEffect("HP Regen"))
            {
                //Log.LogInfo($"Total Val Has Effect HP${hp}");
                SE_HPRegen effect = __instance.GetStatusEffect("HP Regen") as SE_HPRegen;
                //Player player = __instance.m_character as Player;
                regenMultiplier += effect.getHealPercent();
                //Log.LogInfo($"Total Val Modded HP${hp} from effect ${effect.getHealthBonus()}");

            } else if (__instance.HaveStatusEffect("Bloated"))
            {
                SE_Bloated effect = __instance.GetStatusEffect("Bloated") as SE_Bloated;
                //Player player = __instance.m_character as Player;
                regenMultiplier += effect.GetRegen();
            }
        }
    }
}
