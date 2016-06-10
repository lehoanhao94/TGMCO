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

        public ActionResult CheckBill()
        {
            try
            {               
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpGet]
        public ActionResult CheckBill(string Order_Code)
        {
            try
            {
                ViewBag.SearchResult = -1;
                if (Order_Code != null)
                {
                    ORDER _ORDER = db.ORDERS.SingleOrDefault(n => n.ORDER_CODE.Equals(Order_Code));
                    if(_ORDER != null)
                    {
                        ViewBag.ORDER_DETAILS = db.ORDER_DETAILS.Where(n => n.ORDER_ID == _ORDER.ORDER_ID).ToList();
                        if (db.ORDER_DETAILS.Where(n => n.ORDER_ID == _ORDER.ORDER_ID).ToList().Count > 0)
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
                
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }

        [HttpDelete]
        public ActionResult DeleteProductFromOrder(int order_id, int product_id)
        {
            try
            {
                ORDER_DETAILS ORDER_DETAILS = db.ORDER_DETAILS.Where(n => n.ORDER_ID == order_id && n.PRODUCT_ID == product_id).SingleOrDefault();
                ORDER ORDER = db.ORDERS.Find(order_id);
                ORDER.SUBTOTAL = ORDER.SUBTOTAL - ORDER_DETAILS.EXTENDED_PRICE;
                db.ORDER_DETAILS.Remove(ORDER_DETAILS);
                db.SaveChanges();

                return RedirectToAction("CheckBill", "ShoppingCart", new { Order_Code = ORDER.ORDER_CODE });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
        [HttpPost]
        public ActionResult UpdateProductFromOrder(int id, int quantity)
        {
            try
            {

                return RedirectToAction("ShoppingCart", "ShoppingCart");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Http404", "Error"); // 404
            }
        }
    }
}
