
using Candal.Core;
using System.IO;
using System;
using System.Configuration;


namespace Candal
{
    internal static class Program2
    {
        public static int Main(string[] args)
        {
            //FillDetectedHoles()

            //before in  ACCESS DB i run the function  "VerificaBuracos" and save de debug in file at "\Resources\Holes.txt"

            try
            {
                string currentDirectory = Environment.CurrentDirectory;
#if DEBUG
                currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
#endif
                string fullNameFile = System.IO.Path.Join(currentDirectory, @"\Resources\Holes.txt");

                StreamReader file = new StreamReader(fullNameFile);

                IDataBaseManager dataBaseManager = DatabaseLink();
                SiteManager siteManager = SiteManagerLink();
                ProgAchivesSiteManager progAchivesSite = new ProgAchivesSiteManager(siteManager, dataBaseManager);

                int counterLines = 0;
                int counterPages = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    counterLines++;

                    if (line.Trim() == "")
                        continue;

                    int pageIni = GetPageFromLine(line);
                    int quantity = GetQuantityFromLine(line);

                    Console.WriteLine(line);
                    int page;
                    for (int i = 0; i < quantity; i++)
                    {
                        page = pageIni + i;
                        counterPages++;
                        try
                        {
                            //progAchivesSite.ProcessArtists(page, true);
                            progAchivesSite.ProcessAlbums(page, true);
                        }
                        catch ( Exception ex)
                        {
                            System.Console.WriteLine("ERROR:");
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine(ex.Source);
                            System.Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

                file.Close();
                dataBaseManager.Close();
                dataBaseManager = null;
                siteManager = null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("ERROR:");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.Source);
                System.Console.WriteLine(ex.StackTrace);
            }

            return 0;
        }

        private static SiteManager SiteManagerLink()
        {
            SiteManager siteManager = null;

            string temp = ConfigurationManager.AppSettings["ProxyNeeded"];
            bool hasProxy = System.Convert.ToBoolean(temp);

            if (hasProxy)
            {
                string userID = ConfigurationManager.AppSettings["ProxyUserId"];
                string userPassword = ConfigurationManager.AppSettings["ProxyUserPassword"];
                string userDomain = ConfigurationManager.AppSettings["ProxyDomain"];
                string address = ConfigurationManager.AppSettings["ProxyAddress"];
                temp = ConfigurationManager.AppSettings["ProxyPort"];
                int port = System.Convert.ToInt32(temp); ;

                siteManager = new SiteManager(userID, userPassword, userDomain, address, port);
            }
            else
                siteManager = new SiteManager();

            return siteManager;
        }

        private static IDataBaseManager DatabaseLink()
        {
            string dbEngine = ConfigurationManager.AppSettings["DataBaseEngine"].ToUpper().Trim();

            switch (dbEngine)
            {
                case "MSACCESS":
                    return DatabaseMsAccess();
                case "SQLITE":
                    return DatabaseSqlite();
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

            //            System.Console.WriteLine($"DataBase Path:{fullNameDB}");

            //            //Open access database
            //            IDataBaseManager dataBaseManager = new DataBaseAccessManager(fullNameDB);
            //            //IDataBaseManager dataBaseManager = new DataBaseSqliteManager(fullNameDB);

            //return dataBaseManager;
        }

        private static IDataBaseManager DatabaseMsAccess()
        {
            string dataBaseLocation = ConfigurationManager.AppSettings["DataBaseLocation"];

            string currentDirectory = Environment.CurrentDirectory;

#if DEBUG
            currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
#endif
            string fullNameDB = System.IO.Path.Join(currentDirectory, dataBaseLocation);

            System.Console.WriteLine($"DataBase Path:{fullNameDB}");

            //Open access database
            IDataBaseManager dataBaseManager = new DataBaseAccessManager(fullNameDB);

            return dataBaseManager;
        }

        private static IDataBaseManager DatabaseSqlite()
        {
            string dataBaseLocation = ConfigurationManager.AppSettings["DataBaseLocation"];

            string currentDirectory = Environment.CurrentDirectory;

#if DEBUG
            currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
#endif
            string fullNameDB = System.IO.Path.Join(currentDirectory, dataBaseLocation);

            System.Console.WriteLine($"DataBase Path:{fullNameDB}");

            //Open access database
            IDataBaseManager dataBaseManager = new DataBaseSqliteManager(fullNameDB);

            return dataBaseManager;
        }


 

        private static int GetPageFromLine(string line)
        {
            int pos1 = line.IndexOf(":");
            int pos2 = line.IndexOf("-");
            string pageS = line.Substring(pos1 + 1, pos2 - pos1 - 1).Trim();

            return System.Convert.ToInt32(pageS);
        }

        private static int GetQuantityFromLine(string line)
        {
            int pos = line.LastIndexOf(":");
            string qtS = line.Substring(pos + 1, line.Length - pos - 1).Trim();

            return System.Convert.ToInt32(qtS);
        }
    }
}
