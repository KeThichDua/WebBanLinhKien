using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class HoaDonModel
    {
        public int id { get; set; }
        public string Ma_hd { get; set; }
        public string Ngay_dat { get; set; }
        public string Ngay_ship { get; set; }
        public string Ma_kh { get; set; }

    }
    public class HoaDonCreateModel
    {
        public string Ma_hd { get; set; }
        public DateTime Ngay_dat { get; set; }
        public DateTime Ngay_ship { get; set; }
        public string Ma_kh { get; set; }

    }
}