using System;
using HarmonyLib;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class AddStatusEffectPatch
{
	[HarmonyPatch(typeof(SEMan), "AddStatusEffect", new Type[]
	{
		typeof(string),
		typeof(bool)
	})]
	public static bool Prefix(SEMan __instance, ref string name) => 
    !__instance.HaveStatusEffect("Waterproof") || name != "Wet";
}
