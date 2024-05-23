using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class OffersCalculationVM : BasePropertyChanged
    {
        private StockBLL stockBLL = new StockBLL();
        public ObservableCollection<CategoryValue> CategoryValues { get; set; }

        public OffersCalculationVM()
        {
            CategoryValues = new ObservableCollection<CategoryValue>();
        }

        private ICommand calculateExpiryOffersCommand;
        public ICommand CalculateExpiryOffersCommand
        {
            get
            {
                if (calculateExpiryOffersCommand == null)
                {
                    calculateExpiryOffersCommand = new RelayCommand<object>(param =>
                    {
                        stockBLL.CalculateExpiryOffers();
                        NotifyPropertyChanged(nameof(CategoryValues));
                    });
                }
                return calculateExpiryOffersCommand;
            }
        }

        private ICommand calculateStockClearanceOffersCommand;
        public ICommand CalculateStockClearanceOffersCommand
        {
            get
            {
                if (calculateStockClearanceOffersCommand == null)
                {
                    calculateStockClearanceOffersCommand = new RelayCommand<object>(param =>
                    {
                        stockBLL.CalculateStockClearanceOffers();
                        NotifyPropertyChanged(nameof(CategoryValues));
                    });
                }
                return calculateStockClearanceOffersCommand;
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
                        CategoryValues = stockBLL.GetCategoryValues();
                        NotifyPropertyChanged(nameof(CategoryValues));
                    });
                }
                return getCategoryValuesCommand;
            }
        }
    }
}
