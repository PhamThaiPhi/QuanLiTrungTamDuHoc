using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quanlytrungtam.Models;
using System.IO;
using System.Net;

namespace quanlytrungtam.Controllers
{
    public class QuanLyHoSoKHController : Controller
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();
        // GET: QuanLyHoSoKH
        public ActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(db.HOSOKHACHHANG);
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
            HOSOKHACHHANG hskh = db.HOSOKHACHHANG.SingleOrDefault(n => n.MAHS == id);
            if (hskh == null) return HttpNotFound();
            return View(hskh);
        }
        [HttpPost]
        public ActionResult editHoso(HOSOKHACHHANG hs,int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            /*            var hoso = (from hs in db.HOSOKHACHHANGs join kh in db.KHACHHANGs on hs.MAKH equals kh.MAKH where hs.MAKH == id select new { hs.MAHS }).FirstOrDefault();
            */
            HOSOKHACHHANG hskh = db.HOSOKHACHHANG.SingleOrDefault(n => n.MAHS == id);
            ////if (hskh != null)
            ////{
            ////    hskh.KHACHHANG.HOTEN = hs.KHACHHANG.HOTEN;
            ////    hskh.KHACHHANG.NGAYSINH = hs.KHACHHANG.NGAYSINH;
            ////    hskh.KHACHHANG.GIOITINH = hs.KHACHHANG.GIOITINH;
            ////    hskh.KHACHHANG.DIACHI = hs.KHACHHANG.DIACHI;
            ////    hskh.KHACHHANG.SDT = hs.KHACHHANG.SDT;
            ////    hskh.KHACHHANG.Email = hs.KHACHHANG.Email;

            ////    db.SaveChanges();
            ////    return RedirectToAction("Index");
            ////}
            if (hskh == null)
            {
                return HttpNotFound();
            }
            //hskh.KHACHHANG.HOTEN = hs.KHACHHANG.HOTEN;
            return View(hskh);
        }

        public ActionResult xoahskh(int? id)
        {
        
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            HOSOKHACHHANG hskh = db.HOSOKHACHHANG.SingleOrDefault(n => n.MAHS == id);
            if (hskh == null) return HttpNotFound();
            return View(hskh);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult xoahskh(int id)
        {
           
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            HOSOKHACHHANG hskh = db.HOSOKHACHHANG.SingleOrDefault(n => n.MAHS == id);
            if (hskh == null) return HttpNotFound();
            db.HOSOKHACHHANG.Remove(hskh);
            db.SaveChanges();
            TempData["result"] = "Xóa thành công !";

            return RedirectToAction("Index", "QuanLyHoSoKH");
        }
        [HttpGet]
        public ActionResult duyeths(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            return View(db.HOSOKHACHHANG.SingleOrDefault(n => n.MAHS == id));
        }
        [HttpPost]
        public ActionResult duyeths(HOSOKHACHHANG hskh)
        {
            HOSOKHACHHANG ddupdate = db.HOSOKHACHHANG.Single(n => n.MAHS == hskh.MAHS);
            ddupdate.TRANGTHAIHS = hskh.TRANGTHAIHS;
            ddupdate.NGAYTAOHS = DateTime.Now;
            KHACHHANG kh = db.KHACHHANG.SingleOrDefault(n => n.MAKH == ddupdate.MAKH);
            LOTRINHDUHOC ltdh = db.LOTRINHDUHOC.SingleOrDefault(n => n.MALT == ddupdate.MALT);
            TRUONGDAIHOC tdh = db.TRUONGDAIHOC.SingleOrDefault(n => n.MATDH == ltdh.MATDH);
            db.SaveChanges();
            var Email = kh.Email;
            var HOTEN = kh.HOTEN;
            var TENLT = ltdh.TENLT;
            var TRUONGDH = tdh.TENTDH;
            var DIACHI = tdh.DIACHI;
            var HOCPHI = ltdh.CHIPHI.Value.ToString("#,##");
            TempData["result"] = "Duyệt Hồ sơ Thành công !";
            //return RedirectToAction("AutoMail", "SendMail",new {email=Email ,hoten=HOTEN,tenlt=TENLT,truong=TRUONGDH,diachi=DIACHI,hocphi=HOCPHI});
            return RedirectToAction("Index", "QuanLyHoSoKH");

        }




        public ActionResult Create()
        {
            // Populate the dropdown list for MAKH
            ViewBag.MAKH = new SelectList(db.KHACHHANG, "MAKH", "HOTEN");

            // Populate the dropdown list for MALT
            ViewBag.MALT = new SelectList(db.LOTRINHDUHOC, "MALT", "TENLT");

            return View();
        }


        // POST: QuanLyHoSoKH/Create
        [HttpPost]
        public ActionResult Create(HOSOKHACHHANG hs, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                // Save the uploaded file
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string uploadFolderPath = Path.Combine(Server.MapPath("~/Uploads"));
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath); // Tạo thư mục nếu nó không tồn tại
                    }
                    string filePath = Path.Combine(uploadFolderPath, fileName);
                    file.SaveAs(filePath);
                    hs.FILEHOSO = fileName;
                }

                // Set other properties as needed
                hs.NGAYTAOHS = DateTime.Now;
                hs.TRANGTHAIHS = "Pending"; // Set the initial status

                // Add the new profile to the database
                db.HOSOKHACHHANG.Add(hs);
                db.SaveChanges();

                TempData["result"] = "Tạo hồ sơ thành công!";
                return RedirectToAction("Index");
            }

            return View(hs);
        }
    }
}