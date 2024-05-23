using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class DailyEarningsVM : BasePropertyChanged
    {
        private ReceiptBLL receiptBLL = new ReceiptBLL();

        public ObservableCollection<DailyEarnings> DailyEarningsList { get; set; }
        public ObservableCollection<string> CashierList { get; set; }
        private string selectedCashier;
        private int selectedMonth;
        private int selectedYear;

        public string SelectedCashier
        {
            get { return selectedCashier; }
            set
            {
                selectedCashier = value;
                NotifyPropertyChanged(nameof(SelectedCashier));
            }
        }

        public int SelectedMonth
        {
            get { return selectedMonth; }
            set
            {
                selectedMonth = value;
                NotifyPropertyChanged(nameof(SelectedMonth));
            }
        }

        public int SelectedYear
        {
            get { return selectedYear; }
            set
            {
                selectedYear = value;
                NotifyPropertyChanged(nameof(SelectedYear));
            }
        }

        public DailyEarningsVM()
        {
            DailyEarningsList = new ObservableCollection<DailyEarnings>();
            CashierList = new ObservableCollection<string>();

            LoadCashiers();
        }

        private void LoadCashiers()
        {
            CashierList.Add("Cashier1");
            CashierList.Add("Cashier2");
        }

        private ICommand getDailyEarningsCommand;
        public ICommand GetDailyEarningsCommand
        {
            get
            {
                if (getDailyEarningsCommand == null)
                {
                    getDailyEarningsCommand = new RelayCommand<object>(param =>
                    {
                        DailyEarningsList = receiptBLL.GetDailyEarnings(SelectedCashier, SelectedMonth, SelectedYear);
                        NotifyPropertyChanged(nameof(DailyEarningsList));
                    });
                }
                return getDailyEarningsCommand;
            }
        }
    }
}
