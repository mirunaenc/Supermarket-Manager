using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class StockBLL
    {
        StockDAL stockDAL = new StockDAL();
        public ObservableCollection<Stock> StockList { get; set; }
        public ObservableCollection<CategoryValue> CategoryValueList { get; set; }


        public StockBLL()
        {
            StockList = new ObservableCollection<Stock>(stockDAL.GetAllStocks());
            CategoryValueList = new ObservableCollection<CategoryValue>(stockDAL.GetCategoryValues());

        }
        public void CalculateExpiryOffers()
        {
            try
            {
                stockDAL.CalculateExpiryOffers();
                MessageBox.Show("Expiry offers calculated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public decimal CalculateSalePrice(decimal purchasePrice)
        {
            decimal markupPercentage = 0.20m; // adaos comercial de 20%
            return purchasePrice * (1 + markupPercentage);
        }
        public void CalculateStockClearanceOffers()
        {
            try
            {
                stockDAL.CalculateStockClearanceOffers();
                MessageBox.Show("Stock clearance offers calculated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<CategoryValue> GetCategoryValues()
        {
            return new ObservableCollection<CategoryValue>(stockDAL.GetCategoryValues());
        }

        public void AddStock(Stock stock)
        {
            string validationError = ValidateStock(stock);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                decimal markup = DALHelper.GetCommercialMarkup();
                stock.SalePrice = stock.PurchasePrice * (1 + markup);

                stockDAL.AddStock(stock);
                StockList.Add(stock);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateSalePrice(int stockId, decimal salePrice)
        {
            var stock = StockList.FirstOrDefault(s => s.Id == stockId);
            if (stock == null)
            {
                MessageBox.Show("Stock cannot be null.");
                return;
            }
            if (salePrice <= stock.PurchasePrice)
            {
                MessageBox.Show("Sale price must be greater than purchase price.");
                return;
            }

            try
            {
                stockDAL.UpdateSalePrice(stockId, salePrice);
                stock.SalePrice = salePrice;
                MessageBox.Show("Sale price updated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void UpdateStockQuantity(int stockId, int quantity)
        {
            try
            {
                stockDAL.UpdateStockQuantity(stockId, quantity);
                var stock = StockList.FirstOrDefault(s => s.Id == stockId);
                if (stock != null)
                {
                    stock.Quantity = quantity;
                    if (quantity == 0)
                    {
                        SetStockInactive(stockId);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SetStockInactive(int stockId)
        {
            try
            {
                stockDAL.SetStockInactive(stockId);
                var stock = StockList.FirstOrDefault(s => s.Id == stockId);
                if (stock != null)
                {
                    StockList.Remove(stock);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
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

        public ObservableCollection<Stock> GetStocksByProductName(string productName)
        {
            return stockDAL.GetStocksByProductName(productName);
        }
        public void UpdateStockAfterSale(string productName, int quantity)
        {
            var stockList = stockDAL.GetStocksByProductName(productName).OrderBy(s => s.SupplyDate).ToList();

            foreach (var stock in stockList)
            {
                if (quantity == 0) break;

                if (stock.Quantity >= quantity)
                {
                    stock.Quantity -= quantity;
                    if (stock.Quantity == 0)
                    {
                        stock.IsActive = false;
                    }
                    stockDAL.UpdateStock(stock);
                    quantity = 0;
                }
                else
                {
                    quantity -= stock.Quantity;
                    stock.Quantity = 0;
                    stock.IsActive = false;
                    stockDAL.UpdateStock(stock);
                }
            }
        }
        public void CheckStockExpiry()
        {
            var stockList = stockDAL.GetAllStocks();
            foreach (var stock in stockList)
            {
                if (stock.ExpiryDate <= DateTime.Now)
                {
                    stock.IsActive = false;
                    stockDAL.UpdateStock(stock);
                }
            }
        }


        private string ValidateStock(Stock stock)
        {
            if (stock == null)
            {
                return "Stock cannot be null.";
            }
            if (stock.Quantity <= 0)
            {
                return "Quantity must be greater than zero.";
            }
            if (string.IsNullOrEmpty(stock.Unit))
            {
                return "Unit must not be empty.";
            }
            if (stock.PurchasePrice <= 0)
            {
                return "Purchase price must be greater than zero.";
            }
            if (stock.SupplyDate == default(DateTime))
            {
                return "Supply date is required.";
            }
            if (stock.ExpiryDate == default(DateTime))
            {
                return "Expiry date is required.";
            }

            return null;
        }
    }
}
