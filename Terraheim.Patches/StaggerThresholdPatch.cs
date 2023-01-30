using HarmonyLib;
using Terraheim.ArmorEffects;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class StaggerThresholdPatch
    {
        [HarmonyPatch(typeof(Character), "GetStaggerTreshold")]
        public static void Postfix(Character __instance, ref float __result)
        {
            if(__instance.GetSEMan().HaveStatusEffect("Stagger Capacity"))
            {
                //Log.LogInfo($"Has Stagger Cap, initial cap {__result}");
                __result += __result * (__instance.GetSEMan().GetStatusEffect("Stagger Capacity") as SE_StaggerCapacity).GetStaggerCap();
                //Log.LogInfo($"Post {__result}");
            }
        }
    }
}
