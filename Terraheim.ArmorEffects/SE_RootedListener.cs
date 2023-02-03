using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects;

public class SE_RootedListener : StatusEffect
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
        m_name = "Rooted Listener";
        base.name = "Rooted Listener";
        m_tooltip = "";
        m_icon = null;
    }

    public override void Setup(Character character)
    {
        TTL = 15f;
        base.Setup(character);
    }
}
