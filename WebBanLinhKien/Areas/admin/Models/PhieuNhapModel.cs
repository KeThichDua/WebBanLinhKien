using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class PhieuNhapModel
    {
        public int id { get; set; }
        public string Ma_pn { get; set; }
        public string NgayNhap { get; set; }
        public string Ma_ncc { get; set; }
    }
}