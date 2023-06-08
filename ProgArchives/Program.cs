
using Candal.Core;
using System.IO;
using System;
using System.Configuration;

namespace Candal
{
    internal static class Program
    {
        public static int Main2(string[] args)
        {
            System.Console.WriteLine("Program starting.");

            //LAST RUN 2022-09-08
            //LAST RUN 2023-03-16
            //LAST RUN 2023-03-27
            //LAST RUN 2023-06-08

            //Find last artist on site
            //http://www.progarchives.com/artist.asp?id=12434
            int toArtistPage = 12489; //while < toArtistPage 
            bool doArtists = false;

            //Find last album on site
            //http://www.progarchives.com/album.asp?id=78430
            int toAlbumPage = 78615; //while < toArtistPage 
            bool doAlbuns = true;

            //Find last country on site
            //http://www.progarchives.com/Bands-country.asp?country=220
            bool doCountries = false;

            bool processOnlyOne = false;

            //processOnlyOne = true;
            //toAlbumPage = 60596; 

            try
            {
                //Open access database
                IDataBaseManager dataBaseManager = DatabaseLink();

                //open site manager
                SiteManager siteManager = SiteManagerLink();


                //ProgGnosisSiteManager progGnosisSiteManager = new ProgGnosisSiteManager(siteManager, dataBaseManager);
                //progGnosisSiteManager.ProcessArtists(13, false);
                //progGnosisSiteManager.ProcessAlbums(19, false);


                //Process ProgAchivesSite
                ProgAchivesSiteManager progAchivesSite = new ProgAchivesSiteManager(siteManager, dataBaseManager);

                //Process Artists
                if (doArtists)
                {
                    System.Console.WriteLine("Process Artists starting");
                    progAchivesSite.ProcessArtists(toArtistPage, processOnlyOne);
                }

                //Process Albuns
                if (doAlbuns)
                {
                    System.Console.WriteLine("Process Albuns starting");

                    progAchivesSite.ProcessAlbums(toAlbumPage, processOnlyOne);
                }

                //Process Countries
                if (doCountries)
                {
                    System.Console.WriteLine("Process Countries starting");
                    progAchivesSite.ProcessCountries(firstDeleteAll: true);
                }

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
                return 1;
            }

            System.Console.WriteLine("Program finished");

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


        private  static void Buracos()
        {
            //Corri dentro do ACCESS a funcao xpto
            //gravei num ficheiro txt

            try
            {
                string currentDirectory = Environment.CurrentDirectory;
#if DEBUG
                currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
#endif
                string fullNameFile = System.IO.Path.Join(currentDirectory, @"\Resources\Buracos.txt");

                StreamReader file = new StreamReader(fullNameFile);

                IDataBaseManager dataBaseManager = DatabaseLink();
                SiteManager siteManager = SiteManagerLink();
                ProgAchivesSiteManager progAchivesSite = new ProgAchivesSiteManager(siteManager, dataBaseManager);

                int counter = 0;
                string ln;
                while ((ln = file.ReadLine()) != null)
                {
                    if (ln.Trim() == "")
                        continue;

                    int pos1 = ln.IndexOf(":");
                    int pos2 = ln.IndexOf("-");
                    string pageS = ln.Substring(pos1 + 1, pos2- pos1 - 1).Trim();

                    pos1 = ln.LastIndexOf(":");
                    string qtS = ln.Substring(pos1 + 1, ln.Length - pos1 - 1).Trim();

                    int pageIni = System.Convert.ToInt32(pageS);
                    int qt = System.Convert.ToInt32(qtS);

                    Console.WriteLine(ln);
                    int page; 
                    for (int i = 0; i < qt; i++)
                    {
                        page = pageIni + i;
                        counter++;
                        //progAchivesSite.ProcessArtists(page, true);
                        progAchivesSite.ProcessAlbums(page, true);
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

        }
    }
}
