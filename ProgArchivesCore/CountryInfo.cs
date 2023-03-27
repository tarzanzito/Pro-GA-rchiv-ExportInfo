
namespace Candal.Core
{
    /// <summary>
    /// Country Info
    /// </summary>
    public class CountryInfo
    {
        public int ID { get; private set; }
        public string Country { get; private set; }
        public bool IsInactive { get; private set; }

        public CountryInfo(int id)
        {
            ID = id;
            Country = "";
            IsInactive = true;
        }

        public CountryInfo(int id, string country)
        {
            ID = id;
            Country = country;
            IsInactive = (Country == "");
        }

        public CountryInfo(int id, string country, bool isInactive)
        {
            ID = id;
            Country = country;
            IsInactive = isInactive;
        }
    }
}
