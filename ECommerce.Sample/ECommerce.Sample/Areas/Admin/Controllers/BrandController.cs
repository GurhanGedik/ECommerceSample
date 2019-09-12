using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Common;
using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Sample.Areas.Admin.Models.ResultModel;

namespace ECommerce.Sample.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {

        InstanceResult<Brand> result = new InstanceResult<Brand>();
        BrandRepository br = new BrandRepository();
        // GET: Admin/Brand
        public ActionResult List()
        {

            result.resultList = br.List();
            return View(result.resultList.ProcessResult);
        }
        public ActionResult AddBrand()
        {
            Brand b = new Brand();
            b.Photo = "denemetestdeneme";
            return View(b);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddBrand(Brand model, HttpPostedFileBase photoPath)
        {
            string photoName = "";
            if (photoPath.ContentLength > 0 & photoPath != null)
            {
                photoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + photoName);
                photoPath.SaveAs(path);
            }
            model.Photo = photoName;
            if (ModelState.IsValid)
            {
                result.resultint = br.Insert(model);
                if (result.resultint.IsSucceeded)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Mesaj = result.resultint.UserMassage;
                    return View(model);
                }
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            result.TResult = br.GetObjById(id);
            return View(result.TResult.ProcessResult);
        }

        [HttpPost]
        public ActionResult Edit(Brand model, HttpPostedFileBase PhotoPath)
        {
            string PhotoName = model.Photo;
            if (PhotoPath.ContentLength > 0 & PhotoPath != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                PhotoPath.SaveAs(path);
            }
            model.Photo = PhotoName;
            result.resultint = br.Update(model);
            if (result.resultint.IsSucceeded)
            {
                return RedirectToAction("List");
            }
            return View(model);

        }
        public ActionResult Delete(int id)
        {
            result.resultint = br.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMassage });
        }
    }
}