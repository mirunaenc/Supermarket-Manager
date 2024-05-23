using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;
using System.Linq;
using System.DirectoryServices;
using System.Windows.Controls;
using supermarket_manager.Views;

namespace supermarket_manager.View_Models
{
    class CashierWindowVM : BasePropertyChanged
    {
        private ProductBLL productBLL = new ProductBLL();
        private ReceiptBLL receiptBLL = new ReceiptBLL();

        private string searchName;
        private string searchBarcode;
        private DateTime? searchExpiryDate;
        private int? searchSupplierId;
        private int? searchCategoryId;

        public string SearchName
        {
            get { return searchName; }
            set
            {
                searchName = value;
                NotifyPropertyChanged(nameof(SearchName));
            }
        }

        public string SearchBarcode
        {
            get { return searchBarcode; }
            set
            {
                searchBarcode = value;
                NotifyPropertyChanged(nameof(SearchBarcode));
            }
        }

        public DateTime? SearchExpiryDate
        {
            get { return searchExpiryDate; }
            set
            {
                searchExpiryDate = value;
                NotifyPropertyChanged(nameof(SearchExpiryDate));
            }
        }

        public int? SearchSupplierId
        {
            get { return searchSupplierId; }
            set
            {
                searchSupplierId = value;
                NotifyPropertyChanged(nameof(SearchSupplierId));
            }
        }

        public int? SearchCategoryId
        {
            get { return searchCategoryId; }
            set
            {
                searchCategoryId = value;
                NotifyPropertyChanged(nameof(SearchCategoryId));
            }
        }

        public ObservableCollection<Product> SearchResults { get; set; }
        public ObservableCollection<ReceiptItem> ReceiptItems { get; set; }

        public ObservableCollection<Receipt> Receipts { get; set; }

        public decimal TotalAmount { get; set; }

        public CashierWindowVM()
        {
            SearchResults = new ObservableCollection<Product>();
            ReceiptItems = new ObservableCollection<ReceiptItem>();
            Receipts = new ObservableCollection<Receipt>();
        }

        private void Receipt(object parameter)
        {
            ReceiptPage receiptPage = new ReceiptPage();
            receiptPage.Show();
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
                        SearchResults = productBLL.SearchProducts(SearchName, SearchBarcode, SearchExpiryDate, SearchSupplierId, SearchCategoryId);
                        NotifyPropertyChanged(nameof(SearchResults));
                    });
                }
                return searchCommand;
            }
        }

        private ICommand addProductToReceiptCommand;
        public ICommand AddProductToReceiptCommand
        {
            get
            {
                if (addProductToReceiptCommand == null)
                {
                    addProductToReceiptCommand = new RelayCommand<Product>(param =>
                    {
                        if (param != null)
                        {
                            decimal unitPrice = 10m; 

                            var receiptItem = new ReceiptItem
                            {
                                ProductId = param.Id,
                                ProductName = param.Name,
                                Quantity = 1,
                                UnitPrice = unitPrice
                            };
                            ReceiptItems.Add(receiptItem);
                            TotalAmount = ReceiptItems.Sum(item => item.Subtotal);
                            NotifyPropertyChanged(nameof(ReceiptItems));
                            NotifyPropertyChanged(nameof(TotalAmount));
                        }
                    });
                }
                return addProductToReceiptCommand;
            }
        }
    }   
}
