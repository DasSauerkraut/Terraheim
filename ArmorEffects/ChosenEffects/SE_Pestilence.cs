using Jotunn.Managers;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_Pestilence : StatusEffect
    {
        private bool m_hasTriggered = false;
        private float m_damagePercent;
        private float m_percentActivate;
        private int m_checkInterval;
        private float m_checkClock = 0;
        private System.Random m_dice = new System.Random();

        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Pestilence";
            base.name = "Pestilence";
            m_tooltip = "beeboo";
        }

        public override void Setup(Character character)
        {
            m_damagePercent = (float)Terraheim.balance["baneSettings"]["pestilence"]["damagePercent"];
            m_percentActivate = (float)Terraheim.balance["baneSettings"]["pestilence"]["chance"];
            m_checkInterval = (int)Terraheim.balance["baneSettings"]["pestilence"]["timeToCheck"];
            m_icon = AssetHelper.SpriteChosenNurgleBane;
            Log.LogWarning("Adding Pestilence");
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_time >= TTL - 0.1f && !m_hasTriggered)
            {
                SEMan seman = m_character.GetSEMan();
                if (seman.HaveStatusEffect("Chosen"))
                    (seman.GetStatusEffect("Chosen") as SE_Chosen).m_currentBanes.Remove(m_name);
            }
            Log.LogInfo($"{m_time} against {m_checkClock}");
            if(m_time >= m_checkClock)
            {
                m_checkClock += m_checkInterval;
                int roll = m_dice.Next(1, 100+1);
                if (roll <= m_percentActivate * 100)
                {
                    AssetHelper.PestilenceExplosion.GetComponent<Aoe>().m_damage.m_poison = m_character.GetMaxHealth() * m_damagePercent;
                    Log.LogInfo($"Damage is {m_character.GetMaxHealth() * m_damagePercent}");
                    Object.Instantiate(AssetHelper.PestilenceExplosion, m_character.GetCenterPoint(), Quaternion.identity);
                }
            }
            base.UpdateStatusEffect(dt);
        }


        public void SetTTL(float newTTL)
        {
            TTL = newTTL;
            m_time = 0;
        }

        public void IncreaseTTL(float increase)
        {
            TTL += increase;
            m_time -= increase;
        }
    }
}
