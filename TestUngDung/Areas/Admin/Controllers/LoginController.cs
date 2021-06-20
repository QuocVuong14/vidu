using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Model;
using ModelEF.DAO;
using TestUngDung.Common;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Index(LoginModel user)
        {
            var dao = new UserDao();
            var result = dao.Login(user.UserName, Encryptor.EncryptMD5(user.Password));
            if (result == 1)
            {
          
                Session.Add(Constants.USER_SESSION, user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login faild");
            }
            return View("Index");
        }
    }
}