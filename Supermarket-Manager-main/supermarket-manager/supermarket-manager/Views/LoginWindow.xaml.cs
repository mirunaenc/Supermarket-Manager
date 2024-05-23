using supermarket_manager.View_Models;
using System.Windows;
using System.Windows.Controls;

namespace supermarket_manager.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is UserVM viewModel)
            {
                viewModel.Password = (sender as PasswordBox).Password;
            }
        }
    }
}
