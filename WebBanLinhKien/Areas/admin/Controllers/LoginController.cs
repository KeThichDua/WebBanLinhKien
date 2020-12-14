using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebBanLinhKien.Areas.admin.Models;
using WebBanLinhKien.Common;

namespace WebBanLinhKien.Areas.admin.Controllers
{
    public class LoginController : Controller
    {

        // GET: admin/Login
        public ActionResult Index()
        {
            Session[CommomConstants.Admin_Session] = null;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginAdModel model)
        {

            if (ModelState.IsValid)
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
                }

                foreach (var i in admins)
                {
                    if ((i.Email == model.UserName || i.User == model.UserName) && i.Password == model.Password)
                    {
                        admin = i;
                        break;
                    }
                }

                if (admin.id > 0)
                {
                    var ad = new Login();
                    ad.id = admin.id;
                    if (admin.Email != null)
                    {
                        ad.TenHienThi = admin.Email;
                    }
                    else
                    {
                        ad.TenHienThi = admin.User;
                    }
                    ad.Password = admin.Password;
                    Session.Add(CommomConstants.Admin_Session, ad);
                    return RedirectToAction("Index", "Admin");
                }
            }
            return RedirectToAction("Index", "Login");
        }
        //[HttpPost]
        //public ActionResult Login(LoginAdModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        IEnumerable<KhachHangModel> users = null;
        //        KhachHangModel user = new KhachHangModel();

        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri("http://206.189.90.147/");
        //            //HTTP GET
        //            var responseTask = client.GetAsync("api/getalluser");
        //            responseTask.Wait();

        //            var result = responseTask.Result;
        //            if (result.IsSuccessStatusCode)
        //            {
        //                var readTask = result.Content.ReadAsAsync<List<KhachHangModel>>();
        //                readTask.Wait();

        //                users = readTask.Result;
        //            }
        //            else //web api sent error response 
        //            {
        //                //log response status here..

        //                users = Enumerable.Empty<KhachHangModel>();

        //                ModelState.AddModelError(string.Empty, "Lỗi server.");
        //            }
        //        }

        //        foreach (var i in users)
        //        {
        //            if ((i.Email == model.UserName || i.User_name == model.UserName) && i.Password == model.Password)
        //            {
        //                user = i;
        //                break;
        //            }
        //        }

        //        if (user.id > 0)
        //        {
        //            var ad = new Login();
        //            ad.id = user.id;
        //            if (user.Email != null)
        //            {
        //                ad.TenHienThi = user.Email;
        //            }
        //            else
        //            {
        //                ad.TenHienThi = user.User_name;
        //            }
        //            ad.Password = user.Password;
        //            Session.Add(CommomConstants.User_Session, ad);
        //            return RedirectToAction("Index", "Home");
        //        }
        //    }
        //    return RedirectToAction("Index", "LoginCli");
        //}
    }
}