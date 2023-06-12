using ProgArchivesCore.Models;

namespace ProgArchivesCore.DataBaseManagers
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
        ArtistInfo SelectArtistById(int id);
        //ArtistInfo SelectArtistByName(string artist);

        AlbumInfo SelectAlbumById(int id);
        //AlbumInfo SelectAlbumByName(string album);
        //AlbumInfo[] SelectAlbumsByArtistId(int ArtistId);

        CountryInfo SelectCountryById(int id);
        CountryInfo SelectCountryByName(string country);

        //Exists
        bool ExistsArtist(ArtistInfo artistInfo);
        bool ExistsAlbum(AlbumInfo albumInfo);
        bool ExistsCountry(CountryInfo countryInfo);
    }
}
