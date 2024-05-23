using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class CategoriesCRUDVM : BasePropertyChanged
    {
        CategoryBLL categoryBLL = new CategoryBLL();
        public ObservableCollection<Category> CategoryList
        {
            get => categoryBLL.CategoryList;
            set => categoryBLL.CategoryList = value;
        }

        private string message;
        public string Message
        {
            get => message;
            set
            {
                message = value;
                NotifyPropertyChanged(nameof(Message));
            }
        }

        private bool isSuccessMessage;
        public bool IsSuccessMessage
        {
            get => isSuccessMessage;
            set
            {
                isSuccessMessage = value;
                NotifyPropertyChanged(nameof(IsSuccessMessage));
            }
        }

        public CategoriesCRUDVM()
        {
            CategoryList = categoryBLL.GetAllCategories();
            categoryBLL.MessageChanged += OnMessageChanged; 
        }

        private void OnMessageChanged(string message, bool isSuccess)
        {
            Message = message;
            IsSuccessMessage = isSuccess;
        }

        #region Commands

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<Category>(categoryBLL.AddCategory);
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
                    deleteCommand = new RelayCommand<Category>(categoryBLL.DeleteCategory);
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
                    updateCommand = new RelayCommand<Category>(categoryBLL.ModifyCategory);
                }
                return updateCommand;
            }
        }
        #endregion
    }
}
