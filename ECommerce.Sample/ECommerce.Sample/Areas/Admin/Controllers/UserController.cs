using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Sample.Areas.Admin.Models.ResultModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        UserRepository mr = new UserRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();
        public ActionResult List(string mesaj, int? id)//? null olabilir demek db den gelen veri
        {
            result.resultList = mr.List();
            if (mesaj != null)
            {
                ViewBag.Mesaj = string.Format("{0} nolu kaydin silme {1}", id, mesaj);
            }
            else
                ViewBag.Mesaj = "";
            return View(result.resultList.ProcessResult);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(Member model)
        {
            model.RoleId = 1;
            result.resultint = mr.Insert(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("List");
            }
            else
                return View(model);
        }

        public ActionResult Edit(int id)
        {
            return View(mr.GetObjById(id).ProcessResult);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Member model)
        {
            result.resultint = mr.Update(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("List");
            }
            else
                return View(model);
        }

        public ActionResult Delete(int id)
        {
            result.resultint = mr.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMassage, @id = id });
        }
    }
}