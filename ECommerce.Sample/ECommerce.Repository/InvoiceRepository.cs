using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class InvoiceRepository : DataRepository<Invoice, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<Invoice> result = new ResultProcess<Invoice>();

        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<List<Invoice>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<Invoice> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Invoice item)
        {
            db.Invoices.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Invoice>> List()
        {
            List<Invoice> invoiceList = db.Invoices.OrderByDescending(t => t.OrderId).ToList();
            return result.GetListResult(invoiceList);
        }

        public override Result<int> Update(Invoice item)
        {
            throw new NotImplementedException();
        }
    }
}
