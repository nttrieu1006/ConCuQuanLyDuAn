using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using System.IO;

namespace CongNghePhanMem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
        public ActionResult Index()
        {
            if (Session["TenDangNhap"] == null || Session["TenDangNhap"].ToString() == "")
            {
                return RedirectToAction("LoginAdmin", "Admin");
            }
            return View();
        }
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAdmin(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                string sTen = f["txtTen"].ToString();
                string sPass = f["txtPass"].ToString();
                NguoiDung nd = cn.NguoiDungs.SingleOrDefault(n => n.TenDangNhap == sTen && n.MatKhau == sPass && n.MaTT == 1);
                if (nd != null)
                {
                    ViewBag.ThongBao = "Thành công";
                    Session["TenDangNhap"] = nd;
                    SetAlert("Đăng nhập thành công!", "success");
                    return RedirectToAction("Index", "Admin");
                }
                ViewBag.ThongBao = "Thất bại";
                return View();
            }
            return View();

        }
        public ActionResult LogOut()
        {
            Session["TenDangNhap"] = null;
            return RedirectToAction("LoginAdmin", "Admin");
        }
        [HttpGet]
        public ActionResult SuaFB()
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == 5);
            if (gd == null)
            {
                Response.StatusCode = 404;
                return null;

            }
            return View(gd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaFB(GiaoDien gd)
        {
            if (ModelState.IsValid)
            {
                GiaoDien gd1 = cn.GiaoDiens.SingleOrDefault(n => n.ID == gd.ID);
                gd1.ThuocTinh = gd.ThuocTinh;
                gd1.GiaTri = gd.GiaTri;
                gd1.GiaTri1 = gd.GiaTri1;
                cn.SaveChanges();
                SetAlert("Sửa địa chỉ mạng xã hội thành công!", "success");

            }
            return View();

        }
    }
}