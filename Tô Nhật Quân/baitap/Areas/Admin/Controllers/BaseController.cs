using baitap.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace baitap.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: Admin/Base
        public ActionResult Index()
        {
            var lstBase=objQuanlybanhangEntities.Products.ToList();
            return View(lstBase);
        }
    }
}