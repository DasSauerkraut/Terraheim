namespace Terraheim.ArmorEffects;

internal class SE_SetBonusIncrease : StatusEffect
{
    public void Awake()
    {
        m_name = "Set Bonus Increase";
        name = "Set Bonus Increase";
        m_tooltip = "A relic of Acca.\nThis belt counts as a piece of an armor set, allowing you to get the set bonus while wearing only two items.";
    }
}
