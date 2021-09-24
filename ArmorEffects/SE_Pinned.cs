using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    public class SE_Pinned : StatusEffect
    {
        static JObject balance = UtilityFunctions.GetJsonFromFile("weaponBalance.json");
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public bool hasTriggered = false;
        public float m_cooldownTTL = 0f;
        public HitData.DamageModifiers m_baseMods;

        public void Awake()
        {
            m_name = "Pinned";
            base.name = "Pinned";
            m_tooltip = "";
            m_icon = null;
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_character.GetSEMan().HaveStatusEffect("Pinned Cooldown"))
                m_character.GetSEMan().RemoveStatusEffect("Pinned");
            if (m_time >= TTL - 0.05f && !hasTriggered)
            {
                m_character.GetSEMan().AddStatusEffect("Pinned Cooldown");
                var effect = m_character.GetSEMan().GetStatusEffect("Pinned Cooldown") as SE_PinnedCooldown;
                effect.SetPinCooldownTTL(m_cooldownTTL);
                effect.SetBaseMods(m_baseMods);
                hasTriggered = true;
            }
            base.UpdateStatusEffect(dt);
        }

        public override void Setup(Character character)
        {
            TTL = 5f;
            Log.LogMessage($"Hit {character.m_name} w/ vulnerable damage! Pinned!");
            List<HitData.DamageModPair> mods = new List<HitData.DamageModPair>();
            m_baseMods = character.m_damageModifiers;
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Blunt, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Pierce, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Slash, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Fire, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Frost, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Lightning, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Poison, m_modifier = HitData.DamageModifier.Weak });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Spirit, m_modifier = HitData.DamageModifier.Weak });
            character.m_damageModifiers.Apply(mods);

            m_startEffects = new EffectList();
            m_startEffects.m_effectPrefabs = new EffectList.EffectData[] { Data.VFXPinned };
            base.Setup(character);
        }

        public override void ModifySpeed(float baseSpeed, ref float speed)
        {
            speed *= 0.5f;
            base.ModifySpeed(baseSpeed, ref speed);
        }

        public void SetPinTTL(float bonus)
        {
            TTL = bonus;
        }

        public void SetPinCooldownTTL(float bonus)
        {
            m_cooldownTTL = bonus;
        }

        public float GetPinCooldownTTL() { return m_cooldownTTL; }
    }
}
