using HarmonyLib;
using System.Collections;
using UnityEngine;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class JudeArmorPatch
    {
        [HarmonyPatch(typeof(ObjectDB), "Awake")]
        [HarmonyPostfix]
        private static void DBAwake(ObjectDB __instance)
        {
            //Log.LogWarning("DBAwake");
            if(Terraheim.hasJudesEquipment)
                __instance.StartCoroutine(DelayedInsertion(__instance, ZNetScene.instance));
        }
        /*
        [HarmonyPatch(typeof(ObjectDB), "CopyOtherDB")]
        [HarmonyPostfix]
        private static void DBAwaake(ObjectDB __instance)
        {
            Log.LogWarning("CopyOtherDB");
            if (Terraheim.hasJudesEquipment)
                __instance.StartCoroutine(DelayedInsertion(__instance, ZNetScene.instance));
        }*/

        public static IEnumerator DelayedInsertion(ObjectDB odb, ZNetScene zns)
        {
            yield return new WaitForSeconds(0.15f);
            //Log.LogWarning("After wait");
            Armor.ModExistingSets.ModJudes();
        }
    }
}
