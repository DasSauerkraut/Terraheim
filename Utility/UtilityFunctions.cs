using System.IO;
using Newtonsoft.Json.Linq;

using BepInEx;


namespace Terraheim.Utility
{
    class UtilityFunctions
    {
        public static JObject GetJsonFromFile(string filename)
        {
            var filePath = Path.Combine(Paths.PluginPath, "DasSauerkraut-Terraheim", filename);
            //Log.LogWarning(filePath);
            string rawText = File.ReadAllText(filePath);
            //Log.LogWarning(rawText);
            return JObject.Parse(rawText);
        }
    }
}
