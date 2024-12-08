using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebPTCOM.Models;

namespace WebPTCOM.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Admin/Product/Index
        public ActionResult Index()
        {
            ProductDAL obj = new ProductDAL();
            ModelState.Clear();
            return View(obj.GetAll());
        }

        // GET: Admin/Product/Insert
        public ActionResult Insert()
        {
            return View();
        }

        // POST: Admin/Product/Insert
        [HttpPost]
        public ActionResult Insert(Product obj)
        {
            HttpPostedFileBase file = Request.Files["Photo"];

            if (ModelState.IsValid)
            {
                // Lưu tên file
                string fileName = Path.GetFileName(file.FileName);

                // Lưu đường dẫn
                string path = Path.Combine(Server.MapPath("~/Uploads/Photo"), fileName);

                // Kiểm tra hình có tồn tại hay không
                if (System.IO.File.Exists(path))
                {
                    ViewBag.msg = "Hình Upload đã tồn tại";
                }
                else
                {
                    // Lưu Hình
                    file.SaveAs(path);
                }

                obj.Photo = fileName;
                // Lưu vào CSDL
                ProductDAL productDAL = new ProductDAL();
                productDAL.Insert(obj);
                return RedirectToAction("Index");
            }
            return View("Create", obj);
        }

        // POST: Admin/Product/Edit
        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            ProductDAL pro = new ProductDAL();
            return View(pro.GetById(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int Id, [Bind] Product obj)
        {
            if (ModelState.IsValid)
            {
                ProductDAL dal = new ProductDAL();
                dal.Update(obj);
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public ActionResult Delete(int Id)
        {
            ProductDAL dal = new ProductDAL();
            if (dal.Delete(Id)) { return RedirectToAction("Index"); }
            return View();
        }
    }
}