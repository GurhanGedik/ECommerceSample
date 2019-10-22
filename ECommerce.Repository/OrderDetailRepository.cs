using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class OrderDetailRepository : DataRepository<OrderDetail, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<OrderDetail> result = new ResultProcess<OrderDetail>();

        public override Result<int> Delete(int id)
        {
            List<OrderDetail> OrdList = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            db.OrderDetails.RemoveRange(OrdList);
            return result.GetResult(db);
        }

        public override Result<List<OrderDetail>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<OrderDetail> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(OrderDetail item)
        {
            db.OrderDetails.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<OrderDetail>> List()
        {
            return result.GetListResult(db.OrderDetails.ToList());
        }

        public override Result<int> Update(OrderDetail item)
        {
            OrderDetail OrdDet = db.OrderDetails.SingleOrDefault(x => x.OrderId == item.OrderId);
            OrdDet.Quantity = item.Quantity;
            OrdDet.Price = item.Price;
            return result.GetResult(db);
        }

        public Result<int> OrderDetailSil(int OrdId, int ProId)
        {
            OrderDetail od = db.OrderDetails.SingleOrDefault(x => x.OrderId == OrdId & x.ProductId == ProId);
            db.OrderDetails.Remove(od);
            return result.GetResult(db);
        }

        public Result<List<OrderDetail>> GetListOrdId(int Id)
        {
            return result.GetListResult(db.OrderDetails.Where(x => x.OrderId == Id).ToList());
        }

        public Result<OrderDetail> GetOrderDetByTwoID(int OD, int PID)
        {
            OrderDetail od = db.OrderDetails.SingleOrDefault(x => x.OrderId == OD & x.ProductId == PID);
            return result.GetT(od);
        }
    }
}
