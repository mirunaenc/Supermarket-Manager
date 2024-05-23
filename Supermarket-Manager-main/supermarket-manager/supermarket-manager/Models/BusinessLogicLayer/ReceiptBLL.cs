using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class ReceiptBLL
    {
        private ReceiptDAL receiptDAL = new ReceiptDAL();
        public ObservableCollection<Receipt> ReceiptList { get; set; }

        public ReceiptBLL()
        {
            ReceiptList = receiptDAL.GetAllReceipts();
        }

        public void AddReceipt(Receipt receipt)
        {
            try
            {
                // total bon
                receipt.TotalAmount = receipt.ReceiptItems.Sum(item => item.Subtotal);
                receiptDAL.AddReceipt(receipt);
                ReceiptList.Add(receipt);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public ObservableCollection<Receipt> GetAllReceipts()
        {
            return receiptDAL.GetAllReceipts();
        }
      

        public ObservableCollection<DailyEarnings> GetDailyEarnings(string cashier, int month, int year)
        {
            try
            {
                return receiptDAL.GetDailyEarnings(cashier, month, year);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return new ObservableCollection<DailyEarnings>();
            }
        }
        public ObservableCollection<ReceiptItem> GetLargestReceipt(DateTime date)
        {
            try
            {
                return receiptDAL.GetLargestReceipt(date);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                return new ObservableCollection<ReceiptItem>();
            }
        }
    }
}
