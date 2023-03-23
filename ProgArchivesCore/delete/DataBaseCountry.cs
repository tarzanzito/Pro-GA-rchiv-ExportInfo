
//namespace Candal
//{
//    public class DataBaseCountry
//    {
//        public string GetInsertSqlStatement(CountryInfo countryInfo)
//        {
//            string sql = "INSERT INTO Countries VALUES ("
//                        + countryInfo.ID.ToString() + ","
//                        + "'" + countryInfo.Country.Replace("'","''") + "',"
//                        + countryInfo.IsInactive.ToString()
//                        + ")";

//            return sql;
//        }

//        public string GetUpdateSqlStatement(CountryInfo countryInfo)
//        {
//            string sql = "UPDATE Countries SET "
//                        + "Country = '" + countryInfo.Country.Replace("'", "''") + "', "
//                        + "Inactive = " + countryInfo.IsInactive.ToString() + " "
//                        + "WHERE ID = " + countryInfo.ID.ToString();

//            return sql;
//        }

//        public string GetDeleteSqlStatement(CountryInfo countryInfo)
//        {
//            string sql = "DELETE Countries WHERE ID = " + countryInfo.ID.ToString();

//            return sql;
//        }

//        public string GetDeleteAllSqlStatement()
//        {
//            string sql = "DELETE Countries";

//            return sql;
//        }
//    }
//}
