using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class KhachHangModel
    {
        public int id { get; set; }
        public string Ma_kh { get; set; }
        public string Ten_kh { get; set; }
        public string Sdt { get; set; }
        public string Gioi_tinh { get; set; }
        public int Tuoi { get; set; }
        public string Dia_chi { get; set; }
        public string User_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string IsVip { get; set; }
    }

}