using Jotunn.Managers;

namespace Terraheim.ArmorEffects
{
    class SE_RetaliationTimer : StatusEffect
    {
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Retaliation Cooldown";
            base.name = "Retaliation Cooldown";
            m_tooltip = $"Countdown to reset stored retalitation damage.";
        }

        public override void Setup(Character character)
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>(Utility.Data.ArmorSets["plate"].HelmetID).m_itemData.GetIcon();
            TTL = 10;
            base.Setup(character);
        }
    }
}
