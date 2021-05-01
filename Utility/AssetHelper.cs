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

        public static GameObject TestProjectile;
        public static GameObject TestExplosion;
        public static GameObject BowFireExplosionPrefab;
        public static GameObject FlamebowWyrdExplosion;

        public static AudioClip AoEReady;
        public static AudioClip SFXExecution;
        public static AudioClip SFXMarkedForDeath;

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

            TestExplosion = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/test_explosion.prefab");
            TestProjectile = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/test_projectile.prefab");
            FlamebowWyrdExplosion = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/flametal/bow/bowFire_wyrdexplosion.prefab");
            BowFireExplosionPrefab = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/flametal/bow/bowFire_explosion.prefab");

            AoEReady = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Dragon_BreathIce5.wav");
            SFXExecution = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/Magic_Spell_EnergyBall8.wav");
            SFXMarkedForDeath = TerraheimAssetBundle.LoadAsset<AudioClip>("Assets/CustomItems/Effects/DarkMagic_SpellImpact15.wav");

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
        }
    }
}
