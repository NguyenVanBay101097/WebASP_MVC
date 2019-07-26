namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Quyens
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(50)]
        public string TenVT { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string TenTW { get; set; }

        public bool Xem { get; set; }

        public bool Them { get; set; }

        public bool Xoa { get; set; }

        public bool Sua { get; set; }

        public virtual TrangWebs TrangWebs { get; set; }

        public virtual VaiTros VaiTros { get; set; }
    }
}
