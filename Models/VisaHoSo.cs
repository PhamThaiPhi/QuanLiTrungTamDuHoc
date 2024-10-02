using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quanlytrungtam.Models
{
    public class VisaHoSo
    {
        public HOSOVISA HOSOVISA { get; set; }
        public HOSOKHACHHANG HOSOKHACHHANG { get; set; }
        public KHACHHANG KHACHHANG { get; set; }
        public string GHICHU { get; set; }  
    }
}