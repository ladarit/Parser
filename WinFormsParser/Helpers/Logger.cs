using System;
using System.IO;
using System.Text;
using log4net;

namespace GovernmentParse.Helpers
{
    public static class Logger
    {
        private static readonly FileInfo ConfigFile;

        private static readonly object SyncRoot = new Object();

        public static readonly string DonloadedLaws;

        static Logger()
        {
            LogManager.GetRepository().LevelMap.Add(LawsLevelExtensions.SavedLawsLevel);
            var logDirectory = Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory);
            ConfigFile = new FileInfo(Path.GetDirectoryName(logDirectory.FullName) + "\\VRU_log4net.config");
            var savedLawsLogPath = Path.GetDirectoryName(logDirectory.FullName) + "\\DetailedLog\\SavedLawsLog.txt";
            DonloadedLaws = File.Exists(savedLawsLogPath) ? File.ReadAllText(savedLawsLogPath, Encoding.Default) : string.Empty;
        }

        public static ILog GetLogger(Type type)
        {
            lock (SyncRoot)
            {
                if (LogManager.GetCurrentLoggers().Length == 0)
                    log4net.Config.XmlConfigurator.Configure(ConfigFile);
                return LogManager.GetLogger(type);
            }
        }
    }

    public static class LawsLevelExtensions
    {
        public static readonly log4net.Core.Level SavedLawsLevel = new log4net.Core.Level(50000, "SAVEDLAWS");

        public static void SavedLaws(this ILog log, string message)
        {
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, SavedLawsLevel, message, null);
        }

        public static void SavedLawsFormat(this ILog log, string message, params object[] args)
        {
            string formattedMessage = string.Format(message, args);
            log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, SavedLawsLevel, formattedMessage, null);
        }

    }
}
