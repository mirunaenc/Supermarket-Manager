using WpfMVVMAgendaCommands.Models;

namespace supermarket_manager.Models.EntityLayer
{
    class Supplier : BasePropertyChanged
    {
        private int id;
        private string? name;
        private string? country;
        private bool? valid;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
            }
        }
        public string? Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public string? Country
        {
            get { return country; }
            set
            {
                country = value;
                NotifyPropertyChanged("Country");
            }
        }

        public bool? Valid
        {
            get { return valid; }
            set
            {
                valid = value;
                NotifyPropertyChanged("Valid");
            }
        }
    }
}
