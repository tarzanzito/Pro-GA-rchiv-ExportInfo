
using System;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.Models;
using ProgArchivesCore.SiteManagers;
using Serilog;

namespace ProgArchivesCore.ProgArchivesSite
{
    /// <summary>
    /// Manage pages from progarchives site and save info in an DB
    /// 1- gel all html
    /// 2- process info in html
    /// 3- save info in db
    /// </summary>
    public class ProgAchivesSiteManager
    {
        public event Delegates.EventCountryInfo EventCountryInfo;
        public event Delegates.EventArtistInfo EventArtistInfo;
        public event Delegates.EventAlbumInfo EventAlbumInfo;


        private SiteManager _siteManager = null;
        private IDataBaseManager _dataBaseManager = null;
        private string _dateNow = null;

        public ProgAchivesSiteManager(SiteManager siteManager, IDataBaseManager dataBaseManager)
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
            if (onlyOne)
                lastTreatedPageId = toPage;
            else
            {
                lastTreatedPageId = _dataBaseManager.GetMaxArtist();
                lastTreatedPageId++;
            }

            ProgAchivesSiteArtist progAchivesSiteArtist = new ProgAchivesSiteArtist();

            for (int page = lastTreatedPageId; page <= toPage; page++)
            {
                string uri = $"http://www.progarchives.com/artist.asp?id={page}";

                string allHtmlData = _siteManager.GetAllHtmlData(uri);

                if (allHtmlData == null)
                    throw new Exception($"{uri} return null");

                ArtistInfo artistInfo = progAchivesSiteArtist.GetArtistInfoFromHtmlData(page, allHtmlData, _dateNow);

                CountryInfo countryInfo = _dataBaseManager.SelectCountryByName(artistInfo.Country);
                artistInfo.SetCountryId(countryInfo.ID);

                if (_dataBaseManager.ExistsArtist(artistInfo))
                    _dataBaseManager.UpdateArtist(artistInfo);
                else
                    _dataBaseManager.InsertArtist(artistInfo);

                Log.Information($"'ProgArchivesCore.ProgArchivesSite.ProcessArtists' - Artist:{page}");

                if (EventArtistInfo != null)
                    EventArtistInfo(artistInfo, uri);
            }
        }

        public void ProcessAlbums(int toPage, bool onlyOne)
        {
            int lastTreatedPageId;
            if (onlyOne)
                lastTreatedPageId = toPage;
            else
            {
                lastTreatedPageId = _dataBaseManager.GetMaxAlbum();
                lastTreatedPageId++;
            }

            ProgAchivesSiteAlbum progAchivesSiteAlbum = new ProgAchivesSiteAlbum();

            for (int page = lastTreatedPageId; page <= toPage; page++)
            {
                string uri = $"http://www.progarchives.com/album.asp?id={page}";
                string allHtmlData = _siteManager.GetAllHtmlData(uri);

                if (allHtmlData == null)
                    throw new Exception($"{uri} return null");

                AlbumInfo albumInfo = progAchivesSiteAlbum.GetAlbumInfoFromHtmlData(page, allHtmlData, _dateNow);


                if (_dataBaseManager.ExistsAlbum(albumInfo))
                    _dataBaseManager.UpdateAlbum(albumInfo);
                else
                    _dataBaseManager.InsertAlbum(albumInfo);

                Log.Information($"'ProgArchivesCore.ProgArchivesSite.ProcessAlbums' - Album:{page}");

                if (EventAlbumInfo != null)
                    EventAlbumInfo(albumInfo, uri);
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
                string uri = $"http://www.progarchives.com/Bands-country.asp?country={page}";
                string htmlData = _siteManager.GetAllHtmlData(uri);

                CountryInfo countryInfo = progAchivesSiteCountry.GetCountryNameFromHtmlData(page, htmlData);

                if (_dataBaseManager.ExistsCountry(countryInfo))
                    _dataBaseManager.UpdateCountry(countryInfo);
                else
                    _dataBaseManager.InsertCountry(countryInfo);

                Log.Information($"'ProgArchivesCore.ProgArchivesSite.ProcessCountries' - Country:{page}");

                if (EventCountryInfo != null)
                    EventCountryInfo(countryInfo, uri);

            }
        }
    }
}
