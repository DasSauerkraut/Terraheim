using System;
using System.IO;
using System.Text;

namespace Terraheim.Utility;

internal class TranslationUtils
{
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
		string text = Path.Combine(m_translationsPath, m_language.ToLowerInvariant() + ".lang");
		if (!File.Exists(text))
		{
			Log.LogWarning("No translation file found for " + m_language + "; defaulting to " + m_defaultLanguage);
			m_language = m_defaultLanguage;
			text = Path.Combine(m_translationsPath, m_defaultLanguage.ToLowerInvariant() + ".lang");
			if (!File.Exists(text))
			{
				Log.LogError("No default translation file found for " + m_language);
				return;
			}
		}
		m_translationPath = text;
	}

	private static void ReadTranslationFile()
	{
		if (m_translationPath == null)
		{
			Log.LogError("No translation found; skipping translation");
			return;
		}
		int num = 0;
		try
		{
			using StreamReader streamReader = new StreamReader(m_translationPath, Encoding.BigEndianUnicode);
			int num2 = 0;
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				string[] array = text.Split('=');
				if (array.Length != 2 && !text.StartsWith("#"))
				{
					num++;
					Log.LogWarning($"Skipping incorrect translation found at line {num2} in {m_language.ToLowerInvariant()}.lang");
				}
				else if (!text.StartsWith("#"))
				{
					Localization.instance.AddWord(array[0], array[1]);
				}
				num2++;
			}
		}
		catch (Exception)
		{
			Log.LogError("Could not read file " + m_translationPath);
		}
		string text2 = "Loaded localization for " + m_language;
		if (num > 0)
		{
			Log.LogWarning($"{text2} with {num} errors.");
		}
		else
		{
			Log.LogInfo(text2);
		}
	}
}
