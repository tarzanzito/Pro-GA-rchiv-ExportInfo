
//namespace Candal
//{
//    public class DataBaseArtist
//    {
//        public string GetInsertSqlStatement(ArtistInfo artistInfo)
//        {
//            string sql = "INSERT INTO Artists VALUES ("
//                        + artistInfo.ID.ToString() + ","
//                        + "'" + artistInfo.Artist.Replace("'","''") + "',"
//                        + "'" + artistInfo.Country.Replace("'", "''") + "',"
//                        + "'" + artistInfo.Style.Replace("'", "''") + "',"
//                        + artistInfo.IsInactive.ToString()
//                        + ")";

//            return sql;
//        }

//        public string GetUpdateSqlStatement(ArtistInfo artistInfo)
//        {
//            string sql = "UPDATE Artists SET "
//                        + "Artist = '" + artistInfo.Artist.Replace("'", "''") + "', "
//                        + "Country = '" + artistInfo.Country.Replace("'", "''") + "', "
//                        + "Style = '" + artistInfo.Style.Replace("'", "''") + "', "
//                        + "Inactive = " + artistInfo.IsInactive.ToString() + " "
//                        + "WHERE Artist_ID = " + artistInfo.ID.ToString().Trim();

//            return sql;
//        }

//        public string GetDeleteSqlStatement(ArtistInfo artistInfo)
//        {
//            string sql = "DELETE Artists WHERE ID = " + artistInfo.ID.ToString();

//            return sql;
//        }

//        public string GetDeleteAllSqlStatement()
//        {
//            string sql = "DELETE Artists";

//            return sql;
//        }
//    }
//}
