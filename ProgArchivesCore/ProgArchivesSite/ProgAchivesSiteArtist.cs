using ProgArchivesCore.Models;

namespace ProgArchivesCore.ProgArchivesSite
{
    /// <summary>
    /// Retrive information from html progarchives "Artists" pages
    /// </summary>
    internal class ProgAchivesSiteArtist
    {
        private string _htmlData;

        public ArtistInfo GetArtistInfoFromHtmlData(int page, string htmlData, string addedOn)
        {
            if (htmlData.Length == 0)
                return new ArtistInfo(page, addedOn);

            _htmlData = htmlData;

            string artist = GetArtist();
            string[] words = GetCountryAndStyle();

            string style = words[0];
            string country = words[1];

            bool isInactive = artist == "";

            ArtistInfo artistInfo = new ArtistInfo(page, artist, 0, country, style, isInactive, addedOn);

            return artistInfo;
        }

        private string GetArtist()
        {
            string beginTag = "<h1>";
            string endTag = "</h1>";
            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string artist = _htmlData.Substring(bPos + len, ePos - bPos - len).Trim().Replace(";", "|");

            return artist;
        }

        private string[] GetCountryAndStyle()
        {
            string beginTag = @"<h2 style=""margin:1px 0px;padding:0;color:#777;font-weight:normal;"">";
            string endTag = "</h2>";

            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string tmp = _htmlData.Substring(bPos + len, ePos - bPos - len);
            tmp = tmp.Replace("&bull;", "~");

            char[] chrsW = new char[1] { '~' };
            string[] words = tmp.Split(chrsW);

            words[0] = words[0].Trim().Replace(";", "|");
            words[1] = words[1].Trim().Replace(";", "|");

            return words;
        }
    }
}
