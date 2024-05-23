using supermarket_manager.Models.DataAccessLayer;
using supermarket_manager.Models.EntityLayer;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace supermarket_manager.Models.BusinessLogicLayer
{
    class SupplierBLL
    {
        SupplierDAL supplierDAL = new SupplierDAL();
        public ObservableCollection<Supplier> SupplierList { get; set; }

        public SupplierBLL()
        {
            SupplierList = new ObservableCollection<Supplier>(supplierDAL.GetAllSuppliers());
        }

        public void AddSupplier(Supplier supplier)
        {
            string validationError = ValidateSupplier(supplier);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                supplierDAL.AddSupplier(supplier);
                SupplierList.Add(supplier);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteSupplier(Supplier supplier)
        {
            if (supplier == null)
            {
                MessageBox.Show("Must select a supplier to delete.");
                return;
            }

            try
            {
                supplierDAL.DeleteSupplier(supplier);
                SupplierList.Remove(supplier);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ModifySupplier(Supplier supplier)
        {
            string validationError = ValidateSupplier(supplier);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError);
                return;
            }

            try
            {
                supplierDAL.ModifySupplier(supplier);
                var index = SupplierList.IndexOf(supplier);
                SupplierList.RemoveAt(index);
                SupplierList.Insert(index, supplier);
                MessageBox.Show("Supplier information updated successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public ObservableCollection<Supplier> GetAllSuppliers()
        {
            return new ObservableCollection<Supplier>(supplierDAL.GetAllSuppliers());
        }

        private string ValidateSupplier(Supplier supplier)
        {
            if (supplier == null)
            {
                return "Supplier cannot be null.";
            }
            if (string.IsNullOrEmpty(supplier.Name))
            {
                return "Supplier name must not be empty.";
            }
            if (string.IsNullOrEmpty(supplier.Country))
            {
                return "Country must not be empty.";
            }

            return null;
        }
    }
}
