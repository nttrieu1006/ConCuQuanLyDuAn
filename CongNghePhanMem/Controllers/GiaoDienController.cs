using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using PagedList;
using PagedList.Mvc;

namespace CongNghePhanMem.Controllers
{
    public class GiaoDienController : Controller
    {
        // GET: GiaoDien
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public PartialViewResult Logo()
        {
            GiaoDien gd = cn.GiaoDiens.SingleOrDefault(n => n.ID == 1);
            return PartialView(gd);
        }
        public PartialViewResult TieuDe()
        {
            GiaoDien tieude = cn.GiaoDiens.SingleOrDefault(n => n.ID == 2);
            return PartialView(tieude);
        }
        public PartialViewResult DienThoai()
        {
            GiaoDien dt = cn.GiaoDiens.SingleOrDefault(n => n.ID == 3);
            return PartialView(dt);
        }
        public PartialViewResult DiaChi()
        {
            GiaoDien diachi = cn.GiaoDiens.SingleOrDefault(n => n.ID == 4);
            return PartialView(diachi);
        }
        public PartialViewResult FaceBook()
        {
            GiaoDien fb = cn.GiaoDiens.SingleOrDefault(n => n.ID == 5);
            return PartialView(fb);
        }
    }
}