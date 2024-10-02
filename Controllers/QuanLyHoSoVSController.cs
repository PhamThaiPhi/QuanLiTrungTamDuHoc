using quanlytrungtam.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace quanlytrungtam.Controllers
{
    public class QuanLyHoSoVSController : Controller
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();

        // GET: QuanLyHoSoVS
        public ActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(db.HOSOVISA);
        }

        public ActionResult editHoso(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            /*            var hoso = (from hs in db.HOSOKHACHHANGs join kh in db.KHACHHANGs on hs.MAKH equals kh.MAKH where hs.MAKH == id select new { hs.MAHS }).FirstOrDefault();
            */
            HOSOVISA hsvs = db.HOSOVISA.SingleOrDefault(n => n.MAHSV == id);
            if (hsvs == null) return HttpNotFound();
            return View(hsvs);
        }

        [HttpPost]
        public ActionResult editHoso(HOSOVISA hs, int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            /*            var hoso = (from hs in db.HOSOKHACHHANGs join kh in db.KHACHHANGs on hs.MAKH equals kh.MAKH where hs.MAKH == id select new { hs.MAHS }).FirstOrDefault();
            */

            HOSOVISA hsvs = db.HOSOVISA.SingleOrDefault(n => n.MAHSV == id);
            if (hsvs != null)
            {
                // Update properties of existingHoso with the new values
                hsvs.NGAYNOPHS = hs.NGAYNOPHS;
                hsvs.FILEHOSOVISA = hs.FILEHOSOVISA;
                hsvs.TRANGTHAIHS = hs.TRANGTHAIHS;
                hsvs.GHICHU = hs.GHICHU;

                db.SaveChanges();

                return RedirectToAction("Index"); // Redirect to the appropriate action
            }

            if (hsvs == null) return HttpNotFound();
            return View(hsvs);
        }


        public ActionResult Create()
        {
            // Populate the dropdown list for MAKH
            ViewBag.MAHS = new SelectList(db.HOSOKHACHHANG, "MAHS");

            // Populate the dropdown list for MALT
            ViewBag.MAKH = new SelectList(db.KHACHHANG, "MAKH", "HOTEN");

            return View();
        }


        // POST: QuanLyHoSoVS/Create
        [HttpPost]
        public ActionResult Create(HOSOVISA hs, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded file
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    file.SaveAs(filePath);
                    hs.FILEHOSOVISA = fileName;
                }

                // Set other properties as needed
                hs.NGAYNOPHS = DateTime.Now;
                hs.TRANGTHAIHS = "Pending"; // Set the initial status

                // Add the new profile to the database
                db.HOSOVISA.Add(hs);
                db.SaveChanges();

                TempData["result"] = "Tạo hồ sơ thành công!";
                return RedirectToAction("Index");
            }

            return View(hs);
        }

        public ActionResult xoahsvs(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            HOSOVISA hsvs = db.HOSOVISA.SingleOrDefault(n => n.MAHSV == id);
            if (hsvs == null) return HttpNotFound();
            return View(hsvs);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult xoahsvs(int id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            HOSOVISA hsvs = db.HOSOVISA.SingleOrDefault(n => n.MAHSV == id);
            if (hsvs == null) return HttpNotFound();
            db.HOSOVISA.Remove(hsvs);
            db.SaveChanges();
            TempData["result"] = "Xóa thành công !";

            return RedirectToAction("Index", "QuanLyHoSoVS");
        }

        [HttpGet]
        public ActionResult duyethsvs(int? id)
        {
           if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           return View(db.HOSOVISA.SingleOrDefault(n => n.MAHSV == id));
        }
        [HttpPost]
        public ActionResult duyethsvs(HOSOVISA hsvs)
        {
            HOSOVISA ddupdate = db.HOSOVISA.Single(n => n.MAHSV == hsvs.MAHSV);
            ddupdate.TRANGTHAIHS = hsvs.TRANGTHAIHS;
            ddupdate.NGAYNOPHS = DateTime.Now;
            //ddupdate.GHICHU = hsvs.GHICHU;
            KHACHHANG kh = db.KHACHHANG.SingleOrDefault(n => n.MAKH == ddupdate.MAKH);
            //HOSOKHACHHANG hskh = db.HOSOKHACHHANGs.SingleOrDefault(n => n.MAHS == ddupdate.MAHS);
            db.SaveChanges();
            var Email = kh.Email;
            var HOTEN = kh.HOTEN;
            var SDT = kh.SDT;
            var DIACHI = kh.DIACHI;
            var GIOITINH = kh.GIOITINH;
            var NGAYSINH = kh.NGAYSINH;
            //var FileHS = hskh.FILEHOSO;
            //var Ghichu = hsvs.GHICHU;
            TempData["result"] = "Duyệt Hồ sơ Thành công !";
            return RedirectToAction("AutoMail2", "SendMail", new { email = Email, hoten = HOTEN,sdt = SDT,diachi = DIACHI, ngaysinh = NGAYSINH});
        }

    }
}