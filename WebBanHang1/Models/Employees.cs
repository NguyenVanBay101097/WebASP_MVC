namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }

        [StringLength(50)][Required(ErrorMessage ="nhập tên nhân viên!")]
        public string EmployeeName { get; set; }
        //save sao huy bo ra ok chưa tư có phone gi? kms phone, t run thu nha

        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "chọn ngày sinh!")]
        public DateTime? BirthDate { get; set; }

        [StringLength(30)]
        [Required(ErrorMessage = "nhập số điện thoại nhân viên!")]
        public string Phone { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "nhập địa chỉ nhân viên!")]
        public string Address { get; set; }
        [Required(ErrorMessage = "chọn giới tính nhân viên!")]
        public bool? gioitinh { get; set; }

        [Required(ErrorMessage = "chọn ảnh nhân viên!")]
        [StringLength(200)]
        public string Picture { get; set; }

        [StringLength(50)]
        public string TenVT { get; set; }

        [Required(ErrorMessage = "nhập tên đăng nhập nhân viên!")]
        [StringLength(50)]
        public string TenDN { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "mật khẩu đăng nhập nhân viên!")]
        public string MatKhau { get; set; }
     
    }
}
