
using System;
using System.Collections.Generic;
using Terraheim.Utility;
using UnityEngine;

namespace Terraheim.ArmorEffects.ChosenEffects
{
    class SE_Chosen : StatusEffect
    {
        public bool m_boonLock = false;
        public float m_lockTimer = 0f;
        public int m_boonBaseTTL = 120;
        public int m_baneBaseTTL = 120;
        public int m_boonMaxTTL = 300;
        public int m_baneMaxTTL = 240;
        public int m_boonTTLIncrease;
        public int m_baneTTLIncrease;
        public List<string> m_currentBoons = new List<string>();
        public List<string> m_currentBanes = new List<string>();
        private System.Random m_dice = new System.Random();


        public void Awake()
        {
            m_name = "Chosen";
            base.name = "Chosen";
            SetTooltip();
            m_icon = AssetHelper.SpriteChosen;
        }

        public void SetTooltip()
        {
            m_tooltip = $"Malignent eyes are upon you. Gain a random Boon or Bane for 2min on kill. Parrying attacks increases Boon time to live by {m_boonTTLIncrease}s. " +
                $"Getting hit increases Bane TTL for {m_baneTTLIncrease}s. Getting 4 Boons or Banes Blesses/Curses you, which clears the other type and increases TTL to 5/4 minutes." +
                $"While Blessed or Cursed, you cannot gain new Boons or Banes.";
        }

        public void SetBoonTTLIncrease(int bonus)
        {
            m_boonTTLIncrease = bonus;
            SetTooltip();
        }

        public void SetBaneTTLIncrease(int bonus)
        {
            m_baneTTLIncrease = bonus;
            SetTooltip();
        }

        public int GetBoonTTLIncrease() { return m_boonTTLIncrease; }

        public int GetBaneTTLIncrease() { return m_baneTTLIncrease; }

        public override void UpdateStatusEffect(float dt)
        {
            if (m_lockTimer > 0)
            {
                //Log.LogInfo(m_lockTimer);
                m_lockTimer -= dt;
                if (m_lockTimer <= 0)
                {
                    m_lockTimer = 0;
                    m_boonLock = false;
                    m_icon = AssetHelper.SpriteChosen;
                    m_name = "Chosen";
                }
            }
            else if (m_boonLock)
            {
                m_boonLock = false;
                m_lockTimer = 0;
                m_icon = AssetHelper.SpriteChosen;
                m_name = "Chosen";
            }
            base.UpdateStatusEffect(dt);
        }

