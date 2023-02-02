namespace Terraheim.ArmorEffects;

internal class SE_MageDamage : StatusEffect
{
    public float m_dmg = 0.05f;

    public float m_bns;

    public void Awake()
    {
        m_name = "Wyrd Damage";
        name = "Wyrd Damage";
        m_tooltip = $"Eitr regen is increased by {m_bns * 100f}%. Magic damage is increased by {m_dmg * 100f}%";
    }

    public void SetMageDmg(float bonus)
    {
        m_dmg = bonus;
        m_tooltip = $"Eitr regen is increased by {m_bns * 100f}%. Magic damage is increased by {m_dmg * 100f}%";
    }

    public float GetMageDmg()
    {
        return m_dmg;
    }

    public void SetMageBns(float bonus)
    {
        m_bns = bonus;
        m_tooltip = $"Eitr regen is increased by {m_bns * 100f}%. Magic damage is increased by {m_dmg * 100f}%";
    }

    public float GetMageBns()
    {
        return m_bns;
    }
}
