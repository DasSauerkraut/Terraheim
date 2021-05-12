using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects
{
    class SE_PinnedCooldown : StatusEffect
    {
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Pinned Cooldown";
            base.name = "Pinned Cooldown";
            m_tooltip = "";
            m_icon = null;
        }

        public override void Setup(Character character)
        {
            TTL = 5f;
            Log.LogMessage($"Cooldown started {character.m_name}!");
            base.Setup(character);
        }

        public void SetPinCooldownTTL(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            TTL = bonus;
        }

        public void SetBaseMods(HitData.DamageModifiers baseMods)
        {
            List<HitData.DamageModPair> mods = new List<HitData.DamageModPair>();
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Blunt, m_modifier = baseMods.m_blunt });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Pierce, m_modifier = baseMods.m_pierce });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Slash, m_modifier = baseMods.m_slash });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Fire, m_modifier = baseMods.m_fire });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Frost, m_modifier = baseMods.m_frost });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Lightning, m_modifier = baseMods.m_lightning });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Poison, m_modifier = baseMods.m_poison });
            mods.Add(new HitData.DamageModPair() { m_type = HitData.DamageType.Spirit, m_modifier = baseMods.m_spirit });
            m_character.m_damageModifiers.Apply(mods);
        }
    }
}
