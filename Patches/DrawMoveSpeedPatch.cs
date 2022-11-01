using HarmonyLib;
using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class DrawMoveSpeedPatch
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        public void Awake()
        {
            Log.LogInfo("Draw Move Speed Patching Complete");
        }

        [HarmonyPatch(typeof(Player), nameof(Player.GetJogSpeedFactor))]

        public static void Postfix(Player __instance, ref float __result)
        {
            if (__instance.GetAttackDrawPercentage() > 0f)
            {
                var moveSpeedMult = (float)balance["baseBowDrawMoveSpeeed"];
                SE_DrawMoveSpeed effect = __instance.GetSEMan().GetStatusEffect("Draw Move Speed") as SE_DrawMoveSpeed;
                if (__instance.GetCurrentWeapon().m_shared.m_name.Contains("bow_fireTH"))
                    moveSpeedMult = 0;
                if (effect != null) moveSpeedMult += effect.GetDrawMoveSpeed();
                __result *= moveSpeedMult;
            }
            else if (__instance.GetSEMan().HaveStatusEffect("Bloodrush"))
            {
                SE_MoveSpeedOnKill effect = __instance.GetSEMan().GetStatusEffect("Bloodrush") as SE_MoveSpeedOnKill;
                __result *= 1 + effect.GetCurrentSpeedBonus();
            }
            //Log.LogMessage(__instance.GetAttackDrawPercentage());
        }

        [HarmonyPatch(typeof(Player), "GetRunSpeedFactor")]
        [HarmonyPostfix]

        public static void RunPostfix(Player __instance, ref float __result)
        {
            if (__instance.GetSEMan().HaveStatusEffect("Bloodrush"))
            {
                SE_MoveSpeedOnKill effect = __instance.GetSEMan().GetStatusEffect("Bloodrush") as SE_MoveSpeedOnKill;
                __result *= 1 + effect.GetCurrentSpeedBonus();
            }
        }
    }
}
