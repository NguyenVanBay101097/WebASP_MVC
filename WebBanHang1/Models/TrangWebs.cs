namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TrangWebs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TrangWebs()
        {
            Quyens = new HashSet<Quyens>();
        }

        [Key]
        [StringLength(50)]
        public string TenTW { get; set; }

        [StringLength(50)]
        public string MoTa { get; set; }

        public bool? IsAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Quyens> Quyens { get; set; }
    }
}
