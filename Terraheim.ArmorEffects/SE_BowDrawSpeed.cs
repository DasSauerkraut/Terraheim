namespace Terraheim.ArmorEffects;

public class SE_BowDrawSpeed : StatusEffect
{
    public float m_bonus = 0f;

    public void Awake()
    {
        m_name = "Bow Draw Speed";
        name = "Bow Draw Speed";
    }

    public void SetSpeed(float bonus)
    {
        m_bonus = bonus;
    }

    public float GetSpeed()
    {
        return m_bonus;
    }
}
