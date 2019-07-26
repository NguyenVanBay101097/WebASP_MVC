namespace WebBanHang1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BanHang : DbContext
    {
        public BanHang()
            : base("name=BanHang")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Category_Childs> Category_Childs { get; set; }
        public virtual DbSet<Category_Fathers> Category_Fathers { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Quyens> Quyens { get; set; }
        public virtual DbSet<SuKiens> SuKiens { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TrangWebs> TrangWebs { get; set; }
        public virtual DbSet<VaiTros> VaiTros { get; set; }
        public virtual DbSet<Lienlac> Lienlac { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region đồng bộ 
            Database.SetInitializer<BanHang>(null);
            base.OnModelCreating(modelBuilder);
            #endregion
            modelBuilder.Entity<Category_Childs>()
                .Property(e => e.CategoryChildID)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Childs>()
                .Property(e => e.CategoryFatherID)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Childs>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category_Childs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category_Fathers>()
                .Property(e => e.CategoryFatherID)
                .IsUnicode(false);

            modelBuilder.Entity<Category_Fathers>()
                .HasMany(e => e.Category_Childs)
                .WithRequired(e => e.Category_Fathers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Customers>()
                .Property(e => e.TenDN)
                .IsUnicode(false);

            modelBuilder.Entity<Customers>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.TenVT)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.TenDN)
                .IsUnicode(false);

            modelBuilder.Entity<Employees>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Orders)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.ProductID)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.CategoryChildID)
                .IsUnicode(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.SuKiens)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("EventProduct").MapLeftKey("ProductID").MapRightKey("SuKienID"));

            modelBuilder.Entity<Quyens>()
                .Property(e => e.TenVT)
                .IsUnicode(false);

            modelBuilder.Entity<Quyens>()
                .Property(e => e.TenTW)
                .IsUnicode(false);

            modelBuilder.Entity<TrangWebs>()
                .Property(e => e.TenTW)
                .IsUnicode(false);

            modelBuilder.Entity<TrangWebs>()
                .HasMany(e => e.Quyens)
                .WithRequired(e => e.TrangWebs)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VaiTros>()
                .Property(e => e.TenVT)
                .IsUnicode(false);

            modelBuilder.Entity<VaiTros>()
                .HasMany(e => e.Quyens)
                .WithRequired(e => e.VaiTros)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Lienlac>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Lienlac>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Lienlac>()
                .Property(e => e.adress)
                .IsUnicode(false);

            modelBuilder.Entity<Lienlac>()
                .Property(e => e.phone)
                .IsUnicode(false);
        }
    }
}
