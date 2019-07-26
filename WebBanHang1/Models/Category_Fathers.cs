namespace WebBanHang1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category Fathers")]
    public partial class Category_Fathers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category_Fathers()
        {
            Category_Childs = new HashSet<Category_Childs>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [StringLength(6)]
        public string CategoryFatherID { get; set; }

        [StringLength(50)][Required(ErrorMessage ="nhập tên danh mục")]
        public string CategoryName { get; set; }

        [Column(TypeName = "ntext")]
        public string icon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category_Childs> Category_Childs { get; set; }
    }
}
