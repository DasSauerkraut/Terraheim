
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_CritChance : StatusEffect
    {
        public float m_chance = 0f;
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Crit Chance";
            base.name = "Crit Chance";
            m_tooltip = m_chance + "% chance to crit with two handed weapons, increasing damage by " + m_bonus + "x.";
            m_icon = null;
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetIron").m_itemData.GetIcon();
        }


        public void SetCritChance(float bonus)
        {
            m_chance = bonus;
            m_tooltip = m_chance + "% chance to crit with two handed weapons, increasing damage by " + m_bonus + "x.";
        }

        public float GetCritChance() { return m_chance; }

        public void SetCritBonus(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = m_chance + "% chance to crit with two handed weapons, increasing damage by " + m_bonus + "x.";
        }

        public float GetCritBonus() { return m_bonus; }
    }
}