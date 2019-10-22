using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;

namespace ECommerce.Common
{
    public class Tools
    {
        public static MyECommerceEntities db = null;
        public static MyECommerceEntities GetConnection()
        {
            if (db==null)
                db = new MyECommerceEntities();
            return db;
        }
    }
}
