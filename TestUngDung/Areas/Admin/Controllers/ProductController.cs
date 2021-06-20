using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.DAO;
using ModelEF.Model;
using TestUngDung.Areas.Admin.Model;
using TestUngDung.Areas.Admin.Controllers;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var product = new ProductDao();
            var model = product.ListAll();
            return View(model.ToPagedList(page, pagesize));
        }

        [HttpPost]
        public ActionResult Index(string searchProduct, int page = 1, int pagesize = 5)
        {
            var product = new ProductDao();
            var model = product.ListWhereAll(searchProduct, page, pagesize);
            ViewBag.SearchProduct = searchProduct;
            return View(model);
        }

        //////////////////////==
        [HttpGet]
        public ActionResult Create()
        {
            //var cate = new CategoryDao();
            //ViewBag.Category_Id = new SelectList(cate.ListAll(),"ID","Name");
            return View();
        }
        ///====================

        ///===================
        [HttpPost]
        public ActionResult Create(Product model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (String.IsNullOrEmpty(model.ID.ToString()))
                    {
                        SetAlert("Không được để trống", "warning");
                        return View();
                    }

                    //string result = "";
                    int result = 0;
                    var dao = new ProductDao();

                    if (dao.FindId(model.ID) != null)
                    {
                        SetAlert("Đã tồn tại", "warning");
                        return RedirectToAction("Create", "Product");
                    }

                    result = dao.Insert(model);
                    //if (!string.IsNullOrEmpty(result))
                    if (result > 0)
                    {
                        SetAlert("Thành công", "success");
                        return RedirectToAction("Index", "Product");
                    }
                    else
                    {
                        SetAlert("Không thành công, đã có lỗi !", "error");
                        ModelState.AddModelError("", "Tạo sản phẩm không thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("Product", "Create-Post", ex.ToString());
            }
            return View();
        }

        ////////////////
        ///tiìm


        ////////////
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new ProductDao().Delete(id);
            return RedirectToAction("Index");
        }

        //////////////
        [HttpGet]

        public ActionResult Edit()
        {
            String value = Request["idProduct"];
            int x = int.Parse(value);
            ViewBag.NameEditProduct = value;
            var s = new ProductDao().FindId(x);
            return View(s);
        }

        //////
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                NguyenVuongContext db = new NguyenVuongContext();
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);

        }

        //================
        [HttpGet]
        public ActionResult Details()
        {
            String value = Request["idProduct"];
            int x = int.Parse(value);
            ViewBag.NameDetailProduct = value;
            var dao = new ProductDao();
            var model = dao.ShowDetail(x);

            return View(model);
        }
    }
}