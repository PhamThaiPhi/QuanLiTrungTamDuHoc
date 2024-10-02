using quanlytrungtam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace quanlytrungtam.Controllers
{
    public abstract class NhanVienTemplateController : Controller
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();

        protected abstract void GetViewData();

        protected abstract void SaveData(NHANVIEN nv);

        public ActionResult add()
        {
            GetViewData();
            return View();
        }
        public ActionResult ThemNhanvien()
        {
            GetViewData();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemNhanvien(NHANVIEN nv)
        {
            GetViewData();
            nv.MATKHAU = nv.SDT;
            SaveData(nv);

            return RedirectToAction("Index", "QuanLyNhanVien");
        }
    }
}