using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entity;

namespace ECommerce.Common
{
    public class ResultProcess<T>
    {
        public Result<int> GetResult(MyECommerceEntities db)
        {
            Result<int> result = new Result<int>();
            int transactionResult = db.SaveChanges();
            if (transactionResult > 0)
            {
                result.UserMassage = "Operation Successful.";
                result.IsSucceeded = true;
                result.ProcessResult = transactionResult;
            }
            else
            {
                result.UserMassage = "Operation Failed!";
                result.IsSucceeded = false;
                result.ProcessResult = transactionResult;
            }
            return result;
        }

        public Result<List<T>> GetListResult(List<T> data)
        {
            Result<List<T>> result = new Result<List<T>>();
            if (data !=null)
            {
                result.UserMassage = "Operation Successful.";
                result.IsSucceeded = true;
                result.ProcessResult = data;
            }
            else
            {
                result.UserMassage = "Operation Failed!";
                result.IsSucceeded = false;
                result.ProcessResult = data;
            }
            return result;
        }
        public Result<T> GetT(T data)
        {
            Result<T> result = new Result<T>();
            if (data != null)
            {
                result.IsSucceeded = true;
                result.UserMassage = "Başarılı";
                result.ProcessResult = data;
            }
            else
            {
                result.IsSucceeded = false;
                result.UserMassage = "Başarısız";
                result.ProcessResult = data;
            }
            return result;
        }
    }
}
