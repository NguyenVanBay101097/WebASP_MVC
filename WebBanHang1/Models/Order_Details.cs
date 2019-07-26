namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order Details")]
    public partial class Order_Details
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(6)]
        public string OrderID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string ProductID { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public double? PricePresent { get; set; }

        public int? Quantity { get; set; }

        public double? TotalMoney { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Products Products { get; set; }
    }
}
