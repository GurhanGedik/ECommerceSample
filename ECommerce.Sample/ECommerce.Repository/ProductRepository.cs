using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class ProductRepository : DataRepository<Product, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<Product> result = new ResultProcess<Product>();

        public override Result<int> Delete(int id)
        {
            Product silinecek = db.Products.SingleOrDefault(x => x.ProductId == id);
            db.Products.Remove(silinecek);
            return result.GetResult(db);
        }

        public override Result<Product> GetObjById(int id)
        {
            return result.GetT(db.Products.SingleOrDefault(x => x.ProductId == id));
        }

        public override Result<int> Insert(Product item)
        {
            db.Products.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Product>> List()
        {
            return result.GetListResult(db.Products.ToList());
        }

        public override Result<int> Update(Product item)
        {
            Product gunP = db.Products.SingleOrDefault(x => x.ProductId == item.ProductId);
            gunP.BrandId = item.BrandId;
            gunP.CategoryId = item.CategoryId;
            gunP.Stock = item.Stock;
            gunP.Photo = item.Photo;
            gunP.Price = item.Price;
            gunP.ProductName = item.ProductName;
            return result.GetResult(db);
        }
    }
}

