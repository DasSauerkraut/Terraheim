using HarmonyLib;
using ValheimLib;
using ValheimLib.ODB;
using Terraheim.ArmorEffects;

namespace Terraheim.Patches
{

    [HarmonyPatch]
    class ThornsPatch
    {
        public void Awake()
        {
            Log.LogInfo("Thorns Patching Complete");
        }


        [HarmonyPatch(typeof(SEMan), "OnDamaged")]
        public static void Postfix(SEMan __instance, HitData hit, ref Character attacker)
        {
            //Log.LogMessage("Enemy Damaged!");
            if (attacker == null || attacker.IsPlayer() || attacker.m_seman == null)
            {
                return;
            }
                //Log.LogMessage("Trying FOr Thorns!");
            if (__instance.HaveStatusEffect("Thorns"))
            {
                SE_Thorns effect = __instance.GetStatusEffect("Thorns") as SE_Thorns;
                //Log.LogMessage($"Damage dealt: {hit.GetTotalDamage()} thorns %${effect.GetReflectPercent()}");
                HitData reflectedDamage = new HitData();
                reflectedDamage.m_damage.Add(hit.m_damage);
                reflectedDamage.m_damage.m_blunt *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_chop *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_damage *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_fire *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_frost *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_lightning *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_pickaxe *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_pierce *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_poison *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_slash *= effect.GetReflectPercent();
                reflectedDamage.m_damage.m_spirit *= effect.GetReflectPercent();

                //Log.LogMessage($"Reflected Damage ${reflectedDamage.m_damage.GetTotalDamage()}");
                attacker.ApplyDamage(reflectedDamage, true, false);            
            }

            if (__instance.HaveStatusEffect("Wolftears"))
            {
                var effect = __instance.GetStatusEffect("Wolftears") as SE_SneakDamageBonus;
                if(__instance.m_character.GetHealthPercentage() <= effect.GetActivationHP())
                {
                    effect.SetIcon();
                }
            }
        }
    }
}
