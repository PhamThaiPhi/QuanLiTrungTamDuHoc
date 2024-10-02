using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quanlytrungtam.Models;
namespace quanlytrungtam.Controllers
{
    public class UserController : Controller
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult dangnhap(string EMAIL, string MATKHAU)
        {         
            NHANVIEN tk = db.NHANVIEN.SingleOrDefault(n => n.Email == EMAIL && n.MATKHAU == MATKHAU);
            if (tk != null)
            {
               
                    Session["TAIKHOAN"] = tk;
                    return RedirectToAction("Index", "Home");
              
              
            }
            return RedirectToAction("Index", "User");
        }
        public ActionResult dangxuat()
        {
            Session["TAIKHOAN"] = null;
            return RedirectToAction("Index", "User");
        }
    }
}