using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class UserBLL
    {
        UserDAL userDAL = new UserDAL();
        public ObservableCollection<User> UserList { get; set; }

        public UserBLL()
        {
            UserList = new ObservableCollection<User>(userDAL.GetAllUsers());
        }

        public void AddUser(User user)
        {
            string validationError = ValidateUser(user);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                userDAL.AddUser(user);
                UserList.Add(user);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteUser(User user)
        {
            if (user == null)
            {
                MessageBox.Show("Must select a user to delete.");
                return;
            }

            try
            {
                userDAL.DeleteUser(user);
                UserList.Remove(user); 
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ModifyUser(User user)
        {
            string validationError = ValidateUser(user);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                userDAL.ModifyUser(user);
                MessageBox.Show("User information updated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<User> GetAllUsers()
        {
            return new ObservableCollection<User>(userDAL.GetAllUsers());
        }

        private string ValidateUser(User user)
        {
            if (user == null)
            {
                return "User cannot be null.";
            }
            if (string.IsNullOrEmpty(user.Username))
            {
                return "Username must not be empty.";
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                return "Password must not be empty.";
            }
            if (string.IsNullOrEmpty(user.Role))
            {
                return "Role must not be empty.";
            }

            return null;
        }

        public Role GetUserByLogin(string username, string password)
        {
            return userDAL.GetUserByLogin(username, password);
        }
    }
}
