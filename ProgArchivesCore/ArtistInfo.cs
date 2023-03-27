
namespace Candal.Core
{
    /// <summary>
    /// Artist Info
    /// </summary>
    public class ArtistInfo
    {
        public int ID { get; private set; }
        public string Artist { get; private set; }
        public int CountryId { get; private set; }
        public string Country { get; private set; }
        public string Style { get; private set; }
        public bool IsInactive { get; private set; }

        public ArtistInfo(int id)
        {
            ID = id;
            Artist = "";
            CountryId = 0;
            Country = "";
            Style = "";
            IsInactive = true;
        }

        public ArtistInfo(int id, string artist, string country, string style)
        {
            ID = id;
            Artist = artist;
            //CountryId = countryId;
            Country = country;
            Style = style;
            IsInactive = (artist == "");
        }
        public void SetCountryId(int countryId)
        {
            CountryId = countryId;
        }
    }
}
