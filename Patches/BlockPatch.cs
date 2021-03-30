using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using Newtonsoft.Json.Linq;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class BlockPatch
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");
        static float baseStaminaUse = (float)balance["baseBlockStaminaUse"];
        public void Awake()
        {
            Log.LogInfo("Block Patching Complete");
        }

        [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
        static void Prefix(ref float ___m_blockStaminaDrain, ref SEMan ___m_seman)
        {
            //Log.LogWarning("Blocking!");
            //Log.LogWarning("Stamina Use: " + ___m_blockStaminaDrain);
            if (___m_seman.HaveStatusEffect("Block Stamina Use"))
            {
                SE_BlockStamUse effect = ___m_seman.GetStatusEffect("Block Stamina Use") as SE_BlockStamUse;
                //Log.LogMessage("Has Effect");
                ___m_blockStaminaDrain = baseStaminaUse * (1 - effect.getBlockStaminaUse());
                //Log.LogWarning("Stamina Use: " + ___m_blockStaminaDrain);
            }
            else if (___m_blockStaminaDrain != baseStaminaUse)
            {
                ___m_blockStaminaDrain = baseStaminaUse;
            }
        }
    }
}
