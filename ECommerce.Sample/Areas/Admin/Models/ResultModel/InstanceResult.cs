using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Common;
using ECommerce.Entity;

namespace ECommerce.Sample.Areas.Admin.Models.ResultModel
{
    public class InstanceResult<T>
    {
        public Result<List<T>> resultList { get; set; }
        public Result<int> resultint { get; set; }
        public Result<T> TResult { get; set; }

        public static implicit operator InstanceResult<T>(InstanceResult<Category> v)
        {
            throw new NotImplementedException();
        }
    }
}