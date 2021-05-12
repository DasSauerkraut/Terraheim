using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;
namespace Terraheim.ArmorEffects
{
    class SE_Thorns : StatusEffect
    {
        public float m_bonus = 0f;

        public void Awake()
        {
            m_name = "Thorns";
            base.name = "Thorns";
            m_tooltip = "Reflect " + (m_bonus*100) + "% of incoming damage back at your attacker.";
            m_icon = null;
        }

        public void SetIcon()
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded").m_itemData.GetIcon();
        }


        public void SetReflectPercent(float bonus)
        {
            m_bonus = bonus;
            m_tooltip = "Reflect " + (m_bonus * 100) + "% of incoming damage back at your attacker.";
        }

        public float GetReflectPercent() { return m_bonus; }
    }
}
