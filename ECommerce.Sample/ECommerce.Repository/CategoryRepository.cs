﻿using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class CategoryRepository : DataRepository<Category, Guid>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<Category> result = new ResultProcess<Category>();

        public override Result<int> Delete(Guid id)
        {
            Category c = db.Categories.SingleOrDefault(x => x.CategoryId == id);
            db.Categories.Remove(c);
            return result.GetResult(db);
        }

        public override Result<Category> GetObjById(Guid id)
        {
            Category c = db.Categories.SingleOrDefault(x => x.CategoryId == id);
            return result.GetT(c);
        }

        public override Result<int> Insert(Category item)
        {
            db.Categories.Add(item);
            return result.GetResult(db);
        }

        public override Result<List<Category>> List()
        {
            List<Category> CatList = db.Categories.ToList();
            return result.GetListResult(CatList);
        }

        public override Result<int> Update(Category item)
        {
            Category c = db.Categories.SingleOrDefault(x => x.CategoryId == item.CategoryId);
            c.CategoryName = item.CategoryName;
            c.Description = item.Description;
            return result.GetResult(db);
        }
    }
}
