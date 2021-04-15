﻿using System.Linq;
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

        public static GameObject TestProjectile;
        public static GameObject TestExplosion;

        public static void Init()
        {
            TerraheimAssetBundle = GetAssetBundleFromResources(AssetBundleName);

            FXLifeSteal = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_Life_Steal_Hit.prefab");
            FXRedTearstone = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_redtearstone.prefab");
            FXDamageAtFullHp = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_damageAtFullHP.prefab");
            FXThorns = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_Thorns_hit.prefab");
            FXAoECharged = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/vfx_AoECharge.prefab");

            TestExplosion = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/test_explosion.prefab");
            TestProjectile = TerraheimAssetBundle.LoadAsset<GameObject>("Assets/CustomItems/Effects/test_projectile.prefab");
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
    }
}