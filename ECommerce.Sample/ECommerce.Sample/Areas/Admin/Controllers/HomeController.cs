using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        CategoryRepository cr = new CategoryRepository();
        Result<List<Category>> ListCategory = new Result<List<Category>>();
        Result<int> resultint = new Result<int>();
        Result<Category> resultt = new Result<Category>();


        public ActionResult List()
        {
            ListCategory = cr.List();
            return View(ListCategory.ProcessResult);
        }

        [HttpPost]
        public ActionResult AddCategory(Category cat)
        {
            Category c = new Category();
            c.CategoryId = Guid.NewGuid();
            c.CategoryName = cat.CategoryName;
            c.Description = cat.Description;
            c.CreatedDate = DateTime.Now;
            resultint = cr.Insert(c);
            ViewBag.Mesaj = resultint.UserMassage;
            return RedirectToAction("List","Admin/Home");
        }
    }
}