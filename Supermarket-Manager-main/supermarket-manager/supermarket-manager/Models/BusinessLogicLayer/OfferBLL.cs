using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class OfferBLL
    {
        OfferDAL offerDAL = new OfferDAL();
        public ObservableCollection<Offer> OfferList { get; set; }

        public OfferBLL()
        {
            OfferList = new ObservableCollection<Offer>(offerDAL.GetAllOffers());
        }


        public void AddOffer(Offer offer)
        {
            string validationError = ValidateOffer(offer);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                offerDAL.AddOffer(offer);
                OfferList.Add(offer);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ModifyOffer(Offer offer)
        {
            string validationError = ValidateOffer(offer);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                offerDAL.ModifyOffer(offer);
                MessageBox.Show("Offer updated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteOffer(Offer offer)
        {
            if (offer == null)
            {
                MessageBox.Show("Must select an offer to delete.");
                return;
            }

            try
            {
                offerDAL.DeleteOffer(offer.Id);
                OfferList.Remove(offer);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<Offer> GetAllOffers()
        {
            return new ObservableCollection<Offer>(offerDAL.GetAllOffers());
        }

        private string ValidateOffer(Offer offer)
        {
            if (offer == null)
            {
                return "Offer cannot be null.";
            }
            if (string.IsNullOrEmpty(offer.Reason))
            {
                return "Reason must not be empty.";
            }
            if (offer.ProductId <= 0)
            {
                return "Product ID must be greater than zero.";
            }
            if (offer.DiscountPercentage <= 0 || offer.DiscountPercentage > 100)
            {
                return "Discount percentage must be between 1 and 100.";
            }
            if (offer.StartDate == default(DateTime))
            {
                return "Start date is required.";
            }
            if (offer.EndDate == default(DateTime))
            {
                return "End date is required.";
            }
            if (offer.EndDate < offer.StartDate)
            {
                return "End date must be after start date.";
            }

            return null;
        }
    }
}
