using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;

namespace supermarket_manager.View_Models
{
    class ReceiptsCRUDVM
    {
        ReceiptBLL receiptBLL = new ReceiptBLL();
        public ObservableCollection<Receipt> ReceiptList { get; set; }

        public ReceiptsCRUDVM()
        {
            ReceiptList = receiptBLL.GetAllReceipts();
        }
    }
}
