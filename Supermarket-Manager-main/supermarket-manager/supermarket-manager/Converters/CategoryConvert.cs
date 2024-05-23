using supermarket_manager.Models.EntityLayer;
using System.Windows.Data;

namespace supermarket_manager.Converters
{
    public class CategoryConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values == null || values.Length < 1)
                return null;

            return new Category
            {
                Name = values[0]?.ToString()
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
