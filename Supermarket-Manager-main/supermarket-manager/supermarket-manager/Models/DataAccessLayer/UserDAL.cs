using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;

namespace supermarket_manager.Models.DataAccessLayer
{
    class UserDAL
    {
        public Role GetUserByLogin(string username, string password)
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetPersonByLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@usern", username));
                cmd.Parameters.Add(new SqlParameter("@psw", password));

                string queryResult = null;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    queryResult = reader["Role"].ToString();
                }
                reader.Close();
                if (queryResult == "Admin")
                {
                    return Role.Admin;
                }
                if (queryResult == "Cashier")
                {
                    return Role.Cashier;
                }
                return Role.None;
            }
            finally
            {
                con.Close();
            }
        }

        public ObservableCollection<User> GetAllUsers()
        {
            SqlConnection con = DALHelper.Connection;
            try
            {
                SqlCommand cmd = new SqlCommand("GetAllUsers", con);
                ObservableCollection<User> result = new ObservableCollection<User>();
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new User()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = reader["Role"].ToString()
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

        public void AddUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("AddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramUsername = new SqlParameter("@username", user.Username);
                SqlParameter paramPassword = new SqlParameter("@password", user.Password);
                SqlParameter paramRole = new SqlParameter("@role", user.Role);

                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);
                cmd.Parameters.Add(paramRole);

                con.Open();
                int newUserId = Convert.ToInt32(cmd.ExecuteScalar());
                user.Id = newUserId;
            }
        }



        public void DeleteUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramUsername = new SqlParameter("@usern", user.Username);
                cmd.Parameters.Add(paramUsername);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ModifyUser(User user)
        {
            using (SqlConnection con = DALHelper.Connection)
            {
                SqlCommand cmd = new SqlCommand("ModifyUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter paramID = new SqlParameter("@id", user.Id);
                SqlParameter paramUsername = new SqlParameter("@usern", user.Username);
                SqlParameter paramPassword = new SqlParameter("@psw", user.Password);
                SqlParameter paramRole = new SqlParameter("@role", user.Role);

                cmd.Parameters.Add(paramID);
                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);
                cmd.Parameters.Add(paramRole);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
