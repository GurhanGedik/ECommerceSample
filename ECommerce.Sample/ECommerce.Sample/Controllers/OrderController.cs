using ECommerce.Common;
using ECommerce.Entity;
using ECommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Sample.Controllers
{
    public class OrderController : Controller
    {
         OrderRepository or = new OrderRepository();
        ProductRepository pr = new ProductRepository();
        OrderDetailRepository ordrep = new OrderDetailRepository();

        public ActionResult Add(int id)
        {
            //Sepetimizi sessionda tutuyoruz. Burada ki sessionun adi: Order(Session[Order])
            if (Session["Order"] == null)//session yarattık
            {
                //Order isimli sessiona bir adet Order ekledik
                Order o = new Order();
                o.OrderDate = DateTime.Now;
                o.IsPay = false;
                or.Insert(o);
                //orderRepository içerisinden son create edilmis bir orderi session uzerinde tutup o yapıya order Detail ekliyoruz.
                Session["Order"] = or.GetLatestObj(1).ProcessResult[0];
                OrderDetail od = new OrderDetail();
                od.OrderId = ((Order)Session["Order"]).OrderId;
                od.ProductId = id;
                //OrderDetail içerisinde quantity ve pricei guncelliyoruz.
                od.Quantity = 1;
                od.Price = pr.GetObjById(id).ProcessResult.Price;
                ordrep.Insert(od);
            }
            else
            {
                Order o = (Order)Session["Order"];
                OrderDetail Update = ordrep.GetOrderDetByTwoID(o.OrderId, id).ProcessResult;
                if (Update == null)
                {
                    OrderDetail od = new OrderDetail();
                    od.OrderId = o.OrderId;
                    od.ProductId = id;
                    od.Quantity = 1;
                    od.Price = pr.GetObjById(id).ProcessResult.Price;
                    ordrep.Insert(od);
                }
                else
                {
                    Update.Quantity++;
                    Update.Price += pr.GetObjById(id).ProcessResult.Price;
                    ordrep.Insert(Update);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DetailList()
        {
            Order sepetim = (Order)Session["Order"];
            decimal? TotalPrice = 0;
            OrderRepository or = new OrderRepository();
            if (sepetim.OrderDetails != null)
            {
                foreach (OrderDetail item in sepetim.OrderDetails)
                {
                    TotalPrice += item.Price;
                }
                sepetim.TotalPrice = TotalPrice;
                or.Update(sepetim);
            }
            else
            {
                sepetim.TotalPrice = 0;
                or.Update(sepetim);
            }
            if (sepetim==null)
            {
                return RedirectToAction("ListAllProduct", "Home");
            }
            else
            {
                return View(sepetim.OrderDetails);
            }
        }

        public ActionResult Delete(int id)
        {
            Order sepetim = (Order)Session["Order"];
            Result<int> result = ordrep.OrderDetailSil(sepetim.OrderId, id);
            return RedirectToAction("DetailList");
        }
    }
}