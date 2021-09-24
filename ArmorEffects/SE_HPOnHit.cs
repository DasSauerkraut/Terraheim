﻿using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
using HarmonyLib;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_HPOnHit : StatusEffect
    {
        public float m_amount = 0f;
        public bool m_lastHitMelee = false;

        public void Awake()
        {
            m_name = "Life Steal";
            base.name = "Life Steal";
            m_icon = null;
            m_tooltip = $"<color=cyan>Heal " + (getHealAmount() * 100) + "%</color> of damage dealt as HP on hitting an enemy with a melee weapon.";
        }

        public void SetIcon()
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather").m_itemData.GetIcon();
        }

        public void setLastHitMelee(bool melee) { m_lastHitMelee = melee; }
        public bool getLastHitMelee() { return m_lastHitMelee; }

        public void setHealAmount(float amount)
        {
            //Log.LogInfo("Setting Reduction: " + reduction * 10 + "%");
            m_amount = amount;
            m_tooltip = $"<color=cyan>Heal " + (getHealAmount() * 100) + "%</color> of damage dealt as HP on hitting an enemy with a melee weapon.";
        }

        public float getHealAmount() { return m_amount; }
    }
}
