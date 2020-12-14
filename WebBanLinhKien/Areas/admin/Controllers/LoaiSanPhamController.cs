using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class LoaiSanPhamController : BaseController
    {
        // GET: admin/LoaiSanPham
        public ActionResult Index()
        {
            IEnumerable<LoaiSanPhamModel> loaiSanPhams = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET 
                var responseTask = client.GetAsync("api/getallcategory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<LoaiSanPhamModel>>();
                    readTask.Wait();

                    loaiSanPhams = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    loaiSanPhams = Enumerable.Empty<LoaiSanPhamModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return View(loaiSanPhams);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(LoaiSanPhamModel loaiSanPham)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<LoaiSanPhamModel>("api/create_category", loaiSanPham);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Lỗi tạo tài khoản.");
            return View(loaiSanPham);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<LoaiSanPhamModel> loaiSanPhams = null;
            LoaiSanPhamModel loaiSanPham = new LoaiSanPhamModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET
                var responseTask = client.GetAsync("api/getallcategory");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<LoaiSanPhamModel>>();
                    readTask.Wait();

                    loaiSanPhams = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    loaiSanPhams = Enumerable.Empty<LoaiSanPhamModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }


                foreach (var i in loaiSanPhams)
                {
                    if (i.id == id)
                    {
                        loaiSanPham = i;
                        break;
                    }
                }
            }
            return View(loaiSanPham);
        }

        [HttpPost]
        public ActionResult Edit(LoaiSanPhamModel loaiSanPham)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<LoaiSanPhamModel>("updatecategory", loaiSanPham);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(loaiSanPham);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deletecategory?id=" + id.ToString());
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