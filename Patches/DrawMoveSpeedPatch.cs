using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
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

        [HarmonyPatch(typeof(Player), "GetJogSpeedFactor")]

        public static void Postfix(Player __instance, ref float __result)
        {
            if (__instance.GetAttackDrawPercentage() > 0f)
            {
                var moveSpeedMult = (float)balance["baseBowDrawMoveSpeeed"];
                SE_DrawMoveSpeed effect = __instance.GetSEMan().GetStatusEffect("Draw Move Speed") as SE_DrawMoveSpeed;
                if (effect != null) moveSpeedMult += effect.GetDrawMoveSpeed();
                __result *= moveSpeedMult;
            }
            //Log.LogMessage(__instance.GetAttackDrawPercentage());
        }
    }
}
