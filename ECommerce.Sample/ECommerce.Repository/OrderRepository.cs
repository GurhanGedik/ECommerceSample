using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class OrderRepository : DataRepository<Order, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<Order> result = new ResultProcess<Order>();
        public override Result<int> Delete(int id)
        {
            Order deleted = db.Orders.SingleOrDefault(x => x.OrderId == id);
            db.Orders.Remove(deleted);
            return result.GetResult(db);
        }

        public override Result<List<Order>> GetLatestObj(int Quantity)
        {
            return result.GetListResult(db.Orders.OrderByDescending(x => x.OrderId).Take(Quantity).ToList());
        }

        public override Result<Order> GetObjById(int id)
        {
            Order obj = db.Orders.SingleOrDefault(x => x.OrderId == id);
            return result.GetT(obj);
        }

        public override Result<int> Insert(Order item)
        {
            db.Orders.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Order>> List()
        {
            List<Order> OrdList = db.Orders.ToList();
            return result.GetListResult(OrdList);
        }

        public override Result<int> Update(Order item)
        {
            Order update = db.Orders.SingleOrDefault(x => x.OrderId == item.OrderId);
            update.IsPay = item.IsPay;
            update.TotalPrice = item.TotalPrice;
            return result.GetResult(db);
        }
    }
}
