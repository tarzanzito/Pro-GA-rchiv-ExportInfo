//using System;
//using System.Collections.Generic;
//using System.Runtime.Remoting.Metadata.W3cXsd2001;
//using System.Text;

//namespace Candal
//{
//    public class DataBaseAlbum
//    {
 

//        public string GetInsertSqlStatement(AlbumInfo albumInfo)
//        {
//            string sql = "INSERT INTO Albuns VALUES ("
//                        + albumInfo.ID.ToString() + ","
//                        + "'" + albumInfo.Album.Replace("'", "''") + "',"
//                        + albumInfo.ArtistId.ToString() + ","
//                        + "'" + albumInfo.Artist.Replace("'", "''") + "',"
//                        + "'" + albumInfo.CoverLink.Replace("'", "''") + "',"
//                        + "'" + albumInfo.YearAndType.Replace("'", "''") + "',"
//                        + "'" + albumInfo.HtmlTracks.Replace("'", "''") + "',"
//                        + "'" + albumInfo.HtmlMusicians.Replace("'", "''") + "',"
//                        + "'" + albumInfo.Year + "',"
//                        + "'" + albumInfo.Type + "',"
//                        + albumInfo.IsInactive.ToString()
//                        + ")";

//            return sql;
//        }

//        public string GetUpdateSqlStatement(AlbumInfo albumInfo)
//        {
//            string sql = "UPDATE albuns SET "
//            + "Album = '" + albumInfo.Album.Replace("'", "''") + "', "
//            + "Artist_ID = " + albumInfo.ArtistId.ToString() + ", "
//            + "Artist = '" + albumInfo.Artist.Replace("'", "''") + "', "
//            + "Cover = '" + albumInfo.CoverLink.Replace("'", "''") + "', "
//            + "YearAndType = '" + albumInfo.YearAndType.Replace("'", "''") + "', "
//            + "Tracks = '" + albumInfo.HtmlTracks.Replace("'", "''") + "', "
//            + "Musicians = '" + albumInfo.HtmlMusicians.Replace("'", "''") + "', "
//            + "YearN = '" + albumInfo.Year + "', "
//            + "Type = '" + albumInfo.Type + "', "
//            + "Inactive = " + albumInfo.IsInactive.ToString() + " "
//            + "WHERE Album_ID = " + albumInfo.ID.ToString();
//            return sql;
//        }
//        public string GetDeleteSqlStatement(AlbumInfo albumInfo)
//        {
//            string sql = "DELETE Albuns WHERE ID = " + albumInfo.ID.ToString();

//            return sql;
//        }

//        public string GetDeleteAllSqlStatement()
//        {
//            string sql = "DELETE Albuns";

//            return sql;
//        }

//        //public static string GetAlbum(string htmlData)
//        //{
//        //    string beginTag = @"<h1 style=""line-height:1em;"">";
//        //    string endTag = "</h1>";
//        //    int bPos = htmlData.IndexOf(beginTag);
//        //    int ePos = htmlData.IndexOf(endTag, bPos);
//        //    int len = beginTag.Length;

//        //    string album = htmlData.Substring(bPos + len, ePos - bPos - len).Trim().Replace(";", "|");

//        //    return album;
//        //}

//        //public static string[] GetArtistAndArtistId(string htmlData)
//        //{
//        //    string beginTag = @"<h2 style=""margin-top:1px;display:inline;""><a href=""artist.asp?id=";
//        //    string endTag = "</a></h2>";
//        //    int bPos = htmlData.IndexOf(beginTag);
//        //    int ePos = htmlData.IndexOf(endTag, bPos);
//        //    int len = beginTag.Length;

//        //    string tmp = htmlData.Substring(bPos + len, ePos - bPos - len);
//        //    tmp = tmp.Replace("\"", "");
//        //    char[] chrsW = { '>' };
//        //    string[] words = tmp.Split(chrsW);

