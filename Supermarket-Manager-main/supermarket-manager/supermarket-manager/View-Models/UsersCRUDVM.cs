using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class UsersCRUDVM : BasePropertyChanged
    {
        UserBLL userBLL = new UserBLL();
        public List<string> Roles { get; set; }

        public ObservableCollection<User> UserList
        {
            get => userBLL.UserList;
            set
            {
                userBLL.UserList = value;
                NotifyPropertyChanged(nameof(UserList));
            }
        }

        public UsersCRUDVM()
        {
            UserList = userBLL.GetAllUsers();
            Roles = new List<string>
            {
                "Admin",
                "Cashier"
            };
        }

        #region Commands

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<User>(userBLL.AddUser);
                }
                return addCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand<User>(userBLL.DeleteUser);
                }
                return deleteCommand;
            }
        }

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                {
                    updateCommand = new RelayCommand<User>(userBLL.ModifyUser);
                }
                return updateCommand;
            }
        }
        #endregion
    }
}
