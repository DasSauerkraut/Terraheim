
using HarmonyLib;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Terraheim;
using Terraheim.ArmorEffects;
using TerraheimItems;
using UnityEngine;

namespace TerraheimItems.Patches
{
    [HarmonyPatch]
    class AnimationSpeedPatches
    {
        //public static Dictionary<long, string> lastAnimations = new Dictionary<long, string>();
        public static Dictionary<string, float> baseAnimationSpeeds = new Dictionary<string, float>();

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CharacterAnimEvent), "FixedUpdate")]
        static void CharacterAnimFixedUpdatePrefix(ref Animator ___m_animator, Character ___m_character)
        {
            //Make sure this is being applied to the right things
            if (Player.m_localPlayer == null || !___m_character.IsPlayer() || ___m_character.IsPlayer() && (___m_character as Player).GetPlayerID() != Player.m_localPlayer.GetPlayerID())
                return;
            //Log.LogMessage(1);
            //Make sure there is animation playing
            if (___m_animator?.GetCurrentAnimatorClipInfo(0)?.Any() != true
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip == null
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Idle")
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Jog")
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Run")
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Walk")
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("Standing")
                || ___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("jump"))
                return;

            //Don't change speed if it is already modified
            if (baseAnimationSpeeds.ContainsKey(___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name))
            {
                if (baseAnimationSpeeds[___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name] != ___m_animator.speed)
                {
                    //Log.LogMessage("Mismatch");
                    return;
                }
            }
            //Log.LogMessage(2);
            float statusAttackSpeedBonus = 0f;
            if (___m_character.GetSEMan().HaveStatusEffect("Attack Speed"))
                statusAttackSpeedBonus += (___m_character.GetSEMan().GetStatusEffect("Attack Speed") as SE_AttackSpeed).GetSpeed();

            if (___m_character.GetSEMan().HaveStatusEffect("KenningFX"))
                statusAttackSpeedBonus += ___m_animator.speed * 2;
            if (statusAttackSpeedBonus != 0f)
            {
                ___m_animator.speed = ChangeSpeed(___m_character, ___m_animator, 0, statusAttackSpeedBonus);
                Log.LogInfo($"TH Animation Name {___m_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name}. Speed {___m_animator.speed}");
            }


        }

        public static float ChangeSpeed(Character character, Animator animator, float speed, float speedMod)
        {
            string name = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;

            if (!baseAnimationSpeeds.ContainsKey(name))
                baseAnimationSpeeds.Add(name, animator.speed);

            if (speedMod < 1)
                speedMod += 1;

            if (speed < 1)
                speed += 1;

            return baseAnimationSpeeds[name] * speed * speedMod;
        }
    }
}
