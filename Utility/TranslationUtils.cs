using System;
using System.IO;
using System.Text;

namespace Terraheim.Utility
{
    class TranslationUtils
    {
        //private static readonly string m_translationsPath = Path.Combine(Terraheim.ModPath, "Translations");
        private static readonly string m_translationsPath = Terraheim.ModPath;
        private static readonly string m_defaultLanguage = "English";
        private static string m_language = Localization.instance.GetSelectedLanguage();
        private static string m_translationPath;

        public static void LoadTranslations()
        {
            GetTranslationFilePath();
            ReadTranslationFile();
        }

        private static void GetTranslationFilePath()
        {
            var filePath = Path.Combine(m_translationsPath, $"{m_language.ToLowerInvariant()}.lang");
            if (!File.Exists(filePath))
            {
                Log.LogWarning($"No translation file found for {m_language}; defaulting to {m_defaultLanguage}");
                m_language = m_defaultLanguage;
                filePath = Path.Combine(m_translationsPath, $"{m_defaultLanguage.ToLowerInvariant()}.lang");
                if (!File.Exists(filePath))
                {
                    Log.LogError($"No default translation file found for {m_language}");
                    return;
                }
            }
            m_translationPath = filePath;
        }

        private static void ReadTranslationFile()
        {
            if (m_translationPath == null)
            {
                Log.LogError($"No translation found; skipping translation");
                return;
            }

            int iError = 0;
            try
            {
                using (StreamReader sr = new StreamReader(m_translationPath, Encoding.BigEndianUnicode))
                {
                    string line;
                    for (int i = 0; (line = sr.ReadLine()) != null; i++)
                    {
                        string[] keyvalue = line.Split('=');
                        if (keyvalue.Length != 2 && !line.StartsWith("#"))
                        {
                            iError++;
                            Log.LogWarning($"Skipping incorrect translation found at line {i} in {m_language.ToLowerInvariant()}.lang");
                            continue;
                        }
                        if (!line.StartsWith("#"))
                            Localization.instance.AddWord(keyvalue[0], keyvalue[1]);
                    }
                }
            }
            catch (Exception)
            {
                Log.LogError($"Could not read file {m_translationPath}");
            }
            var logMsg = $"Loaded localization for {m_language}";

            if (iError > 0) Log.LogWarning($"{logMsg} with {iError} errors.");
            else Log.LogInfo(logMsg);
        }
    }
}
