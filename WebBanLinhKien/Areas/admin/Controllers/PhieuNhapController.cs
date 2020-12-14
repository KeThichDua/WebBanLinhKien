using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class PhieuNhapController : BaseController
    {
        // GET: admin/PhieuNhap
        public ActionResult Index()
        {
            IEnumerable<PhieuNhapModel> phieuNhaps = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET 
                var responseTask = client.GetAsync("api/getallpn");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PhieuNhapModel>>();
                    readTask.Wait();

                    phieuNhaps = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    phieuNhaps = Enumerable.Empty<PhieuNhapModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return View(phieuNhaps);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(PhieuNhapModel phieuNhap)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<PhieuNhapModel>("api/create_pn", phieuNhap);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Lỗi khởi tạo.");
            return View(phieuNhap);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<PhieuNhapModel> phieuNhaps = null;
            PhieuNhapModel phieuNhap = new PhieuNhapModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET
                var responseTask = client.GetAsync("api/getallpn");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<PhieuNhapModel>>();
                    readTask.Wait();

                    phieuNhaps = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    phieuNhaps = Enumerable.Empty<PhieuNhapModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }


                foreach (var i in phieuNhaps)
                {
                    if (i.id == id)
                    {
                        phieuNhap = i;
                        break;
                    }
                }
            }
            return View(phieuNhap);
        }

        [HttpPost]
        public ActionResult Edit(PhieuNhapModel phieuNhap)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<PhieuNhapModel>("update_pn", phieuNhap);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(phieuNhap);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("delete_pn?id=" + id.ToString());
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