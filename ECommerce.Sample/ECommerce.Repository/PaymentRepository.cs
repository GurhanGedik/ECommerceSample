using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    class PaymentRepository
    {
        private static MyECommerceEntities db = Tools.GetConnection();

        public static List<Payment> List()
        {
            return db.Payments.ToList();
        }
    }
}
