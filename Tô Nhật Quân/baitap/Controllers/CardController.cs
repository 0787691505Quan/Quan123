using baitap.Context;
using baitap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap.Controllers
{

    public class CardController : Controller
    {
        QuanlybanhangEntities  objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: Card

        public ActionResult Index()
        {
            return View((List<CartModel>)Session["card"]);

        }

        public ActionResult AddToCard(int id, int Quantity)
        {
            if (Session["card"] == null)
            {
                List<CartModel> card = new List<CartModel>();
                card.Add(new CartModel { Products = objQuanlybanhangEntities.Products.Find(id), Quantity = Quantity });
                Session["card"] = card;
                Session["count"] = 1;
            }
            else
            {
                List<CartModel> card = (List<CartModel>)Session["card"];
                //kiểm tra sản phẩm có tồn tại trong giỏ hàng chưa???
                int index = isExist(id);
                if (index != -1)
                {
                    //nếu sp tồn tại trong giỏ hàng thì cộng thêm số lượng
                    card[index].Quantity += Quantity;
                }
                else
                {
                    //nếu không tồn tại thì thêm sản phẩm vào giỏ hàng
                    card.Add(new CartModel { Products = objQuanlybanhangEntities.Products.Find(id), Quantity = Quantity });
                    //Tính lại số sản phẩm trong giỏ hàng
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                Session["card"] = card;
            }
            return Json(new { Message = "Thêm thành công", JsonRequestBehavior.AllowGet });
        }
        private int isExist(int id)
        {
            List<CartModel> card = (List<CartModel>)Session["card"];
            for (int i = 0; i < card.Count; i++)
                if (card[i].Products.Id.Equals(id))
                    return i;
            return -1;
        }
        //xóa sản phẩm khỏi giỏ hàng theo id
        public ActionResult Remove(int Id)
        {
            List<CartModel> li = (List<CartModel>)Session["card"];
            li.RemoveAll(x => x.Products.Id == Id);
            Session["card"] = li;
            Session["count"] = Convert.ToInt32(Session["count"]) - 1;
            return Json(new { Message = "Thành công", JsonRequestBehavior.AllowGet });
        }
    }

}
