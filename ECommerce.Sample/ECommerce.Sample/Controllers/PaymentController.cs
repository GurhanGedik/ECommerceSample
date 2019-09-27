using ECommerce.Entity;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Controllers
{
    public class PaymentController : Controller
    {
        [HttpGet]
        public ActionResult Pay()
        {
            ViewBag.PaymentTypes = new SelectList(PaymentRepository.List(), "PaymentId", "PaymentName");
            return View();
        }
        [HttpPost]
        public ActionResult Pay(Invoice model, List<string> PaymentTypes)
        {
            model.PaymentDate = DateTime.Now;
            foreach (string item in PaymentTypes)
            {
                int PaymentId = Convert.ToInt32(item);
                model.PaymentTypeId = PaymentId;
            }
            model.OrderId = ((Order)Session["Order"]).OrderId;
            InvoiceRepository ip = new InvoiceRepository();
            if (ip.Insert(model).IsSucceeded)
            {
                Order ord = (Order)Session["Order"];
                OrderRepository ordrep = new OrderRepository();
                ord.IsPay = true;
                ordrep.Update(ord);
                Session.Abandon();
                return RedirectToAction("ListAllProduct", "Home");
            }
            else
            {
                return View(model);
            }
        }
    }
}