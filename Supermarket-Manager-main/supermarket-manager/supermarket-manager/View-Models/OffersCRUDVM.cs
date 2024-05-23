using supermarket_manager.Models.BusinessLogicLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfMVVMAgendaCommands.ViewModels;

namespace supermarket_manager.View_Models
{
    class OffersCRUDVM
    {
        OfferBLL offerBLL = new OfferBLL();
        public ObservableCollection<Offer> OfferList
        {
            get => offerBLL.OfferList;
            set => offerBLL.OfferList = value;
        }

        public OffersCRUDVM()
        {
            OfferList = offerBLL.GetAllOffers();
        }

        #region Commands

        private ICommand addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (addCommand == null)
                {
                    addCommand = new RelayCommand<Offer>(offerBLL.AddOffer);
                }
                return addCommand;
            }
        }

        private ICommand modifyCommand;
        public ICommand ModifyCommand
        {
            get
            {
                if (modifyCommand == null)
                {
                    modifyCommand = new RelayCommand<Offer>(offerBLL.ModifyOffer);
                }
                return modifyCommand;
            }
        }

        private ICommand deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                {
                    deleteCommand = new RelayCommand<Offer>(offerBLL.DeleteOffer);
                }
                return deleteCommand;
            }
        }

        #endregion
    }
}
