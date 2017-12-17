using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CongNghePhanMem.Models;

namespace CongNghePhanMem.Controllers
{
    public class TinTucController : Controller
    {
        // GET: TinTuc
        public ActionResult Index()
        {
            return View();
        }
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        public ActionResult TinTucPartial()
        {
            var lst = cn.TinTucs.ToList();
            return PartialView(lst);
        }
    }
}