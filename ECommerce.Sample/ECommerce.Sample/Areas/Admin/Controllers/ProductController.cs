using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Sample.Areas.Admin.Models.ResultModel;
using ECommerce.Sample.Areas.Admin.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository pr = new ProductRepository();
        InstanceResult<Product> result = new InstanceResult<Product>();
        CategoryRepository cr = new CategoryRepository();
        BrandRepository br = new BrandRepository();

        #region List
        public ActionResult List()
        {
            result.resultList = pr.List();
            return View(result.resultList.ProcessResult);
        }
        #endregion

        #region Add Product
        [HttpGet]
        public ActionResult AddProduct()
        {
            ProductViewModel pwm = new ProductViewModel();
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            foreach (Category item in cr.List().ProcessResult)
            {
                CatList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            foreach (Brand item in br.List().ProcessResult)
            {
                BrandList.Add(new SelectListItem { Value = item.BrandId.ToString(), Text = item.BrandName });
            }
            pwm.BrandList = BrandList;
            pwm.CategoryList = CatList;
            pwm.Product = null;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult AddProduct(ProductViewModel model, HttpPostedFileBase photo)
        {
            string PhotoName = "";
            if (photo.ContentLength > 0 & photo != null)
            {
                PhotoName = Guid.NewGuid().ToString().Replace("-", "") + ".jpg";
                string path = Server.MapPath("~/Upload/" + PhotoName);
                photo.SaveAs(path);
            }
            model.Product.Photo = PhotoName;
            result.resultint = pr.Insert(model.Product);
            if (result.resultint.ProcessResult > 0)
            {
                return RedirectToAction("List");
            }
            return View(model);
        }
        #endregion

        #region Edit Product
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductViewModel pwm = new ProductViewModel();
            List<SelectListItem> CatList = new List<SelectListItem>();
            List<SelectListItem> BrandList = new List<SelectListItem>();
            foreach (Category item in cr.List().ProcessResult)
            {
                CatList.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            foreach (Brand item in br.List().ProcessResult)
            {
                BrandList.Add(new SelectListItem { Value = item.BrandId.ToString(), Text = item.BrandName });
            }
            pwm.BrandList = BrandList;
            pwm.CategoryList = CatList;
            pwm.Product = pr.GetObjById(id).ProcessResult;
            return View(pwm);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel model, HttpPostedFileBase photoPath)
        {
            string photoName = model.Product.Photo;
            if (photoPath != null)
            {
                if (photoPath.ContentLength > 0)
                {
                    string ext = Path.GetExtension(photoPath.FileName);
                    photoName = Guid.NewGuid().ToString().Replace("-", "");
                    if (ext == ".jpg")
                    {
                        photoName += ext;
                    }
                    else if (ext == ".png")
                    {
                        photoName += ext;
                    }
                    else if (ext == ".bmp")
                    {
                        photoName += ext;
                    }
                    else
                    {
                        ViewBag.Mesaj = "Lütfen .jpg,.png,.bmp tipinde resim yükleyiniz";
                        return View(model);
                    }
                    string path = Server.MapPath("~/Upload/" + photoName);
                    photoPath.SaveAs(path);
                }
            }
            model.Product.Photo = photoName;
            result.resultint = pr.Update(model.Product);
            if (result.resultint.ProcessResult > 0)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View(model);
            }
        }
        #endregion

        #region Delete Product
        public ActionResult Delete(int id)
        {
            result.resultint = pr.Delete(id);
            return RedirectToAction("List", new { @mesaj = result.resultint.UserMassage });
        }
        #endregion
    }
}