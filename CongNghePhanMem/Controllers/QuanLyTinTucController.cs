﻿using System;
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
    }
}