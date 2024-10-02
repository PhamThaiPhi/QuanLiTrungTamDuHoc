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
    public class HomeClienController : Controller
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();
        // GET: HomeClien
        public ActionResult Index()
        {
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View();
        }
       
        public ActionResult truongdh(int ? MANUOC)
        {
            if (MANUOC == null)
            {
                return View(db.TRUONGDAIHOC.OrderBy(n => n.MATDH));
            }
            ViewBag.MANUOC = MANUOC;
            return View(db.TRUONGDAIHOC.Where(n => n.MANUOC == MANUOC).OrderBy(n => n.MATDH));
        }
        public ActionResult truongDhNuoc(int? MANUOC)
        {
            if (MANUOC == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.MANUOC = MANUOC;
            return View(db.TRUONGDAIHOC.Where(n=>n.MANUOC==MANUOC).OrderBy(n => n.MATDH));
        }
       
        public ActionResult deltailDH(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           TRUONGDAIHOC tdh = db.TRUONGDAIHOC.SingleOrDefault(n => n.MATDH == id);

            if (tdh == null) return HttpNotFound();

            return View(tdh);

        }
        [HttpGet]
        public ActionResult datlichtuvan()
        {
           
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult datlichtuvan(LICHHENTUVAN ltv)
        {
           
            db.LICHHENTUVAN.Add(ltv);
            //ltv.TRANGTHAI = "Chưa liên hệ";
            db.SaveChanges();

            TempData["result"] = "Bạn đã đặt lịch hẹn tư vấn thành công!";
            return RedirectToAction("Index", "HomeClien");
        }
        public ActionResult hocbong(int? MANUOC)
        {
            if (MANUOC == null)
            {
                return View(db.HOCBONG.OrderBy(n => n.MAHB));
            }
            ViewBag.MANUOC = MANUOC;
            return View(db.HOCBONG.Where(n => n.MANUOC == MANUOC).OrderBy(n => n.MAHB));
        }
        public ActionResult detailHocBong(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            HOCBONG hb = db.HOCBONG.SingleOrDefault(n => n.MAHB == id);

            if (hb == null) return HttpNotFound();

            return View(hb);

        }
        [HttpGet]
        public ActionResult nophoso()
        {
            if (Session["KHACHHANG"] != null && Session["KHACHHANG"] is KHACHHANG)
            {
                ViewBag.MALT = new SelectList(db.LOTRINHDUHOC.OrderBy(n => n.MALT), "MALT", "TENLT");
                var HoSoKh = new KhachHangHoSo();

                KHACHHANG khachhang = (KHACHHANG)Session["KHACHHANG"];
                HoSoKh.KHACHHANG = khachhang;

                return View(HoSoKh);
            }

            return RedirectToAction("dangnhap", "HomeClien");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult nophoso(KhachHangHoSo khhs, HttpPostedFileBase FILEHOSO)
        {
            if (Session["KHACHHANG"] != null && Session["KHACHHANG"] is KHACHHANG)
            {
                KHACHHANG khachhang = (KHACHHANG)Session["KHACHHANG"];

                if (FILEHOSO != null)
                {
                    var fileName = Path.GetFileName(FILEHOSO.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);

                    if (System.IO.File.Exists(path))
                    {
                        khhs.HOSOKHACHHANG.FILEHOSO = fileName;
                    }
                    else
                    {
                        FILEHOSO.SaveAs(path);
                        khhs.HOSOKHACHHANG.FILEHOSO = fileName;
                    }
                }

                ViewBag.MALT = new SelectList(db.LOTRINHDUHOC.OrderBy(n => n.MALT), "MALT", "TENLT");
                khhs.HOSOKHACHHANG.MAKH = khachhang.MAKH;
                //db.KHACHHANGs.Add(khhs.KHACHHANG);
                //db.SaveChanges();

                khhs.HOSOKHACHHANG.NGAYTAOHS = DateTime.Now;
                khhs.HOSOKHACHHANG.TRANGTHAIHS = "Chưa duyệt";
                db.HOSOKHACHHANG.Add(khhs.HOSOKHACHHANG);
                db.SaveChanges();

                TempData["result"] = "Nộp hồ sơ thành công !";
                return RedirectToAction("Index", "HomeClien");
            }
            return RedirectToAction("dangnhap", "HomeClien");
        }

        [HttpGet]
        public ActionResult nopvisa()
        {
            if (Session["KHACHHANG"] != null && Session["KHACHHANG"] is KHACHHANG)
            {
                ViewBag.MAHS = new SelectList(db.HOSOKHACHHANG.OrderBy(n => n.MAHS), "MAHS");
                var HoSoVisa = new VisaHoSo();

                KHACHHANG khachhang = (KHACHHANG)Session["KHACHHANG"];
                //HOSOKHACHHANG hskh = (HOSOKHACHHANG)Session["HOSOKHACHHANG"];
                //HoSoVisa.HOSOKHACHHANG = hskh;
                HoSoVisa.KHACHHANG = khachhang;
                return View(HoSoVisa);
            }
            return RedirectToAction("dangnhap", "HomeClien");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult nopvisa(VisaHoSo hosovisa, HttpPostedFileBase FILEHOSOVISA)
        {
            
            if (Session["KHACHHANG"] != null && Session["KHACHHANG"] is KHACHHANG)
            {
                
                HOSOKHACHHANG hskh = (HOSOKHACHHANG)Session["HOSOKHACHHANG"];
                KHACHHANG tk = (KHACHHANG)Session["KHACHHANG"];
                //HOSOVISA dhupdate = db.HOSOVISAs.SingleOrDefault(n => n.MAHSV == hosovisa.HOSOVISA.MAHSV);

                if (FILEHOSOVISA != null)
                {
                    if (hosovisa.HOSOVISA == null)
                    {
                        hosovisa.HOSOVISA = new HOSOVISA(); // Create a new instance if it's null
                    }
                    // lấy tên hình ảnh
                    var fileName = Path.GetFileName(FILEHOSOVISA.FileName);
                    //lấy hình ảnh chuyển vào thư mục hình ảnh
                    var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        hosovisa.HOSOVISA.FILEHOSOVISA = fileName;
                    }
                    else
                    {
                        FILEHOSOVISA.SaveAs(path);
                        hosovisa.HOSOVISA.FILEHOSOVISA = fileName;

                    }
                }
                ViewBag.MAHS = new SelectList(db.HOSOKHACHHANG.OrderBy(n => n.MAHS), "MAHS");
                ViewBag.MAKH = new SelectList(db.HOSOKHACHHANG.OrderBy(n => n.MAKH), "MAKH", "HOTEN");

                try
                {
                    hosovisa.HOSOVISA.NGAYNOPHS = DateTime.Now;
                    hosovisa.HOSOVISA.MAKH = tk.MAKH;
                    hosovisa.HOSOVISA.TRANGTHAIHS = "Chưa duyệt";
                    //hosovisa.HOSOVISA.GHICHU = GHICHU;
                    hosovisa.HOSOVISA.GHICHU = hosovisa.GHICHU;
                    db.HOSOVISA.Add(hosovisa.HOSOVISA);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Exception innerException = ex;
                    while (innerException != null)
                    {
                        Console.WriteLine(innerException.Message);
                        innerException = innerException.InnerException;
                    }
                }

                //hosovisa.HOSOVISA.MAKH = hosovisa.KHACHHANG.MAKH;
                TempData["result"] = "Nộp hồ sơ thành công !";
                return RedirectToAction("Index", "HomeClien");
            }
            TempData["error"] = "Nộp hồ sơ thất bại !";
            return RedirectToAction("Index", "HomeClien");
        }
        [HttpGet]
        public ActionResult dangky()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult dangky(KHACHHANG user)
        {
            user.ROLES = 2;
            db.KHACHHANG.Add(user);
            db.SaveChanges();
            TempData["result"] = "Đăng ký thành công !";
            return RedirectToAction("Index", "HomeClien");

        }
        [HttpGet]
        public ActionResult dangnhap()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult dangnhap(string TAIKHOAN, string MATKHAU)
        {
            var user = db.KHACHHANG.FirstOrDefault(u => u.TAIKHOAN == TAIKHOAN && u.MATKHAU == MATKHAU);
            var admin = db.NHANVIEN.FirstOrDefault(d => d.TAIKHOAN == TAIKHOAN && d.MATKHAU == MATKHAU);

            if (user != null)
            {
                if (user.ROLES == 2)
                {
                    // Đăng nhập thành công bằng tài khoản User với roles là 1
                    // Lưu thông tin người dùng vào Session
                    Session["KHACHHANG"] = user;
                    return RedirectToAction("Index", "HomeClien");
                }
            }

            if (admin != null)
            {
                if (admin.ROLES == 1)
                {
                    // Đăng nhập thành công bằng tài khoản User với roles là 2
                    // Lưu thông tin người dùng vào Session
                    Session["NHANVIEN"] = admin;
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "HomeClien");
        }

        public ActionResult dangxuat(string TAIKHOAN, string MATKHAU)
        {
            // Xóa giá trị của DocGiaRoles trong Session
            Session["NHANVIEN"] = null;
            Session["KHACHHANG"] = null;
            // Sau đó chuyển hướng đến trang chủ
            return RedirectToAction("Index", "HomeClien");
        }
        [HttpGet]
        public ActionResult tinhtranghskh(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            KHACHHANG tk = db.KHACHHANG.SingleOrDefault(n => n.MAKH == id);
            return View(tk);
        }
        public ActionResult thanhtoan(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            KHACHHANG kh = db.KHACHHANG.SingleOrDefault(n => n.MAKH == id);
            HOSOKHACHHANG hskh = db.HOSOKHACHHANG.SingleOrDefault(n => n.MAKH == kh.MAKH);
            LOTRINHDUHOC lotrinh = db.LOTRINHDUHOC.SingleOrDefault(n => n.MALT == hskh.MALT);
            HOADON hd = new HOADON();
            hd.MAKH = kh.MAKH;
            hd.NGAYDAT = DateTime.Now;
            hd.TINHTRANGHOADON = 0;
            hd.DATHANHTOAN = 0;
            db.HOADON.Add(hd);
            db.SaveChanges();
            
            CHITIETHOADON cthd = new CHITIETHOADON();
            cthd.MALT = lotrinh.MALT;
            cthd.MAHD = hd.MAHD;
            cthd.TENLT = lotrinh.TENLT;

            // Check if the user clicked "Cọc tiền" button
            if (Request.Form["btnCocTien"] != null)
            {
                // Reduce CHIPHI by 20%
                int chiphiAsDecimal = (int)lotrinh.CHIPHI;
                hskh.HOCPHI = (int?)(decimal)(chiphiAsDecimal * 0.8);
                hskh.TRANGTHAIHS = "Đã cọc";// Chuyển đổi về int sau khi giảm giá
            }
            else
            {
                hskh.HOCPHI = (int)hskh.HOCPHI; // Chuyển đổi về int nếu cần
            }
            if (Request.Form["btnCocTienall"] != null)
            {
                // Reduce CHIPHI by 20%
                int chiphiAsDecimal = (int)lotrinh.CHIPHI;
                hskh.HOCPHI = (int?)(decimal)(chiphiAsDecimal * 0);
                hskh.TRANGTHAIHS = "HOÀN TẤT HỒ SƠ";// Chuyển đổi về int sau khi giảm giá
            }
            else
            {
                hskh.HOCPHI = (int)hskh.HOCPHI; // Chuyển đổi về int nếu cần
            }



            db.CHITIETHOADON.Add(cthd);
            db.SaveChanges();

            return RedirectToAction("tinhtranghskh", "HomeClien",new {id = kh.MAKH } );
        }



    }
}