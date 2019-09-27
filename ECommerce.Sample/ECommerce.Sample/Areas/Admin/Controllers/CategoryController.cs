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
        CategoryRepository cr = new CategoryRepository();
        InstanceResult<Category> result = new InstanceResult<Category>();

        #region List

        public ActionResult List()
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value; ;
            }
            result.resultList = cr.List();
            ViewBag.Resultt = TempData["Operation"];
            return View(result.resultList.ProcessResult);
        }
        #endregion

        #region Add Category
        public ActionResult AddCategory()
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value; ;
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            model.CategoryId = Guid.NewGuid();
            result.resultint = cr.Insert(model);
            TempData["Operation"] = result.resultint.IsSucceeded;
            return RedirectToAction("List");

        }
        #endregion

        #region Edit Category
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value; ;
            }
            result.TResult = cr.GetObjById(id);
            return View(result.TResult.ProcessResult);
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            result.resultint = cr.Update(model);
            ViewBag.Mesaj = result.resultint.UserMassage;
            TempData["Operation"] = result.resultint.IsSucceeded;
            return RedirectToAction("List");
        }
        #endregion

        #region Delete Category
        public ActionResult Delete(Guid id)
        {
            result.resultint = cr.Delete(id);
            TempData["Operation"] = result.resultint.IsSucceeded;
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMassage });
        }
        #endregion

    }
}