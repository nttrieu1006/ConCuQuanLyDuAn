﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;

namespace CongNghePhanMem.Controllers
{
    public class ThongKeController : Controller
    {
        
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ThongKeDTTheoTG(DateTime? froms, DateTime? tos)
        {
            if (froms == null)
                froms = DateTime.Today.AddMonths(-1);
            if (tos == null)
                tos = DateTime.Today;
            ViewBag.froms = froms.Value.ToShortDateString();
            ViewBag.tos = tos.Value.ToShortDateString();
            DonHangKHModel donhang = new DonHangKHModel();
            return PartialView("TheoTime", donhang.ThongKeDoanhThu(froms, tos).ToList());
        }
    }
}