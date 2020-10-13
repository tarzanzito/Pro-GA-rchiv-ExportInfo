
namespace ProgArchives
{
    public class AccessManager
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

        public AccessManager(string DatabaseName)
        {
            try
            {
                _databaseName = DatabaseName;
                _connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                                    @"Data source=" + _databaseName;

                _connection = new System.Data.OleDb.OleDbConnection();
                _connection.ConnectionString = _connectionString;

            }
            catch (System.Exception ex)
            {
                _connection = null;
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
            }
        }

        public void InsertArtist(ProgArchives.ArtistInfo Artist)
        {
            Insert(Artist.GetInsertStatement());
        }

        public void InsertAlbum(ProgArchives.AlbumInfo Album)
        {
            Insert(Album.GetInsertStatement());
        }

        private void Insert(string InsertStatement)
        {
            if (!IsOpen)
                return;

            try
            {
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(InsertStatement, _connection);
                command.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("Failed to connect to data source");
            }
        }

        public int GetLastArtistPage()
        {
            return GetLastPage("Artists", "Artist_ID");
        }

        public int GetLastAlbumPage()
        {
            return GetLastPage("Albuns", "Album_ID");
        }

        private int GetLastPage(string TableName, string FieldName)
        {
            if (!IsOpen)
                return 0;

            string selectStatement = "SELECT MAX(" + FieldName + ") AS MaxValue FROM " + TableName;

            int value = 0;

            try
            {
                System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(selectStatement, _connection);

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
            }

            return value;
        }
    }
}
