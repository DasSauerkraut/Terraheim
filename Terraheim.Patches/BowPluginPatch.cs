using HarmonyLib;

namespace Terraheim.Patches;

[HarmonyPatch]
internal class BowPluginPatch
{
    [HarmonyPatch(typeof(Player), "PlayerAttackInput")]
    public static bool Prefix(Player __instance, float dt)
    {
        if (Player.m_localPlayer == __instance)
        {
            if (Terraheim.hasBowPlugin)
            {
                ItemDrop.ItemData currentWeapon = __instance.GetCurrentWeapon();
                if (currentWeapon != null && currentWeapon.m_shared.m_name.Contains("bow_blackmetalTH"))
                {
                    currentWeapon.m_shared.m_attack.m_drawDurationMin = 1.5f;
                }

                if (currentWeapon != null && currentWeapon.m_shared.m_name.Contains("bow_fireTH"))
                {
                    currentWeapon.m_shared.m_attack.m_drawDurationMin = 0.5f;
                }
            }
        }

        return true;
    }
}