
using System;
using ProgArchivesCore.Models;

namespace ProgArchivesCore.ProgGnosisSite
{
    /// <summary>
    /// Retrive information from html proggnosis "Artists" pages
    /// </summary>
    internal class ProgGnosisSiteArtist
    {
        //https://www.proggnosis.com/Artist/14

        private string _htmlData;

        public ArtistInfo GetArtistInfoFromHtmlData(int page, string htmlData, string addedOn)
        {
            if (htmlData.Length == 0)
                return new ArtistInfo(page, addedOn);

            _htmlData = htmlData;

            string artist = GetArtist();
            string country = GetCountry();
            string style = GetStyle();

            bool isInactive = artist == "";

            ArtistInfo artistInfo = new ArtistInfo(page, artist, 0, country, style, isInactive, addedOn);

            return artistInfo;
        }

        private string GetArtist()
        {
            //<h3>Abraxas</h3>
            string beginTag = "<h3>";
            string endTag = "</h3>";
            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string artist = _htmlData.Substring(bPos + len, ePos - bPos - len).Trim().Replace(";", "|");

            return artist;
        }

        private string GetCountry()
        {
            string beginTag = "<span id=\"displayCountry\">";
            string endTag = "</span>";

            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string country = _htmlData.Substring(bPos + len, ePos - bPos - len).Trim();

            return country;
        }

        private string GetStyle()
        {
            string beginTag = "id=\"displayGenre\" name=\"displayGenre\">";
            string endTag = "</div>";

            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string style = _htmlData.Substring(bPos + len, ePos - bPos - len).Trim();


            return style;
        }
    }
}
