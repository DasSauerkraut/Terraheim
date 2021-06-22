using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects
{
    public class SE_ShieldFireListener : StatusEffect
    {
        private JObject wepBalance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");

        private int m_killThreshold;
        private int m_currentKills = 0;
        private float m_killTotalHp = 0f;
        public void Awake()
        {
            m_name = "Svalinn";
            base.name = "Svalinn";
            m_tooltip = "";
            m_killThreshold = (int)wepBalance["ShieldFireTower"]["effectVal"];
        }

        public override void Setup(Character character)
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("ShieldFireTowerTH").m_itemData.GetIcon();
            base.Setup(character);
        }

        public float OnKill(float killHP)
        {
            m_currentKills += 1;
            m_killTotalHp += killHP;
            m_name = $"Svalinn\nKills: {m_currentKills} / {m_killThreshold} | HP {((double)Math.Pow(m_killTotalHp / m_killThreshold, 1f / 1.35f)):#.#}";
            if (m_currentKills >= m_killThreshold)
            {
                m_currentKills = 0;
                float avgHP = m_killTotalHp / m_killThreshold;
                float amtToHeal = (float)Math.Pow(avgHP, 1f / 1.35f);
                Log.LogInfo($"Kill exceeded threshold, Aoe should heal {amtToHeal}, with {amtToHeal / 4} HP per tick.");
                m_killTotalHp = 0f;
                m_name = "Svalinn";
                return amtToHeal;
            }
            else
                return -1f;
        }

        public override void OnDamaged(HitData hit, Character attacker)
        {
            if(IsRangedAttack((attacker as Humanoid).GetCurrentWeapon()))
            {
                //Log.LogInfo($"Is Ranged Attack : {hit.GetTotalDamage()}");
                float damageMod = 1 - (float)wepBalance["ShieldFireTower"]["projProtection"];
                hit.m_damage.m_blunt *= damageMod;
                hit.m_damage.m_chop *= damageMod;
                hit.m_damage.m_damage *= damageMod;
                hit.m_damage.m_fire *= damageMod;
                hit.m_damage.m_frost *= damageMod;
                hit.m_damage.m_lightning *= damageMod;
                hit.m_damage.m_pickaxe *= damageMod;
                hit.m_damage.m_pierce *= damageMod;
                hit.m_damage.m_poison *= damageMod;
                hit.m_damage.m_slash *= damageMod;
                hit.m_damage.m_spirit *= damageMod;
                //Log.LogInfo($"Modified {hit.GetTotalDamage()}");
            }
            base.OnDamaged(hit, attacker);
        }

        private bool IsRangedAttack(ItemDrop.ItemData weapon)
        {
            if (weapon == null || weapon.m_shared == null || weapon.m_dropPrefab == null)
                return false;
            if (weapon.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                return true;
            switch (weapon.m_shared.m_name)
            {
                case "cold ball":
                    return true;
                case "dragon breath":
                    return true;
                case "fireballattack":
                    return true;
            }
            if (weapon.m_dropPrefab.name == "GoblinSpear")
                return true;
            return false;
        }
    }
}
