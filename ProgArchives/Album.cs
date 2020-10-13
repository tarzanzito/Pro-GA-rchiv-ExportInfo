using System;
using System.Collections.Generic;
using System.Text;

namespace ProgArchives
{
    public class AlbumInfo
    {
        private int _id;
        private string _album;
        private int _artistId;
        private string _artist;
        private string _coverLink;
        private string _yearAndType;
        private string _htmlTracks;
        private string _htmlMusicians;
        private string _year;
        private string _type;
        private bool _isValid;
        private bool _downloaded;

        public AlbumInfo(int ID)
        {
            _id = ID;
            _album = "";
            _artistId = 0;
            _artist = "";
            _coverLink = "";
            _yearAndType = "";
            _htmlTracks = "";
            _htmlMusicians = "";
            _year = "";
            _type = "";
            _isValid = false;
            _downloaded = false;
        }

        public void FindAllFields(string HtmlData)
        {
            string album = "";
            int artistId = 0;
            string artist = "";
            string coverLink = "";
            string yearAndType = "";
            string htmlTracks = "";
            string htmlMusicians = "";
            string type = "";
            string year = "";

            string strB;
            string strE;
            int posB;
            int posE;
            int len;

            //album
            strB = @"<h1 style=""line-height:1em;"">";
            strE = "</h1>";
            posB = HtmlData.IndexOf(strB);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length;

            album = HtmlData.Substring(posB + len, posE - posB - len).Trim().Replace(";", "|");

            //artist
            strB = @"<h2 style=""margin-top:1px;display:inline;""><a href=""artist.asp?id=";
            strE = "</a></h2>";
            posB = HtmlData.IndexOf(strB);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length;

            string tmp = HtmlData.Substring(posB + len, posE - posB - len);
            tmp = tmp.Replace("\"", "");
            char[] chrsW = { '>' };
            string[] words = tmp.Split(chrsW);

            System.Int32.TryParse(words[0], out artistId);

            artist = words[1].Replace(";", "|");

            //coverLink
            strB = @"<img id=""imgCover"" src=""";
            strE = "alt=";
            posB = HtmlData.IndexOf(strB);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length;

            coverLink = HtmlData.Substring(posB + len, posE - posB - len).Replace("\"", "").Trim();

            //yearAndType
            //<strong>Studio Album, released in 1972</strong><br><br>
            strB = @"<strong>";
            strE = "</strong>";
            posB = HtmlData.IndexOf(strB);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length;

            yearAndType = HtmlData.Substring(posB + len, posE - posB - len).Trim();

            //confirm if year (is  NEW !!!!
            posB = yearAndType.IndexOf("released in");
            if (posB == -1)
            {
              //  posB = posE;
                posB = HtmlData.IndexOf(strB, posE);
                posE = HtmlData.IndexOf(strE, posB);
                len = strB.Length;
                yearAndType = HtmlData.Substring(posB + len, posE - posB - len).Trim();
            }


            //htmlTracks
            strB = @"<strong>Songs / Tracks Listing</strong>";
            strE = "</p>";
            //strE = "<strong>Lyrics</strong>";
            posB = HtmlData.IndexOf(strB, posE);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length;
            tmp = HtmlData.Substring(posB + len, posE - posB - len);

            htmlTracks = tmp.Replace("<p style=\"padding-left:5px;\">", "").Replace("\n", "").Replace("\r", "").Replace("</p>", "").Replace(";", "|");

            //htmlMusicians
            //<strong>Line-up / Musicians</strong>
            strB = @"<strong>Line-up / Musicians</strong>";
            strE = "</p>";
            //strE = "<strong>Releases information</strong>";
            posB = HtmlData.IndexOf(strB, posE);
            if (posB != -1)
            {
                posE = HtmlData.IndexOf(strE, posB);
                len = strB.Length;
                tmp = HtmlData.Substring(posB + len, posE - posB - len);

                htmlMusicians = tmp.Replace("<p style=\"padding-left:5px;\">", "").Replace("\n", "").Replace("\r", "").Replace("</p>", "").Replace(";", "|");
            }
            else
                htmlMusicians = "";

            //year and type
            if (yearAndType != "")
            {
                posB = yearAndType.IndexOf(",", 0);
                type = yearAndType.Substring(0, posB);

                posB = yearAndType.Length - 4;
                year = yearAndType.Substring(posB, 4);
            }

            ////////////

            _album = album;
            _artistId = artistId;
            _artist = artist;
            _coverLink = coverLink;
            _yearAndType = yearAndType;
            _htmlTracks = htmlTracks;
            _htmlMusicians = htmlMusicians;
            _year = year;
            _type = type;

            _isValid = false;
        }

        public string GetInsertStatement()
        {
            string sql = "INSERT INTO Albuns VALUES ("
                        + _id.ToString() + ","
                        + "'" + _album.Replace("'", "''") + "',"
                        + _artistId.ToString() + ","
                        + "'" + _artist.Replace("'", "''") + "',"
                        + "'" + _coverLink.Replace("'", "''") + "',"
                        + "'" + _yearAndType.Replace("'", "''") + "',"
                        + "'" + _htmlTracks.Replace("'", "''") + "',"
                        + "'" + _htmlMusicians.Replace("'", "''") + "',"
                        + "'" + _year + "',"
                        + "'" + _type + "',"
                        + _isValid.ToString() + ","
                        + _downloaded.ToString() 
                        + ")";

            return sql;
        }
    }
}


