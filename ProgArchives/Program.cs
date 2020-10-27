﻿
namespace ProgArchives
{
    /////////////////////////
    // VERSION: 2020-10-14 //
    /////////////////////////

    static class Program
    {
        private static ProgArchives.SiteManager siteManager = null;
        private static ProgArchives.AccessDbManager accessDbManager = null;

        [System.STAThread]
        static int Main(string[] args)
        {
            //LAST RUN 2020-10-15
            //LAST RUN 2019-06-20

            //Find last artist on site
            //http://www.progarchives.com/artist.asp?id=9129
            int toArtistPage = 11510; //11505
            bool doArtists = true;

            //Find last album on site
            //http://www.progarchives.com/album.asp?id=47590
            int toAlbumPage = 70616; //62415;  //70616
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
            string relName = @"\..\..\Resources\" + dbName;
#else
            string relName = @"\" + dbName;
#endif
            string fullNameDB = System.Environment.CurrentDirectory + relName;

            //Open access database
            accessDbManager = new ProgArchives.AccessDbManager(fullNameDB);
            accessDbManager.Open();

            //open site map
            
            if (hasProxy)
                siteManager = new SiteManager(userID, userPassword, userDomain, proxyAddress, proxyPort);
            else
                siteManager = new SiteManager();

            //////////
            //accessDbManager.ReadTable("Artists", ProcessRowArtist);
            //accessDbManager.ReadTable("Albuns", ProcessRowAlbum);
            //return 0;
            ////////

            //Process Artists
            if (doArtists)
            {
                int lastArtistPageInDd = accessDbManager.GetLastPage("Artists", "Artist_ID") + 1;
                ProcessArtists(lastArtistPageInDd, toArtistPage, siteManager, accessDbManager);
            }

            //Process Albuns
            if (doAlbuns)
            {
                int lastAlbumPageInDb = accessDbManager.GetLastPage("Albuns", "Album_ID") + 1;
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

                string sql = ArtistManager.GetSqlInsertStatement(artistInfo);
                accessDbManager.ExecuteNonQuery(sql);

                System.Console.WriteLine("Artist:" + page.ToString().Trim());
            }
        }

        private static void ProcessAlbuns(int fromPage, int toPage, SiteManager siteManager, AccessDbManager accessDbManager)
        {

            for (int page = fromPage; page <= toPage; page++)
            {
                string htmlData = siteManager.GetAllData("http://www.progarchives.com/album.asp?id=" + page.ToString().Trim());

                ProgArchives.AlbumInfo albumInfo = AlbumManager.CreateFromHtmlData(page, htmlData);

                string sql = AlbumManager.GetSqlInsertStatement(albumInfo);
                accessDbManager.ExecuteNonQuery(sql);

                System.Console.WriteLine("Album:" + page.ToString().Trim());
            }
        }

        private static void ProcessCountries(SiteManager siteManager, AccessDbManager accessDbManager)
        {
            int fromPage = 1;
            int toPage = 220;
            int i = 0;
            for (int page = fromPage; page <= toPage; page++)
            {
                string htmlData = siteManager.GetAllData("http://www.progarchives.com/Bands-country.asp?country=" + page.ToString().Trim());

                string countryName = CreateCountryFromHtmlData(page, htmlData);

                System.Console.WriteLine(page.ToString().Trim() + ";" + countryName);
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

///////////////////

        private static void ProcessRowArtist(System.Data.OleDb.OleDbDataReader dataReader)
        {
            string artist = dataReader["Artist"].ToString();
            int artist_id = System.Convert.ToInt32(dataReader["Artist_ID"].ToString());
            bool inactive = System.Convert.ToBoolean(dataReader["Inactive"].ToString());

            if (!inactive)
            {
                if ((artist.IndexOf("?") != -1) || (artist.IndexOf("&#") != -1))
                {
                    string htmlData = siteManager.GetAllData("http://www.progarchives.com/artist.asp?id=" + artist_id.ToString().Trim());
                    ProgArchives.ArtistInfo artistInfo = ArtistManager.CreateFromHtmlData(artist_id, htmlData);
                    if (artistInfo.Artist != artist)
                    {
                        string sql = ArtistManager.GetSqlUpdateStatement(artistInfo);
                        accessDbManager.ExecuteNonQuery(sql);
                    }
                }
            }
        }

        private static void ProcessRowAlbum(System.Data.OleDb.OleDbDataReader dataReader)
        {
            string artist = dataReader["Artist"].ToString();
            string album = dataReader["Album"].ToString();
            int album_id = System.Convert.ToInt32(dataReader["Album_ID"].ToString());
            bool inactive = System.Convert.ToBoolean(dataReader["Inactive"].ToString());

            if (!inactive)
            {
                if ((artist.IndexOf("?") != -1) || (artist.IndexOf("&#") != -1) ||
                    (album.IndexOf("?") != -1) || (album.IndexOf("&#") != -1))
                {
                    string htmlData = siteManager.GetAllData("http://www.progarchives.com/album.asp?id=" + album_id.ToString().Trim());
                    ProgArchives.AlbumInfo albumInfo = AlbumManager.CreateFromHtmlData(album_id, htmlData);
                    if ((albumInfo.Artist != artist) || (albumInfo.Album != album))
                    {
                        string sql = AlbumManager.GetSqlUpdateStatement(albumInfo);
                        accessDbManager.ExecuteNonQuery(sql);
                    }

                }
            }
        }

    }
}
