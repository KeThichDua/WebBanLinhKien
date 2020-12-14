using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class NhaCungCapController : BaseController
    {
        // GET: admin/NhaCungCap
        public ActionResult Index()
        {
            IEnumerable<NhaCungCapModel> nhaCungCaps = null;

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

                    nhaCungCaps = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhaCungCaps = Enumerable.Empty<NhaCungCapModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return View(nhaCungCaps);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(NhaCungCapModel nhaCungCap)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<NhaCungCapModel>("api/create_nhacc", nhaCungCap);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Lỗi tạo mới.");
            return View(nhaCungCap);
        }

        public ActionResult Edit(int id)
        {
            IEnumerable<NhaCungCapModel> nhaCungCaps = null;
            NhaCungCapModel nhaCungCap = new NhaCungCapModel();

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

                    nhaCungCaps = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    nhaCungCaps = Enumerable.Empty<NhaCungCapModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }


                foreach (var i in nhaCungCaps)
                {
                    if (i.id == id)
                    {
                        nhaCungCap = i;
                        break;
                    }
                }
            }
            return View(nhaCungCap);
        }

        [HttpPost]
        public ActionResult Edit(NhaCungCapModel nhaCungCap)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<NhaCungCapModel>("updatenhacc", nhaCungCap);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(nhaCungCap);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deletenhacc?id=" + id.ToString());
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