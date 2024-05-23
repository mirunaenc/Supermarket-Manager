using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class StocksCRUDVM : BasePropertyChanged
    {
        StockBLL stockBLL = new StockBLL();

        public ObservableCollection<Stock> StockList
        {
            get => stockBLL.StockList;
            set
            {
                stockBLL.StockList = value;
                NotifyPropertyChanged(nameof(StockList));
            }
        }

        public ObservableCollection<CategoryValue> CategoryValueList
        {
            get => stockBLL.CategoryValueList;
            set
            {
                stockBLL.CategoryValueList = value;
                NotifyPropertyChanged(nameof(CategoryValueList));
            }
        }

        private Stock selectedStock;
        public Stock SelectedStock
        {
            get => selectedStock;
            set
            {
                selectedStock = value;
                NotifyPropertyChanged(nameof(SelectedStock));
            }
        }

        private Stock newStock;
        public Stock NewStock
        {
            get => newStock;
            set
            {
                newStock = value;
                NotifyPropertyChanged(nameof(NewStock));
            }
        }

        private decimal newSalePrice;
        public decimal NewSalePrice
        {
            get => newSalePrice;
            set
            {
                newSalePrice = value;
                NotifyPropertyChanged(nameof(NewSalePrice));
            }
        }

        private string searchProductName;
        public string SearchProductName
        {
            get => searchProductName;
            set
            {
                searchProductName = value;
                NotifyPropertyChanged(nameof(SearchProductName));
            }
        }

        public StocksCRUDVM()
        {
            StockList = stockBLL.StockList;
            CategoryValueList = stockBLL.GetCategoryValues();
            NewStock = new Stock(); 
        }

        #region Commands

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<Stock>(param =>
                    {
                        stockBLL.AddStock(param);
                        NotifyPropertyChanged(nameof(StockList));
                    });
                }
                return addCommand;
            }
        }

        private ICommand updatePriceCommand;
        public ICommand UpdatePriceCommand
        {
            get
            {
                if (updatePriceCommand == null)
                {
                    updatePriceCommand = new RelayCommand<object>(param =>
                    {
                        if (SelectedStock == null)
                        {
                            MessageBox.Show("Must select a stock to update the price.");
                            return;
                        }
                        if (NewSalePrice <= SelectedStock.PurchasePrice)
                        {
                            MessageBox.Show("Sale price must be greater than purchase price.");
                            return;
                        }
                        stockBLL.UpdateSalePrice(SelectedStock.Id, NewSalePrice);

                        SelectedStock.SalePrice = NewSalePrice;
                        NotifyPropertyChanged(nameof(StockList)); 
                    });
                }
                return updatePriceCommand;
            }
        }

        private ICommand searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (searchCommand == null)
                {
                    searchCommand = new RelayCommand<object>(param =>
                    {
                        StockList = stockBLL.GetStocksByProductName(SearchProductName);
                        NotifyPropertyChanged(nameof(StockList));
                    });
                }
                return searchCommand;
            }
        }

        private ICommand getCategoryValuesCommand;
        public ICommand GetCategoryValuesCommand
        {
            get
            {
                if (getCategoryValuesCommand == null)
                {
                    getCategoryValuesCommand = new RelayCommand<object>(param =>
                    {
                        CategoryValueList = stockBLL.GetCategoryValues();
                    });
                }
                return getCategoryValuesCommand;
            }
        }

        #endregion
    }
}
