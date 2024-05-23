using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class ProductsCRUDVM : BasePropertyChanged
    {
        ProductBLL productBLL = new ProductBLL();
        public ObservableCollection<Product> ProductList
        {
            get => productBLL.ProductList;
            set
            {
                productBLL.ProductList = value;
                NotifyPropertyChanged(nameof(ProductList));
            }
        }

        private string supplierName;
        public string SupplierName
        {
            get => supplierName;
            set
            {
                supplierName = value;
                NotifyPropertyChanged(nameof(SupplierName));
            }
        }

        private string infoMessage;
        public string InfoMessage
        {
            get => infoMessage;
            set
            {
                infoMessage = value;
                NotifyPropertyChanged(nameof(InfoMessage));
            }
        }

        private Brush infoMessageColor;
        public Brush InfoMessageColor
        {
            get => infoMessageColor;
            set
            {
                infoMessageColor = value;
                NotifyPropertyChanged(nameof(InfoMessageColor));
            }
        }

        public ProductsCRUDVM()
        {
            ProductList = productBLL.GetAllProducts();
        }

        #region Commands

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<Product>(productBLL.AddProduct);
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
                    deleteCommand = new RelayCommand<Product>(productBLL.DeleteProduct);
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
                    updateCommand = new RelayCommand<Product>(productBLL.ModifyProduct);
                }
                return updateCommand;
            }
        }

        private ICommand getProductsBySupplierCommand;
        public ICommand GetProductsBySupplierCommand
        {
            get
            {
                if (getProductsBySupplierCommand == null)
                {
                    getProductsBySupplierCommand = new RelayCommand<string>(supplierName =>
                    {
                        if (!string.IsNullOrEmpty(supplierName))
                        {
                            ProductList = productBLL.GetProductsBySupplier(supplierName);
                            if (ProductList.Count == 0)
                            {
                                InfoMessage = "No products found for the given supplier.";
                                InfoMessageColor = Brushes.Red;
                            }
                            else
                            {
                                InfoMessage = $"{ProductList.Count} products found for the given supplier.";
                                InfoMessageColor = Brushes.Green;
                            }
                        }
                        else
                        {
                            InfoMessage = "Please enter a supplier name.";
                            InfoMessageColor = Brushes.Red;
                        }
                    });
                }
                return getProductsBySupplierCommand;
            }
        }

        #endregion
    }
}
