using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using WpfMVVMAgendaCommands.Models;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class LargestReceiptVM : BasePropertyChanged
    {
        private ReceiptBLL receiptBLL = new ReceiptBLL();
        public ObservableCollection<ReceiptItem> ReceiptItems { get; set; }
        private DateTime? selectedDate;

        public DateTime? SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                NotifyPropertyChanged(nameof(SelectedDate));
            }
        }

        public LargestReceiptVM()
        {
            ReceiptItems = new ObservableCollection<ReceiptItem>();
        }

        private ICommand getLargestReceiptCommand;
        public ICommand GetLargestReceiptCommand
        {
            get
            {
                if (getLargestReceiptCommand == null)
                {
                    getLargestReceiptCommand = new RelayCommand<object>(param =>
                    {
                        if (SelectedDate.HasValue)
                        {
                            ReceiptItems = receiptBLL.GetLargestReceipt(SelectedDate.Value);
                            NotifyPropertyChanged(nameof(ReceiptItems));
                        }
                        else
                        {
                            MessageBox.Show("Please select a date.");
                        }
                    });
                }
                return getLargestReceiptCommand;
            }
        }
    }
}
