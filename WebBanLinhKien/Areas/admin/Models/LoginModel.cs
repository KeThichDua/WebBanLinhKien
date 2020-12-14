using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class LoginAdModel
    {
        public string UserName { set; get; }

        public string Password { set; get; }

        public bool RememberMe { set; get; }
    }
}