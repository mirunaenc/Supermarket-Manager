using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class CategoryBLL
    {
        CategoryDAL categoryDAL = new CategoryDAL();
        public ObservableCollection<Category> CategoryList { get; set; }
        public event Action<string, bool> MessageChanged; 

        public CategoryBLL()
        {
            CategoryList = new ObservableCollection<Category>(categoryDAL.GetAllCategories());
        }

        public void AddCategory(Category category)
        {
            string validationError = ValidateCategory(category);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageChanged?.Invoke(validationError, false);
                return;
            }

            try
            {
                categoryDAL.AddCategory(category);
                CategoryList.Add(category);
                MessageChanged?.Invoke("Category added successfully!", true);
            }
            catch (SqlException ex)
            {
                MessageChanged?.Invoke(ex.Message, false);
            }
        }

        public void DeleteCategory(Category category)
        {
            if (category == null)
            {
                MessageChanged?.Invoke("Must select a category to delete.", false);
                return;
            }

            try
            {
                categoryDAL.DeleteCategory(category);
                CategoryList.Remove(category);
                MessageChanged?.Invoke("Category deleted successfully!", true);
            }
            catch (SqlException ex)
            {
                MessageChanged?.Invoke(ex.Message, false);
            }
        }

        public void ModifyCategory(Category category)
        {
            string validationError = ValidateCategory(category);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageChanged?.Invoke(validationError, false);
                return;
            }

            try
            {
                categoryDAL.ModifyCategory(category);
                MessageChanged?.Invoke("Category information updated successfully!", true);
            }
            catch (SqlException ex)
            {
                MessageChanged?.Invoke(ex.Message, false);
            }
        }

        public ObservableCollection<Category> GetAllCategories()
        {
            return new ObservableCollection<Category>(categoryDAL.GetAllCategories());
        }

        private string ValidateCategory(Category category)
        {
            if (category == null)
            {
                return "Category cannot be null.";
            }
            if (string.IsNullOrEmpty(category.Name))
            {
                return "Category name must not be empty.";
            }

            return null;
        }
    }
}
