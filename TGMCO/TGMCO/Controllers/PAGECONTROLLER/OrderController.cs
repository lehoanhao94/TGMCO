using RazorEngine;
using RazorEngine.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;
using TGMCO.Models;
using TGMCO.Models.Entity;
using RazorEngine.Templating;

namespace TGMCO.Controllers.PAGECONTROLLER
{
    public class OrderController : Controller
    {
        TGMCOEntitiesDB db = new TGMCOEntitiesDB();
        StringRandom m_STRING_RAMDOM = new StringRandom(10);

        public ActionResult Order()
        {
            try
            {
                if (string.IsNullOrEmpty(Session["SUPPLIER"].ToString()))
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }
                List<ProductCart> _lstProductCart = (List<ProductCart>)Session["ShoppingCart"];
                if (Session["ShoppingCart"] == null || _lstProductCart.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                int id = 0;
                ORDER _ORDER = new ORDER();
                if(Session["Order_Id"] != null)
                {
                    id = (int)Session["Order_Id"];
                    _ORDER = db.ORDERS.Find(id);                   
                }
               
                ViewBag.ListShipper = new SelectList(db.SHIPPERS.OrderByDescending(n => n.SHIPPER_ID).ToList(), "Shipper_ID", "SHIPPER_NAME");
                return View(_ORDER);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }         
        }

