
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    class ProductDAL
    {
        public ObservableCollection<Product> GetAllProducts()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllProducts", con);
                ObservableCollection<Product> result = new ObservableCollection<Product>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Product()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Barcode = reader["Barcode"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        SupplierId = Convert.ToInt32(reader["SupplierId"])
                    });
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

     
        public void AddProduct(Product product)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", product.Name);
                SqlParameter paramBarcode = new SqlParameter("@barcode", product.Barcode);
                SqlParameter paramCategoryId = new SqlParameter("@categoryId", product.CategoryId);
                SqlParameter paramSupplierId = new SqlParameter("@supplierId", product.SupplierId);

                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramBarcode);
                cmd.Parameters.Add(paramCategoryId);
                cmd.Parameters.Add(paramSupplierId);
                con.Open();
                product.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void DeleteProduct(Product product)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter("@id", product.Id);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyProduct(Product product)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter("@id", product.Id);
                SqlParameter paramName = new SqlParameter("@name", product.Name);
                SqlParameter paramBarcode = new SqlParameter("@barcode", product.Barcode);
                SqlParameter paramCategoryId = new SqlParameter("@categoryId", product.CategoryId);
                SqlParameter paramSupplierId = new SqlParameter("@supplierId", product.SupplierId);

                cmd.Parameters.Add(paramId);
                cmd.Parameters.Add(paramName);
                cmd.Parameters.Add(paramBarcode);
                cmd.Parameters.Add(paramCategoryId);
                cmd.Parameters.Add(paramSupplierId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public ObservableCollection<Product> GetProductsBySupplier(int supplierId)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetProductsBySupplier", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@supplierId", supplierId));
                ObservableCollection<Product> result = new ObservableCollection<Product>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Product()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["ProductName"].ToString(),
                        Barcode = reader["Barcode"].ToString(),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        SupplierId = Convert.ToInt32(reader["SupplierId"])
                    });
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
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

        public ObservableCollection<Product> SearchProductsByName(string name)
        {
            return SearchProducts(name, null, null, null, null);
        }

        public ObservableCollection<Product> SearchProductsByBarcode(string barcode)
        {
            return SearchProducts(null, barcode, null, null, null);
        }

        public ObservableCollection<Product> SearchProductsByExpiryDate(DateTime expiryDate)
        {
            return SearchProducts(null, null, expiryDate, null, null);
        }

        public ObservableCollection<Product> SearchProductsBySupplierName(string supplierName)
        {
            int supplierId = GetSupplierIDByName(supplierName);
            return SearchProducts(null, null, null, supplierId, null);
        }

        public int GetCategoryIDByName(string categoryName)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetCategoryIDByName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@name", categoryName));

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

        public ObservableCollection<Product> SearchProductsByCategoryName(string categoryName)
        {
            int categoryId = GetCategoryIDByName(categoryName);
            return SearchProducts(null, null, null, null, categoryId);
        }


        public ObservableCollection<Product> SearchProducts(string name, string barcode, DateTime? expiryDate, int? supplierId, int? categoryId)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("SearchProducts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@name", name ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@barcode", barcode ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@expiryDate", expiryDate ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@supplierId", supplierId ?? (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@categoryId", categoryId ?? (object)DBNull.Value));

                ObservableCollection<Product> result = new ObservableCollection<Product>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    bool alreadyExists = result.Any(p => p.Id == Convert.ToInt32(reader["Id"]));
                    if (!alreadyExists)
                    {
                        result.Add(new Product()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Barcode = reader["Barcode"].ToString(),
                            CategoryId = Convert.ToInt32(reader["CategoryId"]),
                            SupplierId = Convert.ToInt32(reader["SupplierId"]),
                            ExpiryDate = reader["ExpiryDate"] != DBNull.Value ? Convert.ToDateTime(reader["ExpiryDate"]) : (DateTime?)null
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


    }
}
