namespace Terraheim.ArmorEffects;

internal class SE_MageCost : StatusEffect
{
    public float m_cst = 0.05f;

    public float m_bns;

    public void Awake()
    {
        m_name = "Wyrd Cost";
        name = "Wyrd Cost";
        m_tooltip = $"Eitr regen is increased by {m_bns * 100f}%. {m_cst * 100f}% reduced Eitr/Blood cost.";
    }

    public void SetMageCst(float bonus)
    {
        m_cst = bonus;
        m_tooltip = $"Eitr regen is increased by {m_bns * 100f}%. {m_cst * 100f}% reduced Eitr/Blood cost.";
    }

    public float GetMageCst()
    {
        return m_cst;
    }

    public void SetMageBns(float bonus)
    {
        m_bns = bonus;
        m_tooltip = $"Eitr regen is increased by {m_bns * 100f}%. {m_cst * 100f}% reduced Eitr/Blood cost.";
    }

    public float GetMageBns()
    {
        return m_bns;
    }
}
