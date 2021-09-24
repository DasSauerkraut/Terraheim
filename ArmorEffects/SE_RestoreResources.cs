
namespace Terraheim.ArmorEffects
{
    class SE_RestoreResources : StatusEffect
    {
        public float m_chance = 0f;
        public float m_stamina = 0f;

        public void Awake()
        {
            m_name = "Restore Resources";
            base.name = "Restore Resources";
            m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
        }

        public void SetChance(float bonus)
        {
            m_chance = bonus * 100;
            m_tooltip = m_chance + "% chance not use ammo on hit. Restore " + m_stamina + " on hit.";
        }

        public float GetChance() { return m_chance; }

        public void SetStaminaAmount(float bonus)
        {
            m_stamina = bonus;
            m_tooltip = $"\n\nOn hit, restore <color=cyan>{GetStaminaAmount()}</color> Stamina, {GetChance()}% chance to <color=cyan>refund ammo</color>.";
        }

        public float GetStaminaAmount() { return m_stamina; }

    }
}