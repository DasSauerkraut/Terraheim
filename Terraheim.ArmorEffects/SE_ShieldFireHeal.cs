namespace Terraheim.ArmorEffects;

internal class SE_ShieldFireHeal : SE_Stats
{
	public void Awake()
	{
		m_name = "ShieldFireHeal";
		base.name = "ShieldFireHeal";
		m_tooltip = "";
		m_healthOverTime = 0f;
		m_healthOverTimeInterval = 0.5f;
		m_ttl = 4f;
	}
}
