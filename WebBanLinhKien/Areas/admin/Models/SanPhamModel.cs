using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanLinhKien.Areas.admin.Models
{
    public class SanPhamModel
    {
        public int id { get; set; }
        public string Ma_sp { get; set; }
        public string Ten_sp { get; set; }
        public int So_luong { get; set; }
        public string DonGia { get; set; }
        public string Mo_ta { get; set; }
        public int Gia_km { get; set; }
        public string Url_anh { get; set; }
        public string Ma_loai_sp { get; set; }

    }
    public class SanPhamIDModel
    {
        public string Ma_sp { get; set; }
        public string Ten_sp { get; set; }
        public int So_luong { get; set; }
        public string Mo_ta { get; set; }
        public int Gia_km { get; set; }
        public string Url_anh { get; set; }
        public string Ma_loai_sp { get; set; }

    }
}