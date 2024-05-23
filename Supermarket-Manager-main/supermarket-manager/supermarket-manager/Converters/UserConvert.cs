using supermarket_manager.Models.EntityLayer;
using System;
using System.Globalization;
using System.Windows.Data;

namespace supermarket_manager.Converters
{
    public class UserConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 3)
                return null;

            string username = values[0]?.ToString();
            string password = values[1]?.ToString();
            string role = values[2]?.ToString();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(role))
            {
                return new User()
                {
                    Username = username,
                    Password = password,
                    Role = role
                };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
