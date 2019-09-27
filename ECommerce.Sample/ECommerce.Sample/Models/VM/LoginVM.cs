using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Sample.Models.VM
{
    public class LoginVM
    {
        [
            EmailAddress(ErrorMessage = "Login in the e-mail form."),
            Required(ErrorMessage = "Please enter your e-mail."),
            DisplayName("E-mail")
        ]
        public string Email { get; set; }
        [
            Required(ErrorMessage = "Enter Password."),
            DisplayName("Password")
        ]
        public string Password { get; set; }
    }
}