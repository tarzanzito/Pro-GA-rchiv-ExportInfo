namespace ProgArchivesCore.Models
{
    /// <summary>
    /// Album Info
    /// </summary>
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
        public bool IsInactive { get; private set; }
        public string AddedOn { get; private set; }

        public AlbumInfo(int id, string addedOn)
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
            IsInactive = true;
            AddedOn = addedOn;
        }

        public AlbumInfo(int id, string album, int artistId, string artist, string coverLink, string yearAndType,
            string htmlTracks, string htmlMusicians, string year, string type, bool isInactive, string addedOn)
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
            IsInactive = isInactive;
            AddedOn = addedOn;
        }
    }
}
