using ModelEF.DAO;
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace TestUngDung.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index()
        {
            var category = new CategoryDao();
            var model = category.ListAll();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string searchStringCategory)
        {
            var category = new CategoryDao();
            var model = category.ListWhereAll(searchStringCategory);
            ViewBag.SearchCategory = searchStringCategory;
            return View(model);
        }

        //////////////
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        ///===================
        [HttpPost]
        public ActionResult Create(Category model)
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
                    var dao = new CategoryDao();
                    // kiểm tra id nếu trùng trả lại trang create
                    if (dao.FindId(model.ID) != null)
                    {
                        SetAlert("Đã tồn tại", "warning");
                        return RedirectToAction("Create", "Category");
                    }

                    result = dao.Insert(model);
                    //if (!string.IsNullOrEmpty(result))
                    if (result > 0)
                    {
                        SetAlert("Thành công", "success");
                        return RedirectToAction("Index", "Category");
                    }
                    else
                    {
                        SetAlert("Không thành công, đã có lỗi !", "error");
                        ModelState.AddModelError("", "Tạo người dùng không thành công");
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Common.WriteLog("Category", "Create-Post", ex.ToString());
            }
            return View();
        }

        ////////////
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        // lay tu db
        public ActionResult Edit()
        {
            String value = Request["idCate"];
            int x = int.Parse(value);
            ViewBag.NameEditProduct = value;
            var s = new CategoryDao().FindId(x);
            return View(s);
        }

        //////luu vao db
        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            if (ModelState.IsValid)
            {
                NguyenVuongContext db = new NguyenVuongContext();
                db.Entry(cate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cate);

        }
    }
}