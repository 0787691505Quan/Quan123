using baitap.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProducDetail
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: ProducDetail
        public ActionResult Detail(int Id)
        {
            var objProducts = objQuanlybanhangEntities.Products.Where(n=>n.Id==Id).FirstOrDefault();
            return View(objProducts);
        }
    }
}