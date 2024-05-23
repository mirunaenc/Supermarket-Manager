using WpfMVVMAgendaCommands.Models;
using System;
using System.Collections.ObjectModel;

namespace supermarket_manager.Models.EntityLayer
{
    class Receipt : BasePropertyChanged
    {
        private int id;
        private DateTime issueDate;
        private string cashier;
        private decimal totalAmount;
        private ObservableCollection<ReceiptItem> receiptItems;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public DateTime IssueDate
        {
            get { return issueDate; }
            set
            {
                issueDate = value;
                NotifyPropertyChanged("IssueDate");
            }
        }

        public string Cashier
        {
            get { return cashier; }
            set
            {
                cashier = value;
                NotifyPropertyChanged("Cashier");
            }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                NotifyPropertyChanged("TotalAmount");
            }
        }

        public ObservableCollection<ReceiptItem> ReceiptItems
        {
            get { return receiptItems; }
            set
            {
                receiptItems = value;
                NotifyPropertyChanged("ReceiptItems");
            }
        }
    }

    public class ReceiptItem : BasePropertyChanged
    {
        private int productId;
        private string productName;
        private int quantity;
        private decimal unitPrice;
        private decimal subtotal;

        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                NotifyPropertyChanged(nameof(ProductId));
            }
        }

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                NotifyPropertyChanged(nameof(ProductName));
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                NotifyPropertyChanged(nameof(Quantity));
                UpdateSubtotal();
            }
        }

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set
            {
                unitPrice = value;
                NotifyPropertyChanged(nameof(UnitPrice));
                UpdateSubtotal();
            }
        }

        public decimal Subtotal
        {
            get { return subtotal; }
             set
            {
                subtotal = value;
                NotifyPropertyChanged(nameof(Subtotal));
            }
        }

        private void UpdateSubtotal()
        {
            Subtotal = Quantity * UnitPrice;
        }
    }



}
