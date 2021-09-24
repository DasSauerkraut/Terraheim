using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_Thorns : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Thorns";
            base.name = "Thorns";
            m_tooltip = "Reflect " + (m_bonus*100) + "% of incoming damage back at your attacker.";
            m_icon = null;
        }

        public void SetIcon()
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded").m_itemData.GetIcon();
        }


        public void SetReflectPercent(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Reflect " + (m_bonus * 100) + "% of incoming damage back at your attacker.";
        }

        public float GetReflectPercent() { return m_bonus; }

        public override void OnDamaged(HitData hit, Character attacker)
        {
            if (!m_character.IsBlocking() && attacker != null && attacker.m_seman != null)
            {
                Log.LogMessage($"Damage dealt: {hit.GetTotalDamage()}, Thorns {m_bonus*100}%");
                HitData reflectedDamage = new HitData();
                reflectedDamage.m_damage.Add(hit.m_damage);
                if(reflectedDamage.GetTotalDamage() > 1000)
                {
                    reflectedDamage.m_damage.m_blunt *= 0.075f;
                    reflectedDamage.m_damage.m_chop *= 0.075f;
                    reflectedDamage.m_damage.m_damage *= 0.075f;
                    reflectedDamage.m_damage.m_fire *= 0.075f;
                    reflectedDamage.m_damage.m_frost *= 0.075f;
                    reflectedDamage.m_damage.m_lightning *= 0.075f;
                    reflectedDamage.m_damage.m_pickaxe *= 0.075f;
                    reflectedDamage.m_damage.m_pierce *= 0.075f;
                    reflectedDamage.m_damage.m_poison *= 0.075f;
                    reflectedDamage.m_damage.m_slash *= 0.075f;
                    reflectedDamage.m_damage.m_spirit *= 0.075f;
                    Log.LogMessage($"Greater than 1000, Reducing to 7.5% -> {reflectedDamage.GetTotalDamage()}");
                }
                reflectedDamage.m_damage.m_blunt *= m_bonus;
                reflectedDamage.m_damage.m_chop *= m_bonus;
                reflectedDamage.m_damage.m_damage *= m_bonus;
                reflectedDamage.m_damage.m_fire *= m_bonus;
                reflectedDamage.m_damage.m_frost *= m_bonus;
                reflectedDamage.m_damage.m_lightning *= m_bonus;
                reflectedDamage.m_damage.m_pickaxe *= m_bonus;
                reflectedDamage.m_damage.m_pierce *= m_bonus;
                reflectedDamage.m_damage.m_poison *= m_bonus;
                reflectedDamage.m_damage.m_slash *= m_bonus;
                reflectedDamage.m_damage.m_spirit *= m_bonus;
                reflectedDamage.m_staggerMultiplier = 0;

                Log.LogMessage($"Reflected Damage: {reflectedDamage.m_damage.GetTotalDamage()}");
                if (attacker.GetHealth() <= reflectedDamage.GetTotalDamage() && attacker.GetHealthPercentage() >= (float)Terraheim.balance["thornsKillThreshold"])
                {
                    var totalDamage = attacker.GetHealth() - 1;
                    reflectedDamage.m_damage.m_blunt = totalDamage * (reflectedDamage.m_damage.m_blunt / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_chop = totalDamage * (reflectedDamage.m_damage.m_chop / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_damage = totalDamage * (reflectedDamage.m_damage.m_damage / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_fire = totalDamage * (reflectedDamage.m_damage.m_fire / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_frost = totalDamage * (reflectedDamage.m_damage.m_frost / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_lightning = totalDamage * (reflectedDamage.m_damage.m_lightning / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_pickaxe = totalDamage * (reflectedDamage.m_damage.m_pickaxe / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_pierce = totalDamage * (reflectedDamage.m_damage.m_pierce / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_poison = totalDamage * (reflectedDamage.m_damage.m_poison / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_slash = totalDamage * (reflectedDamage.m_damage.m_slash / reflectedDamage.GetTotalDamage());
                    reflectedDamage.m_damage.m_spirit = totalDamage * (reflectedDamage.m_damage.m_spirit / reflectedDamage.GetTotalDamage());
                    //Log.LogMessage($"Would Kill attacker! New damage: {reflectedDamage.m_damage.GetTotalDamage()}, attacker health: {attacker.GetHealth()}");
                }

                attacker.ApplyDamage(reflectedDamage, true, false);

                var vfx = Object.Instantiate(AssetHelper.FXThorns, attacker.GetCenterPoint(), Quaternion.identity);
                ParticleSystem[] children = vfx.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in children)
                {
                    particle.Play();
                    //Log.LogMessage("Playing particle");
                }
                base.OnDamaged(hit, attacker);
            }
        }
    }
}
