using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;


namespace CongNghePhanMem.Controllers
{
    public class QuanLyTinTucController : Controller
    {
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: QuanLyTinTuc
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
        public ActionResult YKien(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var pn = cn.PhanHois.Where(n => n.TraLoi == null).ToList().OrderBy(n => n.MaPH).ToPagedList(pageNumber, pageSize);
            return View(pn);
        }
        [HttpGet]
        [ValidateInput(false)]
        public ActionResult TraLoi(int MaPH = 0)
        {
            PhanHoi ph = cn.PhanHois.SingleOrDefault(n => n.MaPH == MaPH);
            return View(ph);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TraLoi(PhanHoi ph)
        {
            if (ModelState.IsValid)
            {
                PhanHoi ph1 = cn.PhanHois.SingleOrDefault(n => n.MaPH == ph.MaPH);
                if (ph1 == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ph1.TraLoi = ph.TraLoi.ToString();
                cn.SaveChanges();
                SetAlert("Trả lời thành công!", "success");
            }
            return RedirectToAction("YKien", "QuanLyTinTuc");


        }
        public ActionResult XoaYKien(int MaPH = 0)
        {
            if (ModelState.IsValid)
            {
                PhanHoi ph = cn.PhanHois.SingleOrDefault(n => n.MaPH == MaPH);
                cn.PhanHois.Remove(ph);
                cn.SaveChanges();
                SetAlert("Xóa thành công", "success");
            }
            return View();
        }
        //Tin Tức
        public ActionResult TinTuc(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var tt = cn.TinTucs.ToList().OrderBy(n => n.MaTT).ToPagedList(pageNumber, pageSize);
            return View(tt);
        }
    }
}