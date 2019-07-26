namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lienlac")]
    public partial class Lienlac
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(6)]
        public string id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(200)]
        public string adress { get; set; }

        [Column(TypeName = "ntext")]
        public string contents { get; set; }

        [StringLength(50)]
        public string phone { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? date { get; set; }

        public bool status { get; set; }
        public bool Xem { get; set; }

    }
}
