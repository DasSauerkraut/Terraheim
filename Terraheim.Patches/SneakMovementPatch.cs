using HarmonyLib;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class SneakMovementPatch
    {
        static JObject balance = Terraheim.balance;
        static float baseStaminaUse = (float)balance["baseSneakStaminaUse"];
        static float baseSpeed = (float)balance["baseSneakSpeed"];
        public void Awake()
        {
            Log.LogInfo("Block Patching Complete");
        }

        [HarmonyPatch(typeof(Player), nameof(Player.OnSneaking))]
        [HarmonyPrefix]
        static void OnSneakingPrefix(Player __instance)
        {
            if (__instance.GetSEMan().HaveStatusEffect("Sneak Movement"))
            {
                var effectMod = (__instance.GetSEMan().GetStatusEffect("Sneak Movement") as SE_SneakMovement).GetBonus();
                //Log.LogMessage("Stamina Usage: " + __instance.m_sneakStaminaDrain);
                __instance.m_sneakStaminaDrain = baseStaminaUse * effectMod;
                (__instance as Character).m_crouchSpeed = baseSpeed * (1 + effectMod);
                //Log.LogMessage("New Stamina Usage: " + __instance.m_sneakStaminaDrain);
            }
            else if (__instance.m_sneakStaminaDrain != baseStaminaUse || __instance.m_crouchSpeed != baseSpeed)
            {
                __instance.m_sneakStaminaDrain = baseStaminaUse;
                __instance.m_crouchSpeed = baseSpeed;
            }
        }
    }
}
