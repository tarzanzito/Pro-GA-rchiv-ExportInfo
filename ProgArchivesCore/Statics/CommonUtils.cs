using System.IO;
using System;
using System.Configuration;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.Config;
using Serilog;

namespace ProgArchivesCore.Statics
{
    public class CommonUtils
    {
        private static string GetCurrentDirectory()
        {
            string currentDirectory = Environment.CurrentDirectory;
#if DEBUG
            currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
#endif
            return currentDirectory;
        }

        public static ConfigurationFields LoadConfiguration()
        {
            string proxyNeeded = ConfigurationManager.AppSettings["ProxyNeeded"];
            bool hasProxy = System.Convert.ToBoolean(proxyNeeded);

            string proxyAddress = "";
                   
            string proxyDomain = "";
            string proxyUserId = "";
            string proxyUserPassword = "";
            int proxyPort = 0;

            if (hasProxy)
            {
                proxyUserId = ConfigurationManager.AppSettings["ProxyUserId"];
                proxyUserPassword = ConfigurationManager.AppSettings["ProxyUserPassword"];
                proxyDomain = ConfigurationManager.AppSettings["ProxyDomain"];
                proxyAddress = ConfigurationManager.AppSettings["ProxyAddress"];
                string temp = ConfigurationManager.AppSettings["ProxyPort"];
                proxyPort = System.Convert.ToInt32(temp);
            }

            string dataBaseEngine = ConfigurationManager.AppSettings["DataBaseEngine"].ToUpper().Trim();
            string dataBaseLocation = ConfigurationManager.AppSettings["DataBaseLocation"];

            string currentDir = GetCurrentDirectory();

            ConfigurationFields conf = new ConfigurationFields(proxyNeeded, proxyAddress, proxyPort, proxyDomain,
                proxyUserId,  proxyUserPassword,  dataBaseEngine,  dataBaseLocation, currentDir);

            return conf;
        }

        public static SiteManager SiteManagerLink(ConfigurationFields configurationFields)
        {
            return new SiteManager(configurationFields);
        }

        public static IDataBaseManager DatabaseLink(ConfigurationFields configurationFields)
        {
            switch (configurationFields.DataBaseEngine)
            {
                case "MSACCESS":
                    return DatabaseMsAccess(configurationFields);
                case "SQLITE":
                    return DatabaseSqlite(configurationFields);
                case "MYSQL":
                    throw new Exception("AppSettings:[DataBaseEngine] not valid."); 
                default:
                    throw new Exception("AppSettings:[DataBaseEngine] not valid. (Options: 'MsAccess', 'Sqlite', 'MySql')");

            }

            //            string dataBaseUser = ConfigurationManager.AppSettings["DataBaseUser"];
            //            string dataBasePassword = ConfigurationManager.AppSettings["DataBasePassword"];


            //            //string dbName = "ProgArchives2010.mdb";
            //            string currentDirectory = Environment.CurrentDirectory;
            //            //string relName = @"\";

            //#if DEBUG
            //            currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            //            //relName = @"\Resources\";
            //#endif
            //            string fullNameDB = System.IO.Path.Combine(currentDirectory, dataBaseLocation);
            //            //string fullNameDB = currentDirectory + dataBaseLocation;

            //         Log.Information($"DataBase Path:{fullNameDB}");

            //            //Open access database
            //            IDataBaseManager dataBaseManager = new DataBaseAccessManager(fullNameDB);
            //            //IDataBaseManager dataBaseManager = new DataBaseSqliteManager(fullNameDB);

            //return dataBaseManager;
        }

        public static IDataBaseManager DatabaseMsAccess(ConfigurationFields configurationFields)
        {
            string currentDirectory = GetCurrentDirectory();

            string fullNameDB = System.IO.Path.Join(currentDirectory, configurationFields.DataBaseLocation);

            Log.Information($"'CommonUtils.DatabaseMsAccess' - DataBase Path:{fullNameDB}");

            //Open access database
            IDataBaseManager dataBaseManager = new DataBaseAccessManager(fullNameDB);

            return dataBaseManager;
        }

        public static IDataBaseManager DatabaseSqlite(ConfigurationFields configurationFields)
        {
            string currentDirectory = GetCurrentDirectory();

            string fullNameDB = System.IO.Path.Join(currentDirectory, configurationFields.DataBaseLocation);

            Log.Information($"'CommonUtils.DatabaseSqlite' - DataBase Path:{fullNameDB}");

            //Open access database
            IDataBaseManager dataBaseManager = new DataBaseSqliteManager(fullNameDB);

            return dataBaseManager;
        }

    }
}

