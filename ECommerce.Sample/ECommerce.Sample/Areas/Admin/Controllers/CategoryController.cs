using ECommerce.Common;
using ECommerce.Entity;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Sample.Areas.Admin.Models.ResultModel;

namespace ECommerce.Sample.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        CategoryRepository cr = new CategoryRepository();
        InstanceResult<Category> result = new InstanceResult<Category>();
        public ActionResult List()
        {
            result.resultList = cr.List();
            return View(result.resultList.ProcessResult);
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            model.CategoryId = Guid.NewGuid();
            result.resultint = cr.Insert(model);
            return RedirectToAction("List");

        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            //id yi çeken methoda ihtiyacım var cunku id üzerinden ilerliyoruz.
            result.TResult = cr.GetObjById(id);
            return View(result.TResult.ProcessResult);

        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            result.resultint = cr.Update(model);
            ViewBag.Mesaj = result.resultint.UserMassage;
            return View();
        }

        public ActionResult Delete(Guid id)
        {
            result.resultint = cr.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMassage });
        }
    }
}