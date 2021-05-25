using Jotunn.Managers;
using Terraheim.ArmorEffects.ChosenEffects;
using Terraheim.Utility;

namespace Terraheim.ArmorEffects
{
    class SE_HiddenKnowledge : StatusEffect
    {
        private bool m_hasTriggered = false;

        public float TTL
        {
            get { return m_ttl; }
            set { m_ttl = value; }
        }

        public void Awake()
        {
            m_name = "Hidden Knowledge";
            base.name = "Hidden Knowledge";
            m_tooltip = "";
        }

        public override void Setup(Character character)
        {
            m_icon = AssetHelper.SpriteChosenTzeentchBoon;
            Log.LogWarning("Adding Hidden Knowledge");
            base.Setup(character);
        }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_time >= TTL - 0.1f && !m_hasTriggered)
            {
                SEMan seman = m_character.GetSEMan();
                if (seman.HaveStatusEffect("Chosen"))
                    (seman.GetStatusEffect("Chosen") as SE_Chosen).m_currentBoons.Remove(m_name);
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
    }
}
