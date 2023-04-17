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
using System.Web.UI;
using static baitap.Common;

namespace baitap.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currentFilter, string SearchString, int? page)
        {
            var lstBrand = new List<Brand>();
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
                lstBrand = objQuanlybanhangEntities.Brand.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstBrand = objQuanlybanhangEntities.Brand.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstBrand = lstBrand.OrderByDescending(n => n.id).ToList();
            return View(lstBrand.ToPagedList(pageNumber, pageSize));

        }

        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Brand objBrand)
        {

            if (ModelState.IsValid)
            {
                this.LoadData();
                try
                {
                    if (objBrand.ImageUpLoad != null)
                    {
                        //iphone.jpg
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                        //jpg
                        fileName = fileName + extension;
                        //iphone.jpg
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objBrand.Avatar = fileName;
                        //luu file hinh
                        objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objBrand.CreatedOnUtc = DateTime.UtcNow;

                    objQuanlybanhangEntities.Brand.Add(objBrand);
                    objQuanlybanhangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)


                {

                    return RedirectToAction("Index");
                }

            }
            return View(objBrand);
        }

        [HttpGet]
        public ActionResult Details()
        {
            var objBrand = objQuanlybanhangEntities.Brand.FirstOrDefault();
            return View(objBrand);

        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objBrand = objQuanlybanhangEntities.Brand.Where(n => n.id == Id).FirstOrDefault();
            return View(objBrand);

        }
        [HttpPost]
        public ActionResult Delete(Products objBr)
        {
            var objBrand = objQuanlybanhangEntities.Brand.Where(n => n.id == objBr.Id).FirstOrDefault();
            objQuanlybanhangEntities.Brand.Remove(objBrand);
            objQuanlybanhangEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objBrand = objQuanlybanhangEntities.Brand.Where(n => n.id == Id).FirstOrDefault();
            return View(objBrand);

        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Brand objBrand, FormCollection form)
        {

            if (objBrand.ImageUpLoad != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpLoad.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpLoad.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpLoad.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }


            objQuanlybanhangEntities.Entry(objBrand).State = EntityState.Modified;
            objQuanlybanhangEntities.SaveChanges();
            return View(objBrand);


            return RedirectToAction("Index");
        }

        void LoadData()
        {
            Common objCommon = new Common();
            // lấy dữ liệu dưới db
            var lstCat = objQuanlybanhangEntities.Category.ToList();
            //convert  sang select list dạng value,text
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(lstCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");
            // lấy dữ diệu thương hiệu dưới db
            var lstBrand = objQuanlybanhangEntities.Brand.ToList();
            DataTable dtBrand = converter.ToDataTable(lstBrand);
            //convert sang select list dang value, text
            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");
            //typeoid
            List<ProductType> lstProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            lstProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            lstProductType.Add(objProductType);



            DataTable dtProductType = converter.ToDataTable(lstProductType);
            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }

    }
}

        