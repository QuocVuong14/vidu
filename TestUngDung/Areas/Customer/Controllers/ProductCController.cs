using ModelEF.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Customer.Controllers
{
    public class ProductCController : Controller
    {
        // GET: Customer/ProductC
        public ActionResult Index()
        {
            var product = new ProductDao();
            var model = product.ListAll();
            return View(model);
        }

  
       
    }
}