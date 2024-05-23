using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    class StockDAL
    {
      
        public void CalculateExpiryOffers()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("CalculateExpiryOffers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStock(Stock stock)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("UpdateStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", stock.Id));
                cmd.Parameters.Add(new SqlParameter("@quantity", stock.Quantity));
                cmd.Parameters.Add(new SqlParameter("@isActive", stock.IsActive));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ObservableCollection<Stock> GetStocksByProductName(string productName)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetStocksByProductName", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@productName", productName ?? (object)DBNull.Value));
                ObservableCollection<Stock> result = new ObservableCollection<Stock>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Stock()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Unit = reader["Unit"].ToString(),
                        SupplyDate = Convert.ToDateTime(reader["SupplyDate"]),
                        ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]),
                        PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"]),
                        SalePrice = Convert.ToDecimal(reader["SalePrice"]),
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


        public void CalculateStockClearanceOffers()
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("CalculateStockClearanceOffers", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ObservableCollection<CategoryValue> GetCategoryValues()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetCategoryValues", con);
                ObservableCollection<CategoryValue> result = new ObservableCollection<CategoryValue>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new CategoryValue()
                    {
                        CategoryName = reader["CategoryName"].ToString(),
                        TotalValue = Convert.ToDecimal(reader["TotalValue"])
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


        public void UpdateStockQuantity(int stockId, int quantity)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("UpdateStockQuantity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", stockId));
                cmd.Parameters.Add(new SqlParameter("@quantity", quantity));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void SetStockInactive(int stockId)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("SetStockInactive", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", stockId));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetProductIdByName(string productName)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetProductIdByName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@name", productName));

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

        public ObservableCollection<Stock> GetAllStocks()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllStocks", con);
                ObservableCollection<Stock> result = new ObservableCollection<Stock>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Stock()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Unit = reader["Unit"].ToString(),
                        SupplyDate = Convert.ToDateTime(reader["SupplyDate"]),
                        ExpiryDate = Convert.ToDateTime(reader["ExpiryDate"]),
                        PurchasePrice = Convert.ToDecimal(reader["PurchasePrice"]),
                        SalePrice = Convert.ToDecimal(reader["SalePrice"]),
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


        public void AddStock(Stock stock)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddStock", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@productName", stock.ProductName)); 
                cmd.Parameters.Add(new SqlParameter("@quantity", stock.Quantity));
                cmd.Parameters.Add(new SqlParameter("@unit", stock.Unit));
                cmd.Parameters.Add(new SqlParameter("@supplyDate", stock.SupplyDate));
                cmd.Parameters.Add(new SqlParameter("@expiryDate", stock.ExpiryDate));
                cmd.Parameters.Add(new SqlParameter("@purchasePrice", stock.PurchasePrice));

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        stock.Id = Convert.ToInt32(reader["NewId"]);
                    }
                }
            }
        }







        public void UpdateSalePrice(int stockId, decimal salePrice)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("UpdateSalePrice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", stockId));
                cmd.Parameters.Add(new SqlParameter("@salePrice", salePrice));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

}

