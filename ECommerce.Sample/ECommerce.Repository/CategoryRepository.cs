using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class CategoryRepository : DataRepository<Category>
    {
        public override Result<int> Delete(Category item)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(Category item)
        {
            throw new NotImplementedException();
        }

        public override Result<List<Category>> List(Category item)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Update(Category item)
        {
            throw new NotImplementedException();
        }
    }
}
