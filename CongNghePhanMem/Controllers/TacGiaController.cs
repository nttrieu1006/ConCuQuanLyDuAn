﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;
using PagedList;
using PagedList.Mvc;

namespace CongNghePhanMem.Controllers
{
    public class TacGiaController : Controller
    {
        //GET: TacGia
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public PartialViewResult TenTacGiaPartial()
        {
            var lst = cn.TacGias.ToList();
            return PartialView(lst);
        }
    }
}
