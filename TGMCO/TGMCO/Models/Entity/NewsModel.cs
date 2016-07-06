using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TGMCO;

namespace TGMCO.Models.Entity
{
    public class NewsModel
    {
        public TGMCOEntitiesDB db = new TGMCOEntitiesDB();


        public string GetImage(int news_id)
        {
            try
            {
                string result = db.IMAGES_UPLOAD.Where(n => n.NEWS_ID == news_id).FirstOrDefault().URL;
                if (!string.IsNullOrEmpty(result))
                {
                    return result;
                }
                return "http://www.trustvets.com/images/NoImageAvailable.png";
            }
            catch
            {
                return "http://www.trustvets.com/images/NoImageAvailable.png";
            }
        }
    }
}