using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SouthApps.Framework.LoggerService
{
    public static class LoggerService
    {
        private static FileStream FileStream;
        private static StreamWriter TraceListener;
        private static LogLevel CurrentLogLevel;
        private static bool Initialized;

        public static void Initialize(string logPath, string logLevel)
        {
            if (!LoggerService.Initialized)
            {
                LoggerService.FileStream = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                LoggerService.TraceListener = new StreamWriter(LoggerService.FileStream);
                TraceListener.AutoFlush = true;
                LogLevel level = (LogLevel)Enum.Parse(typeof(LogLevel), logLevel, true);
                LoggerService.CurrentLogLevel = level;

                LoggerService.Initialized = true;
            }
        }

        public static void Log(LogLevel logLevel, string message, params object[] logParameters)
        {
            Log(logLevel, message, null, logParameters);
        }

        public static void Log(LogLevel logLevel, string message, Exception ex, params object[] logParameters)
        {
            if (LoggerService.LevelEnabled(logLevel))
            {
                string header = DateTime.Now.ToString() + ": " + logLevel.ToString() + " - ";
                TraceListener.WriteLine(header + FormatLogParameters(message, logParameters) + (ex != null ? "Exception: " + ex.Message + ex.StackTrace : ""));
            }
        }

        private static string FormatLogParameters(string message, params object[] logParameters)
        {
            return string.Format(message, logParameters);
        }

        private static bool LevelEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Verbose:
                    return LoggerService.CurrentLogLevel == LogLevel.Verbose;
                case LogLevel.Debug:
                    return LoggerService.CurrentLogLevel == LogLevel.Verbose || LoggerService.CurrentLogLevel == LogLevel.Debug;
                case LogLevel.Warning:
                    return LoggerService.CurrentLogLevel == LogLevel.Verbose || LoggerService.CurrentLogLevel == LogLevel.Debug || LoggerService.CurrentLogLevel == LogLevel.Warning;
                case LogLevel.Error:
                    return true;
                default:
                    return true;
            }
        }
    }
}
