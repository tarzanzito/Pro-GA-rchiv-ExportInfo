
using System;
using System.Data.OleDb;
using ProgArchivesCore.Models;

#pragma warning disable CA1416 // Validate platform compatibility

namespace ProgArchivesCore.DataBaseManagers
{
    /// <summary>
    /// Manage "Microsoft Access" Database
    /// </summary>
    public class DataBaseAccessManager : IDataBaseManager
    {
        private string _databaseName;
        private string _connectionString;
        private OleDbConnection _connection = null;

        private bool IsValid
        {
            get
            {
                return _connection != null;
            }
        }

        private bool IsOpen
        {
            get
            {
                return _connection.State == System.Data.ConnectionState.Open;
            }
        }

        public DataBaseAccessManager(string DatabaseName)
        {
            _databaseName = DatabaseName;
            //_connectionString = "Provider=Microsoft.Jet.OLEDB.12.0;" + "Data source=" + _databaseName;
            _connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data source=" + _databaseName;

            _connection = new OleDbConnection(_connectionString);
            Open();
            Close();
        }

        public void Open()
        {
            if (!IsValid)
                throw new Exception("Database is Invalid.");

            if (IsOpen)
                return;

            _connection.Open();
        }

        public void Close()
        {

            if (!IsValid)
                throw new Exception("Database is Invalid.");

            if (!IsOpen)
                return;

            _connection.Close();
        }

        private void ExecuteNonQuery(string sql)
        {
            Open();

            OleDbCommand command = new OleDbCommand(sql, _connection);

            int result = command.ExecuteNonQuery();
        }

        private OleDbDataReader ExecuteReader(string sql)
        {
            Open();

            OleDbCommand command = new OleDbCommand(sql, _connection);

            return command.ExecuteReader();
        }


        private string GetDbField(OleDbDataReader dataReader, string field)
        {
            string value = null;

            if (dataReader[field] != null)
                value = dataReader[field].ToString().Trim();

            return value;
        }

        private int SelectMax(DataBaseTables dataBaseTable)
        {
            int value = 0;
            string sql = "";

            Open();

            sql = $"SELECT MAX(ID) AS MaxValue FROM {dataBaseTable.ToString()}";

            OleDbCommand command = new OleDbCommand(sql, _connection);

            OleDbDataReader dataReader = command.ExecuteReader();

            string temp = "";
            if (dataReader.HasRows)
            {
                _ = dataReader.Read();
                temp = dataReader["MaxValue"].ToString();
            }

            _ = int.TryParse(temp, out value);

            dataReader.Close();

            return value;
        }

        //Maxs
        public int GetMaxArtist()
        {
            return SelectMax(DataBaseTables.Artists);
        }

        public int GetMaxAlbum()
        {
            return SelectMax(DataBaseTables.Albums);
        }

        public int GetMaxCountry()
        {
            return SelectMax(DataBaseTables.Countries);
        }

        // Inserts
        public void InsertArtist(ArtistInfo artistInfo)
        {
            string sql = "INSERT INTO " +
                DataBaseTables.Artists.ToString() +
                " VALUES (" +
                artistInfo.ID.ToString() + "," +
                "'" + artistInfo.Artist.Replace("'", "''") + "'," +
                artistInfo.CountryId.ToString() + "," +
                "'" + artistInfo.Country.Replace("'", "''") + "'," +
                "'" + artistInfo.Style.Replace("'", "''") + "'," +
                artistInfo.IsInactive.ToString() + "," +
                "'" + artistInfo.AddedOn + "'" +
                ")";

            ExecuteNonQuery(sql);
        }

        public void InsertAlbum(AlbumInfo albumInfo)
        {
            string sql = "INSERT INTO " +
                DataBaseTables.Albums.ToString() +
                " VALUES (" +
                albumInfo.ID.ToString() + "," +
                "'" + albumInfo.Album.Replace("'", "''") + "'," +
                albumInfo.ArtistId.ToString() + "," +
                "'" + albumInfo.Artist.Replace("'", "''") + "'," +
                "'" + albumInfo.CoverLink.Replace("'", "''") + "'," +
                "'" + albumInfo.YearAndType.Replace("'", "''") + "'," +
                "'" + albumInfo.HtmlTracks.Replace("'", "''") + "'," +
                "'" + albumInfo.HtmlMusicians.Replace("'", "''") + "'," +
                "'" + albumInfo.Year + "'," +
                "'" + albumInfo.Type + "'," +
                albumInfo.IsInactive.ToString() + "," +
                 "'" + albumInfo.AddedOn + "'" +
                ")";

            ExecuteNonQuery(sql);
        }

