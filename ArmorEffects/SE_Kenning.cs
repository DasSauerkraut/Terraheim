using Terraheim.Utility;
using UnityEngine;
using Jotunn.Managers;
using BepInEx;
using BepInEx.Configuration;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.GUI;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Terraheim.ArmorEffects
{
    class SE_Kenning : StatusEffect
    {
        public int m_activationCount = 0;
        public int m_count = 0;
        public int m_maxCount = 30;
        public float m_aoeSize = 0f;

        public int m_duration;
        public float m_damageReduction;
        public float m_attackSpeed = 1;

        public void Awake()
        {
            m_name = "Kenning";
            base.name = "Kenning";
            //SetTooltip();
        }

        public void SetTooltip()
        {
            m_tooltip = $"Hitting enemies builds Ken. By pressing {Terraheim.KenningButton.Key}, you spend {m_activationCount} Ken and enter into Ken Mode. While in Ken Mode, you take {m_damageReduction * 100}% less damage," +
                $"have {m_attackSpeed * 100}% more attack speed, and attacks cost no stamina but deal no damage. After Ken Mode ends, all the damage you would have dealt is applied at once.\n" +
                $"You can sheathe your weapon to end Ken Mode.";
        }

        public void IncreaseCounter()
        {
            if (m_character.GetSEMan().HaveStatusEffect("KenningFX"))
                return;

            m_count += 1;
            if (m_count >= m_maxCount)
                m_count = m_maxCount;
            m_name = base.name + " " + m_count;
        }

        public void SetIcon()
        {
            if (Terraheim.hasJudesEquipment)
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>(Data.ArmorSets["wanderer"].HelmetID).m_itemData.GetIcon();
            else
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
        }

        public void SetDuration(int bonus)
        {
            m_duration = bonus;
            SetTooltip();
        }

        public float GetTTL() { return m_duration; }

        public void SetActivationCost(int count)
        {
            m_activationCount = count;
            SetTooltip();
        }

        public float GetActivationCost() { return m_activationCount; }

        public void SetDamageReduction(float aoe)
        {
            m_damageReduction = aoe;
            SetTooltip();
        }

        public float GetDamageReduction() { return m_damageReduction; }

        public override void UpdateStatusEffect(float dt)
        {
            if(Terraheim.KenningButton != null && MessageHud.instance != null)
            {
                if(ZInput.GetButtonDown(Terraheim.KenningButton.Name))
                {
                    if(m_count < m_activationCount)
                    {
                        if(MessageHud.instance.m_msgQeue.Count == 0)
                            MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "$kenning_not_enough");
                    } else if(!m_character.GetSEMan().HaveStatusEffect("KenningFX"))
                    {
                        Log.LogInfo("Kenning active");
                        m_count -= m_activationCount;
                        m_name = base.name + " " + m_count;
                        //if(MessageHud.instance.m_msgQeue.Count == 0)
                        //    MessageHud.instance.ShowMessage(MessageHud.MessageType.Center, "$kenning_active");
                        SE_KenningFX effect = new SE_KenningFX();
                        effect.SetTTL(m_duration);
                        effect.SetDamageReduction(m_damageReduction);
                        m_character.GetSEMan().AddStatusEffect(effect);
                    }
                }
            }
            base.UpdateStatusEffect(dt);
        }
    }
}
