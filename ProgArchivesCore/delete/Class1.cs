using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace Candal
{
    public class DataBaseOperations
    {
        public void ReadTable()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Resources\ProgArchives2010.mdb");
            string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbPath;
            string strSQL = "SELECT * FROM artists";
              
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProcessRow(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void ProcessRow(OleDbDataReader reader)
        {
            string artist = reader["Artist"].ToString();
            int artist_id = System.Convert.ToInt32(reader["Artist_ID"].ToString());
            bool inactive = System.Convert.ToBoolean(reader["Inactive"].ToString());
            
            if (!inactive)
            {
                if (artist.IndexOf("?") != -1)
                {

                }
            }

        }
    }
}

