
using System;
using ProgArchivesCore.Config;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.Models;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.Statics;
using Serilog;
using WinFormsProgArchives;

namespace Candal
{
    internal static class Program
    {
        public static int Main(string[] args)
        {
            LoggerUtils.Start();

            Log.Information("Program starting.");

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
                    Log.Information("Process Artists starting");
                    progAchivesSite.EventArtistInfo += FireEventArtistInfo;
                    progAchivesSite.ProcessArtists(toArtistPage, processOnlyOne);
                }

                //Process Albuns
                if (doAlbuns)
                {
                    Log.Information("Process Albuns starting");
                    progAchivesSite.EventAlbumInfo += FireEventAlbumInfo;
                    progAchivesSite.ProcessAlbums(toAlbumPage, processOnlyOne);
                }

                //Process Countries
                if (doCountries)
                {
                    Log.Information("Process Countries starting");
                    progAchivesSite.EventCountryInfo += FireEventCountryInfo;
                    progAchivesSite.ProcessCountries(firstDeleteAll: true);
                }

                dataBaseManager.Close();
                dataBaseManager = null;
                siteManager = null;
            }
            catch (Exception ex)
            {
                
                Log.Error("ERROR:");
                Log.Error(ex.Message);
                Log.Error(ex.Source);
                Log.Error(ex.StackTrace);
                return 1;
            }

            Log.Information("Program finished");

            return 0;
        }

        #region events

        public static void FireEventCountryInfo(CountryInfo countryInfo, string uri)
        {
            Log.Information($"'MusicCollectionMsDos.FireEventCountryInfo' | URI={uri} | CountryInfo={countryInfo}");
        }

        public static void FireEventArtistInfo(ArtistInfo artistInfo, string uri)
        {
            Log.Information($"'MusicCollectionMsDos.FireEventArtistInfo' | URI={uri} | ArtistInfo={artistInfo}");
        }

        public static void FireEventAlbumInfo(AlbumInfo albumInfo, string uri)
        {
            Log.Information($"'MusicCollectionMsDos.FireEventAlbumInfo' | URI={uri} | AlbumInfo={albumInfo}");
        }

        #endregion
    }

}

