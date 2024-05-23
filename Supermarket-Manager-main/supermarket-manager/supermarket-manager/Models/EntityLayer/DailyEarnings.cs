using System;
using WpfMVVMAgendaCommands.Models;

namespace supermarket_manager.Models.EntityLayer
{
    public class DailyEarnings : BasePropertyChanged
    {
        private int day;
        private decimal totalAmount;

        public int Day
        {
            get { return day; }
            set
            {
                day = value;
                NotifyPropertyChanged(nameof(Day));
            }
        }

        public decimal TotalAmount
        {
            get { return totalAmount; }
            set
            {
                totalAmount = value;
                NotifyPropertyChanged(nameof(TotalAmount));
            }
        }
    }
}
