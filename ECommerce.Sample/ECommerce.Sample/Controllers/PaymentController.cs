using ECommerce.Entity;
using ECommerce.Repository;
using ECommerce.Sample.Models.Mail;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (HttpContext.Request.Cookies["UserLogin"] != null)
            {
                ViewBag.welcome = HttpContext.Request.Cookies["UserLogin"].Value;
                ViewBag.userId = HttpContext.Request.Cookies["UserId"].Value;
            }
            ViewBag.PaymentTypes = new SelectList(PaymentRepository.List(), "PaymentId", "PaymentName");
            return View();
        }

        public string GetEmailTemplate()
        {
            ViewData.Model = ViewData["model"];
            using (StringWriter stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, "MailTemplate");
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
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
                ord.MemberId = Convert.ToInt32(HttpContext.Request.Cookies["UserId"].Value);
                ordrep.Update(ord);


                ViewData["model"] = model;
                var body = GetEmailTemplate();
                body.Replace("C2", "C2");
                SendingMail SendMail = new SendingMail();
                if (SendMail.sendMail(model.Order.Member.FirstName, model.Order.Member.LastName, model.Order.Member.Email, body, model.Order.OrderId))
                {
                    ViewBag.Mesaj = "Message delivered";
                }
                else
                {
                    ViewBag.Mesaj = "Message not delivered";
                }



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