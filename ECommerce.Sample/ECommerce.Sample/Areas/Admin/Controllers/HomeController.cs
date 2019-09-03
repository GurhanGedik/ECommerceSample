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
        public ActionResult List()
        {
            ListCategory = cr.List();
            return View(ListCategory.ProcessResult);
        }
    }
}