namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            Order_Details = new HashSet<Order_Details>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(6)]
        public string OrderID { get; set; }

        public string CustomerID { get; set; }

        public DateTime? OrderDate { get; set; }

        [StringLength(50)]
        public string RecevierName { get; set; }

        [Column(TypeName = "ntext")]
        public string Address { get; set; }

        public bool Status { get; set; }

        public double? TotalMoney { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool Tralai { get; set; }
        public bool Xem { get; set; }

        public virtual Customers Customers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }
    }
}
