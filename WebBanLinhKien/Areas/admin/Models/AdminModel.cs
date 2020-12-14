using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class AdminModel
    {
        public int id { get; set; }
        public string Ma_Admin { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    
}