using ProgArchivesCore.Models;

namespace ProgArchivesCore.ProgGnosisSite
{
    /// <summary>
    /// Retrive information from html progarchives "Albums" pages
    /// </summary>
    internal class ProgAchivesSiteAlbum
    {
        private string _htmlData;

        public AlbumInfo GetAlbumInfoFromHtmlData(int page, string htmlData, string addedOn)
        {
            if (htmlData.Length == 0)
                return new AlbumInfo(page, addedOn);

            _htmlData = htmlData;

            string album = GetAlbum();
            string[] words = GetArtistAndArtistId();
            int artistId = int.Parse(words[0]);
            string artist = words[1];
            string coverLink = GetCoverLink();
            string yearAndType = GetYearAndType();
            string htmlTracks = GetHhtmlTracks();
            string htmlMusicians = GetHtmlMusicians();

            string year = GetYear(yearAndType);
            string type = GetType(yearAndType);

            bool isInactive = album == "";

            AlbumInfo albumInfo = new AlbumInfo(page, album, artistId, artist, coverLink, yearAndType, htmlTracks,
               htmlMusicians, year, type, isInactive, addedOn);

            return albumInfo;
        }

        private string GetAlbum()
        {
            string beginTag = @"<h1 style=""line-height:1em;"">";
            string endTag = "</h1>";
            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string album = _htmlData.Substring(bPos + len, ePos - bPos - len).Trim().Replace(";", "|");

            return album;
        }

        private string[] GetArtistAndArtistId()
        {
            string beginTag = @"<h2 style=""margin-top:1px;display:inline;""><a href=""artist.asp?id=";
            string endTag = "</a></h2>";
            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string tmp = _htmlData.Substring(bPos + len, ePos - bPos - len);
            tmp = tmp.Replace("\"", "");
            char[] chrsW = { '>' };
            string[] words = tmp.Split(chrsW);

            int artistId;
            _ = int.TryParse(words[0], out artistId);
            words[0] = artistId.ToString();
            words[1] = words[1].Replace(";", "|");

            return words;
        }

        private string GetCoverLink()
        {
            string beginTag = @"<img id=""imgCover"" src=""";
            string endTag = "alt=";
            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string coverLink = _htmlData.Substring(bPos + len, ePos - bPos - len).Replace("\"", "").Trim();

            return coverLink;
        }

        private string GetYearAndType()
        {
            string yearAndType = "";
            string beginTag = "<strong>";
            string endTag = "</strong>";
            int len = beginTag.Length;

            int bPos = 0;
            int ePos;
            int count = 0;
            int rPos = 0;
            do
            {
                count++;
                bPos++;

                bPos = _htmlData.IndexOf(beginTag, bPos);
                ePos = _htmlData.IndexOf(endTag, bPos);
                if (bPos > 0)
                    yearAndType = _htmlData.Substring(bPos + len, ePos - bPos - len).Trim();
                else
                    yearAndType = "";

                //confirm if year and type
                rPos = yearAndType.IndexOf("released in");
            }
            while (rPos == -1 && count < 5);

            return yearAndType;
        }

        private string GetHhtmlTracks()
        {
            string beginTag = @"<strong>Songs / Tracks Listing</strong>";
            string endTag = "</p>";

            int bPos = _htmlData.IndexOf(beginTag);
            int ePos = _htmlData.IndexOf(endTag, bPos);
            int len = beginTag.Length;

            string tmp = _htmlData.Substring(bPos + len, ePos - bPos - len);

            string htmlTracks = tmp.Replace("<p style=\"padding-left:5px;\">", "")
                .Replace("\n", "")
                .Replace("\r", "")
                .Replace("</p>", "")
                .Replace(";", "|");

            return htmlTracks;
        }

        private string GetHtmlMusicians()
        {
            string htmlMusicians = "";
            string beginTag = "<strong>Line-up / Musicians</strong>";
            string endTag = "</p>";

            int bPos = _htmlData.IndexOf(beginTag);

            if (bPos != -1)
            {
                int ePos = _htmlData.IndexOf(endTag, bPos);
                int len = beginTag.Length;
                string tmp = _htmlData.Substring(bPos + len, ePos - bPos - len);

                htmlMusicians = tmp.Replace("<p style=\"padding-left:5px;\">", "")
                    .Replace("\n", "")
                    .Replace("\r", "")
                    .Replace("</p>", "")
                    .Replace(";", "|");
            }

            return htmlMusicians;
        }

        private string GetYear(string data)
        {
            string year = "";
            if (data != "")
            {
                int bPos = data.Length - 4;
                year = data.Substring(bPos, 4);
            }

            return year;
        }

        private string GetType(string data)
        {
            string type = "";
            if (data != "")
            {
                int bPos = data.IndexOf(",");
                if (bPos != -1)
                    type = data.Substring(0, bPos);
            }

            return type;
        }
    }
}
