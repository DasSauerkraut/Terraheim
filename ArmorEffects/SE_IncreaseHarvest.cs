
namespace Terraheim.ArmorEffects
{
    class SE_IncreaseHarvest : StatusEffect
    {
        public void Awake()
        {
            m_name = "Harvest Yield Up";
            base.name = "Harvest Yield Up";
            m_tooltip = "More items from crops and forageables.";
        }
    }
}
