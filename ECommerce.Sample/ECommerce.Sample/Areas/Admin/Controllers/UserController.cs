using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Sample.Areas.Admin.Models.ResultModel;
using ECommerce.Sample.Areas.Admin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        MemberRepository mr = new MemberRepository();
        UserRepository ur = new UserRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();

        #region List
        public ActionResult List(string mesaj, int? id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value; ;
            }
            result.resultList = mr.List();
            if (mesaj != null)
            {
                ViewBag.Mesaj = string.Format("{0} nolu kaydin silme {1}", id, mesaj);
            }
            else
                ViewBag.Mesaj = "";
            return View(result.resultList.ProcessResult);
        }
        #endregion

        #region Add User 
        public ActionResult AddUser()
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value; ;
            }
            UserViewModel uvm = new UserViewModel();
            List<SelectListItem> UserListt = new List<SelectListItem>();
            foreach (UserRole item in ur.List().ProcessResult)
            {
                UserListt.Add(new SelectListItem { Value = item.RoleId.ToString(), Text = item.RoleName });
            }
            uvm.UserList = UserListt;
            return View(uvm);
        }

        [HttpPost]
        public ActionResult AddUser(UserViewModel model)
        {
            model.Member.RoleId = model.Member.UserId;
            result.resultint = mr.Insert(model.Member);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("List");
            }
            else
                return View(model);
        }
        #endregion

        #region Edit User
        public ActionResult Edit(int id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value; ;
            }
            UserViewModel uvm = new UserViewModel();
            List<SelectListItem> UserListt = new List<SelectListItem>();
            foreach (UserRole item in ur.List().ProcessResult)
            {
                UserListt.Add(new SelectListItem { Value = item.RoleId.ToString(), Text = item.RoleName });
            }
            uvm.UserList = UserListt;
            uvm.Member = mr.GetObjById(id).ProcessResult;
            return View(uvm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel model)
        {
            result.resultint = mr.Update(model.Member);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("List");
            }
            else
                return RedirectToAction("List");
        }
        #endregion

        #region Delete User
        public ActionResult Delete(int id)
        {
            result.resultint = mr.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMassage, @id = id });
        }
        #endregion
    }
}