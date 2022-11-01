﻿using HarmonyLib;

namespace Terraheim.Patches
{
    [HarmonyPatch]
    class AddStatusEffectPatch
    {
        [HarmonyPatch(typeof(SEMan), nameof(SEMan.AddStatusEffect), typeof(string), typeof(bool))]
        public static bool Prefix(SEMan __instance, ref string name) => 
            !__instance.HaveStatusEffect("Waterproof") || name != "Wet";
    }
}
