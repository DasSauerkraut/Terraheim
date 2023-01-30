namespace Terraheim.ArmorEffects;

internal class SE_CoinDrop : StatusEffect
{
	public float m_chance = 0f;

	public float m_bonus = 0f;

	public void Awake()
	{
		m_name = "Coin Drop";
		base.name = "Coin Drop";
		m_tooltip = m_chance + "% chance to drop " + m_bonus + " coins.";
	}

	public void SetChance(float bonus)
	{
		m_chance = bonus * 100f;
		m_tooltip = m_chance + "% chance to drop " + m_bonus + " coins.";
	}

	public float GetChance()
	{
		return m_chance;
	}

	public void SetCoinAmount(float bonus)
	{
		m_bonus = bonus;
		m_tooltip = m_chance + "% chance to drop " + m_bonus + " coins.";
	}

	public float GetCoinAmount()
	{
		return m_bonus;
	}
}
