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
    
    public partial class HOSOKHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOSOKHACHHANG()
        {
            this.HOSOVISA = new HashSet<HOSOVISA>();
        }
    
        public int MAHS { get; set; }
        public Nullable<int> MAKH { get; set; }
        public Nullable<int> MANV { get; set; }
        public Nullable<int> MALT { get; set; }
        public Nullable<System.DateTime> NGAYTAOHS { get; set; }
        public string FILEHOSO { get; set; }
        public string TRANGTHAIHS { get; set; }
        public string GHICHU { get; set; }
        public Nullable<int> HOCPHI { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOSOVISA> HOSOVISA { get; set; }
        public virtual KHACHHANG KHACHHANG { get; set; }
        public virtual LOTRINHDUHOC LOTRINHDUHOC { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
