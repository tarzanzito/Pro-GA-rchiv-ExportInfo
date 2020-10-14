using System;
using System.Collections.Generic;
using System.Text;

namespace ProgArchives
{
    public static class AlbumManager
    {
        public static AlbumInfo CreateFromHtmlData(int id, string htmlData)
        {
            if (htmlData.Length == 0)
                return new AlbumInfo(id);

            string strB;
            string strE;
            int posB;
            int posE;
            int len;

            //album
            strB = @"<h1 style=""line-height:1em;"">";
            strE = "</h1>";
            posB = htmlData.IndexOf(strB);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length;

            string album = htmlData.Substring(posB + len, posE - posB - len).Trim().Replace(";", "|");

            //artist
            strB = @"<h2 style=""margin-top:1px;display:inline;""><a href=""artist.asp?id=";
            strE = "</a></h2>";
            posB = htmlData.IndexOf(strB);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length;

            string tmp = htmlData.Substring(posB + len, posE - posB - len);
            tmp = tmp.Replace("\"", "");
            char[] chrsW = { '>' };
            string[] words = tmp.Split(chrsW);

            string artist = words[1].Replace(";", "|");

            //artistId
            int artistId;
            System.Int32.TryParse(words[0], out artistId);

            //coverLink
            strB = @"<img id=""imgCover"" src=""";
            strE = "alt=";
            posB = htmlData.IndexOf(strB);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length;

            string coverLink = htmlData.Substring(posB + len, posE - posB - len).Replace("\"", "").Trim();

            //yearAndType
            //<strong>Studio Album, released in 1972</strong><br><br>
            strB = @"<strong>";
            strE = "</strong>";
            posB = htmlData.IndexOf(strB);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length;

            string yearAndType = htmlData.Substring(posB + len, posE - posB - len).Trim();

            //confirm if year (is  NEW !!!!
            posB = yearAndType.IndexOf("released in");
            if (posB == -1)
            {
                //  posB = posE;
                posB = htmlData.IndexOf(strB, posE);
                posE = htmlData.IndexOf(strE, posB);
                len = strB.Length;
                yearAndType = htmlData.Substring(posB + len, posE - posB - len).Trim();
            }


            //htmlTracks
            strB = @"<strong>Songs / Tracks Listing</strong>";
            strE = "</p>";
            //strE = "<strong>Lyrics</strong>";
            posB = htmlData.IndexOf(strB, posE);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length;
            tmp = htmlData.Substring(posB + len, posE - posB - len);

            string htmlTracks = tmp.Replace("<p style=\"padding-left:5px;\">", "").Replace("\n", "").Replace("\r", "").Replace("</p>", "").Replace(";", "|");

            //htmlMusicians
            string htmlMusicians = "";
            //<strong>Line-up / Musicians</strong>
            strB = @"<strong>Line-up / Musicians</strong>";
            strE = "</p>";
            //strE = "<strong>Releases information</strong>";
            posB = htmlData.IndexOf(strB, posE);
            if (posB != -1)
            {
                posE = htmlData.IndexOf(strE, posB);
                len = strB.Length;
                tmp = htmlData.Substring(posB + len, posE - posB - len);

                htmlMusicians = tmp.Replace("<p style=\"padding-left:5px;\">", "").Replace("\n", "").Replace("\r", "").Replace("</p>", "").Replace(";", "|");
            }

            //year and type
            string type="";
            string year="";
            if (yearAndType != "")
            {
                posB = yearAndType.IndexOf(",", 0);
                type = yearAndType.Substring(0, posB);

                posB = yearAndType.Length - 4;
                year = yearAndType.Substring(posB, 4);
            }
          

            bool downloaded = true;

            return new AlbumInfo(id, album, artistId, artist, coverLink, yearAndType, htmlTracks,
                htmlMusicians, year, type, downloaded);
        }

        public static string GetSqlInsertStatement(AlbumInfo albumInfo)
        {
            string sql = "INSERT INTO Albuns VALUES ("
                        + albumInfo.ID.ToString() + ","
                        + "'" + albumInfo.Album.Replace("'", "''") + "',"
                        + albumInfo.ArtistId.ToString() + ","
                        + "'" + albumInfo.Artist.Replace("'", "''") + "',"
                        + "'" + albumInfo.CoverLink.Replace("'", "''") + "',"
                        + "'" + albumInfo.YearAndType.Replace("'", "''") + "',"
                        + "'" + albumInfo.HtmlTracks.Replace("'", "''") + "',"
                        + "'" + albumInfo.HtmlMusicians.Replace("'", "''") + "',"
                        + "'" + albumInfo.Year + "',"
                        + "'" + albumInfo.Type + "',"
                        + albumInfo.IsValid.ToString() + ","
                        + albumInfo.Downloaded.ToString()
                        + ")";

            return sql;
        }
    }
}
