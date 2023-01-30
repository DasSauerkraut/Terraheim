namespace Terraheim.ArmorEffects;

internal class SE_AfterburnCooldown : StatusEffect
{
	public float TTL
	{
		get
		{
			return m_ttl;
		}
		set
		{
			m_ttl = value;
		}
	}

	public void Awake()
	{
		m_name = "Afterburn Cooldown";
		base.name = "Afterburn Cooldown";
		m_tooltip = "";
		m_icon = null;
	}

	public override void Setup(Character character)
	{
		TTL = 5f;
		Log.LogMessage("Cooldown started " + character.m_name + "!");
		base.Setup(character);
	}
}
