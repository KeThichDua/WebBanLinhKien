using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Models
{
    public class SP_loaiSP_nhaCC
    {
        public IEnumerable<SanPhamModel> SP { get; set; }
        public IEnumerable<LoaiSanPhamModel> loaiSP { get; set; }
        public IEnumerable<NhaCungCapModel> nhaCC { get; set; }
    }
}