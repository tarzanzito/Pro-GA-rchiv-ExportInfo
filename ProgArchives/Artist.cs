
namespace ProgArchives
{
    public class ArtistInfo
    {
        private int _id;
        private string _artist;
        private string _country;
        private string _style;
        private bool _isValid;

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public string Artist
        {
            get
            {
                return _artist;
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
        }


        public ArtistInfo(int ID)
        {
            _id = ID;
            _artist = "";
            _country = "";
            _style = "";
            _isValid = true;
        }

        public void FindAllFields(string HtmlData)
        {
            _artist = "";
            _country = "";
            _style = "";

            string strB;
            string strE;
            int posB;
            int posE;
            int len;

            //get Artist
            strB = "<h1>";
            strE = "</h1>";
            posB = HtmlData.IndexOf(strB);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length; // HtmlData.Length;

            _artist = HtmlData.Substring(posB + len, posE - posB - len).Trim().Replace(";", "|");

            //get Style, Country
            strB = @"<h2 style=""margin:1px 0px;padding:0;color:#777;font-weight:normal;"">";
            strE = "</h2>";
            posB = HtmlData.IndexOf(strB);
            posE = HtmlData.IndexOf(strE, posB);
            len = strB.Length; //HtmlData.Length;

            string tmp = HtmlData.Substring(posB + len, posE - posB - len);
            tmp = tmp.Replace("&bull;", "~");

            char[] chrsW = new char[1] { '~' };
            string[] words = tmp.Split(chrsW);

            _style = words[0].Trim().Replace(";", "|");
            _country = words[1].Trim().Replace(";", "|");

            _isValid = false;
        }

        public string GetInsertStatement()
        {
            string sql = "INSERT INTO Artists VALUES ("
                        + _id.ToString() + ","
                        + "'" + _artist.Replace("'","''") + "',"
                        + "'" + _country.Replace("'", "''") + "',"
                        + "'" + _style.Replace("'", "''") + "',"
                        + _isValid.ToString()
                        + ")";

            return sql;
        }
    }
}