        public void OnKill()
        {
            Log.LogInfo("Chosen Kill");
            if (m_boonLock)
                return;
            //Log.LogInfo("Not Locked");
            bool added = false;
            SEMan seman = m_character.GetSEMan();
            while (!added)
            {
            int roll = m_dice.Next(1, 8+1);
            //Log.LogInfo("roll " + roll);

                switch (roll)
                {
                    case 1:
                        if (!seman.HaveStatusEffect("Bloated"))
                        {
                            seman.AddStatusEffect("Bloated");
                            (seman.GetStatusEffect("Bloated") as SE_Bloated).TTL = m_boonBaseTTL;
                            m_currentBoons.Add("Bloated");
                            added = true;
                            
                            var executionVFX = Instantiate(AssetHelper.FXBoonGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBoonGain);
                        }
                        break;
                    case 2:
                        if (!seman.HaveStatusEffect("Bloodlust"))
                        {
                            seman.AddStatusEffect("Bloodlust");
                            (seman.GetStatusEffect("Bloodlust") as SE_Bloodlust).TTL = m_boonBaseTTL;
                            m_currentBoons.Add("Bloodlust");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBoonGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBoonGain);
                        }
                        break;
                    case 3:
                        if (!seman.HaveStatusEffect("Hidden Knowledge"))
                        {
                            seman.AddStatusEffect("Hidden Knowledge");
                            (seman.GetStatusEffect("Hidden Knowledge") as SE_HiddenKnowledge).TTL = m_boonBaseTTL;
                            m_currentBoons.Add("Hidden Knowledge");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBoonGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBoonGain);
                        }
                        break;
                    case 4:
                        if (!seman.HaveStatusEffect("Adrenaline"))
                        {
                            seman.AddStatusEffect("Adrenaline");
                            (seman.GetStatusEffect("Adrenaline") as SE_Adrenaline).TTL = m_boonBaseTTL;
                            m_currentBoons.Add("Adrenaline");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBoonGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBoonGain);
                        }
                        break;
                    case 5:
                        if (!seman.HaveStatusEffect("Blinding Rage"))
                        {
                            seman.AddStatusEffect("Blinding Rage");
                            (seman.GetStatusEffect("Blinding Rage") as SE_BlindingRage).TTL = m_baneBaseTTL;
                            m_currentBanes.Add("Blinding Rage");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBaneGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBaneGain);
                        }
                        break;
                    case 6:
                        if (!seman.HaveStatusEffect("Pestilence"))
                        {
                            seman.AddStatusEffect("Pestilence");
                            (seman.GetStatusEffect("Pestilence") as SE_Pestilence).TTL = m_baneBaseTTL;
                            m_currentBanes.Add("Pestilence");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBaneGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBaneGain);
                        }
                        break;
                    case 7:
                        if (!seman.HaveStatusEffect("Maddening Visions"))
                        {
                            seman.AddStatusEffect("Maddening Visions");
                            (seman.GetStatusEffect("Maddening Visions") as SE_MaddeningVisions).TTL = m_baneBaseTTL;
                            m_currentBanes.Add("Maddening Visions");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBaneGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBaneGain);
                        }
                        break;
                    case 8:
                        if (!seman.HaveStatusEffect("Withdrawals"))
                        {
                            seman.AddStatusEffect("Withdrawals");
                            (seman.GetStatusEffect("Withdrawals") as SE_Withdrawals).TTL = m_baneBaseTTL;
                            m_currentBanes.Add("Withdrawals");
                            added = true;

                            var executionVFX = Instantiate(AssetHelper.FXBaneGain, m_character.GetCenterPoint(), Quaternion.identity);
                            ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                            foreach (ParticleSystem particle in children)
                            {
                                particle.Play();
                            }
                            var audioSource = m_character.GetComponent<AudioSource>();
                            if (audioSource == null)
                            {
                                audioSource = m_character.gameObject.AddComponent<AudioSource>();
                                audioSource.playOnAwake = false;
                            }
                            audioSource.PlayOneShot(AssetHelper.SFXBaneGain);
                        }
                        break;
                    default:
                        break;
                }
            }
            if(m_currentBoons.Count == 4)
            {
                Log.LogInfo("Boon Set of 4");

                m_boonLock = true;
                m_lockTimer = (float)m_boonMaxTTL;
                m_icon = AssetHelper.SpriteChosenBoon;
                m_name = base.name + "\nBlessed";
                var executionVFX = Instantiate(AssetHelper.FXBoonLock, m_character.GetCenterPoint(), Quaternion.identity);
                ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in children)
                {
                    particle.Play();
                }
                var audioSource = m_character.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = m_character.gameObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                }
                audioSource.PlayOneShot(AssetHelper.SFXBoonLock);

                SE_Bloated boon1 = seman.GetStatusEffect("Bloated") as SE_Bloated;
                boon1.SetTTL(m_boonMaxTTL);
                SE_Bloodlust boon2 = seman.GetStatusEffect("Bloodlust") as SE_Bloodlust;
                boon2.SetTTL(m_boonMaxTTL);
                SE_HiddenKnowledge boon3 = seman.GetStatusEffect("Hidden Knowledge") as SE_HiddenKnowledge;
                boon3.SetTTL(m_boonMaxTTL);
                SE_Adrenaline boon4 = seman.GetStatusEffect("Adrenaline") as SE_Adrenaline;
                boon4.SetTTL(m_boonMaxTTL);

                if (seman.HaveStatusEffect("Blinding Rage"))
                    seman.RemoveStatusEffect("Blinding Rage");
                if (seman.HaveStatusEffect("Pestilence"))
                    seman.RemoveStatusEffect("Pestilence");
                if (seman.HaveStatusEffect("Maddening Visions"))
                    seman.RemoveStatusEffect("Maddening Visions");
                if (seman.HaveStatusEffect("Withdrawals"))
                    seman.RemoveStatusEffect("Withdrawals");
            }
            else if(m_currentBanes.Count == 4)
            {
                Log.LogInfo("Bane Set of 4");

                m_boonLock = true;
                m_lockTimer = (float)m_baneMaxTTL;
                m_icon = AssetHelper.SpriteChosenBane;
                m_name = base.name + "\nCursed";

                var executionVFX = Instantiate(AssetHelper.FXBaneLock, m_character.GetCenterPoint(), Quaternion.identity);
                ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                foreach (ParticleSystem particle in children)
                {
                    particle.Play();
                }
                var audioSource = m_character.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = m_character.gameObject.AddComponent<AudioSource>();
                    audioSource.playOnAwake = false;
                }
                audioSource.PlayOneShot(AssetHelper.SFXBaneLock);

                SE_BlindingRage bane1 = seman.GetStatusEffect("Blinding Rage") as SE_BlindingRage;
                bane1.SetTTL(m_baneMaxTTL);
                SE_Pestilence bane2 = seman.GetStatusEffect("Pestilence") as SE_Pestilence;
                bane2.SetTTL(m_baneMaxTTL);
                SE_MaddeningVisions bane3 = seman.GetStatusEffect("Maddening Visions") as SE_MaddeningVisions;
                bane3.SetTTL(m_baneMaxTTL);
                SE_Withdrawals bane4 = seman.GetStatusEffect("Withdrawals") as SE_Withdrawals;
                bane4.SetTTL(m_baneMaxTTL);

                if (seman.HaveStatusEffect("Bloated"))
                    seman.RemoveStatusEffect("Bloated");
                if (seman.HaveStatusEffect("Bloodlust"))
                    seman.RemoveStatusEffect("Bloodlust");
                if (seman.HaveStatusEffect("Hidden Knowledge"))
                    seman.RemoveStatusEffect("Hidden Knowledge");
                if (seman.HaveStatusEffect("Adrenaline"))
                    seman.RemoveStatusEffect("Adrenaline");
            }
        }

        public void OnParry()
        {
            Log.LogInfo("Chosen Parry");
            SEMan seman = m_character.GetSEMan();
            if (seman.HaveStatusEffect("Bloated"))
                (seman.GetStatusEffect("Bloated") as SE_Bloated).IncreaseTTL(m_boonTTLIncrease);
            if (seman.HaveStatusEffect("Bloodlust"))
                (seman.GetStatusEffect("Bloodlust") as SE_Bloodlust).IncreaseTTL(m_boonTTLIncrease);
            if (seman.HaveStatusEffect("Hidden Knowledge"))
                (seman.GetStatusEffect("Hidden Knowledge") as SE_HiddenKnowledge).IncreaseTTL(m_boonTTLIncrease);
            if (seman.HaveStatusEffect("Adrenaline"))
                (seman.GetStatusEffect("Adrenaline") as SE_Adrenaline).IncreaseTTL(m_boonTTLIncrease);
        }

        public void OnTakeDamage()
        {
            Log.LogInfo("Chosen Take Damage");
            SEMan seman = m_character.GetSEMan();
            if (seman.HaveStatusEffect("Blinding Rage"))
                (seman.GetStatusEffect("Blinding Rage") as SE_BlindingRage).IncreaseTTL(m_baneTTLIncrease);
            if (seman.HaveStatusEffect("Pestilence"))
                (seman.GetStatusEffect("Pestilence") as SE_Pestilence).IncreaseTTL(m_baneTTLIncrease);
            if (seman.HaveStatusEffect("Maddening Visions"))
                (seman.GetStatusEffect("Maddening Visions") as SE_MaddeningVisions).IncreaseTTL(m_baneTTLIncrease);
            if (seman.HaveStatusEffect("Withdrawals"))
                (seman.GetStatusEffect("Withdrawals") as SE_Withdrawals).IncreaseTTL(m_baneTTLIncrease);
        }

        public void OnKnifeUse()
        {
            Log.LogInfo("Knife used");
            if (!m_boonLock)
            {
                int hpChange = (m_currentBoons.Count * 15) - (m_currentBanes.Count * 25);
                Log.LogInfo("HP Change " + hpChange);
                if(hpChange < 0)
                {
                    hpChange *= -1;
                    HitData damage = new HitData();
                    damage.m_damage.m_damage = hpChange;
                    m_character.Damage(damage);
                }
                else if (hpChange > 0)
                {
                    m_character.Heal(hpChange);
                }
                SEMan seman = m_character.GetSEMan();
                foreach(var boon in m_currentBoons)
                    seman.RemoveStatusEffect(boon);
                foreach (var bane in m_currentBanes)
                    seman.RemoveStatusEffect(bane);
                m_currentBoons.Clear();
                m_currentBanes.Clear();

                if(hpChange != 0)
                {
                    var executionVFX = Instantiate(AssetHelper.FXBoonLock, m_character.GetCenterPoint(), Quaternion.identity);
                    ParticleSystem[] children = executionVFX.GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem particle in children)
                    {
                        particle.Play();
                    }
                    var audioSource = m_character.GetComponent<AudioSource>();
                    if (audioSource == null)
                    {
                        audioSource = m_character.gameObject.AddComponent<AudioSource>();
                        audioSource.playOnAwake = false;
                    }
                    audioSource.PlayOneShot(AssetHelper.SFXBoonLock);

                    var banelock = Instantiate(AssetHelper.FXBaneLock, m_character.GetCenterPoint(), Quaternion.identity);
                    ParticleSystem[] children2 = banelock.GetComponentsInChildren<ParticleSystem>();
                    foreach (ParticleSystem particle in children2)
                    {
                        particle.Play();
                    }
                    var audioSource2 = m_character.GetComponent<AudioSource>();
                    if (audioSource2 == null)
                    {
                        audioSource2 = m_character.gameObject.AddComponent<AudioSource>();
                        audioSource2.playOnAwake = false;
                    }
                    audioSource.PlayOneShot(AssetHelper.SFXBaneLock);
                }
            }
        }
    }
}
