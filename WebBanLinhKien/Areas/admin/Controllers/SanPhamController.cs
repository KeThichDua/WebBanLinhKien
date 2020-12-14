using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class SanPhamController : BaseController
    
    {
        // GET: admin/SanPham
        public ActionResult Index()
        {
            IEnumerable<SanPhamModel> sanPhams = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET 
                var responseTask = client.GetAsync("api/getallproduct");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SanPhamModel>>();
                    readTask.Wait();

                    sanPhams = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    sanPhams = Enumerable.Empty<SanPhamModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }

            return View(sanPhams);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(SanPhamModel sanPham)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SanPhamModel>("api/create_product", sanPham);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Lỗi khởi tạo.");
            return View(sanPham);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<SanPhamModel> sanPhams = null;
            SanPhamModel sanPham = new SanPhamModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET
                var responseTask = client.GetAsync("api/getallproduct");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<SanPhamModel>>();
                    readTask.Wait();

                    sanPhams = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    sanPhams = Enumerable.Empty<SanPhamModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }


                foreach (var i in sanPhams)
                {
                    if (i.id == id)
                    {
                        sanPham = i;
                        break;
                    }
                }
            }
            return View(sanPham);
        }

        [HttpPost]
        public ActionResult Edit(SanPhamModel sanPham)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<SanPhamModel>("updateproduct", sanPham);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(sanPham);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deleteproduct?id=" + id.ToString());
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