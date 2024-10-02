﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using quanlytrungtam.Models;

namespace quanlytrungtam.Controllers
{
    public class QuanLyNhanVienController : Controller
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();
        // GET: QuanLyNhanVien
        public ActionResult Index()
        {
            return View(db.NHANVIEN.OrderBy(n=>n.MANV));
        }
        [HttpGet]
        public ActionResult ThemNhanvien()
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemNhanvien(NHANVIEN nv)
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
            nv.MATKHAU = nv.SDT;
            db.NHANVIEN.Add(nv);
            db.SaveChanges();

            return RedirectToAction("Index", "QuanLyNhanVien");
        }

        public ActionResult editNhanvien(int? id)
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHANVIEN nv = db.NHANVIEN.SingleOrDefault(n => n.MANV == id);
            if (nv == null) return HttpNotFound();
            return View(nv);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult editNhanvien(NHANVIEN nv)
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
            db.Entry(nv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyNhanVien");
        }
        public ActionResult xoaNhanvien(int? id)
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHANVIEN nv = db.NHANVIEN.SingleOrDefault(n => n.MANV == id);
            if (nv == null) return HttpNotFound();
            return View(nv);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult xoaNhanvien(int id)
        {
            ViewBag.MACV = new SelectList(db.CHUCVU.OrderBy(n => n.MACV), "MACV", "TENCV");
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            NHANVIEN nv = db.NHANVIEN.SingleOrDefault(n => n.MANV == id);
            if (nv == null) return HttpNotFound();
            db.NHANVIEN.Remove(nv);
            db.SaveChanges();
            return RedirectToAction("Index", "QuanLyNhanVien");
        }


    }
}