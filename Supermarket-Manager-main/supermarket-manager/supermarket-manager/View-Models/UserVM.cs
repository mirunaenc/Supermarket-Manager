using supermarket_manager.Models;
using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using supermarket_manager.Views;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    public class UserVM : BasePropertyChanged
    {
        UserBLL userBLL = new UserBLL();
        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    NotifyPropertyChanged(nameof(Username));
                }
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                if (password != value)
                {
                    password = value;
                    NotifyPropertyChanged(nameof(Password));
                }
            }
        }

        private string loginMessage;
        public string LoginMessage
        {
            get { return loginMessage; }
            set
            {
                if (loginMessage != value)
                {
                    loginMessage = value;
                    NotifyPropertyChanged(nameof(LoginMessage));
                }
            }
        }

        public ICommand LoginCommand { get; }

        public UserVM()
        {
            LoginCommand = new RelayCommand<object>(async param => await Login(param));
        }

        private async Task Login(object parameter)
        {
            Role role = userBLL.GetUserByLogin(username, password);

            if (role == Role.None)
            {
                LoginMessage = "Invalid username or password.";
                return;
            }

            LoginMessage = $"Logging in as: {username} with role: {role}";

            await Task.Delay(2000);

            if (role == Role.Admin)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
            }
            else if (role == Role.Cashier)
            {
                CashierWindow cashierWindow = new CashierWindow();
                cashierWindow.Show();
            }

            Application.Current.Windows[0]?.Close();
        }
    }
}
