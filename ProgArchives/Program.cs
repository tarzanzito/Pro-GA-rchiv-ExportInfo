
namespace ProgArchives
{
    /////////////////////////
    // VERSION: 2020-10-14 //
    /////////////////////////

    static class Program
    {
        [System.STAThread]
        static int Main(string[] args)
        {
            //LAST RUN 2019-06-20

            //Find last artist on site
            //http://www.progarchives.com/artist.asp?id=9129
            int toArtistPage = 10918;  //11505
            bool doArtists = false;

            //Find last album on site
            //http://www.progarchives.com/album.asp?id=47590
            int toAlbumPage = 62415;  //70616
            bool doAlbuns = false;

            ////

            bool hasProxy = false;
            string userID = "user";
            string userPassword = "pass";
            string userDomain = "CENTRAL";
            string proxyAddress = "proxz.tottb.gt.corp:8080";
            int proxyPort = 8080;
            string dbName = "ProgArchives2010.mdb";

#if DEBUG
            string relName = @"\..\..\..\" + dbName;
#else
            string relName = @"\" + dbName;
#endif
            string fullNameDB = System.Environment.CurrentDirectory + relName;

            //Open access database
            AccessDbManager accessDbManager = new ProgArchives.AccessDbManager(fullNameDB);
            accessDbManager.Open();

            //open site map
            ProgArchives.SiteManager siteManager = null;
            if (hasProxy)
                siteManager = new SiteManager(userID, userPassword, userDomain, proxyAddress, proxyPort);
            else
                siteManager = new SiteManager();

            //Process Artists
            if (doArtists)
            {
                int lastArtistPageInDd = accessDbManager.GetLastArtistPage() + 1;
                ProcessArtists(lastArtistPageInDd, toArtistPage, siteManager, accessDbManager);
            }

            //Process Albuns
            if (doAlbuns)
            {
                int lastAlbumPageInDb = accessDbManager.GetLastAlbumPage() + 1;
                ProcessAlbuns(lastAlbumPageInDb, toAlbumPage, siteManager, accessDbManager);
            }

            accessDbManager.Close();

            return 0;
        }

        private static void ProcessArtists(int fromPage, int toPage, SiteManager siteManager, AccessDbManager accessDbManager)
        {
            for (int page = fromPage; page <= toPage; page++)
            {
                string htmlData = siteManager.GetAllData("http://www.progarchives.com/artist.asp?id=" + page.ToString().Trim());

                    ProgArchives.ArtistInfo artistInfo = ArtistManager.CreateFromHtmlData(page, htmlData);

                    accessDbManager.InsertArtist(artistInfo);

                    System.Console.WriteLine("Artist:" + page.ToString().Trim());
            }
        }

        private static void ProcessAlbuns(int fromPage, int toPage, SiteManager siteManager, AccessDbManager accessDbManager)
        {

            for (int page = fromPage; page <= toPage; page++)
            {
                string htmlData = siteManager.GetAllData("http://www.progarchives.com/album.asp?id=" + page.ToString().Trim());

                ProgArchives.AlbumInfo albumInfo = AlbumManager.CreateFromHtmlData(page, htmlData);

                accessDbManager.InsertAlbum(albumInfo);

                System.Console.WriteLine("Album:" + page.ToString().Trim());
            }
        }

        private static void ProcessCountries( SiteManager siteManager, AccessDbManager accessDbManager)
        {
            int fromPage = 1;
            int toPage = 220;
            int i = 0;
            for (int page = fromPage; page <= toPage; page++)
            {
                string htmlData = siteManager.GetAllData("http://www.progarchives.com/Bands-country.asp?country=" + page.ToString().Trim());

                 string countryName = CreateCountryFromHtmlData(page, htmlData);

                System.Console.WriteLine(page.ToString().Trim()  +";" + countryName);
                System.Diagnostics.Debug.Print(page.ToString().Trim() + ";" + countryName);
                i = 1;
            }
        }

        public static string CreateCountryFromHtmlData(int page, string htmlData)
        {
            if (htmlData.Length == 0)
                return "NOT FOUND";

            string strB = "<title>";
            string strE = "</title>";

            int posB = htmlData.IndexOf(strB);
            int posE = htmlData.IndexOf(strE, posB);
            int len = strB.Length; 

            string title = htmlData.Substring(posB + len, posE - posB - len).Trim();
            string countryName = title.Replace("Progressive Rock & Related Bands/artists from", "").Trim();

            return countryName;
        }
    }
}
