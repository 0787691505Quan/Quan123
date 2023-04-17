using baitap.Context;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static baitap.Common;

namespace baitap.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstCategory = new List<Category>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                lstCategory = objQuanlybanhangEntities.Category.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstCategory = objQuanlybanhangEntities.Category.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstCategory = lstCategory.OrderByDescending(n => n.id).ToList();
            return View(lstCategory.ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {


            return View();


        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Category objCategory)
        {


            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpLoad != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpLoad.FileName);
                        fileName = fileName + extension;
                        objCategory.Avartar = fileName;
                        objCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items/"), fileName));
                    }
                    objCategory.CreatedOnUtc = DateTime.Now;
                    objQuanlybanhangEntities.Category.Add(objCategory);
                    objQuanlybanhangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }

                catch (Exception)

                {
                    return RedirectToAction("Index");
                }
            }

            return View(objCategory);
        }

    


[HttpGet]
        public ActionResult Details()
        {
            var lstCategory = objQuanlybanhangEntities.Category.FirstOrDefault();
            return View(lstCategory);

        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var lstCategory = objQuanlybanhangEntities.Category.Where(n => n.id == Id).FirstOrDefault();
            return View(lstCategory);

        }
        [HttpPost]
        public ActionResult Delete(Category objCate)
        {
            var lstCategory = objQuanlybanhangEntities.Category.Where(n => n.id == objCate.id).FirstOrDefault();
            objQuanlybanhangEntities.Category.Remove(lstCategory);
            objQuanlybanhangEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var lstCategory = objQuanlybanhangEntities.Category.Where(n => n.id == Id).FirstOrDefault();
            return View(lstCategory);

        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category lstCategory, FormCollection form)
        {

            if (lstCategory.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(lstCategory.ImageUpLoad.FileName);
                string extension = Path.GetExtension(lstCategory.ImageUpLoad.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                lstCategory.Avartar = fileName;
                lstCategory.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }


            objQuanlybanhangEntities.Entry(lstCategory).State = EntityState.Modified;
            objQuanlybanhangEntities.SaveChanges();
            return View(lstCategory);


            return RedirectToAction("Index");
        }

    }
}

        