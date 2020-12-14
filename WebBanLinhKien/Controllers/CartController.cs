using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using WebBanLinhKien.Areas.admin.Models;
using WebBanLinhKien.Common;

namespace WebBanLinhKien.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CommomConstants.Cart_Session];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }

        public ActionResult AddItem(int id, int quantity)
        {
            SanPhamIDModel sanPham = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET 
                var responseTask = client.GetAsync("api/getdetailproductbyid?id="+ id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SanPhamIDModel>>();
                    readTask.Wait();

                    sanPham = readTask.Result[0];
                }
                else //web api sent error response 
                {
                    //log response status here..


                    ModelState.AddModelError(string.Empty, "Lỗi không tìm thấy sản phẩm.");
                    return RedirectToAction("Index");
                }
            }

            var cart = Session[CommomConstants.Cart_Session];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Count > 0 && list.Exists(x => x.Product.Ma_sp == sanPham.Ma_sp))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.Ma_sp == sanPham.Ma_sp)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng sp
                    var item = new CartItem();
                    item.Product = sanPham;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[CommomConstants.Cart_Session] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new CartItem();
                item.Product = sanPham;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gán vào Session
                Session[CommomConstants.Cart_Session] = list;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            var cart = Session[CommomConstants.Cart_Session];
            var list = (List<CartItem>)cart;
            if (list.Exists(x => x.Product.Ma_sp == id))
            {
                foreach (var item in list.ToList())
                {
                    if (item.Product.Ma_sp == id)
                    {
                        list.Remove(item);
                    }
                }
            }
            if(list.Count() == 1)
            {
                list.RemoveAt(0);
            }

            Session[CommomConstants.Cart_Session] = list;
            return RedirectToAction("Index");
        }

        public ActionResult PayMent()
        {
            var cart = Session[CommomConstants.Cart_Session];
            var list = (List<CartItem>)cart;
            var kh = Session[CommomConstants.User_Session];
            var khach = (Login)kh;
            //tạo hóa đơn
            var order = new HoaDonCreateModel();
            IEnumerable<CTHoaDonModel> CTorder = null;
            CTorder = Enumerable.Empty<CTHoaDonModel>(); 

            // lấy khách hàng theo id
             IEnumerable<KhachHangModel> khachHangs = null;
            KhachHangModel khachHang = new KhachHangModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET
                var responseTask = client.GetAsync("api/getalluser");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<KhachHangModel>>();
                    readTask.Wait();

                    khachHangs = readTask.Result;
                }
                else //web api sent error response 
                {
                    khachHangs = Enumerable.Empty<KhachHangModel>();
                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
                foreach (var i in khachHangs)
                {
                    if (i.id == khach.id)
                    {
                        khachHang = i;
                        break;
                    }
                }
            }

            order.Ma_hd = DateTime.Now.ToString();
            order.Ngay_dat = DateTime.Now;
            order.Ma_kh = khachHang.Ma_kh;

            foreach(var i in list)
            {
                CTHoaDonModel ct = new CTHoaDonModel();
                ct.Ma_hd = order.Ma_hd;
                ct.Ma_sp = i.Product.Ma_sp;
                ct.So_luong = i.Quantity.ToString();
                CTorder.Append(ct);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://206.189.90.147/");

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<HoaDonCreateModel>("api/create_hoadon", order);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                       
                    }
                    else
                    {
                        //ghi log
                        return RedirectToAction("Index");
                    }

                }
                using (var client = new HttpClient())
                {
                    foreach (var i in CTorder)
                    {
                        var postTask = client.PostAsJsonAsync<CTHoaDonModel>("api/createdetailbill", i);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                        }
                        else
                        {
                            //ghi log
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception)
            {
                //ghi log
                return RedirectToAction("Index");
            }
            ViewData["Message"] = "Success";
            list.Clear();
            Session[CommomConstants.Cart_Session] = list;
            return RedirectToAction("Index");
        }
    }
}