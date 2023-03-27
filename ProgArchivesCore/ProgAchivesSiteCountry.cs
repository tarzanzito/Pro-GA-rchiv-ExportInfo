
namespace Candal.Core
{
    /// <summary>
    /// Retrive information from html progarchives "Countries" pages
    /// </summary>
    internal class ProgAchivesSiteCountry
    {
        private string _htmlData;

        public CountryInfo GetCountryNameFromHtmlData(int page, string htmlData)
        {
            if (htmlData.Length == 0)
                return new CountryInfo(page);

            _htmlData = htmlData;

            string country = GetCountry();

            CountryInfo artistInfo = new CountryInfo(page, country);

            return artistInfo;
        }

        private string GetCountry()
        {
            if (_htmlData.Length == 0)
                return "NOT FOUND";

            string strB = "<title>";
            string strE = "</title>";

            int posB = _htmlData.IndexOf(strB);
            int posE = _htmlData.IndexOf(strE, posB);
            int len = strB.Length;

            string title = _htmlData.Substring(posB + len, posE - posB - len).Trim();
            string country = title.Replace("Progressive Rock & Related Bands/artists from", "").Trim();

            return country;
        }
    }
}
