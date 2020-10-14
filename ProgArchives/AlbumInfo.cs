using System;
using System.Collections.Generic;
using System.Text;

namespace ProgArchives
{
    public class AlbumInfo
    {
        public int ID { get; private set; }
        public string Album { get; private set; }
        public int ArtistId { get; private set; }
        public string Artist { get; private set; }
        public string CoverLink { get; private set; }
        public string YearAndType { get; private set; }
        public string HtmlTracks { get; private set; }
        public string HtmlMusicians { get; private set; }
        public string Year { get; private set; }
        public string Type { get; private set; }
        public bool IsValid { get; private set; }
        public bool Downloaded { get; private set; }

        public AlbumInfo(int id)
        {
            ID = id;
            Album = "";
            ArtistId = 0;
            Artist = "";
            CoverLink = "";
            YearAndType = "";
            HtmlTracks = "";
            HtmlMusicians = "";
            Year = "";
            Type = "";
            IsValid = false;
            Downloaded = false;
        }

        public AlbumInfo(int id, string album, int artistId, string artist, string coverLink, string yearAndType,
            string htmlTracks, string htmlMusicians, string year, string type, bool downloaded)
        {
            ID = id;
            Album = album;
            ArtistId = artistId;
            Artist = artist;
            CoverLink = coverLink;
            YearAndType = yearAndType;
            HtmlTracks = htmlTracks;
            HtmlMusicians = htmlMusicians;
            Year = year;
            Type = type;
            IsValid = true;
            Downloaded = downloaded;
        } 
    }
}
