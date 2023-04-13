using baitap.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap.Controllers
{
    public class CategoryController : Controller
    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var lstCategory = objQuanlybanhangEntities.Category.ToList();
            return View(lstCategory);
        }
        public ActionResult ProductCategory(int Id )
        {
            var lstProduct=objQuanlybanhangEntities.Products.Where(n=>n.CategoryId==Id).ToList();
            return View(lstProduct);
        }
    }
}