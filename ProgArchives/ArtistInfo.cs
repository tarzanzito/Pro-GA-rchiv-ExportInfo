
namespace ProgArchives
{
    public class ArtistInfo
    {
        public int ID { get; private set; }
        public string Artist { get; private set; }
        public string Country { get; private set; }
        public string Style { get; private set; }
        public bool IsValid { get; private set; }

        public ArtistInfo(int id)
        {
            ID = id;
            Artist = "";
            Country = "";
            Style = "";
            IsValid = false;
        }

        public ArtistInfo(int id, string artist, string country, string style)
        {
            ID = id;
            Artist = artist;
            Country = country;
            Style = style;
            IsValid = true;
        }
    }
}
