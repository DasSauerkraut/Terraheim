using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;

namespace Terraheim.Patches
{
    
    [HarmonyPatch]
    class HitPatch
    {
        public void Awake()
        {
            Log.LogInfo("Hit Patching Complete");
        }


        [HarmonyPatch(typeof(MonsterAI), "OnDamaged")]
        static void Prefix(float damage, ref Character attacker)
        {
            //Log.LogMessage("Enemy Damaged!");
            if (attacker == null || !attacker.IsPlayer() || attacker.m_seman == null)
            {
                return;
            }
            try
            { 
                //Log.LogMessage("Trying FOr Life Steal!");
                if (attacker.GetSEMan().HaveStatusEffect("Life Steal"))
                {
                    SE_HPOnHit effect = attacker.GetSEMan().GetStatusEffect("Life Steal") as SE_HPOnHit;
                    //Log.LogMessage($"Has Life Steal! Last hit melee: {effect.getLastHitMelee()}");
                    //Log.LogMessage($"Inflicted {damage} damage!, healing {damage * effect.getHealAmount()}HP");
                    if (effect.getLastHitMelee())
                    {
                        attacker.Heal(damage * effect.getHealAmount());
                    }
                }
            } catch
            {
                return;
            }            
        }
    }
}
