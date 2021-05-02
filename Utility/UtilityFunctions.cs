using System.IO;
using Newtonsoft.Json.Linq;

using BepInEx;


namespace Terraheim.Utility
{
    class UtilityFunctions
    {
        public static JObject GetJsonFromFile(string filename)
        {
            var filePath = Path.Combine(Terraheim.ModPath, filename);
            //Log.LogWarning(filePath);
            string rawText = File.ReadAllText(filePath);
            //Log.LogWarning(rawText);
            return JObject.Parse(rawText);
        }

        public static bool HasTooltipEffect(SEMan seman)
        {
            foreach( var effect in seman.GetStatusEffects())
            {
                switch (effect.m_name)
                {
                    case "One Hand Damage Bonus":
                        return true;
                    case "Block Power Bonus":
                        return true;
                    case "Dagger/Spear Damage Bonus":
                        return true;
                    case "Melee Damage Bonus":
                        return true;
                    case "Ranged Damage Bonus":
                        return true;
                    case "Silver Damage Bonus":
                        return true;
                    case "Spirit Damage Bonus":
                        return true;
                    case "Two Handed Damage Bonus":
                        return true;
                    case "Backstab Bonus":
                        return true;
                    case "Throwing Damage Bonus":
                        return true;
                    default:
                        break;
                }
            }
            return false;
        }
        
        public static bool CheckBarbarian()
        {
            if (!File.Exists(Terraheim.ModPath + "/../barbarianArmor.dll"))
            {
                Log.LogWarning("Barbarian armor not found!");
                return false;
            }
            Log.LogInfo("Barbarian Armor Found!");
            return true;
        }
    }
}
