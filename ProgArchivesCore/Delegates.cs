using ProgArchivesCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ProgArchivesCore
{
    public class Delegates
    {
        public delegate void EventCountryInfo(CountryInfo artistInfo, string uri);
        public delegate void EventArtistInfo(ArtistInfo artistInfo, string uri);
        public delegate void EventAlbumInfo(AlbumInfo albumInfo, string uri);
    }
}
