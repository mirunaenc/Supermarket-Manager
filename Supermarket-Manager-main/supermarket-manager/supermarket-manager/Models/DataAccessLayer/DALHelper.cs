using System;
using System.Configuration;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    public static class DALHelper
    {
        public static SqlConnection Connection
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SupermarketDB"].ConnectionString;
                return new SqlConnection(connectionString);
            }
        }

        public static bool TestConnection()
        {
            using (SqlConnection con = Connection)
            {
                try
                {
                    con.Open();
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Eroare la realizarea conexiunii: {ex.Message}");
                    return false; 
                }
                finally
                {
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        public static decimal GetCommercialMarkup()
        {
            string markupValue = ConfigurationManager.AppSettings["CommercialMarkup"];
            if (decimal.TryParse(markupValue, out decimal markup))
            {
                return markup;
            }
            return 0.2m;
        }
    }
}
