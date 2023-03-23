
namespace Candal.Core
{
    public class ProgAchivesSiteManager
    {
        private SiteManager _siteManager = null;
        private IDataBaseManager _dataBaseManager = null;

        public ProgAchivesSiteManager(SiteManager siteManager, IDataBaseManager dataBaseManager)
        {
            _siteManager = siteManager;
            _dataBaseManager = dataBaseManager;

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

                ArtistInfo artistInfo = progAchivesSiteArtist.GetArtistInfoFromHtmlData(page, allHtmlData);

                _dataBaseManager.InsertArtist(artistInfo);

                System.Console.WriteLine($"Artist:{page}");
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
                string htmlData = _siteManager.GetAllHtmlData(uri);

                AlbumInfo albumInfo = progAchivesSiteAlbum.GetAlbumInfoFromHtmlData(page, htmlData);

                _dataBaseManager.InsertAlbum(albumInfo);

                System.Console.WriteLine($"Album:{page}");
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
                
                System.Console.WriteLine($"Country:{page}");
            }
        }
    }
}
