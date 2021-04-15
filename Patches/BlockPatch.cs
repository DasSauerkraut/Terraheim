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
            if (___m_seman.HaveStatusEffect("Block Stamina Use") && (___m_seman.m_character as Humanoid).GetCurrentWeapon() != (___m_seman.m_character as Humanoid).m_unarmedWeapon.m_itemData)
            {
                SE_BlockStamUse effect = ___m_seman.GetStatusEffect("Block Stamina Use") as SE_BlockStamUse;
                //Log.LogMessage("Has Effect");
                ___m_blockStaminaDrain = baseStaminaUse * (1 - effect.getBlockStaminaUse());
                //Log.LogWarning("Stamina Use: " + ___m_blockStaminaDrain);
            }
            else if (___m_blockStaminaDrain != baseStaminaUse)
            {
                //Log.LogMessage("1");
                ___m_blockStaminaDrain = baseStaminaUse;
                //Log.LogMessage("2");

            }
            //Log.LogMessage("3");
        }

        [HarmonyPatch(typeof(Humanoid), "BlockAttack")]
        static void Postfix(HitData hit, Character attacker, ref SEMan ___m_seman, float ___m_blockTimer)
        {

            if (___m_seman.HaveStatusEffect("Heal On Block"))
            {
                if ((___m_seman.m_character as Humanoid) && (___m_seman.m_character as Humanoid).m_leftItem != (___m_seman.m_character as Humanoid).m_unarmedWeapon.m_itemData 
                    && (___m_seman.m_character as Humanoid).m_leftItem != null)
                {
                    var effect = ___m_seman.GetStatusEffect("Heal On Block") as SE_HealOnBlock;
                    ItemDrop.ItemData blocker = (___m_seman.m_character as Humanoid).m_leftItem;

                    //Log.LogMessage("Defender is staggered: " + ___m_seman.m_character.IsStaggering() + ". Attacker is staggered: " + attacker.IsStaggering());
                    bool parryFlag = ___m_blockTimer != -1f && (float)balance["perfectBlockWindow"] >= ___m_blockTimer;

                    if (blocker.m_shared.m_name.Contains("tower") || blocker.m_shared.m_name.Contains("shield_serpentscale"))
                    {
                        //Log.LogWarning("Has tower shield");
                        //Do Heal on Block
                        if ((___m_seman.m_character as Humanoid).HaveStamina() || parryFlag)
                        {
                            var healAmount = blocker.GetBaseBlockPower() * effect.GetBlockHeal();
                            Log.LogInfo("Terraheim | Heal on Block: Block Power: " + blocker.GetBaseBlockPower() + " Heal Amount: " + healAmount);
                            ___m_seman.m_character.Heal(healAmount);
                        }
                    }
                    else if(blocker.m_shared.m_name.Contains("shield") && parryFlag)
                    {
                        //Log.LogWarning("Has Small Shield");
                        //Do Heal on Parry
                        var healAmount = blocker.GetBaseBlockPower() * effect.GetBlockHeal() + 
                            (blocker.GetBaseBlockPower() * blocker.m_shared.m_timedBlockBonus * effect.GetBlockHeal());
                        Log.LogInfo("Terraheim | Heal on Parry: Block Power: " + blocker.GetBaseBlockPower() + " Parry Bonus: " + blocker.m_shared.m_timedBlockBonus + " Heal Amount: " + healAmount);
                        ___m_seman.m_character.Heal(healAmount);
                    }
                }
            }
        }
    }
}
