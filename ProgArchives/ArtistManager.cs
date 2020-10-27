
namespace ProgArchives
{
    public static class ArtistManager
    {
        public static ProgArchives.ArtistInfo CreateFromHtmlData(int page, string htmlData)
        {
            if (htmlData.Length == 0)
                return new ProgArchives.ArtistInfo(page);

            string artist = GetArtist(htmlData);
            string[] words = GetCountryAndStyle(htmlData);

            string style = words[0];
            string country = words[1];

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

        public static string GetSqlUpdateStatement(ProgArchives.ArtistInfo artistInfo)
        {
            string sql = "UPDATE Artists SET "
                        + "Artist = '" + artistInfo.Artist.Replace("'", "''") + "', "
                        + "Country = '" + artistInfo.Country.Replace("'", "''") + "', "
                        + "Style = '" + artistInfo.Style.Replace("'", "''") + "', "
                        + "Inactive = " + artistInfo.IsInactive.ToString() + " "
                        + "WHERE Artist_ID = " + artistInfo.ID.ToString().Trim();

            return sql;
        }

        private static string GetArtist(string htmlData)
        {
            //get Artist
            string beginTag = "<h1>";
            string endTag = "</h1>";
            int bPos = htmlData.IndexOf(beginTag);
            int ePos = htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string artist = htmlData.Substring(bPos + len, ePos - bPos - len).Trim().Replace(";", "|");

            return artist;
        }

        private static string[] GetCountryAndStyle(string htmlData)
        {
            string beginTag = @"<h2 style=""margin:1px 0px;padding:0;color:#777;font-weight:normal;"">";
            string endTag = "</h2>";

            int bPos = htmlData.IndexOf(beginTag);
            int ePos = htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string tmp = htmlData.Substring(bPos + len, ePos - bPos - len);
            tmp = tmp.Replace("&bull;", "~");

            char[] chrsW = new char[1] { '~' };
            string[] words = tmp.Split(chrsW);

            words[0] = words[0].Trim().Replace(";", "|");
            words[1] = words[1].Trim().Replace(";", "|");

            return words;
        }
    }
}
