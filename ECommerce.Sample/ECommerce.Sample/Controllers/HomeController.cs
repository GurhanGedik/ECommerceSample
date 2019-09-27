using ECommerce.Entity;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository pr = new ProductRepository();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.welcome = "Welcome " + User.Identity.Name.ToUpper();
            }
            return View(pr.GetLatestObj(12).ProcessResult);
        }
        public ActionResult Detail(int id)
        {
            Product p = pr.GetObjById(id).ProcessResult;
            return View(p);
        }

        public ActionResult List(Guid id)
        {
            List<Product> pList = pr.List().ProcessResult.Where(x => x.CategoryId == id).ToList();
            return View(pList);
        }
        public ActionResult ListByBrand(int? id)
        {
            List<Product> pList = pr.List().ProcessResult.Where(x => x.BrandId == id).ToList();
            return View(pList);
        }

        public ActionResult ListAllProduct()
        {
            return View(pr.List().ProcessResult);
        }
    }
}