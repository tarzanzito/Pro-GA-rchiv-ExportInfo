
namespace ProgArchives
{
    public class AccessDbManager
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

        public AccessDbManager(string DatabaseName)
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

        public void ExecuteNonQuery(string Statement)
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

        public int GetLastPage(string TableName, string FieldName)
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
                throw ex; //TODO
            }

            return value;
        }


        public void ReadTable(string TableName, System.Action<System.Data.OleDb.OleDbDataReader> action)
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
