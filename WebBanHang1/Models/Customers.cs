namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customers()
        {
            Orders = new HashSet<Orders>();
        }

        [Key]
        [StringLength(6)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerID { get; set; }
        [Column(TypeName = "ntext")]
        public string Address { get; set; }
        [StringLength(50)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string TenDN { get; set; }
        public string Email { get; set; }
        [StringLength(50)]
        public string MatKhau { get; set; }
        [StringLength(50)]
        public string phonenumber { get; set; }
        [StringLength(50)]
        public string Picture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
