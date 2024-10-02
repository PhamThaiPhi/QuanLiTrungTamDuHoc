using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quanlytrungtam.Models;


namespace quanlytrungtam.Controllers
{
    public class QuanLyThuChiController : Controller
    {

        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();
        // GET: QuanLyThuChi
        
       
        public ActionResult Index(int? id)
        {
            if (id == 0)
            {
                return View(db.HOADON.OrderByDescending(n => n.MAHD).Where(n=>n.DATHANHTOAN==id));
            }
            return View(db.HOADON.OrderByDescending(n=>n.MAHD));
        }
        [HttpGet]
        public ActionResult duyethoadon(int ?id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            HOADON hd = db.HOADON.SingleOrDefault(n => n.MAHD == id);
            return View(hd);
        }
        [HttpPost]
        public ActionResult duyethoadon(HOADON hd)
        {
           
            HOADON hdupdate = db.HOADON.SingleOrDefault(n => n.MAHD == hd.MAHD);
            hdupdate.DATHANHTOAN = hd.DATHANHTOAN;
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyThuChi");
        }
        public ActionResult xoahd(int? id)
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
        public ActionResult xoahd(int id)
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
    }
}