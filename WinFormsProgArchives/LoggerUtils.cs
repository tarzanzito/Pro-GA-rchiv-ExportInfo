using System;
using System.IO;

using Serilog;

namespace WinFormsProgArchives
{
    internal class LoggerUtils
    {
        public static void Start()
        {
            //string frameWorkVersion = AppContext.TargetFrameworkName;
            //string appFolder = AppContext.BaseDirectory;

            string appName = AppDomain.CurrentDomain.FriendlyName;
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            //string appFolder = Directory.GetCurrentDirectory();

            string logFileFullName = Path.Combine(appFolder, appName);

            Log.Logger = new LoggerConfiguration()
               .WriteTo.File(logFileFullName, rollingInterval: RollingInterval.Minute)
               .WriteTo.Console()
               .CreateLogger();

            Log.Information("Logger Started...");
            Log.Information($"LogFile:{logFileFullName}");
        }
    }
}
