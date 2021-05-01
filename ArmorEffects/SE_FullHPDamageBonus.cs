using Jotunn;
using Jotunn.Entities;
using Jotunn.Managers;

namespace Terraheim.ArmorEffects
{
    class SE_FullHPDamageBonus : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public float m_activationHealth = 0f;

        public void Awake()
        {
            m_name = "Battle Furor";
            base.name = "Battle Furor";
            m_tooltip = "When above " + m_activationHealth *100 +"% health, all damage is increased by " + m_damageBonus * 100 + "%.";
            m_icon = null;
        }

        public void InitIcon()
        {
            m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather").m_itemData.GetIcon();
        }

        public void SetIcon()
        {
            if(m_icon == null)
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather").m_itemData.GetIcon();
            if (m_character.GetHealthPercentage() >= m_activationHealth && !m_character.GetSEMan().HaveStatusEffect("Battle FurorFX"))
            {
                Log.LogInfo("Adding status");
                m_character.GetSEMan().AddStatusEffect("Battle FurorFX");
                SetIcon();
            }
        }

        public void ClearIcon()
        {
            if (m_icon != null)
                m_icon = null;
            if (m_character.GetHealthPercentage() < m_activationHealth && m_character.GetSEMan().HaveStatusEffect("Battle FurorFX"))
            {
                Log.LogInfo("Remove status");
                m_character.GetSEMan().RemoveStatusEffect("Battle FurorFX");
                ClearIcon();
            }
        }

        public override void UpdateStatusEffect(float dt)
        {
            base.UpdateStatusEffect(dt);
            if (m_character.GetHealthPercentage() >= m_activationHealth)
            {
                SetIcon();
            }
            else if (m_character.GetHealthPercentage() >= m_activationHealth)
            {
                ClearIcon();
            }
        }

        public void SetDamageBonus(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_damageBonus = bonus;
            m_tooltip = "When above " + m_activationHealth * 100 + "% health, all damage is increased by " + m_damageBonus * 100 + "%.";
        }

        public float GetDamageBonus() { return m_damageBonus; }

        public void SetActivationHP(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_activationHealth = bonus;
            m_tooltip = "When above " + m_activationHealth * 100 + "% health, all damage is increased by " + m_damageBonus * 100 + "%.";
        }

        public float GetActivationHP() { return m_activationHealth; }
    }
}
