namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category Childs")]
    public partial class Category_Childs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category_Childs()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(6)]
        public string CategoryChildID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(6)]
        public string CategoryFatherID { get; set; }

        public virtual Category_Fathers Category_Fathers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Products> Products { get; set; }
    }
}
