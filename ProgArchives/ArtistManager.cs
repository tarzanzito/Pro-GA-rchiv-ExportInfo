
namespace ProgArchives
{
    public static class ArtistManager
    {
        public static ProgArchives.ArtistInfo CreateFromHtmlData(int page, string htmlData)
        {
            if (htmlData.Length == 0)
                return new ProgArchives.ArtistInfo(page);

            string strB;
            string strE;
            int posB;
            int posE;
            int len;

            //get Artist
            strB = "<h1>";
            strE = "</h1>";
            posB = htmlData.IndexOf(strB);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length; // HtmlData.Length;

            string artist = htmlData.Substring(posB + len, posE - posB - len).Trim().Replace(";", "|");

            //get Style, Country
            strB = @"<h2 style=""margin:1px 0px;padding:0;color:#777;font-weight:normal;"">";
            strE = "</h2>";
            posB = htmlData.IndexOf(strB);
            posE = htmlData.IndexOf(strE, posB);
            len = strB.Length; //HtmlData.Length;

            string tmp = htmlData.Substring(posB + len, posE - posB - len);
            tmp = tmp.Replace("&bull;", "~");

            char[] chrsW = new char[1] { '~' };
            string[] words = tmp.Split(chrsW);

            string style = words[0].Trim().Replace(";", "|");
            string country = words[1].Trim().Replace(";", "|");

            bool isValid = (artist != "" && country != "");

            return new ProgArchives.ArtistInfo(page, artist, country, style);
        }

        public static string GetSqlInsertStatement(ProgArchives.ArtistInfo artistInfo)
        {
            string sql = "INSERT INTO Artists VALUES ("
                        + artistInfo.ID.ToString() + ","
                        + "'" + artistInfo.Artist.Replace("'","''") + "',"
                        + "'" + artistInfo.Country.Replace("'", "''") + "',"
                        + "'" + artistInfo.Style.Replace("'", "''") + "',"
                        + artistInfo.IsInactive.ToString()
                        + ")";

            return sql;
        }
    }
}
