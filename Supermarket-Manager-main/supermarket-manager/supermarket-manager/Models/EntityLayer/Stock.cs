using WpfMVVMAgendaCommands.Models;
using System;

namespace supermarket_manager.Models.EntityLayer
{
    class Stock : BasePropertyChanged
    {
        private int id;
        private int productId;
        public string ProductName { get; set; } 
        private int quantity;
        private string? unit;
        private DateTime supplyDate;
        private DateTime expiryDate;
        private decimal purchasePrice;
        private decimal salePrice;
        private bool isActive;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }

        public int ProductId
        {
            get { return productId; }
            set
            {
                productId = value;
                NotifyPropertyChanged("ProductId");
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                NotifyPropertyChanged("Quantity");
            }
        }

        public string? Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                NotifyPropertyChanged("Unit");
            }
        }

        public DateTime SupplyDate
        {
            get { return supplyDate; }
            set
            {
                supplyDate = value;
                NotifyPropertyChanged("SupplyDate");
            }
        }

        public DateTime ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                expiryDate = value;
                NotifyPropertyChanged("ExpiryDate");
            }
        }

        public decimal PurchasePrice
        {
            get { return purchasePrice; }
            set
            {
                purchasePrice = value;
                NotifyPropertyChanged("PurchasePrice");
            }
        }

        public decimal SalePrice
        {
            get { return salePrice; }
            set
            {
                salePrice = value;
                NotifyPropertyChanged("SalePrice");
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                NotifyPropertyChanged("IsActive");
            }
        }
    }
}
