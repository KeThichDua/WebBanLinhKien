using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;
using WebBanLinhKien.Models;

namespace WebBanLinhKien.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Trang chủ";
            var model = Lay();           

            return View(model);
        }

        public SP_loaiSP_nhaCC Lay() {
            var model = new SP_loaiSP_nhaCC();
            model.SP = null;
            model.loaiSP = null;
            model.nhaCC = null;

            // lay ds san pham
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

                    model.SP = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model.SP = Enumerable.Empty<SanPhamModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }

            // lay ds laoi sp
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

                    model.loaiSP = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model.loaiSP = Enumerable.Empty<LoaiSanPhamModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }

            //lay ds nha cc
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET 
                var responseTask = client.GetAsync("api/getallnhacc");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<NhaCungCapModel>>();
                    readTask.Wait();

                    model.nhaCC = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    model.nhaCC = Enumerable.Empty<NhaCungCapModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return model;
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        
        public ActionResult All()
        {
            var model = Lay();
            return View(model);
        }

        public ActionResult LoaiSP(string MaLoaiSP)
        {
            var model = Lay();            
            return View(model);
        }

        public ActionResult Nhacc(int id)
        {
            var model = Lay();
            return View(model);
        }

        
    }
}
