
using System;
using ProgArchivesCore.Config;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.Statics;

namespace Candal
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            System.Console.WriteLine("Program starting.");

            ConfigurationFields configurationFields = CommonUtils.LoadConfiguration();

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
                IDataBaseManager dataBaseManager = CommonUtils.DatabaseLink(configurationFields);

                //open site manager
                SiteManager siteManager = CommonUtils.SiteManagerLink(configurationFields);


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

    }
}
