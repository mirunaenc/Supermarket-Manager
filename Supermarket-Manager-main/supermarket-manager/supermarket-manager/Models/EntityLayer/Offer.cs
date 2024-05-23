using WpfMVVMAgendaCommands.Models;
using System;

namespace supermarket_manager.Models.EntityLayer
{
    class Offer : BasePropertyChanged
    {
        private int id;
        private string reason;
        private int productId;
        private string productName;
        private decimal discountPercentage;
        private DateTime startDate;
        private DateTime endDate;
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

        public string Reason
        {
            get { return reason; }
            set
            {
                reason = value;
                NotifyPropertyChanged("Reason");
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

        public string ProductName
        {
            get { return productName; }
            set
            {
                productName = value;
                NotifyPropertyChanged("ProductName");
            }
        }

        public decimal DiscountPercentage
        {
            get { return discountPercentage; }
            set
            {
                discountPercentage = value;
                NotifyPropertyChanged("DiscountPercentage");
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                NotifyPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                NotifyPropertyChanged("EndDate");
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
