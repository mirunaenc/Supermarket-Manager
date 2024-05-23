using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class SuppliersCRUDVM
    {
        SupplierBLL suplierBLL = new SupplierBLL();
        public ObservableCollection<Supplier> SuplierList
        {
            get => suplierBLL.SupplierList;
            set => suplierBLL.SupplierList = value;
        }
        public SuppliersCRUDVM()
        {
            SuplierList = suplierBLL.GetAllSuppliers();
        }

        #region Commands

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<Supplier>(suplierBLL.AddSupplier);
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
                    deleteCommand = new RelayCommand<Supplier>(suplierBLL.DeleteSupplier);
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
                    updateCommand = new RelayCommand<Supplier>(suplierBLL.ModifySupplier);
                }
                return updateCommand;
            }
        }
        #endregion
    }
}
