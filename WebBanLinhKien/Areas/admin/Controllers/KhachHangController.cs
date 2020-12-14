using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    // Thêm sửa xóa khách hàng thành công
    public class KhachHangController : BaseController
    {
        // GET: admin/KhachHang
        public ActionResult Index()
        {
            IEnumerable<KhachHangModel> khachHangs = null;

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
                    //log response status here..

                    khachHangs = Enumerable.Empty<KhachHangModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return View(khachHangs);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(KhachHangModel khachHang)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<KhachHangModel>("api/create_user", khachHang);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Lỗi tạo tài khoản.");
            return View(khachHang);
        }

        public ActionResult Edit(int id)
        {
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
                    //log response status here..

                    khachHangs = Enumerable.Empty<KhachHangModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }


                foreach (var i in khachHangs)
                {
                    if (i.id == id)
                    {
                        khachHang = i;
                        break;
                    }
                }
            }
            return View(khachHang);
        }

        [HttpPost]
        public ActionResult Edit(KhachHangModel khachHang)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<KhachHangModel>("updateuserbyadmin", khachHang);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(khachHang);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deleteuser?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }
    }
}