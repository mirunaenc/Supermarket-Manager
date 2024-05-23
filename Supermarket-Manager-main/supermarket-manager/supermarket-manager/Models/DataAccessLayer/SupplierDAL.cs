using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;

namespace supermarket_manager.Models.DataAccessLayer
{
    class SupplierDAL
    {
        public ObservableCollection<Supplier> GetAllSuppliers()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllSupliers", con);
                ObservableCollection<Supplier> result = new ObservableCollection<Supplier>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (Convert.ToBoolean(reader["IsActive"]))  
                    {
                        result.Add(new Supplier()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Country = reader["Country"].ToString(),
                        });
                    }
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddSupplier(Supplier supplier)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddSuplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", supplier.Name);
                SqlParameter paramCountry = new SqlParameter("@country", supplier.Country);

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramCountry);
                con.Open();

                int newId = Convert.ToInt32(cmd.ExecuteScalar());
                supplier.Id = newId;  
            }
        }


        public void DeleteSupplier(Supplier supplier)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteSuplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter("@id", supplier.Id);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifySupplier(Supplier supplier)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifySuplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter("@id", supplier.Id);
                SqlParameter paramName = new SqlParameter("@name", supplier.Name);
                SqlParameter paramCountry = new SqlParameter("@country", supplier.Country);

                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramCountry);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetSupplierIDByName(string supplierName)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetSupplierIDByName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@name", supplierName));

                int queryResult = 0;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    queryResult = Convert.ToInt32(reader["Id"]);
                }
                reader.Close();
                return queryResult;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
