using Jotunn;

namespace Terraheim;

internal static class Log
{
	internal static void LogDebug(object data)
	{
		Logger.LogDebug(data);
	}

	internal static void LogError(object data)
	{
		Logger.LogError(data);
	}

	internal static void LogFatal(object data)
	{
		Logger.LogFatal(data);
	}

	internal static void LogInfo(object data)
	{
		Logger.LogInfo(data);
	}

	internal static void LogMessage(object data)
	{
		Logger.LogMessage(data);
	}

	internal static void LogWarning(object data)
	{
		Logger.LogWarning(data);
	}
}
