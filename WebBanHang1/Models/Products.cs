namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Order_Details = new HashSet<Order_Details>();
            SuKiens = new HashSet<SuKiens>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(6)]
        public string ProductID { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(50)]
        public string Picture1 { get; set; }

        [StringLength(50)]
        public string Picture2 { get; set; }

        [StringLength(50)]
        public string Picture3 { get; set; }

        [StringLength(50)]
        public string Picture4 { get; set; }

        [StringLength(50)]
        public string Picture { get; set; }

        [Column(TypeName = "ntext")]
        public string InfoDetail { get; set; }

        public bool? Discount { get; set; }

        public bool? ProductHot { get; set; }

        public double? UnitPrice { get; set; }

        public double? PricePresent { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PostDate { get; set; }

        //[Required]
        [StringLength(6)]
        public string CategoryChildID { get; set; }

        public int? SoLuongTrongKho { get; set; }

        public virtual Category_Childs Category_Childs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Details> Order_Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuKiens> SuKiens { get; set; }


        public double ThanhTien
        {
            get
            {
                if (PricePresent==null)
                {
                    return 1;
                }
                return (double)PricePresent * SoLuong;
            }
        }
        public int SoLuong = 0;
    }
}
