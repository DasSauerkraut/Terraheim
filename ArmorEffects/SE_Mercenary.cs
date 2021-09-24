using Jotunn.Managers;

namespace Terraheim.ArmorEffects
{
    class SE_Mercenary : StatusEffect
    {

        private float m_maxDamage;
        private float m_damagePerCoin;
        private float m_currentDamage;
        private float m_coinThreshold;
        private int m_coinUse;

        public void Awake()
        {
            m_name = "Mercenary";
            base.name = "Mercenary";
            SetTooltip();
        }
        public void SetIcon()
        {
            if(Terraheim.hasJudesEquipment)
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>(Utility.Data.ArmorSets["nomad"].HelmetID).m_itemData.GetIcon();
            else
                m_icon = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather").m_itemData.GetIcon();
        }

        public void SetMaxDamage(float val)
        {
            m_maxDamage = val;
            m_damagePerCoin = m_maxDamage / m_coinThreshold;
            SetTooltip();
        }

        public float GetCurrentDamage() { return m_currentDamage; }

        public void SetCoinThreshold(int coin)
        {
            m_coinThreshold = coin;
            SetTooltip();
        }

        public void SetCoinUse(int coin)
        {
            m_coinUse = coin;
            SetTooltip();
        }

        private void SetTooltip()
        {
            m_tooltip = $"Gain {m_damagePerCoin * 100}% damage per coin in your inventory, up to a {m_maxDamage * 100}% boost when at or above {m_coinThreshold} coins. " +
                $"Whenever you attack, {m_coinUse} coin is consumed.";
        }

        private float lastdt = -1;
        private float m_clock = 0;
        public override void UpdateStatusEffect(float dt)
        {
            m_clock += dt;
            //Log.LogInfo($"last dt{lastdt} dt{m_clock} diff{m_clock - lastdt}");
            if (m_clock - lastdt > 0.75f || lastdt == -1)
            {
                lastdt = m_clock;
                int numCoins = (m_character as Player).GetInventory().CountItems("$item_coins");
                //Log.LogWarning($"Num coins {numCoins}");
                m_currentDamage = m_damagePerCoin * numCoins;
                if (numCoins > 0)
                {
                    m_name = $"{base.name}\n{(m_currentDamage * 100):#.##}%";
                }
                else
                {
                    m_name = base.name;
                    m_currentDamage = 0;
                }
            }
            if(m_clock > 10000)
            {
                m_clock = 0;
                lastdt = -1;
            }
            //Log.LogInfo(m_coin.m_shared.m_name);
            base.UpdateStatusEffect(dt);
        }
    }
}