//    ProgArchives.ArtistInfo artistInfo = new ProgArchives.ArtistInfo(Page);

//    try
//    {
//        byte[] arr = _webClient.DownloadData("http://www.progarchives.com/artist.asp?id=" + Page.ToString().Trim());

//        string respHTML;

//        //System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding(); 
//        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
//        respHTML = enc.GetString(arr);

//        ProcessArtist(respHTML, artistInfo);
//    }
//    catch (System.Exception ex)
//    {
//        string aaa = ex.Message;
//    }

//    return artistInfo;
//}


//public ProgArchives.AlbumInfo GetAlbumInfo(int Page)
//{
//   ProgArchives.AlbumInfo albumInfo = new ProgArchives.AlbumInfo(Page);

//    try
//    {
//        string site = "http://www.progarchives.com/album.asp?id=" + Page.ToString().Trim();
//        byte[] arr = _webClient.DownloadData(site);

//        string respHTML;

//        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
//        respHTML = enc.GetString(arr);

//        ProcessAlbum(respHTML, albumInfo);
//    }
//    catch (System.Exception ex)
//    {
//        string aaa = ex.Message;
//    }

//    return albumInfo;
//}


//private void ProcessAlbum(string respHTML, ProgArchives.AlbumInfo AlbumInfo)
//{
//    string str = respHTML; // this.webBrowser1.Document.Body.InnerHtml; 

//    string album = "";
//    int artistId = 0;
//    string artist = "";
//    string coverLink = "";
//    string yearAndType = "";
//    string htmlTracks = "";
//    string htmlMusicians = "";
//    string type = "";
//    string year = "";

//    try
//    {
//        string strB = @"<h1 style=""line-height:1em;"">";
//        string strE = "</h1>";
//        int posB = str.IndexOf(strB);
//        int posE = str.IndexOf(strE, posB);
//        int len = strB.Length;

//        album = str.Substring(posB + len, posE - posB - len).Trim().Replace(";", "|");

//        /////////

//        strB = @"<h2 style=""margin-top:1px;display:inline;""><a href=""artist.asp?id=";
//        strE = "</a></h2>";
//        posB = str.IndexOf(strB);
//        posE = str.IndexOf(strE, posB);
//        len = strB.Length;

//        string tmp = str.Substring(posB + len, posE - posB - len);
//        tmp = tmp.Replace("\"", "");
//        char[] chrsW = { '>' };
//        string[] words = tmp.Split(chrsW);

//        System.Int32.TryParse(words[0], out artistId);

//        artist = words[1].Replace(";", "|");

//        //////////

//        strB = @"<img id=""imgCover"" src=""";
//        strE = "alt=";
//        posB = str.IndexOf(strB);
//        posE = str.IndexOf(strE, posB);
//        len = strB.Length;

//        coverLink = str.Substring(posB + len, posE - posB - len).Replace("\"", "").Trim();

//        //////////

//        //<strong>Studio Album, released in 1972</strong><br><br>
//        strB = @"<strong>";
//        strE = "</strong>";
//        posB = str.IndexOf(strB);
//        posE = str.IndexOf(strE, posB);
//        len = strB.Length;

//        yearAndType = str.Substring(posB + len, posE - posB - len).Trim();

//        /////////////

//        strB = @"<strong>Songs / Tracks Listing</strong>";
//        strE = "</p>";
//        //strE = "<strong>Lyrics</strong>";
//        posB = str.IndexOf(strB, posE);
//        posE = str.IndexOf(strE, posB);
//        len = strB.Length;
//        tmp = str.Substring(posB + len, posE - posB - len);

//        htmlTracks = tmp.Replace("<p style=\"padding-left:5px;\">", "").Replace("\n", "").Replace("\r", "").Replace("</p>", "").Replace(";", "|");

//        /////////
//        //<strong>Line-up / Musicians</strong>
//        strB = @"<strong>Line-up / Musicians</strong>";
//        strE = "</p>";
//        //strE = "<strong>Releases information</strong>";
//        posB = str.IndexOf(strB, posE);
//        posE = str.IndexOf(strE, posB);
//        len = strB.Length;
//        tmp = str.Substring(posB + len, posE - posB - len);

//        htmlMusicians = tmp.Replace("<p style=\"padding-left:5px;\">", "").Replace("\n", "").Replace("\r", "").Replace("</p>", "").Replace(";", "|");

//        /////////////

//        if (yearAndType != "")
//        {
//            posB = yearAndType.IndexOf(",", 0);
//            type = yearAndType.Substring(0, posB);

//            posB = yearAndType.Length - 4;
//            year = yearAndType.Substring(posB, 4);
//        }

//        ////////////

//        AlbumInfo.Album = album;
//        AlbumInfo.ArtistId = artistId;
//        AlbumInfo.Artist = artist;
//        AlbumInfo.CoverLink = coverLink;
//        AlbumInfo.YearAndType = yearAndType;
//        AlbumInfo.htmlTracks = htmlTracks;
//        AlbumInfo.htmlMusicians = htmlMusicians;
//        AlbumInfo.Year = year;
//        AlbumInfo.Type = type;

//        AlbumInfo.IsValid = false;
//    }
//    catch (System.Exception ex)
//    {
//        string err = ex.Message;
//    }

//    //System.IO.File.AppendAllText("d:\\albuns.txt", page.ToString().Trim() + ";" + ALBUM + ";" + ID + ";" + ARTIST + ";" + COVER + ";" + YEAR + ";" + TRACKS + ";" + MUSICIANS + "\n");
//}
