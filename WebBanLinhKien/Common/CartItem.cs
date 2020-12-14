using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Common
{
    [Serializable]
    public class CartItem
    {
        public SanPhamIDModel Product { set; get; }
        public int Quantity { set; get; }
    }
}