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
    public class DonHangController : Controller
    {
        // GET: DonHang
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

        public ActionResult DonHang(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var dh = cn.DonDatHangs.Where(n => n.TinhTrangGiaoHang == null || n.DaThanhToan == null).ToList().OrderBy(n => n.MaDH).ToPagedList(pageNumber, pageSize);
            return View(dh);
        }
    }
}