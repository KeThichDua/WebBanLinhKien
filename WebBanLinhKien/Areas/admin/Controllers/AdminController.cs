using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class AdminController : BaseController
    {
        //GET: admin/Admin
        public ActionResult Index()
        {
            IEnumerable<AdminModel> admins = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET
                var responseTask = client.GetAsync("api/getalladmin");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<AdminModel>>();
                    readTask.Wait();

                    admins = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    admins = Enumerable.Empty<AdminModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return View(admins);
        }

        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminModel admin)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<AdminModel>("api/createadmin", admin);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            ModelState.AddModelError(string.Empty, "Lỗi tạo tài khoản.");
            return View(admin);
        }


        public ActionResult Edit(int id)
        {
            IEnumerable<AdminModel> admins = null;
            AdminModel admin = new AdminModel();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET
                var responseTask = client.GetAsync("api/getalladmin");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<AdminModel>>();
                    readTask.Wait();

                    admins = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    admins = Enumerable.Empty<AdminModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }


                foreach (var i in admins)
                {
                    if (i.id == id)
                    {
                        admin = i;
                        break;
                    }
                }
            }
            return View(admin);
        }

        [HttpPost]
        public ActionResult Edit(AdminModel admin)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<AdminModel>("updateadmin", admin);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(admin);
        }


        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/api/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("deleteadmin?id=" + id.ToString());
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