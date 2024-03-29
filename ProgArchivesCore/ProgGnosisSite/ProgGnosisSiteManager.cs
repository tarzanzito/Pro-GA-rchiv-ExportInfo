﻿
using System;
using System.Security.Policy;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.Models;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.SiteManagers;
using Serilog;
using static ProgArchivesCore.Delegates;

namespace ProgArchivesCore.ProgGnosisSite
{
    /// <summary>
    /// Manage pages from progarchives site and save info in an DB
    /// 1- gel all html
    /// 2- process info in html
    /// 3- save info in db
    /// </summary>
    public class ProgGnosisSiteManager
    {
        private SiteManager _siteManager = null;
        private IDataBaseManager _dataBaseManager = null;
        private string _dateNow = null;

        public ProgGnosisSiteManager(SiteManager siteManager, IDataBaseManager dataBaseManager)
        {
            _siteManager = siteManager;
            _dataBaseManager = dataBaseManager;

            _dateNow = DateTime.Now.ToString("yyyy/MM/dd");

            //Open databases
            _dataBaseManager.Open();
        }

        public void ProcessArtists(int toPage, bool onlyOne)
        {
            int lastTreatedPageId;
            //if (onlyOne)
            lastTreatedPageId = toPage;
            //else
            //{
            //    lastTreatedPageId = _dataBaseManager.GetMaxArtist();
            //    lastTreatedPageId++;
            //}

            ProgGnosisSiteArtist progGnosisSiteArtist = new ProgGnosisSiteArtist();

            for (int page = lastTreatedPageId; page <= toPage; page++)
            {
                string uri = $"https://www.proggnosis.com/Artist/{page}";

                string allHtmlData = _siteManager.GetAllHtmlData(uri);


                ArtistInfo artistInfo = progGnosisSiteArtist.GetArtistInfoFromHtmlData(page, allHtmlData, _dateNow);

                ////CountryInfo countryInfo  = _dataBaseManager.SelectCountryByName(artistInfo.Country);
                //artistInfo.SetCountryId(countryInfo.ID);

                //_dataBaseManager.InsertArtist(artistInfo);

                Log.Information($"'ProgGnosisSiteManager.ProcessArtists' - Artist:{page}");

                //if (EventCountryInfo != null)
                //    EventCountryInfo(countryInfo, uri);
            }
        }


        public void ProcessAlbums(int toPage, bool onlyOne)
        {
            int lastTreatedPageId;
            //if (onlyOne)
            lastTreatedPageId = toPage;
            //else
            //{
            //    lastTreatedPageId = _dataBaseManager.GetMaxAlbum();
            //    lastTreatedPageId++;
            //}

            ProgGnosisSiteAlbum progGnosisSiteAlbum = new ProgGnosisSiteAlbum();

            for (int page = lastTreatedPageId; page <= toPage; page++)
            {
                string uri = $"https://www.proggnosis.com/Release/{page}";
                string htmlData = _siteManager.GetAllHtmlData(uri);

                AlbumInfo albumInfo = progGnosisSiteAlbum.GetAlbumInfoFromHtmlData(page, htmlData, _dateNow);

                // _dataBaseManager.InsertAlbum(albumInfo);

                Log.Information($"'ProgGnosisSiteManager.ProcessAlbums' - Album:{page}");
                //if (EventCountryInfo != null)
                //    EventCountryInfo(countryInfo, uri);
            }
        }

        public void ProcessCountries(bool firstDeleteAll)
        {
            int fromPage = 1;
            int toPage = 220;

            if (firstDeleteAll)
                _dataBaseManager.DeleteAllCountries();

            ProgAchivesSiteCountry progAchivesSiteCountry = new ProgAchivesSiteCountry();

            for (int page = fromPage; page <= toPage; page++)
            {
                string htmlData = _siteManager.GetAllHtmlData($"http://www.progarchives.com/Bands-country.asp?country={page}");

                CountryInfo countryInfo = progAchivesSiteCountry.GetCountryNameFromHtmlData(page, htmlData);

                _dataBaseManager.InsertCountry(countryInfo);

                Log.Information($"'ProgGnosisSiteManager.ProcessCountries' - Country:{page}");
                //if (EventCountryInfo != null)
                //    EventCountryInfo(countryInfo, uri);
            }
        }
    }
}
