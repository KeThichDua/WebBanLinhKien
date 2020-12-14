using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class NhaCungCapModel
    {
        public int id { get; set; }
        public string Ma_ncc { get; set; }
        public string Ten_ncc { get; set; }
        public string Dia_chi { get; set; }
        public string Sdt { get; set; }
        public string Email { get; set; }
    }
}