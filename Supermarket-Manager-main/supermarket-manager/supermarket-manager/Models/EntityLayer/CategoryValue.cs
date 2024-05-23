using System;
using WpfMVVMAgendaCommands.Models;

namespace supermarket_manager.Models.EntityLayer
{
    public class CategoryValue : BasePropertyChanged
    {
        private string categoryName;
        private decimal totalValue;

        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                categoryName = value;
                NotifyPropertyChanged(nameof(CategoryName));
            }
        }

        public decimal TotalValue
        {
            get { return totalValue; }
            set
            {
                totalValue = value;
                NotifyPropertyChanged(nameof(TotalValue));
            }
        }
    }
}
