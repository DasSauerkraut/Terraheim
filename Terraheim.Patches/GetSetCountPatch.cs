using HarmonyLib;
using Newtonsoft.Json.Linq;
using Terraheim.ArmorEffects;
using Terraheim.Utility;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class GetSetCountPatch
{
    public void Awake()
    {
        Log.LogInfo("Get Set Count Patching Complete");
    }

    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.GetSetCount))]
    [HarmonyPostfix]
    public static void RunPostfix(Humanoid __instance, string setName, ref int __result)
    {
        if (__instance.GetSEMan().HaveStatusEffect("Set Bonus Increase"))
        {
            Log.LogInfo("Set Bonus Increased");
            __result += 1;
        }
    }
}