        public void InsertCountry(CountryInfo countryInfo)
        {
            string sql = "INSERT INTO " +
                DataBaseTables.Countries.ToString() +
                " VALUES (" +
                countryInfo.ID.ToString() + "," +
                "'" + countryInfo.Country.Replace("'", "''") + "'," +
                countryInfo.IsInactive.ToString() +
                ")";

            ExecuteNonQuery(sql);
        }

        // Updates
        public void UpdateArtist(ArtistInfo artistInfo)
        {
            string sql = "UPDATE " +
                 DataBaseTables.Artists.ToString() +
                 " SET " +
                 "Artist = '" + artistInfo.Artist.Replace("'", "''") + "', " +
                 "Country_ID = " + artistInfo.CountryId + ", " +
                 "Country = '" + artistInfo.Country.Replace("'", "''") + "', " +
                 "Style = '" + artistInfo.Style.Replace("'", "''") + "', " +
                 "Inactive = " + artistInfo.IsInactive.ToString() + ", " +
                 "AddedOn = '" + artistInfo.AddedOn + "' " +
                 "WHERE ID = " + artistInfo.ID.ToString().Trim();

            ExecuteNonQuery(sql);
        }

        public void UpdateAlbum(AlbumInfo albumInfo)
        {
            string sql = "UPDATE " +
                DataBaseTables.Albums.ToString() +
                " SET " +
                "Album = '" + albumInfo.Album.Replace("'", "''") + "', " +
                "Artist_ID = " + albumInfo.ArtistId.ToString() + ", " +
                "Artist = '" + albumInfo.Artist.Replace("'", "''") + "', " +
                "Cover = '" + albumInfo.CoverLink.Replace("'", "''") + "', " +
                "YearAndType = '" + albumInfo.YearAndType.Replace("'", "''") + "', " +
                "Tracks = '" + albumInfo.HtmlTracks.Replace("'", "''") + "', " +
                "Musicians = '" + albumInfo.HtmlMusicians.Replace("'", "''") + "', " +
                "YearN = '" + albumInfo.Year + "', " +
                "Type = '" + albumInfo.Type + "', " +
                "Inactive = " + albumInfo.IsInactive.ToString() + ", " +
                "AddedOn = '" + albumInfo.AddedOn + "' " +
                "WHERE ID = " + albumInfo.ID.ToString();

            ExecuteNonQuery(sql);
        }

        public void UpdateCountry(CountryInfo countryInfo)
        {
            string sql = "UPDATE " +
                DataBaseTables.Countries.ToString() +
                " SET " +
                "Country = '" + countryInfo.Country.Replace("'", "''") + "', " +
                "Inactive = " + countryInfo.IsInactive.ToString() + " " +
                "WHERE ID = " + countryInfo.ID.ToString();

            ExecuteNonQuery(sql);
        }

        //Deletes

        public void DeleteArtist(ArtistInfo artistInfo)
        {
            string sql = "DELETE " +
                DataBaseTables.Artists.ToString() +
                " WHERE ID = " + artistInfo.ID.ToString();

            ExecuteNonQuery(sql);
        }

        public void DeleteAlbum(AlbumInfo albumInfo)
        {
            string sql = "DELETE " +
                DataBaseTables.Albums.ToString() +
                " WHERE ID = " + albumInfo.ID.ToString();

            ExecuteNonQuery(sql);
        }

        public void DeleteCountry(CountryInfo countryInfo)
        {
            string sql = "DELETE " +
                DataBaseTables.Countries.ToString() +
                " WHERE ID = " + countryInfo.ID.ToString();

            ExecuteNonQuery(sql);
        }

        //Delete All

        public void DeleteAllArtists()
        {
            string sql = "DELETE " + DataBaseTables.Artists.ToString();

            ExecuteNonQuery(sql);
        }

        public void DeleteAllAlbuns()
        {
            string sql = "DELETE " + DataBaseTables.Albums.ToString();

            ExecuteNonQuery(sql);
        }

        public void DeleteAllCountries()
        {
            string sql = "DELETE " + DataBaseTables.Countries.ToString();

            ExecuteNonQuery(sql);
        }

        //Select

        public ArtistInfo SelectArtistById(int id)
        {
            ArtistInfo artistInfo = null;

            string sql = $"SELECT * FROM {DataBaseTables.Artists} WHERE id = {id}";

            OleDbDataReader dataReader = ExecuteReader(sql);

            int idF;
            string artistF;
            int countryIdF;
            string countryF;
            string styleF;
            bool isInactiveF;
            string addedOnF;

            if (dataReader.HasRows)
            {
                _ = dataReader.Read();

                idF = Convert.ToInt32(GetDbField(dataReader, "ID"));
                artistF = GetDbField(dataReader, "Artist");
                countryIdF = Convert.ToInt32(GetDbField(dataReader, "Country_ID"));
                countryF = GetDbField(dataReader, "Country");
                styleF = GetDbField(dataReader, "Style");
                isInactiveF = Convert.ToBoolean(GetDbField(dataReader, "Inactive"));
                addedOnF = GetDbField(dataReader, "AddedOn");

                artistInfo = new ArtistInfo(idF, artistF, countryIdF, countryF, styleF, isInactiveF, addedOnF);
            }

            dataReader.Close();

            return artistInfo;
        }

