using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebPTCOM.Models;


namespace WebPTCOM.Controllers
{
    public class HomeController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            ProductDAL obj = new ProductDAL();
            ModelState.Clear();
            return View(obj.GetAll());
        }

        public ActionResult ProductDetail(int? Id)
        {
            ProductDAL pro = new ProductDAL();
            return View(pro.GetById(Id));
        }

        // GET: /User/Login/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            string User = collection["Username"];
            string Pass = collection["Password"];
            if (string.IsNullOrEmpty(User))
            {
                ViewData["errUser"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(Pass))
            {
                ViewData["errUser"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                UserDAL userDAL = new UserDAL();
                User user = userDAL.GetUser(User, Pass);
                if (user != null)
                {
                    Session["ssUser"] = user;
                    if (user.Username == "ldphong")
                    {
                        return RedirectToAction("Index", "Admin/Default");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.msg = "Tên đăng nhập không tồn tại";
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Xoá session
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

    }
}