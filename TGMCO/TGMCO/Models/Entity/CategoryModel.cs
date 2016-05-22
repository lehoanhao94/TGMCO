using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class CategoryModel
    {
        public int CATEGORY_ID { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string IMAGE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public string GetCategoryName(int id)
        {
            try
            {
                return db.CATEGORIES.Find(id).CATEGORY_NAME;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExistCategoryInSupplier(int id_sup, int id_cate)
        {
            try
            {
                List<CATEGORIES_BY_SUPPLIER> lstCategoryBySupplier = db.CATEGORIES_BY_SUPPLIER.Where(n => n.SUPPLIER_ID == id_sup).ToList();
                foreach(var category in lstCategoryBySupplier)
                {
                    if(category.CATEGORY_ID == id_cate)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<CATEGORIES_EXTRA> GetListCategoryExtra(int id)
        {
            try
            {
                return db.CATEGORIES_EXTRA.Where(n => n.CATEGORY_ID == id).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}