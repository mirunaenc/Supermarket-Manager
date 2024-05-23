using WpfMVVMAgendaCommands.Models;

namespace supermarket_manager.Models.EntityLayer
{
    class Product : BasePropertyChanged
    {
        private int id;
        private string? name;
        private string? barcode;
        private int categoryId;
        private int supplierId;
        private DateTime? expiryDate;

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
        public DateTime? ExpiryDate
        {
            get { return expiryDate; }
            set
            {
                expiryDate = value;
                NotifyPropertyChanged(nameof(ExpiryDate));
            }
        }
        public string? Barcode
        {
            get { return barcode; }
            set
            {
                barcode = value;
                NotifyPropertyChanged("Barcode");
            }
        }

        public int CategoryId
        {
            get { return categoryId; }
            set
            {
                categoryId = value;
                NotifyPropertyChanged("CategoryId");
            }
        }

        public int SupplierId
        {
            get { return supplierId; }
            set
            {
                supplierId = value;
                NotifyPropertyChanged("SupplierId");
            }
        }
    }
}
