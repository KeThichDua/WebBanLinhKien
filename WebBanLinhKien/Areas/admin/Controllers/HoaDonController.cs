using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class HoaDonController : BaseController
    {
        // GET: admin/HoaDon
        public ActionResult Index()
        {
            IEnumerable<HoaDonModel> hoaDons = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");
                //HTTP GET 
                var responseTask = client.GetAsync("api/getallbills");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<List<HoaDonModel>>();
                    readTask.Wait();

                    hoaDons = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    hoaDons = Enumerable.Empty<HoaDonModel>();

                    ModelState.AddModelError(string.Empty, "Lỗi server.");
                }
            }
            return View(hoaDons);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://206.189.90.147/");

                //HTTP DELETE
                var deleteTask = client.DeleteAsync("api/deletebill?id=" + id.ToString());
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