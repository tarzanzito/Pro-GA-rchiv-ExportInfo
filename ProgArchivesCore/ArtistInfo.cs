
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
        public string AddedOn { get; private set; }

        public ArtistInfo(int id, string addedOn)
        {
            ID = id;
            Artist = "";
            CountryId = 0;
            Country = "";
            Style = "";
            IsInactive = true;
            AddedOn = addedOn;
        }

        public ArtistInfo(int id, string artist, string country, string style, string addedOn)
        {
            ID = id;
            Artist = artist;
            //CountryId = countryId;
            Country = country;
            Style = style;
            IsInactive = (artist == "");
            AddedOn = addedOn;
        }

        public void SetCountryId(int countryId)
        {
            CountryId = countryId;
        }

    }
}
