using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class LoaiSanPhamModel
    {
        public int id { get; set; }
        public string MaLoaiSP { get; set; }
        public string Ten_loai { get; set; }
        public string Mo_ta { get; set; }
        public string Url_anh { get; set; }
    }
}