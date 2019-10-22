using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ECommerce.Entity;


namespace ECommerce.Sample.Areas.Admin.Models.ViewModel
{
    public class UserViewModel
    {
        public Member Member { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}