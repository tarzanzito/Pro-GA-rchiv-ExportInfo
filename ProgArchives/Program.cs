
using Candal.Core;
using System.IO;
using System;

namespace Candal
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            System.Console.WriteLine("Program starting");

            bool hasProxy = false;

            //LAST RUN 2022-09-08
            //LAST RUN 2023-03-16
            //LAST RUN 2023-03-27

            //Find last artist on site
            //http://www.progarchives.com/artist.asp?id=12434
            int toArtistPage = 12434; //while < toArtistPage 
            bool doArtists = false;

            //Find last album on site
            //http://www.progarchives.com/album.asp?id=78430
            int toAlbumPage = 78430; //while < toArtistPage 
            bool doAlbuns = true;

            //Find last country on site
            //http://www.progarchives.com/Bands-country.asp?country=220
            bool doCountries = false;

            bool processOnlyOne = false;

            try
            {
                //Open access database
                IDataBaseManager dataBaseManager = DatabaseLink();

                //open site manager
                SiteManager siteManager = SiteManagerLink(hasProxy);


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

        private static SiteManager SiteManagerLink(bool hasProxy)
        {
            SiteManager siteManager = null;

            if (hasProxy)
            {
                string userID = "user";
                string userPassword = "pass";
                string userDomain = "CENTRAL";
                string proxyAddress = "proxz.tottb.gt.corp:8080";
                int proxyPort = 8080;
                siteManager = new SiteManager(userID, userPassword, userDomain, proxyAddress, proxyPort);
            }
            else
                siteManager = new SiteManager();

            return siteManager;
        }

        private static IDataBaseManager DatabaseLink()
        {
            string dbName = "ProgArchives2010.mdb";
            string currentDirectory = Environment.CurrentDirectory;
            string relName = @"\";

#if DEBUG
            currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
             relName = @"\Resources\";
#endif
            string fullNameDB = currentDirectory + relName + dbName;

            System.Console.WriteLine($"DataBase Path:{fullNameDB}");

            //Open access database
            IDataBaseManager dataBaseManager = new DataBaseAccessManager(fullNameDB);
            //IDataBaseManager dataBaseManager = new DataBaseSqliteManager(fullNameDB);

            return dataBaseManager;
        }
    }
}
