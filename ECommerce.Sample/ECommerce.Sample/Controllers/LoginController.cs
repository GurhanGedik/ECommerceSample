using ECommerce.Sample.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Repository;
using ECommerce.Entity;
using ECommerce.Common;
using System.Web.Security;

namespace ECommerce.Sample.Controllers
{
    public class LoginController : Controller
    {
        private static MyECommerceEntities db = Tools.GetConnection();

        // GET: Login
        public ActionResult SingIn()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SingIn(LoginVM model)
        {

            if (ModelState.IsValid)
            {
                var user = db.Members.FirstOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.FirstName, true);
                    return RedirectToAction("Index", "Home");
                }


            }
            ViewBag.Massage = "Incorrect username or password";


            return View();
        }
    }
}