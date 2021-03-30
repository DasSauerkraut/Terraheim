using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_HPRegen : StatusEffect
    {
        public static float m_bonus = 0f;

        public void Awake()
        {
            m_name = "HP Regen";
            base.name = "HP Regen";
            m_tooltip = "Health Regen is increased by " + (m_bonus + 1) + "x.";
            m_icon = null;
        }

        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetBronze").m_itemData.GetIcon();
        }


        public void setHealPercent(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Health Regen is increased by " + (m_bonus + 1) + "x.";
        }

        public float getHealPercent() { return m_bonus; }
    }
}
