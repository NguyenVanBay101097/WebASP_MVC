namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SuKiens
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SuKiens()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int SuKienID { get; set; }

        [StringLength(50)]
        public string TieuDe { get; set; }

        [StringLength(50)]
        public string Picture { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TuNgay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DenNgay { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