        [HttpPost]
        public ActionResult Create(FormCollection f)
        {
            try
            {
                string _Name = f.Get("Name").ToString();
                string _Address = f.Get("Address").ToString();
                string _Note = f.Get("Note").ToString();
                string _Email = f.Get("Email").ToString();
                string _Mobile = f.Get("Mobile").ToString();
                int _ShipVia = int.Parse(f.Get("ListShipper").ToString());

                if(Session["Order_Id"] == null)
                {
                    ORDER _ORDER = new ORDER();
                    ORDER_DETAILS _ORDER_DETAILS = null;

                    USER _USER = (USER)Session["SS_USER"];
                    if (_USER != null)
                        _ORDER.USER_ID = _USER.USER_ID;
                    else
                        _ORDER.USER_ID = 0;
                    _ORDER.ORDER_CODE = m_STRING_RAMDOM.RandomString();
                    _ORDER.ORDER_DATE = DateTime.Now;
                    _ORDER.SHIP_NAME = _Name;
                    _ORDER.SHIP_ADDRESS = _Address;
                    _ORDER.SHIP_PHONE = _Mobile;
                    _ORDER.SHIP_EMAIL = _Email;
                    _ORDER.NOTE = _Note;
                    _ORDER.ORDER_STATUS_ID = 3;
                    _ORDER.SHIP_VIA_ID = _ShipVia;
                    _ORDER.FREIGHT = (decimal)db.SHIPPERS.Single(n => n.SHIPPER_ID == _ORDER.SHIP_VIA_ID).FREIGHT;
                    _ORDER.SUBTOTAL = decimal.Parse(Session["TOTAL_PRICE"].ToString());
                    db.ORDERS.Add(_ORDER);
                    db.SaveChanges();
                    List<ProductCart> _lstProductCart = (List<ProductCart>)Session["ShoppingCart"];
                    foreach (var product in _lstProductCart)
                    {
                        _ORDER_DETAILS = new ORDER_DETAILS();
                        _ORDER_DETAILS.ORDER_ID = _ORDER.ORDER_ID;
                        _ORDER_DETAILS.PRODUCT_ID = product.PRODUCT_ID;
                        _ORDER_DETAILS.UNIT_PRICE = product.UNIT_PRICE;
                        _ORDER_DETAILS.UNIT = product.UNIT;
                        _ORDER_DETAILS.QUANTITY = product.QUANTITY;
                        _ORDER_DETAILS.EXTENDED_PRICE = product.TOTAL_PRICE;
                        db.ORDER_DETAILS.Add(_ORDER_DETAILS);
                    }
                    db.SaveChanges();
                    Session["Order_Id"] = _ORDER.ORDER_ID;
                }
                else
                {
                    int _Order_Id = (int)Session["Order_Id"];
                    ORDER _ORDER = db.ORDERS.Find(_Order_Id);

                    USER _USER = (USER)Session["SS_USER"];
                    if (_USER != null)
                        _ORDER.USER_ID = _USER.USER_ID;
                    else
                        _ORDER.USER_ID = 0;
                    _ORDER.ORDER_CODE = m_STRING_RAMDOM.RandomString();

                    _ORDER.ORDER_DATE = DateTime.Now;
                    _ORDER.SHIP_NAME = _Name;
                    _ORDER.SHIP_ADDRESS = _Address;
                    _ORDER.SHIP_PHONE = _Mobile;
                    _ORDER.SHIP_EMAIL = _Email;
                    _ORDER.NOTE = _Note;
                    _ORDER.ORDER_STATUS_ID = 3;
                    _ORDER.SHIP_VIA_ID = _ShipVia;
                    _ORDER.FREIGHT = (decimal)db.SHIPPERS.Single(n => n.SHIPPER_ID == _ORDER.SHIP_VIA_ID).FREIGHT;
                    _ORDER.SUBTOTAL = decimal.Parse(Session["TOTAL_PRICE"].ToString());
                    db.SaveChanges();                   
                    Session["Order_Id"] = _ORDER.ORDER_ID;
                }

                return RedirectToAction("Payment", "Payment");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public async Task<ActionResult> PrintBill(int id)
        {
            try
            {
                if (Session["SUPPLIER"] == null)
                {
                    Session["SUPPLIER"] = "DEFAULT";
                    Session["SUPPLIER_MODEL"] = db.SUPPLIERS.Find(20);
                }
                List<ProductCart> _lstProductCart = (List<ProductCart>)Session["ShoppingCart"];
                if (Session["ShoppingCart"] == null || _lstProductCart.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }

                Session["ShoppingCart"] = null;
                Session["Order_Id"] = null;

                ORDER _ORDER = db.ORDERS.Find(id);                
                List<ORDER_DETAILS> _lstORDER_DETAILS = db.ORDER_DETAILS.Where(n => n.ORDER_ID == id).ToList();
                ViewBag.ORDER_DETAIL = _lstORDER_DETAILS;

                OrderMailModel _OrderMail = new OrderMailModel(_ORDER, _lstORDER_DETAILS);

                var mailMessage = new MailMessage();

                var _tempOrder = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/EmailTemplates/Order.cshtml"));
                var _tempService = new TemplateService();
                
                mailMessage.To.Add(_ORDER.SHIP_EMAIL);
                mailMessage.Subject = "Thân chào " + _ORDER.SHIP_NAME + ", đơn hàng [#VNT-" + _ORDER.ORDER_ID + "] đã được đặt thành công !";
                mailMessage.Body = _tempService.Parse(_tempOrder, _OrderMail, null, null);
                mailMessage.IsBodyHtml = true;

                await SendMail(mailMessage);
                
                return View(_ORDER);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        public async Task SendMail(MailMessage _mailMessage)
        {
            try
            {
                var smtpClient = new SmtpClient { EnableSsl = false };
                await smtpClient.SendMailAsync(_mailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        public ActionResult CheckBill(string Order_Code)
        {
            try
            {
                Session["SUPPLIER"] = "DEFAULT";
                ViewBag.SearchResult = -1;
                if (Order_Code != null)
                {
                    ORDER _ORDER = db.ORDERS.SingleOrDefault(n => n.ORDER_CODE.Equals(Order_Code));
                    if(_ORDER != null)
                    {
                        ViewBag.ORDER_DETAILS = db.ORDER_DETAILS.Where(n => n.ORDER_ID == _ORDER.ORDER_ID).ToList();
                        if (db.ORDER_DETAILS.Where(n => n.ORDER_ID == _ORDER.ORDER_ID).ToList().Count > 0 && _ORDER.ORDER_STATUS_ID != 4)
                        {
                            ViewBag.SearchResult = 1;
                            return View(_ORDER);
                        }
                        else
                        {
                            ViewBag.SearchResult = 2;
                            return View(_ORDER);
                        }
                    }
                    
                    ViewBag.SearchResult = 0;                 
                }
                
                return View(new ORDER());
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpDelete]
        public int DeleteProductFromOrder(int order_id, int product_id)
        {
            try
            {
                ORDER_DETAILS ORDER_DETAILS = db.ORDER_DETAILS.Where(n => n.ORDER_ID == order_id && n.PRODUCT_ID == product_id).SingleOrDefault();
                ORDER ORDER = db.ORDERS.Find(order_id);                
                ORDER.SUBTOTAL = ORDER.SUBTOTAL - ORDER_DETAILS.EXTENDED_PRICE;
                db.ORDER_DETAILS.Remove(ORDER_DETAILS);
                db.SaveChanges();
                int numProducts = db.ORDER_DETAILS.Where(n => n.ORDER_ID == order_id).ToList().Count;
                if (numProducts == 0)
                    ORDER.ORDER_STATUS_ID = 4;
                db.SaveChanges();
                return numProducts;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        [HttpPost]
        public decimal UpdateQuantity(int id, int id_product, int quantity)
        {
            try
            {
                ORDER_DETAILS _ORDER_DETAIL = db.ORDER_DETAILS.Where(n => n.ORDER_ID == id && n.PRODUCT_ID == id_product).SingleOrDefault();
                ORDER _ORDER = db.ORDERS.Find(id);
                _ORDER_DETAIL.QUANTITY = quantity;
                decimal _Total_Before = _ORDER_DETAIL.EXTENDED_PRICE;
                _ORDER_DETAIL.EXTENDED_PRICE = _ORDER_DETAIL.UNIT_PRICE * quantity;
                _ORDER.SUBTOTAL += (_ORDER_DETAIL.EXTENDED_PRICE - _Total_Before);
                db.SaveChanges();

                return _ORDER.SUBTOTAL;
            }
            catch (Exception ex)
            {
                return 0; // 404
            }
        }
        [HttpDelete]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                ORDER _ORDER = db.ORDERS.Find(id);
                _ORDER.ORDER_STATUS_ID = 4;
                _ORDER.NOTE += "-Người dùng hủy đơn hàng-";
                db.SaveChanges();
                return RedirectToAction("CheckBill", "ShoppingCart", new { Order_Code = _ORDER.ORDER_CODE });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        [HttpPost]
        public ActionResult CheckBill(int id, FormCollection f)
        {
            try
            {
                ORDER _ORDER = db.ORDERS.Find(id);
                _ORDER.SHIP_NAME = f.Get("txtName").ToString();
                _ORDER.SHIP_PHONE = f.Get("txtMobile").ToString();
                _ORDER.SHIP_EMAIL = f.Get("txtEmail").ToString();
                _ORDER.SHIP_ADDRESS = f.Get("txtAddress").ToString();
                _ORDER.NOTE = f.Get("txtNote").ToString();
                db.SaveChanges();
                return RedirectToAction("CheckBill", "Order", new { Order_Code = _ORDER .ORDER_CODE});
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        [HttpPost]
        public void UpdateOrder(int id, FormCollection f)
        {
            try
            {
                ORDER _ORDER = db.ORDERS.Find(id);
                _ORDER.SHIP_NAME = f.Get("txtName").ToString();
                _ORDER.SHIP_PHONE = f.Get("txtMobile").ToString();
                _ORDER.SHIP_EMAIL = f.Get("txtEmail").ToString();
                _ORDER.SHIP_ADDRESS = f.Get("txtAddress").ToString();
                _ORDER.NOTE = f.Get("txtNote").ToString();
                db.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }
        [HttpPost]
        public void UpdateStatusOrder(int id, int status_id)
        {
            try
            {
                var ORDER = db.ORDERS.Find(id);
                ORDER.ORDER_STATUS_ID = status_id;
                switch(status_id)
                {
                    case 2:
                        TempData["Success"] = "Đã chuyển trạng thái đơn hàng #VNT-" + ORDER.ORDER_ID + " thành 'Đã thanh toán - Chưa giao hàng'.";
                        break;
                    case 3:
                        TempData["Success"] = "Đã chuyển trạng thái đơn hàng #VNT-" + ORDER.ORDER_ID + " thành 'Chưa thanh toán'.";
                        break;
                    case 4:
                        TempData["Success"] = "Đã hủy đơn hàng #VNT-" + ORDER.ORDER_ID;
                        break;
                }              
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Deliver(FormCollection f)
        {
            try
            {
                int _ORDER_ID = int.Parse(f.Get("txtOrderId"));
                string _SHIP_CODE = f.Get("txtShipCode");
                DateTime _SHIP_DATE = DateTime.Parse(f.Get("dtpShipDate"));
                decimal _FREIGHT = decimal.Parse(f.Get("txtFreight"));
                string _NOTE_EMPLOYEE = f.Get("txtNote");
                USER _ADMIN = (USER)(Session["SS_USER_ADMIN"]);
                
                var ORDER = db.ORDERS.Find(_ORDER_ID);

                //Cập nhật điểm người dùng khi mua hàng
                if(ORDER.USER_ID != 0)
                {
                    USER_PROFILES _USER = db.USER_PROFILES.Single(n => n.USER_ID == ORDER.USER_ID);
                    _USER.POINTS += (int)(ORDER.SUBTOTAL/1000000);
                }
               
                //Cập nhật số hàng bán ra
                List<ORDER_DETAILS> _lstORDER_DETAILS = db.ORDER_DETAILS.Where(n => n.ORDER_ID == ORDER.ORDER_ID).ToList();
                foreach(var _order_details in _lstORDER_DETAILS)
                {
                    PRODUCT _PRODUCT = db.PRODUCTS.Find(_order_details.PRODUCT_ID);
                    _PRODUCT.QUANTITY_SOLD += _order_details.QUANTITY;
                    db.SaveChanges();
                }

                ORDER.ORDER_STATUS_ID = 1;
                ORDER.EMPLOYEE_ID = _ADMIN.USER_ID;
                ORDER.SHIP_CODE = _SHIP_CODE;
                ORDER.SHIPPED_DATE = _SHIP_DATE;
                ORDER.FREIGHT = _FREIGHT;
                ORDER.NOTE_EMPLOYEE = _NOTE_EMPLOYEE;



                TempData["Success"] = "Đơn hàng #VNT-" + ORDER.ORDER_ID + " đã được giao !";
                db.SaveChanges();

                return RedirectToAction("ManagingOrders", "Admin", new { order_status_id = 1, Time = 1});
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
