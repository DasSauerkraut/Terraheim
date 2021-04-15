using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_AoECounter : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public int m_activationCount = 0;
        public int m_count = 0;
        public float m_aoeSize = 0f;
        public GameObject m_lastProjectile;

        public void Awake()
        {
            m_name = "Wyrdarrow";
            base.name = "Wyrdarrow";
            m_tooltip = "Hitting an enemy " + m_activationCount + " times with bows, daggers, or spears charges your weapon. The next special attack (Or normal attack, if using a bow) will deal " 
                + m_damageBonus * 100 + " spirt/frost damage in a " + m_aoeSize + "m radius.";
            m_icon = null;
        }

        public void IncreaseCounter()
        {
            m_count += 1;
            if(m_count >= m_activationCount && !m_character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
            {
                Log.LogInfo("Adding status");
                m_character.GetSEMan().AddStatusEffect("WyrdarrowFX");
            }
            m_name = base.name + " " + m_count;
        }

        public void ClearCounter()
        {
            m_count -= m_activationCount;
            if(m_count < m_activationCount && m_character.GetSEMan().HaveStatusEffect("WyrdarrowFX"))
            {
                m_character.GetSEMan().RemoveStatusEffect("WyrdarrowFX");
            }

            if(m_count < 1)
                m_name = base.name;
            else
                m_name = base.name + " " + m_count;
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
        }

        public void SetDamageBonus(float bonus)
        {
            m_damageBonus = bonus;
            m_tooltip = "Hitting an enemy " + m_activationCount + " times with bows, daggers, or spears charges your weapon. The next special attack (Or normal attack, if using a bow) will deal "
                + m_damageBonus * 100 + " spirt/frost damage in a " + m_aoeSize + "m radius.";
        }

        public float GetDamageBonus() { return m_damageBonus; }

        public void SetActivationCount(int count)
        {
            m_activationCount = count;
            m_tooltip = "Hitting an enemy " + m_activationCount + " times with bows, daggers, or spears charges your weapon. The next special attack (Or normal attack, if using a bow) will deal "
                + m_damageBonus * 100 + " spirt/frost damage in a " + m_aoeSize + "m radius.";
        }

        public float GetActivationCount() { return m_activationCount; }

        public void SetAoESize(float aoe)
        {
            m_aoeSize = aoe;
            m_tooltip = "Hitting an enemy " + m_activationCount + " times with bows, daggers, or spears charges your weapon. The next special attack (Or normal attack, if using a bow) will deal "
                + m_damageBonus * 100 + " spirt/frost damage in a " + m_aoeSize + "m radius.";
        }

        public float GetAoESize() { return m_aoeSize; }

        public void SetLastProjectile(GameObject proj){ m_lastProjectile = proj; }

        public GameObject GetLastProjectile() { return m_lastProjectile; }
    }
}
