using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    class ReceiptDAL
    {
        public ObservableCollection<Receipt> GetAllReceipts()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllReceipts", con);
                ObservableCollection<Receipt> result = new ObservableCollection<Receipt>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var receipt = new Receipt()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                        Cashier = reader["Cashier"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        ReceiptItems = GetReceiptItems(Convert.ToInt32(reader["Id"]))
                    };
                    result.Add(receipt);
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddReceipt(Receipt receipt)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddReceipt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@issueDate", receipt.IssueDate));
                cmd.Parameters.Add(new SqlParameter("@cashier", receipt.Cashier));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", receipt.TotalAmount));

                con.Open();
                receipt.Id = Convert.ToInt32(cmd.ExecuteScalar());

                foreach (var item in receipt.ReceiptItems)
                {
                    AddReceiptItem(item, receipt.Id);
                }
            }
        }

        private void AddReceiptItem(ReceiptItem item, int receiptId)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddReceiptItem", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@receiptId", receiptId));
                cmd.Parameters.Add(new SqlParameter("@productId", item.ProductId));
                cmd.Parameters.Add(new SqlParameter("@productName", item.ProductName));
                cmd.Parameters.Add(new SqlParameter("@quantity", item.Quantity));
                cmd.Parameters.Add(new SqlParameter("@subtotal", item.Subtotal));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        private ObservableCollection<ReceiptItem> GetReceiptItems(int receiptId) //
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetReceiptItems", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@receiptId", receiptId));
                ObservableCollection<ReceiptItem> result = new ObservableCollection<ReceiptItem>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ReceiptItem()
                    {
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"])
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

        public ObservableCollection<DailyEarnings> GetDailyEarnings(string cashier, int month, int year)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetDailyEarnings", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@cashier", cashier));
                cmd.Parameters.Add(new SqlParameter("@month", month));
                cmd.Parameters.Add(new SqlParameter("@year", year));

                ObservableCollection<DailyEarnings> result = new ObservableCollection<DailyEarnings>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new DailyEarnings()
                    {
                        Day = Convert.ToInt32(reader["Day"]),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"])
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


        public ObservableCollection<ReceiptItem> GetLargestReceipt(DateTime date)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetLargestReceipt", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@date", date));
                ObservableCollection<ReceiptItem> result = new ObservableCollection<ReceiptItem>();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new ReceiptItem()
                    {
                        ProductName = reader["ProductName"].ToString(),
                        Quantity = Convert.ToInt32(reader["Quantity"]),
                        Subtotal = Convert.ToDecimal(reader["Subtotal"])
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
    }
    }

