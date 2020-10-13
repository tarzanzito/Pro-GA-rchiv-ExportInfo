
namespace ProgArchives
{
    static class Program
    {
        [System.STAThread]
        static int Main(string[] args)
        {
            /////////////////////////
            // VERSION: 2019-06-20 //
            /////////////////////////

            //LAST RUN 2019-06-20

            bool hasProxy = false;
            string userID = "user";
            string userPassword = "pass";
            string userDomain = "CENTRAL";
            string proxyAddress = "proxz.tottb.gt.corp:8080";
            int proxyPort = 8080;
            //string relName = @"\..\..\..\..\ProgArchives2010.mdb";
            string relName = @"\..\..\..\ProgArchives2010.mdb";

            string path = System.Windows.Forms.Application.ExecutablePath;

            //Find last artist on site
            //http://www.progarchives.com/artist.asp?id=9129
            int lastArtistPage = 10915;  //11505
            bool doArtists = false;

            //Find last album on site
            //http://www.progarchives.com/album.asp?id=47590
            int lastAlbumPage = 62415;  //70616
            bool doAlbuns = true;

            string fullNameDB = System.Environment.CurrentDirectory + relName;

            //Open access database
            ProgArchives.AccessManager accessManager = new ProgArchives.AccessManager(fullNameDB);
            accessManager.Open();

            int artistPageDB = accessManager.GetLastArtistPage() + 1;
            int albumPageDB = accessManager.GetLastAlbumPage() + 1;

            //open site map
            ProgArchives.SiteManager site = null;
            if (hasProxy)
                site = new SiteManager(userID, userPassword, userDomain, proxyAddress, proxyPort);
            else
                site = new SiteManager();

            //Process Artists
            if (doArtists)
            {
                for (int page = artistPageDB; page <= lastArtistPage; page++)
                {
                    string htmlData = site.GetAllData("http://www.progarchives.com/artist.asp?id=" + page.ToString().Trim());

                    //if (htmlData != "")
                    //{
                        ProgArchives.ArtistInfo artistInfo = new ArtistInfo(page);

                        if (htmlData != "")
                        {
                            artistInfo.FindAllFields(htmlData);
                        }

                        accessManager.InsertArtist(artistInfo);
                        System.Console.WriteLine("Artist:" + page.ToString().Trim());
                    //}
                }
            }


            //Process Albuns
            if (doAlbuns)
            {
                for (int page = albumPageDB; page <= lastAlbumPage; page++)
                {
                    string htmlData = site.GetAllData("http://www.progarchives.com/album.asp?id=" + page.ToString().Trim());

                    //if (htmlData != "")
                    //{
                        ProgArchives.AlbumInfo albumInfo = new AlbumInfo(page);

                        if (htmlData != "")
                        {
                            albumInfo.FindAllFields(htmlData);
                        }

                        accessManager.InsertAlbum(albumInfo);
                        System.Console.WriteLine("Album:" + page.ToString().Trim());
                    //}
                }
            }

            accessManager.Close();

            return 0;
        }
    }
}
