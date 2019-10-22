using ECommerce.Common;
using ECommerce.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository
{
    public class UserRepository : DataRepository<UserRole, int>
    {
        private static MyECommerceEntities db = Tools.GetConnection();
        ResultProcess<UserRole> result = new ResultProcess<UserRole>();

        public override Result<int> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<List<UserRole>> GetLatestObj(int Quantity)
        {
            throw new NotImplementedException();
        }

        public override Result<UserRole> GetObjById(int id)
        {
            throw new NotImplementedException();
        }

        public override Result<int> Insert(UserRole item)
        {
            throw new NotImplementedException();
        }

        public override Result<List<UserRole>> List()
        {
            return result.GetListResult(db.UserRoles.ToList());
        }

        public override Result<int> Update(UserRole item)
        {
            throw new NotImplementedException();
        }
    }
}
