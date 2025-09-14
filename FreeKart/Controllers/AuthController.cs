using FreeKart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FreeKart.Controllers
{
    public class AuthController : Controller
    {
   
        // GET: Auth
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult Login()
        
        {
            LoginModel loginModel = new LoginModel();
            loginModel.UserName = "bhumesh";
            loginModel.Password = "1234";
            var username ="bhumesh";
            var encrytedpassword ="1234";
            if (encrytedpassword.Equals(loginModel.Password)&&username.Equals(loginModel.UserName))
            {
                var roles = new string[] { "SuperAdmin", "User", "Admin" };
                var token = Authentication.GenerateJwtToken(loginModel.UserName, roles.ToList());
                Session["LoggedIn"] = loginModel.UserName;
                return RedirectToAction("Index", "Home", new { token = token });
            }

            

            return View();
        }
    }
}