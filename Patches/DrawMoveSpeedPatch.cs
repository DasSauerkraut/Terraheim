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

        public static bool Prefix(Player __instance, ref float __result)
        {
            float moveSpeed = (float)balance["baseBowDrawMoveSpeeed"];

            if (__instance.GetSEMan().HaveStatusEffect("Draw Move Speed"))
            {
                SE_DrawMoveSpeed effect = __instance.GetSEMan().GetStatusEffect("Draw Move Speed") as SE_DrawMoveSpeed;
                moveSpeed += effect.GetDrawMoveSpeed();
            }
            //Log.LogMessage(__instance.GetAttackDrawPercentage());
            __result = (1f + __instance.GetEquipmentMovementModifier()) * ((__instance.GetAttackDrawPercentage() > 0f) ? moveSpeed : 1f);
            return false;
        }
    }
}
