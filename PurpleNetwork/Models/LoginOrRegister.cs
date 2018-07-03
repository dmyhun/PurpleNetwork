using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PurpleNetwork.Models
{
    public class LoginOrRegister
    {
        public LoginModel Login { get; set; }

        public RegisterModel Register { get; set; }

        public string ReturnUrl { get; set; }
    }
}