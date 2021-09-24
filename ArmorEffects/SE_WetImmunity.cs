
namespace Terraheim.ArmorEffects
{
    class SE_WetImmunity : StatusEffect
    {
        public void Awake()
        {
            m_name = "Waterproof";
            base.name = "Waterproof";
            m_tooltip = $"\nYou will not gain the <color=cyan>Wet</color> Ailment.";
        }

        public override void UpdateStatusEffect(float dt)
        {
            base.UpdateStatusEffect(dt);
            if (m_character.GetSEMan().HaveStatusEffect("Wet"))
                m_character.GetSEMan().RemoveStatusEffect("Wet");
        }
    }
}
