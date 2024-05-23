using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    class OfferDAL
    {
        public ObservableCollection<Offer> GetAllOffers()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllOffers", con);
                ObservableCollection<Offer> result = new ObservableCollection<Offer>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Offer()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Reason = reader["Reason"].ToString(),
                        ProductId = Convert.ToInt32(reader["ProductId"]),
                        ProductName = reader["ProductName"].ToString(),
                        DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                        StartDate = Convert.ToDateTime(reader["StartDate"]),
                        EndDate = Convert.ToDateTime(reader["EndDate"]),
                        IsActive = Convert.ToBoolean(reader["IsActive"])
                    });
                }
                reader.Close();
                return result;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddOffer(Offer offer)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddOffer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@reason", offer.Reason));
                cmd.Parameters.Add(new SqlParameter("@productId", offer.ProductId));
                cmd.Parameters.Add(new SqlParameter("@discountPercentage", offer.DiscountPercentage));
                cmd.Parameters.Add(new SqlParameter("@startDate", offer.StartDate));
                cmd.Parameters.Add(new SqlParameter("@endDate", offer.EndDate));
                cmd.Parameters.Add(new SqlParameter("@isActive", offer.IsActive));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteOffer(int id)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteOffer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyOffer(Offer offer)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyOffer", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", offer.Id));
                cmd.Parameters.Add(new SqlParameter("@reason", offer.Reason));
                cmd.Parameters.Add(new SqlParameter("@productId", offer.ProductId));
                cmd.Parameters.Add(new SqlParameter("@discountPercentage", offer.DiscountPercentage));
                cmd.Parameters.Add(new SqlParameter("@startDate", offer.StartDate));
                cmd.Parameters.Add(new SqlParameter("@endDate", offer.EndDate));
                cmd.Parameters.Add(new SqlParameter("@isActive", offer.IsActive));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
