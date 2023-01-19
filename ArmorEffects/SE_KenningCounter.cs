using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Threading;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_KenningCounter : StatusEffect
    {
        private HitData m_totalHit = new HitData();
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_time >= TTL - 0.1f)
            {
                m_character.ApplyDamage(GetDamage(), true, false);
                var executionVFX = Instantiate(AssetHelper.KenningActivate, m_character.GetCenterPoint(), Quaternion.identity);
                ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in children)
                {
                    particle.Play();
                }
                m_character.GetSEMan().RemoveStatusEffect("KenningCounter");
            }
            base.UpdateStatusEffect(dt);
        }

        public void Awake()
        {
            m_name = "KenningCounter";
            base.name = "KenningCounter";
        }

        public void AddDamage(HitData hit, float newTTL, float newTime)
        {
            m_totalHit.m_damage.Add(hit.m_damage);
            TTL = newTTL;
            m_time = newTime;
        }

        public HitData GetDamage() { return m_totalHit; }
    }
}
