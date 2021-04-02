using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_SneakDamageBonus : StatusEffect
    {
        public static float m_damageBonus = 0.05f;
        public static float m_activationHealth = 0f;

        public void Awake()
        {
            m_name = "Wolftears";
            base.name = "Wolftears";
            m_tooltip = "When below " + m_activationHealth * 100 + "% health, all damage is increased by " + m_damageBonus * 100 + "%. While crouching, you cannot regain health.";
            m_icon = null;
        }
        public void SetIcon()
        {
            m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetDrake").m_itemData.GetIcon();
        }

        public void ClearIcon()
        {
            m_icon = null;
        }


        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "When below " + m_activationHealth * 100 + "% health, all damage is increased by " + m_damageBonus * 100 + "%. While crouching, you cannot regain health.";
        }

        public float GetDamageBonus() { return m_damageBonus; }

        public void SetActivationHP(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_activationHealth = bonus;
            m_tooltip = "When below " + m_activationHealth * 100 + "% health, all damage is increased by " + m_damageBonus * 100 + "%. While crouching, you cannot regain health.";
        }

        public float GetActivationHP() { return m_activationHealth; }
    }
}
