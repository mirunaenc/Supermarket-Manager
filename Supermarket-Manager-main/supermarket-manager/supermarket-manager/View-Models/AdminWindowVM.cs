using supermarket_manager.Views;
using System.Windows;
using System.Windows.Input;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    internal class AdminWindowVM
    {
        public ICommand UserCommand { get; }
        public ICommand SuplierCommand { get; }
        public ICommand CategoryCommand { get; }
        public ICommand ProductCommand { get; }
        public ICommand StockCommand { get; }
        public ICommand ReceiptCommand { get; }
        public ICommand OfferCommand { get; }
        public ICommand DailyEarningsCommand { get; }
        public ICommand CalculateOfferCommand { get; }
        public ICommand LargestReceiptCommand { get; }



        public AdminWindowVM()
        {
            UserCommand = new RelayCommand<object>(UserPage);
            SuplierCommand = new RelayCommand<object>(SuplierPage);
            CategoryCommand = new RelayCommand<object>(CategoryPage);
            ProductCommand = new RelayCommand<object>(ProductPage);
            StockCommand = new RelayCommand<object>(StockPage);
            ReceiptCommand = new RelayCommand<object>(ReceiptPage);
            OfferCommand = new RelayCommand<object>(OfferPage);
            DailyEarningsCommand = new RelayCommand<object>(DailyEarningsPage);
            CalculateOfferCommand = new RelayCommand<object>(CalculateOfferPage);
            LargestReceiptCommand = new RelayCommand<object>(LargestReceiptPage);

        }

        private void CalculateOfferPage(object parameter)
        {
            OffersCalculationView offersCalculationView = new OffersCalculationView();
            offersCalculationView.Show();
        }
        private void UserPage(object parameter)
        {
            UsersCRUD usersCRUD = new UsersCRUD();
            usersCRUD.Show();
        }

        private void SuplierPage(object parameter)
        {
            SuppliersCRUD suppliersCRUD = new SuppliersCRUD();
            suppliersCRUD.Show();
        }

        private void CategoryPage(object parameter)
        {
            CategoriesCRUD categoriesCRUD = new CategoriesCRUD();
            categoriesCRUD.Show();
        }

        private void ProductPage(object parameter)
        {
            ProductsCRUD productsCRUD = new ProductsCRUD();
            productsCRUD.Show();
        }

        private void StockPage(object parameter)
        {
            StocksCRUD stocksCRUD = new StocksCRUD();
            stocksCRUD.Show();
        }

        private void ReceiptPage(object parameter)
        {
            ReceiptsPage receiptsCRUD = new ReceiptsPage();
            receiptsCRUD.Show();
        }

        private void OfferPage(object parameter)
        {
            OffersCRUD offersCRUD = new OffersCRUD();
            offersCRUD.Show();
        }

        private void DailyEarningsPage(object parameter)
        {
            DailyEarningsView dailyEarningsView = new DailyEarningsView();
            dailyEarningsView.Show();
        }
        private void LargestReceiptPage(object parameter)
        {
            LargestReceiptView largestReceiptView = new LargestReceiptView();
            largestReceiptView.Show();
        }
    }
}
