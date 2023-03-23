
namespace Candal.Core
{
    internal class DataBaseSqliteManager : IDataBaseManager
    {
        private string _databaseName;
        private string _connectionString;
        private System.Data.OleDb.OleDbConnection _connection = null;

        public bool IsValid
        {
            get
            {
                return (_connection != null);
            }
        }

        public bool IsOpen
        {
            get
            {
                if (IsValid)
                    return (_connection.State == System.Data.ConnectionState.Open);

                return false;
            }
        }

        public DataBaseSqliteManager(string DatabaseName)
        {
            try
            {
                _databaseName = DatabaseName;
                _connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data source=" + _databaseName;

                _connection = new System.Data.OleDb.OleDbConnection();
                _connection.ConnectionString = _connectionString;

            }
            catch (System.Exception ex)
            {
                _connection = null;
                throw ex; //TODO
            }
        }

        public void Open()
        {
            if (!IsValid)
                return;

            if (IsOpen)
                return;

            try
            {
                _connection.Open();
            }
            catch (System.Exception ex)
            {
                _connection = null;
                throw ex; //TODO
            }
        }

        public void Close()
        {
            if (!IsValid)
                return;

            if (!IsOpen)
                return;

            try
            {
                _connection.Close();
            }
            catch (System.Exception ex)
            {
                _connection = null;
                throw ex;
            }
        }

        private void ExecuteNonQuery(string Statement)
        {
            if (!IsOpen)
                return;

            try
            {
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(Statement, _connection);
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw ex; //TODO
            }
        }

        private int SelectMax(DataBaseTables dataBaseTable)
        {
            if (!IsOpen)
                return 0;

            int value = 0;
            string sql = "";

            try
            {
                sql = $"SELECT MAX(ID) AS MaxValue FROM {dataBaseTable.ToString()}";

                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(sql, _connection);

                System.Data.OleDb.OleDbDataReader dataReader = command.ExecuteReader();

                string temp = "";
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    temp = dataReader["MaxValue"].ToString();
                }

                System.Int32.TryParse(temp, out value);
            }
            catch (System.Exception ex)
            {
                throw ex; //TODO
            }

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
                "'" + artistInfo.Country.Replace("'", "''") + "'," +
                "'" + artistInfo.Style.Replace("'", "''") + "'," +
                artistInfo.IsInactive.ToString() +
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
                albumInfo.IsInactive.ToString() +
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
                 "Country = '" + artistInfo.Country.Replace("'", "''") + "', " +
                 "Style = '" + artistInfo.Style.Replace("'", "''") + "', " +
                 "Inactive = " + artistInfo.IsInactive.ToString() + " " +
                 "WHERE Artist_ID = " + artistInfo.ID.ToString().Trim();

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
                "Inactive = " + albumInfo.IsInactive.ToString() + " " +
                "WHERE Album_ID = " + albumInfo.ID.ToString();

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

        ////////////////////////////////////////

        private void ReadTable(string TableName, System.Action<System.Data.OleDb.OleDbDataReader> action)
        {
            if (!IsOpen)
                return;

            string selectStatement = "SELECT * FROM " + TableName;

            try
            {
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(selectStatement, _connection);

                System.Data.OleDb.OleDbDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    action(dataReader);
                }
                dataReader.Close();
            }
            catch (System.Exception ex)
            {
                throw ex; //TODO
            }
        }
    }
}


//CREATE TABLE Countries(
//    ID INTEGER PRIMARY KEY,
//    Country TEXT,
//    Inactive INTEGER
//);

//CREATE UNIQUE INDEX Countries_Country_Index ON Countries(
//    Country,
//    ID
//);


//CREATE TABLE Artists(
//    ID INTEGER PRIMARY KEY,
//    Artist TEXT,
//    Country TEXT,
//    Style TEXT,
//    Inactive INTEGER
//);


//CREATE UNIQUE INDEX Artists_Artist_Index ON Artists(
//    Artist,
//    ID
//);

//CREATE TABLE Albums(
//    ID INTEGER PRIMARY KEY,
//    Album TEXT,
//    Artist_ID INTEGER,
//    Artist TEXT,
//    Cover TEXT,
//    YearAndType TEXT,
//    Tracks TEXT,
//    Musicians TEXT,
//    YearN STRING,
//    Type TEXT,
//    Inactive INTEGER
//);

//CREATE UNIQUE INDEX Albums_Album_Index ON Albums(
//    Album,
//    ID
//);
