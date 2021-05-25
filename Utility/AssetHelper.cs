using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Terraheim.Utility
{
    class AssetHelper
    {
        public const string AssetBundleName = "bundle_terraheimeffects";
        public static AssetBundle TerraheimAssetBundle;

        public static GameObject FXLifeSteal;
        public static GameObject FXRedTearstone;
        public static GameObject FXDamageAtFullHp;
        public static GameObject FXThorns;
        public static GameObject FXAoECharged;
        public static GameObject FXExecution;
        public static GameObject FXMarkedForDeath;
        public static GameObject FXMarkedForDeathHit;
        public static GameObject FXPinned;
        public static GameObject FXBoonGain;
        public static GameObject FXBaneGain;
        public static GameObject FXBoonLock;
        public static GameObject FXBaneLock;
        
        public static GameObject ItemSalamanderFurPrefab;
        public static GameObject PieceReforgerPrefab;
        public static GameObject PieceReforgerExt1Prefab;
        public static GameObject PieceReforgerExt2Prefab;
        public static GameObject PieceReforgerExt3Prefab;
        public static GameObject PieceReforgerExt4Prefab;
        public static GameObject PieceReforgerExt5Prefab;

        public static GameObject TestProjectile;
        public static GameObject TestExplosion;
        public static GameObject BowFireExplosionPrefab;
        public static GameObject FlamebowWyrdExplosion;

        public static AudioClip AoEReady;
        public static AudioClip SFXExecution;
        public static AudioClip SFXMarkedForDeath; 
        public static AudioClip SFXBoonGain;
        public static AudioClip SFXBaneGain;
        public static AudioClip SFXBoonLock;
        public static AudioClip SFXBaneLock;

        public static Sprite SpriteTempStatus;

        public static Sprite SpriteChosen;
        public static Sprite SpriteChosenBoon;
        public static Sprite SpriteChosenBane;
        public static Sprite SpriteChosenKhorneBoon;
        public static Sprite SpriteChosenKhorneBane;
        public static Sprite SpriteChosenTzeentchBoon;
        public static Sprite SpriteChosenTzeentchBane;
        public static Sprite SpriteChosenNurgleBoon;
        public static Sprite SpriteChosenNurgleBane;
        public static Sprite SpriteChosenSlaaneshBoon;
        public static Sprite SpriteChosenSlaaneshBane;


        public static void Init()
        {
            TerraheimAssetBundle = GetAssetBundleFromResources(AssetBundleName);

            FXLifeSteal = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_Life_Steal_Hit.prefab");
            FXRedTearstone = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_redtearstone.prefab");
            FXDamageAtFullHp = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_damageAtFullHP.prefab");
            FXThorns = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_Thorns_hit.prefab");
            FXAoECharged = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_AoECharge.prefab");
            FXExecution = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_Execution.prefab");
            FXMarkedForDeath = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_MarkedForDeath.prefab");
            FXMarkedForDeathHit = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_MarkedForDeathHit.prefab");
            FXPinned = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_Pinned.prefab");

            TestExplosion = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/test_explosion.prefab");
            TestProjectile = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/test_projectile.prefab");
            FlamebowWyrdExplosion = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/flametal/bow/bowFire_wyrdexplosion.prefab");
            BowFireExplosionPrefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/flametal/bow/bowFire_explosion1.prefab");

            ItemSalamanderFurPrefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/material/SalamanderFurTH.prefab");
            PieceReforgerPrefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/CraftingBench/reforger.prefab");
            PieceReforgerExt1Prefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/CraftingBench/ext1/reforger_ext1.prefab");
            PieceReforgerExt2Prefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/CraftingBench/ext2/reforger_ext2.prefab");
            PieceReforgerExt3Prefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/CraftingBench/ext3/reforger_ext3.prefab");
            PieceReforgerExt4Prefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/CraftingBench/ext4/reforger_ext4.prefab");
            PieceReforgerExt5Prefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/CraftingBench/ext5/reforger_ext5.prefab");

            AoEReady = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Dragon_BreathIce5.wav");
            SFXExecution = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Magic_Spell_EnergyBall8.wav");
            SFXMarkedForDeath = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/DarkMagic_SpellImpact15.wav");

            SpriteTempStatus = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/TEMPICON.png");

            SpriteChosen = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/chosen.png");
            SpriteChosenBoon = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/chosenBoonLock.png");
            SpriteChosenBane = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/chosenBaneLock.png");
            SpriteChosenKhorneBoon = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/KhorneBoon.png");
            SpriteChosenKhorneBane = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/khorneBane.png");
            SpriteChosenTzeentchBoon = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/tzeenBoon.png");
            SpriteChosenTzeentchBane = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/tzeenBane.png");
            SpriteChosenNurgleBoon = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/nurgBoon.png");
            SpriteChosenNurgleBane = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/nurgBane.png");
            SpriteChosenSlaaneshBoon = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/slaanBoon.png");
            SpriteChosenSlaaneshBane = TerraheimAssetBundle.LoadAsset<Sprite>("Assets/CustomItems/Effects/Chosen/SlaanBane.png");


            FXBoonGain = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/Chosen/vfx_boonGain.prefab");
            FXBaneGain = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/Chosen/vfx_baneGain.prefab");
            FXBoonLock = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/Chosen/vfx_boonLock.prefab");
            FXBaneLock = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/Chosen/vfx_baneLock.prefab");
            SFXBoonGain = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Chosen/boongain.wav");
            SFXBaneGain = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Chosen/baingain.wav");
            SFXBoonLock = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Chosen/boonLock.wav");
            SFXBaneLock = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Chosen/banelock.wav");
        }

        public static AssetBundle GetAssetBundleFromResources(string filename)
        {
            var execAssembly = Assembly.GetExecutingAssembly();
            var resourceName = execAssembly.GetManifestResourceNames().Single(str => str.EndsWith(filename));

            using (var stream = execAssembly.GetManifestResourceStream(resourceName))
            {
                return AssetBundle.LoadFromStream(stream);
            }
        }

        public static void SetupVFX()
        {
            Data.VFXRedTearstone = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXRedTearstone,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = false,
                m_inheritParentScale = false,
                m_randomRotation = false,
                m_scale = true
            };

            Data.VFXDamageAtFullHp = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXDamageAtFullHp,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = false,
                m_inheritParentScale = false,
                m_randomRotation = false,
                m_scale = true
            };

            Data.VFXAoECharged = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXAoECharged,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = true,
                m_inheritParentScale = false,
                m_randomRotation = false,
                m_scale = true
            };

            Data.VFXMarkedForDeath = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXMarkedForDeath,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = false,
                m_inheritParentScale = true,
                m_randomRotation = false,
                m_scale = true
            };

            Data.VFXPinned = new EffectList.EffectData()
            {
                m_prefab = Utility.AssetHelper.FXPinned,
                m_enabled = true,
                m_attach = true,
                m_inheritParentRotation = false,
                m_inheritParentScale = true,
                m_randomRotation = false,
                m_scale = true
            };
        }
    }
}
