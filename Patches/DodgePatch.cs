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
    class DodgePatch
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        static float baseStaminaUse = (float)balance["baseDodgeStaminaUse"];
        public void Awake()
        {
            Log.LogInfo("Dodge Patching Complete");
        }

        [HarmonyPatch(typeof(Player), nameof(Player.Dodge))]
        static void Prefix(ref float ___m_dodgeStaminaUsage, ref SEMan ___m_seman)
        {
            //Log.LogWarning("Dodging!");
            //Log.LogWarning("Stamina Use: " + ___m_dodgeStaminaUsage);
            if(___m_seman.HaveStatusEffect("Dodge Stamina Use"))
            {
                SE_DodgeStamUse effect = ___m_seman.GetStatusEffect("Dodge Stamina Use") as SE_DodgeStamUse;
                //Log.LogMessage("Has Effect");
                ___m_dodgeStaminaUsage = baseStaminaUse * (1 - effect.getDodgeStaminaUse());
                //Log.LogWarning("Stamina Use: " + ___m_dodgeStaminaUsage);
            }
            else if (___m_seman.HaveStatusEffect("Challenge Dodge Bonus"))
            {
                SE_ChallengeDodgeBonus effect = ___m_seman.GetStatusEffect("Challenge Dodge Bonus") as SE_ChallengeDodgeBonus;
                //Log.LogMessage("Has Effect");
                ___m_dodgeStaminaUsage = baseStaminaUse * (1 - effect.GetDodgeBonus());
                //Log.LogWarning("Stamina Use: " + ___m_dodgeStaminaUsage);
            }
            else if(___m_dodgeStaminaUsage != baseStaminaUse)
            {
                ___m_dodgeStaminaUsage = baseStaminaUse;
            }
        }
    }
}
