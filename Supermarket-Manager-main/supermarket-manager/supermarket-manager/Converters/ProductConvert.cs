using System;
using System.Globalization;
using System.Windows.Data;
using supermarket_manager.Models.EntityLayer;

namespace supermarket_manager.Converters
{
    public class ProductConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 4)
                return null;

            string name = values[0]?.ToString();
            string barcode = values[1]?.ToString();
            int categoryId = 0;
            int supplierId = 0;

            if (!string.IsNullOrWhiteSpace(values[2]?.ToString()))
            {
                int.TryParse(values[2]?.ToString(), out categoryId);
            }

            if (!string.IsNullOrWhiteSpace(values[3]?.ToString()))
            {
                int.TryParse(values[3]?.ToString(), out supplierId);
            }

            return new Product
            {
                Name = name,
                Barcode = barcode,
                CategoryId = categoryId,
                SupplierId = supplierId
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
