using System;
using System.Globalization;
using System.Windows.Data;
using supermarket_manager.Models.EntityLayer;

namespace supermarket_manager.Converters
{
    public class OfferConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 5)
                return null;

            string reason = values[0]?.ToString();
            int productId = 0;
            decimal discountPercentage = 0;
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MinValue;

            if (!string.IsNullOrWhiteSpace(values[1]?.ToString()))
            {
                int.TryParse(values[1]?.ToString(), out productId);
            }

            if (!string.IsNullOrWhiteSpace(values[2]?.ToString()))
            {
                decimal.TryParse(values[2]?.ToString(), out discountPercentage);
            }

            if (values[3] != null)
            {
                DateTime.TryParse(values[3]?.ToString(), out startDate);
            }

            if (values[4] != null)
            {
                DateTime.TryParse(values[4]?.ToString(), out endDate);
            }

            return new Offer
            {
                Reason = reason,
                ProductId = productId,
                DiscountPercentage = discountPercentage,
                StartDate = startDate,
                EndDate = endDate
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
