using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Sample.Areas.Admin.Models.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Controllers
{
    public class ProfileController : Controller
    {
        MemberRepository mr = new MemberRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();

        #region Edit Profile
        public ActionResult Edit(int id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            result.TResult = mr.GetObjById(id);
            return View(result.TResult.ProcessResult);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Member model)
        {
            result.resultint = mr.Update(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
                return View(model);
        }
        #endregion

        #region Delete Profile
        public ActionResult Delete(int id)
        {
            result.resultint = mr.Delete(id);
            return RedirectToAction("LogOut", "Login");
        }
        #endregion
    }
}