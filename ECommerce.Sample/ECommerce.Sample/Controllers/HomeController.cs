using ECommerce.Entity;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ECommerce.Sample.Controllers
{
    public class HomeController : Controller
    {
        ProductRepository pr = new ProductRepository();

        public ActionResult Index()
        {

            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            return View(pr.GetLatestObj(12).ProcessResult);
        }
        public ActionResult Detail(int id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            Product p = pr.GetObjById(id).ProcessResult;
            return View(p);
        }

        public ActionResult List(Guid id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            List<Product> pList = pr.List().ProcessResult.Where(x => x.CategoryId == id).ToList();
            return View(pList);
        }
        public ActionResult ListByBrand(int? id)
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            List<Product> pList = pr.List().ProcessResult.Where(x => x.BrandId == id).ToList();
            return View(pList);
        }

        public ActionResult ListAllProduct()
        {
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            return View(pr.List().ProcessResult);
        }
    }
}