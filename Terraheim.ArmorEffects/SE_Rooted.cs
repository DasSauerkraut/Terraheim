using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects;

public class SE_Rooted : StatusEffect
{
    public bool hasTriggered = false;

    public float m_cooldownTTL = 0f;

    public float TTL
    {
        get
        {
            return m_ttl;
        }
        set
        {
            m_ttl = value;
        }
    }

    public void Awake()
    {
        m_name = "Rooted";
        base.name = "Rooted";
        m_tooltip = "";
        m_icon = null;
    }

    public override void UpdateStatusEffect(float dt)
    {
        if (m_character.GetSEMan().HaveStatusEffect("Rooted Cooldown"))
        {
            m_character.GetSEMan().RemoveStatusEffect("Rooted".GetStableHashCode());
        }
        if (m_time >= 3f && !hasTriggered)
        {
            Object.Instantiate(AssetHelper.RootingExplosion, m_character.GetCenterPoint(), Quaternion.identity);
            hasTriggered = true;
        }
        base.UpdateStatusEffect(dt);
    }

    public override void Setup(Character character)
    {
        TTL = 5f;
        m_startEffects = new EffectList();
        m_startEffects.m_effectPrefabs = new EffectList.EffectData[1] { Data.VFXRooted };
        base.Setup(character);
    }

    public override void ModifySpeed(float baseSpeed, ref float speed)
    {
        speed = 0f;
        base.ModifySpeed(baseSpeed, ref speed);
    }

    public void SetRootedTTL(float bonus)
    {
        TTL = bonus;
    }

    public void SetRootedCooldownTTL(float bonus)
    {
        m_cooldownTTL = bonus;
    }

    public float GetRootedCooldownTTL()
    {
        return m_cooldownTTL;
    }
}
