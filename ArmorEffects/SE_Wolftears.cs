using Newtonsoft.Json.Linq;
using Terraheim.Utility;
using UnityEngine;
using ValheimLib;

namespace Terraheim.ArmorEffects
{
    class SE_Wolftears : StatusEffect
    {
        public float m_damageBonus = 0.05f;
        public float m_activationHealth = 0f;
        public float m_damageIncrement = 0f;
        static JObject balance = UtilityFunctions.GetJsonFromFile("balance.json");

        public void Awake()
        {
            m_name = "Wolftears";
            base.name = "Wolftears";
            m_tooltip = "Gain +" + m_damageIncrement * 100 + "% damage for every 10% of HP missing, up to " + m_damageBonus * 100 + "% when below " + m_activationHealth * 100 + "% HP." +
                $"\nEvery {(float)balance["wolftearOneHitTTL"]} seconds, when an attack would have killed you, survive at 1 hp.";
            m_icon = null;
        }

        public void SetIcon()
        {
            if(m_icon == null)
                m_icon = Prefab.Cache.GetPrefab<ItemDrop>("HelmetIron").m_itemData.GetIcon();
            int damageDisplay = Mathf.RoundToInt((1-m_character.GetHealthPercentage()) * 10);
            if (m_character.GetHealthPercentage() <= m_activationHealth)
                damageDisplay = 10;

            if (damageDisplay > 0)
                m_name = base.name + " " + damageDisplay;
            else
                m_name = base.name;

            if (damageDisplay > 4 && !m_character.GetSEMan().HaveStatusEffect("WolftearsFX"))
            {
                Log.LogInfo("Adding status");
                m_character.GetSEMan().AddStatusEffect("WolftearsFX");
            } else if (damageDisplay <= 4 && m_character.GetSEMan().HaveStatusEffect("WolftearsFX"))
            {
                Log.LogInfo("Remove status");
                m_character.GetSEMan().RemoveStatusEffect("WolftearsFX");
            }
        }

        public void ClearIcon()
        {
            m_icon = null;
        }

        public override void UpdateStatusEffect(float dt)
        {
            base.UpdateStatusEffect(dt);
            SetIcon();
        }


        public void SetDamageBonus(float bonus)
        {
            m_damageBonus = bonus;
            m_damageIncrement = m_damageBonus / 10;
            m_tooltip = "Gain +" + m_damageIncrement * 100 + "% damage for every 10% of HP missing, up to " + m_damageBonus * 100 + "% when below " + m_activationHealth * 100 + "% HP." +
                $"\nEvery {(float)balance["wolftearOneHitTTL"]} seconds, when an attack would have killed you, survive at 1 hp.";
        }

        public float GetDamageBonus()
        {
            if (m_character.GetHealthPercentage() <= m_activationHealth)
                return m_damageBonus;
            else if (m_character.GetHealthPercentage() < 0.90f)
                return (Mathf.RoundToInt((1 - m_character.GetHealthPercentage()) * 10)) * m_damageIncrement;
            else
                return 0f;
        }

        public void SetActivationHP(float bonus)
        {
            //Log.LogInfo("Setting Bonus: " + bonus * 10 + "%");
            m_activationHealth = bonus;
            m_tooltip = "Gain +" + m_damageIncrement * 100 + "% damage for every 10% of HP missing, up to " + m_damageBonus * 100 + "% when below " + m_activationHealth * 100 + "% HP." +
                $"\nEvery {(float)balance["wolftearOneHitTTL"]} seconds, when an attack would have killed you, survive at 1 hp.";
        }

        public float GetActivationHP() { return m_activationHealth; }
    }
}
