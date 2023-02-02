using Jotunn.Managers;

namespace Terraheim.ArmorEffects;

public class SE_BloodPact : StatusEffect
{
    public float m_damageBns = 0.2f;

    public int m_eitrToHpRatio = 3;

    public void Awake()
    {
        m_name = "Blood Pact";
        name = "Blood Pact";
        UpdateTooltip();
    }

    public void SetIcon()
    {
        m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetMage").m_itemData.GetIcon();
    }

    public void UpdateTooltip()
    {
        m_tooltip = $"When you lack sufficient Eitr to use a spell, you instead draw from your HP instead. Every {m_eitrToHpRatio} Eitr you lack corresponds to 1% of your Health used. These spells are also empowered, gaining a {m_damageBns * 100}% damage increase.";
    }

    public void SetDamageBonus(float bonus)
    {
        m_damageBns = bonus;
        UpdateTooltip();
    }

    public float GetDamageBonus()
    {
        return m_damageBns;
    }

    public void SetRatio(int bonus)
    {
        m_eitrToHpRatio = bonus;
        UpdateTooltip();
    }

    public int GetRatio()
    {
        return m_eitrToHpRatio;
    }
}
