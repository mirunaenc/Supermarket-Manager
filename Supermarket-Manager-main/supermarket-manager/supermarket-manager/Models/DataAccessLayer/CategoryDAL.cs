using supermarket_manager.Models.EntityLayer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    class CategoryDAL
    {
        public ObservableCollection<Category> GetAllCategories()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllCategories", con);
                ObservableCollection<Category> result = new ObservableCollection<Category>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Category()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString()
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


        public int GetCategoryID(string name)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetCategoryID", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@name", name));

                int queryResult = 0;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    queryResult = Convert.ToInt32(reader["Id"]);
                }
                reader.Close();
                return queryResult;
            }
            finally
            {
                con.Close();
            }
        }

        public void AddCategory(Category category)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramName = new SqlParameter("@name", category.Name);

                cmd.Parameters.Add(paramName);
                con.Open();
                category.Id = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }


        public void DeleteCategory(Category category)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramId = new SqlParameter("@id", category.Id);
                cmd.Parameters.Add(paramId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void ModifyCategory(Category category)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyCategory", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramID = new SqlParameter("@id", category.Id);
                SqlParameter paramName = new SqlParameter("@name", category.Name);

                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramName);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
