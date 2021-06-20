using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelEF.DAO;
using ModelEF.Model;
using TestUngDung.Common;
using PagedList;
using System.Data.Entity;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pagesize = 5)
        {
            var user = new UserDao();
            var model = user.ListAll();
            return View(model.ToPagedList(page, pagesize));
        }
        /////////////////////////////
        // tìm
        [HttpPost]
        public ActionResult Index(string searchString, int page = 1, int pagesize = 5)
        {
            var user = new UserDao();
            var model = user.ListWhereAll(searchString, page, pagesize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        ////////////////

        [HttpPost]
        public ActionResult Create(UserAccount model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (string.IsNullOrEmpty(model.Password))
                    {
                        SetAlert("Không được để trống", "warning");
                        return View();
                    }


                    int result = 0;
                    var dao = new UserDao();

                    if (dao.FindId(model.ID) != null)
                    {
                        SetAlert("Đã tồn tại", "warning");
                        return RedirectToAction("Create", "User");
                    }


                    var pass = Encryptor.EncryptMD5(model.Password);
                    model.Password = pass;
                    result = dao.Insert(model);

                    if (result > 0)
                    {
                        SetAlert("Thành công", "success");
                        return RedirectToAction("Index", "User");
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
                Common.Common.WriteLog("User", "Create-Post", ex.ToString());
            }
            return View();
        }

        ////////////

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao().Delete(id);
            return RedirectToAction("Index");
        }
        ////////////

        [HttpGet]
        public ActionResult Edit(string username)
        {
            String value = Request["idUser"];
            int x = int.Parse(value);
            ViewBag.NameEdit = value;
            var s = new UserDao().FindId(x);
            return View(s);
        }

        //////
        [HttpPost]
        public ActionResult Edit(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                NguyenVuongContext db = new NguyenVuongContext();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);

        }

        //================
        [HttpGet]
        public ActionResult Details()
        {
            String value = Request["idUser"];
            int x = int.Parse(value);
            ViewBag.NameEdit = value;
            var s = new UserDao().FindId(x);
            return View(s);
        }
    }
}