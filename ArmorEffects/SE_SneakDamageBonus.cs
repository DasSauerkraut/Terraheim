using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_SneakDamageBonus : StatusEffect
    {
        public static float m_damageBonus = 0.05f;
        public static int m_aoeChance = 0;

        public void Awake()
        {
            m_name = "Sneak Damage Bonus";
            base.name = "Sneak Damage Bonus";
            m_tooltip = "While crouching, melee damage is increased by " + m_damageBonus * 100 + "%";
            m_icon = null;
        }
        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
        }


        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "While crouching, melee damage is increased by " + m_damageBonus * 100 + "%";
        }

        public float GetDamageBonus() { return m_damageBonus; }

        public void SetAOEChance(int bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_aoeChance = bonus;
            m_tooltip = "While crouching, melee damage is increased by " + m_damageBonus * 100 + "%";
        }

        public float GetAOEChance() { return m_aoeChance; }
    }
}
