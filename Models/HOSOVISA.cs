//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace quanlytrungtam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOSOVISA
    {
        public int MAHSV { get; set; }
        public Nullable<int> MAHS { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<System.DateTime> NGAYNOPHS { get; set; }
        public string FILEHOSOVISA { get; set; }
        public string TRANGTHAIHS { get; set; }
        public string GHICHU { get; set; }
    
        public virtual HOSOKHACHHANG HOSOKHACHHANG { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
