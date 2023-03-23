
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

            //LAST RUN 2022-06-03
            //LAST RUN 2021-09-20
            //LAST RUN 2020-10-15
            //LAST RUN 2019-06-20
            //LAST RUN 2022-09-08
            //LAST RUN 2023-03-16

            //Find last artist on site
            //http://www.progarchives.com/artist.asp?id=12412
            int toArtistPage = 12412; //while < toArtistPage 
            bool doArtists = false;

            //Find last album on site
            //http://www.progarchives.com/album.asp?id=78320
            int toAlbumPage = 78320; //while < toArtistPage 
            bool doAlbuns = true;

            //Find last country on site
            //http://www.progarchives.com/Bands-country.asp?country=220
            bool doCountries = false;


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
                progAchivesSite.ProcessArtists(toArtistPage, onlyOne: false);
            }

            //Process Albuns
            if (doAlbuns)
            {
                System.Console.WriteLine("Process Albuns starting");
            
                progAchivesSite.ProcessAlbums(25584, onlyOne: true);
                //progAchivesSite.ProcessAlbums(toAlbumPage, onlyOne: false);
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
