using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class CategoryExtraModel
    {
        public int CATEGORY_EXTRA_ID { get; set; }
        public string CATEGORY_EXTRA_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int CATEGORY_ID { get; set; }
        public bool IS_ACTIVE { get; set; }
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public string GetCategoryExtraName(int id)
        {
            try
            {
                return db.CATEGORIES_EXTRA.Find(id).CATEGORY_EXTRA_NAME;
            }
            catch
            {
                return null;
            }
        }


        public object GetCategoryExtraName(int? id)
        {
            try
            {
                return db.CATEGORIES_EXTRA.Find(id).CATEGORY_EXTRA_NAME;
            }
            catch
            {
                return null;
            }
        }
    }
}