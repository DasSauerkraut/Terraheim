using Jotunn.Managers;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects
{
    class SE_Withdrawals : StatusEffect
    {
        private bool m_hasTriggered = false;
        private float m_foodEffect;
        private float m_meadEffect;
        private float m_positiveMeadEffect;
        private const int m_meadbaseTTL = 120;
        private const int m_positiveMeadTTL = 600;
        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Withdrawals";
            base.name = "Withdrawals";
            m_tooltip = "Mead cooldowns last 2x longer, Food and other Meads last half as long";
        }

        public override void Setup(Character character)
        {
            m_foodEffect = (float)Terraheim.balance["baneSettings"]["withdrawals"]["foodDegradeRate"];
            m_meadEffect = (float)Terraheim.balance["baneSettings"]["withdrawals"]["meadCooldown"];
            m_positiveMeadEffect = (float)Terraheim.balance["baneSettings"]["withdrawals"]["meadDuration"];
            m_icon = AssetHelper.SpriteChosenSlaaneshBane;
            Log.LogWarning("Adding Withdrawals");
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            SEMan seman = m_character.GetSEMan();
            foreach (var effect in m_character.GetSEMan().GetStatusEffects())
            {
                if(effect.m_name.Contains("mead_hp") || effect.m_name.Contains("mead_stamina"))
                {
                    if (effect.m_ttl == m_meadbaseTTL)
                    {
                        effect.m_time -= m_ttl * m_meadEffect;
                        if (effect.m_time < 0)
                            effect.m_time = 0;
                        effect.m_ttl *= m_meadEffect;
                        //Log.LogInfo($"TTL {effect.m_ttl}, Time {effect.m_time}");
                    }
                }
                else if(effect.m_name.Contains("barleywine") || effect.m_name.Contains("mead_frostres") || effect.m_name.Contains("mead_poisonres"))
                {
                    if(effect.m_ttl == m_positiveMeadTTL)
                    {
                        effect.m_ttl *= m_positiveMeadEffect;
                    }
                }
            }
            if(seman.HaveStatusEffect("$item_mead_stamina_medium") || seman.HaveStatusEffect("$item_mead_stamina_minor") || seman.HaveStatusEffect("$item_mead_hp_minor") || seman.HaveStatusEffect("$item_mead_hp_medium"))
            if (m_time >= TTL - 0.1f && !m_hasTriggered)
            {
                if (seman.HaveStatusEffect("Chosen"))
                    (seman.GetStatusEffect("Chosen") as SE_Chosen).m_currentBanes.Remove(m_name);
            }
            base.UpdateStatusEffect(dt);
        }

        public void SetTTL(float newTTL)
        {
            TTL = newTTL;
            m_time = 0;
        }

        public void IncreaseTTL(float increase)
        {
            TTL += increase;
            m_time -= increase;
        }

        public float GetMeadEffect() { return m_meadEffect; }
        public float GetFoodEffect() { return m_foodEffect; }
    }
}
