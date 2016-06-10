using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TGMCO.Models;
using TGMCO.Models.Entity;


namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class ShoppingCartController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();

        public ActionResult ShoppingCart()
        {
            try
            {
                if (Session["SUPPLIER"] == null)
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }
                List<ProductCart> _lstProductCart = GetShoppingCart();
                ViewBag.TotalPrice = GetTotalPrice();
                Session["TOTAL_PRICE"] = GetTotalPrice();
                Session["Order_Id"] = null;
                return View(_lstProductCart);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }


        public decimal GetTotalPrice()
        {
            try
            {
                decimal _TotalPrice = 0;
                List<ProductCart> _lstProductCart = Session["ShoppingCart"] as List<ProductCart>;

                if (_lstProductCart != null)
                {
                    _TotalPrice = _lstProductCart.Sum(n => n.TOTAL_PRICE);
                }
                return _TotalPrice;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ProductCart> GetShoppingCart()
        {
            try
            {
                List<ProductCart> _lstProductCart = Session["ShoppingCart"] as List<ProductCart>;

                if(_lstProductCart == null)
                {
                    _lstProductCart = new List<ProductCart>();
                    Session["ShoppingCart"] = _lstProductCart;
                }

                return _lstProductCart;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddProductToShoppingCart(int id, int quantity)
        {
            try
            {
                List<ProductCart> _lstProductCart = GetShoppingCart();

                ProductCart _ProductCart = _lstProductCart.Find(n => n.PRODUCT_ID == id);
                if (_ProductCart == null)
                {
                    _ProductCart = new ProductCart(id, quantity);
                    _lstProductCart.Add(_ProductCart);
                }
                else
                {
                    _ProductCart.QUANTITY+= quantity;
                }

                return GetTotalQuantity();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpDelete]
        public ActionResult DeleteProductFromShoppingCart(int id)
        {
            try
            {
                List<ProductCart> _lstProductCart = GetShoppingCart();
                ProductCart _ProductCart = _lstProductCart.Find(n => n.PRODUCT_ID == id);
                _lstProductCart.Remove(_ProductCart);

                return RedirectToAction("ShoppingCart", "ShoppingCart");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        [HttpPost]
        public ActionResult UpdateProductFromShoppingCart(int id, int quantity)
        {
            try
            {
                List<ProductCart> _lstProductCart = GetShoppingCart();
                _lstProductCart.Find(n => n.PRODUCT_ID == id).QUANTITY = quantity;
                
                return RedirectToAction("ShoppingCart", "ShoppingCart");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public int GetTotalQuantity()
        {
            try
            {
                int _TotalQuantity = 0;
                List<ProductCart> _lstProductCart = Session["ShoppingCart"] as List<ProductCart>;

                if(_lstProductCart != null)
                {
                    _TotalQuantity = _lstProductCart.Sum(n => n.QUANTITY);
                }

                return _TotalQuantity;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
