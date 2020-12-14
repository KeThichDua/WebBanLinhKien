using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Common
{

        [Serializable]
        public class Login
        {
            public int id { set; get; }
            public string TenHienThi { set; get; }
            public string Password { set; get; }

        }
    
}