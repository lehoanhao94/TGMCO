using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models
{
    public class NumberObject
    {
        private TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public int GetNumOfSuppliers()
        {
            try
            {
                List<SUPPLIER> _lstSupplier = db.SUPPLIERS.ToList();
                return _lstSupplier.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNumOfCategories()
        {
            try
            {
                List<CATEGORy> _lstCategory = db.CATEGORIES.ToList();
                return _lstCategory.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNumOfCategoriesExtra()
        {
            try
            {
                List<CATEGORIES_EXTRA> _lstCategoryExtra = db.CATEGORIES_EXTRA.ToList();
                return _lstCategoryExtra.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNumOfCategoriesBySupplier(int Supplier_Id)
        {
            try
            {
                List<CATEGORIES_BY_SUPPLIER> _lstCategory = db.CATEGORIES_BY_SUPPLIER.Where(n => n.SUPPLIER_ID == Supplier_Id).ToList();
                return _lstCategory.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNumOfProducts()
        {
            try
            {
                List<PRODUCT> _lstProduct = db.PRODUCTS.ToList();
                return _lstProduct.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetNumOfUsers()
        {
            try
            {
                List<USER> _lstUser = db.USERS.ToList();
                return _lstUser.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}