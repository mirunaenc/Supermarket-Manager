
using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class ProductBLL
    {
        ProductDAL productDAL = new ProductDAL();
        public ObservableCollection<Product> ProductList { get; set; }

        public ProductBLL()
        {
            ProductList = new ObservableCollection<Product>(productDAL.GetAllProducts());
        }

        public void AddProduct(Product product)
        {
            string validationError = ValidateProduct(product);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                productDAL.AddProduct(product);
                ProductList.Add(product);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteProduct(Product product)
        {
            if (product == null)
            {
                MessageBox.Show("Must select a product to delete.");
                return;
            }

            try
            {
                productDAL.DeleteProduct(product);
                ProductList.Remove(product);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ModifyProduct(Product product)
        {
            string validationError = ValidateProduct(product);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                productDAL.ModifyProduct(product);
                MessageBox.Show("Product information updated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<Product> GetProductsBySupplier(string supplierName)
        {
            int supplierId = productDAL.GetSupplierIDByName(supplierName);
            return productDAL.GetProductsBySupplier(supplierId);
        }

        public ObservableCollection<Product> GetAllProducts()
        {
            return new ObservableCollection<Product>(productDAL.GetAllProducts());
        }

        public ObservableCollection<Product> SearchProducts(string name, string barcode, DateTime? expiryDate, int? supplierId, int? categoryId)
        {
            return productDAL.SearchProducts(name, barcode, expiryDate, supplierId, categoryId);
        }

        public ObservableCollection<Product> GetProductsBySupplierAndCategory(int supplierId)
        {
            return productDAL.GetProductsBySupplier(supplierId);
        }


        private string ValidateProduct(Product product)
        {
            if (product == null)
            {
                return "Product cannot be null.";
            }
            if (string.IsNullOrEmpty(product.Name))
            {
                return "Product name must not be empty.";
            }
            if (string.IsNullOrEmpty(product.Barcode))
            {
                return "Barcode must not be empty.";
            }
            if (product.CategoryId <= 0)
            {
                return "Category ID must be greater than zero.";
            }
            if (product.SupplierId <= 0)
            {
                return "Supplier ID must be greater than zero.";
            }

            return null;
        }
    }
}




