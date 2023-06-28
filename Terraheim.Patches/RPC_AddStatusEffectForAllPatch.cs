
using HarmonyLib;
using System;

namespace Terraheim.Patches
{
    [HarmonyPatch]

    internal class RPC_AddStatusEffectForAllPatch
    {
        public static SEMan m_seman;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SEMan), MethodType.Constructor, new Type[] { typeof(Character), typeof(ZNetView) })]
        public static void SEManPostfix(SEMan __instance, ref Character character, ref ZNetView nview)
        {
            m_seman = __instance;
            //var action = new Action<long>();
            nview.Register<string, bool, int, float>("RPC_AddStatusEffectForAll", RPC_AddStatusEffectForAll);
        }

        public static void RPC_AddStatusEffectForAll(long sender, string name, bool resetTime, int itemLevel, float skillLevel)
        {
            m_seman.Internal_AddStatusEffect(name.GetStableHashCode(), resetTime, itemLevel, skillLevel);
        }
    }
}