//        //    int artistId;
//        //    System.Int32.TryParse(words[0], out artistId);
//        //    words[0] = artistId.ToString();
//        //    words[1] = words[1].Replace(";", "|");

//        //    return words;
//        //}

//        //public static string GetCoverLink(string htmlData)
//        //{
//        //    string beginTag = @"<img id=""imgCover"" src=""";
//        //    string endTag = "alt=";
//        //    int bPos = htmlData.IndexOf(beginTag);
//        //    int ePos = htmlData.IndexOf(endTag, bPos);
//        //    int len = beginTag.Length;

//        //    string coverLink = htmlData.Substring(bPos + len, ePos - bPos - len).Replace("\"", "").Trim();

//        //    return coverLink;
//        //}


//        //public static string GetYearAndType(string htmlData)
//        //{
//        //    string yearAndType = "";
//        //    string beginTag = "<strong>";
//        //    string endTag = "</strong>";
//        //    int len = beginTag.Length;

//        //    int bPos = 0;
//        //    int ePos;
//        //    int count = 0;
//        //    int rPos = 0;
//        //    do
//        //    {
//        //        count++;
//        //        bPos++;

//        //        bPos = htmlData.IndexOf(beginTag, bPos);
//        //        ePos = htmlData.IndexOf(endTag, bPos);
//        //        if (bPos > 0)
//        //            yearAndType = htmlData.Substring(bPos + len, ePos - bPos - len).Trim();
//        //        else
//        //            yearAndType = "";

//        //        //confirm if year and type
//        //        rPos = yearAndType.IndexOf("released in");
//        //    }
//        //    while ((rPos == -1) && (count < 5));

//        //    return yearAndType;
//        //}

//        //public static string GetHhtmlTracks(string htmlData)
//        //{
//        //    string beginTag = @"<strong>Songs / Tracks Listing</strong>";
//        //    string endTag = "</p>";

//        //    int bPos = htmlData.IndexOf(beginTag);
//        //    int ePos = htmlData.IndexOf(endTag, bPos);
//        //    int len = beginTag.Length;

//        //    string tmp = htmlData.Substring(bPos + len, ePos - bPos - len);

//        //    string htmlTracks = tmp.Replace("<p style=\"padding-left:5px;\">", "")
//        //        .Replace("\n", "")
//        //        .Replace("\r", "")
//        //        .Replace("</p>", "")
//        //        .Replace(";", "|");

//        //    return htmlTracks;
//        //}

//        //public static string GetHtmlMusicians(string htmlData)
//        //{
//        //    string htmlMusicians = "";
//        //    string beginTag = "<strong>Line-up / Musicians</strong>";
//        //    string endTag = "</p>";

//        //    int bPos = htmlData.IndexOf(beginTag);

//        //    if (bPos != -1)
//        //    {
//        //        int ePos = htmlData.IndexOf(endTag, bPos);
//        //        int len = beginTag.Length;
//        //        string tmp = htmlData.Substring(bPos + len, ePos - bPos - len);

//        //        htmlMusicians = tmp.Replace("<p style=\"padding-left:5px;\">", "")
//        //            .Replace("\n", "")
//        //            .Replace("\r", "")
//        //            .Replace("</p>", "")
//        //            .Replace(";", "|");
//        //    }

//        //    return htmlMusicians;
//        //}

//        //public static string GetYear(string data)
//        //{
//        //    string year = "";
//        //    if (data != "")
//        //    {
//        //        int bPos = data.Length - 4;
//        //        year = data.Substring(bPos, 4);
//        //    }

//        //    return year;
//        //}

//        //public static string GetType(string data)
//        //{
//        //    string type = "";
//        //    if (data != "")
//        //    {
//        //        int bPos = data.IndexOf(",");
//        //        if (bPos != -1)
//        //            type = data.Substring(0, bPos);
//        //    }

//        //    return type;
//        //}
//    }
//}
