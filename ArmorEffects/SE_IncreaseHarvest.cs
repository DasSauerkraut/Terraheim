
namespace Terraheim.ArmorEffects
{
    class SE_IncreaseHarvest : StatusEffect
    {
        public void Awake()
        {
            m_name = "Harvest Yield Up";
            base.name = "Harvest Yield Up";
            m_tooltip = $"When you harvest wild plants, gain <color=cyan>2</color> more items from each harvest.\nWhen you harvest grown plants, gain <color=cyan>1</color> more item from each harvest.";
        }
    }
}
