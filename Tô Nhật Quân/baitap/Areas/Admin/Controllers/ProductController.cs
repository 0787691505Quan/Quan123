using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using baitap.Context;
using PagedList;
using static baitap.Common;

namespace baitap.Areas.admin.Controllers
{
    public class ProductController : Controller
    {
        QuanlybanhangEntities objQuanlybanhangEntities = new QuanlybanhangEntities();
        // GET: admin/Product
    
       public ActionResult Index(string currentFilter, string SearchString, int? page)
        {

            var lstProduct = new List<Products>();
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
                lstProduct = objQuanlybanhangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
            }
            else
            {
                lstProduct = objQuanlybanhangEntities.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            lstProduct = lstProduct.OrderByDescending(n => n.Id).ToList();
            return View(lstProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();

        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(Products objProduct)
        {

            if (ModelState.IsValid)
            {
                this.LoadData();
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        //iphone.jpg
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        //jpg
                        fileName = fileName + extension;
                        //iphone.jpg
                        fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                        objProduct.Avartar = fileName;
                        //luu file hinh
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
                    }
                    objProduct.CreatedOnUtc = DateTime.UtcNow;

                    objQuanlybanhangEntities.Products.Add(objProduct);
                    objQuanlybanhangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)


                {

                    return RedirectToAction("Index");
                }

            }
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Details()
        {
            var objProduct = objQuanlybanhangEntities.Products.FirstOrDefault();
            return View(objProduct);

        }
        [HttpGet]
        public ActionResult Delete(int Id )
        {
            var objProduct = objQuanlybanhangEntities.Products.Where(n=> n.Id==Id).FirstOrDefault();
            return View(objProduct);

        }
        [HttpPost]
        public ActionResult Delete(Products objPro)
        {
            var objProducts=objQuanlybanhangEntities.Products.Where(n =>n.Id== objPro.Id).FirstOrDefault();
            objQuanlybanhangEntities.Products.Remove(objProducts);
            objQuanlybanhangEntities.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var objProduct = objQuanlybanhangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);

        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Products objProduct, FormCollection form)
        {

            if (objProduct.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
                objProduct.Avartar = fileName;
                objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/items"), fileName));
            }


            objQuanlybanhangEntities.Entry(objProduct).State = EntityState.Modified;
            objQuanlybanhangEntities.SaveChanges();
            return View(objProduct);


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

