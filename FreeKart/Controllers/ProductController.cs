using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeKart.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Page()
        {
            return View();
        }
        public ActionResult Details()
        {
            return View();
        }
    }
}