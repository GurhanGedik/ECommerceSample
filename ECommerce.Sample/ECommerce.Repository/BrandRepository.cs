using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class BrandRepository : DataRepository<Brand, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<Brand> result = new ResultProcess<Brand>();


        public override Result<int> Delete(int id)
        {
            Brand b = db.Brands.SingleOrDefault(x => x.BrandId == id);
            db.Brands.Remove(b);
            return result.GetResult(db);
        }

        public override Result<Brand> GetObjById(int id)
        {
            Brand b = db.Brands.SingleOrDefault(x => x.BrandId == id);
            return result.GetT(b);
        }

        public override Result<int> Insert(Brand item)
        {
            db.Brands.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Brand>> List()
        {
            return result.GetListResult(db.Brands.ToList());
        }

        public override Result<int> Update(Brand item)
        {
            Brand guncellenecek = db.Brands.SingleOrDefault(x => x.BrandId == item.BrandId);
            guncellenecek.BrandName = item.BrandName;
            guncellenecek.Description = item.Description;
            guncellenecek.Photo = item.Photo;
            return result.GetResult(db);
        }
    }
}
