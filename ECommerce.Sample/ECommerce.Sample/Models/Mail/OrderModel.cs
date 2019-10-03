using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Sample.Models.Mail
{
    public class OrderModel
    {
        public Order Order { get; set; }
        public Invoice Invoice { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}