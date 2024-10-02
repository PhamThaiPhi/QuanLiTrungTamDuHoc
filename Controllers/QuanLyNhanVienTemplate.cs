using quanlytrungtam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quanlytrungtam.Controllers
{
    
    public class QuanLyNhanVienTemplateController : NhanVienTemplateController
    {
        private QUANLYTRUNGTAMDUHOCEntities1 db; // Thay "YourDbContext" bằng DbContext thực tế của bạn

        public QuanLyNhanVienTemplateController()
        {
            db = new QUANLYTRUNGTAMDUHOCEntities1(); // Thay "YourDbContext" bằng DbContext thực tế của bạn
        }

        protected override void GetViewData()
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
        }

        protected override void SaveData(NHANVIEN nv)
        {
            db.NHANVIEN.Add(nv);
            db.SaveChanges();
        }
    }
}