        public AlbumInfo SelectAlbumById(int id)
        {
            AlbumInfo albumInfo = null;

            string sql = $"SELECT * FROM {DataBaseTables.Albums} WHERE id = {id}";

            OleDbDataReader dataReader = ExecuteReader(sql);

            int idF;
            string albumF;
            int artistIdF;
            string artistF;
            string coverLinkF;
            string YearAndTypeF;
            string HtmlTracksF;
            string HtmlMusiciansF;
            string yearF;
            string typeF;
            bool isInactiveF;
            string addedOnF;

            if (dataReader.HasRows)
            {
                _ = dataReader.Read();

                idF = Convert.ToInt32(GetDbField(dataReader, "ID"));
                albumF = GetDbField(dataReader, "Album");
                artistIdF = Convert.ToInt32(GetDbField(dataReader, "Artist_ID"));
                artistF = GetDbField(dataReader, "Artist");
                coverLinkF = GetDbField(dataReader, "Cover");
                YearAndTypeF = GetDbField(dataReader, "YearAndType");
                HtmlTracksF = GetDbField(dataReader, "Tracks");
                HtmlMusiciansF = GetDbField(dataReader, "Musicians");
                yearF = GetDbField(dataReader, "YearN");
                typeF = GetDbField(dataReader, "Type");
                isInactiveF = Convert.ToBoolean(GetDbField(dataReader, "Inactive"));
                addedOnF = GetDbField(dataReader, "AddedOn");

                albumInfo = new AlbumInfo(idF, albumF, artistIdF, artistF, coverLinkF, YearAndTypeF, HtmlTracksF, HtmlMusiciansF, yearF, typeF, isInactiveF, addedOnF);
            }

            dataReader.Close();

            return albumInfo;
        }

        public CountryInfo SelectCountryById(int id)
        {
            CountryInfo countryInfo = null;

            string sql = $"SELECT * FROM {DataBaseTables.Countries} WHERE id = {id}";

            OleDbDataReader dataReader = ExecuteReader(sql);

            int idF;
            string countryF;
            bool isInactiveF;

            if (dataReader.HasRows)
            {
                _ = dataReader.Read();

                idF = Convert.ToInt32(GetDbField(dataReader, "ID"));
                countryF = GetDbField(dataReader, "Country");
                isInactiveF = Convert.ToBoolean(GetDbField(dataReader, "Inactive"));

                countryInfo = new CountryInfo(idF, countryF, isInactiveF);
            }

            dataReader.Close();

            return countryInfo;
        }

        public CountryInfo SelectCountryByName(string country)
        {
            CountryInfo countryInfo = null;

            string sql = $"SELECT * FROM {DataBaseTables.Countries} WHERE UCASE(Country) = '{country.ToUpper()}'";

            OleDbDataReader dataReader = ExecuteReader(sql);

            int idF;
            string countryF;
            bool isInactiveF;

            if (dataReader.HasRows)
            {
                _ = dataReader.Read();

                idF = Convert.ToInt32(GetDbField(dataReader, "ID"));
                countryF = GetDbField(dataReader, "Country");
                isInactiveF = Convert.ToBoolean(GetDbField(dataReader, "Inactive"));

                countryInfo = new CountryInfo(idF, countryF, isInactiveF);
            }

            dataReader.Close();

            return countryInfo;
        }

        //Exists

        public bool ExistsArtist(ArtistInfo artistInfo)
        {
            ArtistInfo artistInfoFound = SelectArtistById(artistInfo.ID);

            return artistInfoFound != null;
        }

        public bool ExistsAlbum(AlbumInfo albumInfo)
        {
            AlbumInfo AlbumInfoFound = SelectAlbumById(albumInfo.ID);

            return AlbumInfoFound != null;
        }

        public bool ExistsCountry(CountryInfo countryInfo)
        {
            CountryInfo countryInfoFound = SelectCountryById(countryInfo.ID);

            return countryInfoFound != null;
        }

        //

        private void ReadTable(string TableName, Action<OleDbDataReader> action)
        {
            string selectStatement = "SELECT * FROM " + TableName;

            Open();

            OleDbCommand command = new OleDbCommand(selectStatement, _connection);

            OleDbDataReader dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                action(dataReader);
            }
            dataReader.Close();
        }

    }
}
