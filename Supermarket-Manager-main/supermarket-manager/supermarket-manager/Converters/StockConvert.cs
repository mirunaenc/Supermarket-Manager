using supermarket_manager.Models.EntityLayer;
using System;
using System.Globalization;
using System.Windows.Data;

namespace supermarket_manager.Converters
{
    class StockConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] != null && values[1] != null && values[2] != null && values[3] != null && values[4] != null && values[5] != null && values[6] != null)
            {
                if (int.TryParse(values[0]?.ToString(), out int productId) &&
                    int.TryParse(values[1]?.ToString(), out int quantity) &&
                    !string.IsNullOrEmpty(values[2]?.ToString()) &&
                    DateTime.TryParse(values[3]?.ToString(), out DateTime supplyDate) &&
                    DateTime.TryParse(values[4]?.ToString(), out DateTime expiryDate) &&
                    decimal.TryParse(values[5]?.ToString(), out decimal purchasePrice) &&
                    decimal.TryParse(values[6]?.ToString(), out decimal salePrice))
                {
                    return new Stock()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        Unit = values[2]?.ToString(),
                        SupplyDate = supplyDate,
                        ExpiryDate = expiryDate,
                        PurchasePrice = purchasePrice,
                        SalePrice = salePrice
                    };
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
