
namespace Candal.Core
{
    /// <summary>
    /// Define DataBase Actions
    /// </summary>
    public interface IDataBaseManager
    {
        void Open();
        void Close();

        //Maxs
        int GetMaxArtist();
        int GetMaxAlbum();
        int GetMaxCountry();

        // Inserts
        void InsertArtist(ArtistInfo artistInfo);
        void InsertAlbum(AlbumInfo albumInfo);
        void InsertCountry(CountryInfo countryInfo);

        // Updates
        void UpdateArtist(ArtistInfo artistInfo);
        void UpdateAlbum(AlbumInfo albumInfo);
        void UpdateCountry(CountryInfo countryInfo);

        //Deletes
        void DeleteArtist(ArtistInfo artistInfo);
        void DeleteAlbum(AlbumInfo albumInfo);
        void DeleteCountry(CountryInfo countryInfo);

        //Delete All
        void DeleteAllArtists();
        void DeleteAllAlbuns();
        void DeleteAllCountries();

        //Select
        CountryInfo SelectCountryByName(string name);
    }
}
