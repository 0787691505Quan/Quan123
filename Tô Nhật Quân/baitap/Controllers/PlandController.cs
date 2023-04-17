using baitap.Context;
using baitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace baitap.Controllers
{
    public class PlandController : Controller


    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: Payment
        public ActionResult Index()
            {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCard = (List<CartModel>)Session["card"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.Status = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                objQuanlybanhangEntities.Order.Add(objOrder);
                objQuanlybanhangEntities.SaveChanges();
                int intOrderId = objOrder.id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCard)
                {
                    OrderDetail obj = new OrderDetail();
                    obj.OderId = intOrderId;
                    obj.Quantity = item.Quantity;
                    obj.UserId = item.Products.Id;
                    lstOrderDetail.Add(obj);
                }
                objQuanlybanhangEntities.OrderDetail.AddRange(lstOrderDetail);
                objQuanlybanhangEntities.SaveChanges();
            }

            return View();

        }
    }
}