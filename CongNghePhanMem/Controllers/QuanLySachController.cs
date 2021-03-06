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
    public class QuanLySachController : Controller
    {
        CongNghePhanMemEntities cn = new CongNghePhanMemEntities();
        // GET: QuanLySach
        public ActionResult Index()
        {
            return View();
        }
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
        public ActionResult ChuDe(int? page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var cd = cn.ChuDes.ToList().OrderBy(n => n.TenChuDe).ToPagedList(pageNumber, pageSize);
            return View(cd);
        }
        [HttpGet]
        public ActionResult ThemCD()
        {
            return View();

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemCD(ChuDe cd)
        {
            if (ModelState.IsValid)
            {
                ChuDe cd1 = new ChuDe();
                cd1.TenChuDe = cd.TenChuDe;
                cn.ChuDes.Add(cd1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("ChuDe", "QuanLySach");
        }
        public ActionResult XoaCD(int MaCD = 0)
        {
            if (ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaCD == MaCD);
                if (sach != null)
                {
                    SetAlert("Tồn tại chủ đề trong quản lý sách!", "warning");
                }
                else
                {
                    ChuDe cd = cn.ChuDes.SingleOrDefault(n => n.MaCD == MaCD);
                    cn.ChuDes.Remove(cd);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }

            }
            return RedirectToAction("ChuDe", "QuanLySach");
        }
        //Viết sách
        public ActionResult VietSach(int? page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var vs = cn.VietSaches.ToList().OrderBy(n => n.MaTG).ToPagedList(pageNumber, pageSize);
            return View(vs);
        }
        [HttpGet]
        public ActionResult ThemVS()
        {
            ViewBag.MaTG = new SelectList(cn.TacGias.ToList(), "MaTG", "TenTG");
            ViewBag.MaSach = new SelectList(cn.Saches.ToList(), "MaSach", "TenSach");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemVS(VietSach vs)
        {
            if (ModelState.IsValid)
            {
                VietSach vs1 = new VietSach();
                vs1.MaTG = vs.MaTG;
                vs1.MaSach = vs.MaSach;
                vs1.VaiTro = vs.VaiTro;
                vs1.ViTri = vs.ViTri;
                cn.VietSaches.Add(vs1);
                cn.SaveChanges();
                SetAlert("Thêm thành công", "success");
            }
            return RedirectToAction("VietSach", "QuanLySach");
        }

        public ActionResult XoaVS(int MaSach = 0, int MaTG = 0)
        {
            if (ModelState.IsValid)
            {
                VietSach vs = cn.VietSaches.SingleOrDefault(n => n.MaSach == MaSach && n.MaTG == MaTG);
                cn.VietSaches.Remove(vs);
                cn.SaveChanges();
                SetAlert("xóa thành công!", "success");
            }
            return RedirectToAction("VietSach", "QuanLySach");
        }
        //Loại sách

        public ActionResult LoaiSach(int? page)
        {
            int pageSize = 25;
            int pageNumber = (page ?? 1);
            var ls = cn.LoaiSaches.ToList().OrderBy(n => n.MaLoai).ToPagedList(pageNumber, pageSize);
            return View(ls);
        }

        public ActionResult XoaLoai(int MaLoai = 0)
        {
            if (ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaLoai == MaLoai);
                if (sach != null)
                {
                    SetAlert("Tồn tại loại trong quản lý sách", "warning");
                }
                else
                {
                    LoaiSach ls = cn.LoaiSaches.SingleOrDefault(n => n.MaLoai == MaLoai);
                    cn.LoaiSaches.Remove(ls);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công!", "success");
                }

            }
            return RedirectToAction("LoaiSach");
        }
        [HttpGet]
        public ActionResult ThemLoai()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemLoai(LoaiSach ls)
        {
            if (ModelState.IsValid)
            {
                LoaiSach ls1 = new LoaiSach();
                ls1.TenLoai = ls.TenLoai;
                cn.LoaiSaches.Add(ls1);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("LoaiSach");
        }
        //Tác giả
        public ActionResult TacGia(int? page)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var tg = cn.TacGias.ToList().OrderBy(n => n.MaTG).ToPagedList(pageNumber, pageSize);
            return View(tg);
        }

        public ActionResult XoaTG(int MaTG = 0)
        {
            if (ModelState.IsValid)
            {
                VietSach vs = cn.VietSaches.FirstOrDefault(n => n.MaTG == MaTG);
                if (vs != null)
                {
                    SetAlert("Tồn tại tác giả trong quản lý viết sách!", "warning");
                }
                else
                {
                    TacGia tg = cn.TacGias.SingleOrDefault(n => n.MaTG == MaTG);
                    cn.TacGias.Remove(tg);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }

            }
            return RedirectToAction("TacGia");

        }

        [HttpGet]
        public ActionResult ThemTG()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]

        public ActionResult ThemTG(TacGia tg)
        {
            if (ModelState.IsValid)
            {
                TacGia t = new TacGia();
                t.TenTG = tg.TenTG;
                t.DiaChi = tg.DiaChi;
                t.TieuSu = tg.TieuSu;
                t.SDT = null;
                cn.TacGias.Add(t);
                cn.SaveChanges();
                SetAlert("Thêm thành công", "success");

            }
            return RedirectToAction("TacGia");
        }

        [HttpGet]
        public ActionResult SuaTG(int MaTG = 0)
        {
            TacGia tg = cn.TacGias.SingleOrDefault(n => n.MaTG == MaTG);
            return View(tg);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTG(TacGia tg)
        {
            if (ModelState.IsValid)
            {
                TacGia tg1 = cn.TacGias.SingleOrDefault(n => n.MaTG == tg.MaTG);
                if (tg1 == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                tg1.TenTG = tg.TenTG;
                tg1.DiaChi = tg.DiaChi;
                tg1.TieuSu = tg.TieuSu;
                cn.SaveChanges();
                SetAlert("Sửa thành công!", "success");
            }
            return RedirectToAction("TacGia");
        }
        //Nhà xuất bản
        public ActionResult NhaXuatBan(int? page)
        {
            int pageSize = 30;
            int pageNumber = (page ?? 1);
            var nxb = cn.NhaXuatBans.ToList().OrderBy(n => n.MaNXB).ToPagedList(pageNumber, pageSize);
            return View(nxb);
        }
        public ActionResult XoaNXB(int MaNXB = 0)
        {
            if (ModelState.IsValid)
            {
                Sach sach = cn.Saches.FirstOrDefault(n => n.MaNXB == MaNXB);
                if (sach != null)
                {
                    SetAlert("Tồn tại nhà xuất bản trong quản lý sách!", "warning");
                }
                else
                {
                    NhaXuatBan nxb = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
                    cn.NhaXuatBans.Remove(nxb);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công!", "success");
                }
            }
            return RedirectToAction("NhaXuatBan");
        }
        [HttpGet]
        public ActionResult ThemNXB()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemNXB(NhaXuatBan n)
        {
            if (ModelState.IsValid)
            {
                NhaXuatBan nn = new NhaXuatBan();
                nn.TenNXB = n.TenNXB;
                nn.DiaChi = n.DiaChi;
                nn.Mail = n.Mail;
                cn.NhaXuatBans.Add(nn);
                cn.SaveChanges();
                SetAlert("Thêm thành công!", "success");
            }
            return RedirectToAction("NhaXuatBan");
        }

        [HttpGet]
        public ActionResult SuaNXB(int MaNXB = 0)
        {
            NhaXuatBan nxb = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            return View(nxb);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaNXB(NhaXuatBan nb)
        {
            if (ModelState.IsValid)
            {
                NhaXuatBan n1 = cn.NhaXuatBans.SingleOrDefault(n => n.MaNXB == nb.MaNXB);
                if (n1 == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                n1.TenNXB = nb.TenNXB;
                n1.DiaChi = nb.DiaChi;
                n1.Mail = nb.Mail;
                cn.SaveChanges();
                SetAlert("Sửa thành công!", "success");
            }
            return RedirectToAction("NhaXuatBan");
        }
        public ActionResult XemSach(int? page)
        {
            int pageSize = 15;
            int pageNumber = (page ?? 1);
            var sach = cn.Saches.ToList().OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize);
            return View(sach);
            //List<Sach> sach = cn.Saches.OrderBy(n => n.TenSach).ToList();
            //return View(sach);
        }

        public ActionResult XoaSach(int MaSach = 0)
        {
            if (ModelState.IsValid)
            {

                Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == MaSach);

                if (sach == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ChiTietDonHang ct = cn.ChiTietDonHangs.Where(n => n.DonDatHang.TinhTrangGiaoHang != true && n.DonDatHang.DaThanhToan != true).FirstOrDefault(n => n.MaSach == MaSach);
                VietSach sach1 = cn.VietSaches.FirstOrDefault(n => n.MaSach == MaSach);
                if (ct != null || sach1 != null)
                {
                    SetAlert("Tồn tại sách trong đơn hàng hoặc tham gia viết sách!", "warning");
                }
                else
                {
                    cn.Saches.Remove(sach);
                    cn.SaveChanges();
                    SetAlert("Xóa thành công", "success");
                }
            }
            return RedirectToAction("XemSach");
        }
        [HttpGet]
        public ActionResult ThemSach()
        {
            ViewBag.MaCD = new SelectList(cn.ChuDes.ToList(), "MaCD", "TenChuDe");
            ViewBag.MaLoai = new SelectList(cn.LoaiSaches.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNXB = new SelectList(cn.NhaXuatBans.ToList(), "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSach(Sach sach, HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileupload.FileName);
                var path = Path.Combine(Server.MapPath("~/image/Sach"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại...";
                    SetAlert("Hình ảnh đã tồn tại", "warning");
                }
                else
                {
                    Sach sach1 = new Sach();
                    fileupload.SaveAs(path);
                    sach1.TenSach = sach.TenSach;
                    sach1.AnhBia = fileName;
                    sach1.SLTon = sach.SLTon;
                    sach1.NgayCapNhat = DateTime.Now;
                    sach1.GiaBan = sach.GiaBan;
                    sach1.MaCD = sach.MaCD;
                    sach1.MaLoai = sach.MaLoai;
                    sach1.MaNXB = sach.MaNXB;
                    sach1.SachMoi = sach.SachMoi;
                    sach1.MoTa = sach.MoTa;
                    cn.Saches.Add(sach1);
                    cn.SaveChanges();
                    SetAlert("Thêm thành công!", "success");
                }
            }
            return RedirectToAction("XemSAch", "QuanLySach");
        }
        [HttpGet]
        public ActionResult SuaSach(int MaSach = 0)
        {
            ViewBag.MaCD = new SelectList(cn.ChuDes.ToList(), "MaCD", "TenChuDe");
            ViewBag.MaLoai = new SelectList(cn.LoaiSaches.ToList(), "MaLoai", "TenLoai");
            ViewBag.MaNXB = new SelectList(cn.NhaXuatBans.ToList(), "MaNXB", "TenNXB");
            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaSach(Sach s)
        {
            //if(ModelState.IsValid)
            //{
            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == s.MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            sach.TenSach = s.TenSach;
            sach.SLTon = s.SLTon;
            sach.GiaBan = s.GiaBan;
            sach.MaCD = s.MaCD;
            sach.MaLoai = s.MaLoai;
            sach.MaNXB = s.MaNXB;
            sach.SachMoi = s.SachMoi;
            sach.MoTa = s.MoTa;
            cn.SaveChanges();
            SetAlert("Sửa thành công!", "QuanLySach");
            //}
            return RedirectToAction("XemSach", "QuanLySach");
        }
        public ActionResult ChiTietSach(int MaSach = 0)
        {

            Sach sach = cn.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public ActionResult TimKiem(FormCollection f, int? page)
        {
            string sTuKhoa = f["txtSearch"].ToString();
            ViewBag.TuKhoa = sTuKhoa;
            List<Sach> lstSearch = cn.Saches.Where(n => n.TenSach.Contains(sTuKhoa)).ToList();
            //phân trang
            int pageNumber = (page ?? 1);
            int pageSize = 18;
            //kiểm tra
            if (lstSearch.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                SetAlert("Không tìm thấy sản phẩm nào!", "warning");
                return View(cn.Saches.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
            }
            ViewBag.ThongBao = "Đã tìm thấy" + lstSearch.Count + "kết quả.";
            SetAlert("Đã tìm thấy:" + lstSearch.Count, "success");
            return View(lstSearch.OrderBy(n => n.TenSach).ToPagedList(pageNumber, pageSize));
        }
    }
}