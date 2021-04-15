using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;
using Terraheim.Utility;
using UnityEngine;

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
                        //Log.LogMessage("Trying particles");
                        attacker.Heal(damage * effect.getHealAmount());
                        var lifestealVfx = Object.Instantiate(AssetHelper.FXLifeSteal, attacker.GetCenterPoint(), Quaternion.identity);
                        ParticleSystem[] children = lifestealVfx.GetComponentsInChildren<ParticleSystem>();
                        foreach(ParticleSystem particle in children)
                        {
                            particle.Play();
                            //Log.LogMessage("Playing particle");
                        }
                        //foreach (var p in lifestealVfx.GetComponentsInChildren<ParticleSystem>()) p.Emit(100);
                        //var vfx = new EffectList();
                        //vfx.m_effectPrefabs = new EffectList.EffectData[] { lifestealVfx };
                        //vfx.Create(attacker.GetCenterPoint(), Quaternion.identity);

                    }
                }
                if (attacker.GetSEMan().HaveStatusEffect("Wyrdarrow"))
                {
                    (attacker.GetSEMan().GetStatusEffect("Wyrdarrow") as SE_AoECounter).IncreaseCounter();
                }
            } catch
            {
                return;
            }            
        }
    }
}
