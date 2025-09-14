using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeKart.Controllers
{
    [Authorize]
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
        public ActionResult CartDetails()
        {
            return View();
        }
        public ActionResult AddAddress(string type)
        {
            if (type=="add")
            {
                ViewData["type"] = "Address Details";
            }
            else
            {
                ViewData["type"] = "Change Details";
            }
            return View();
        }
        public ActionResult PaymentProcess()
        {
            return View();
        }
    }
}