using ProgArchivesCore.Models;

namespace ProgArchivesCore
{
    public class Delegates
    {
        public delegate void EventCountryInfo(CountryInfo artistInfo, string uri);
        public delegate void EventArtistInfo(ArtistInfo artistInfo, string uri);
        public delegate void EventAlbumInfo(AlbumInfo albumInfo, string uri);
    }
}
