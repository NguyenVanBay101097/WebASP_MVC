using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebBanHang1.Models
{
    public class BH : IBH
    {
        BanHang db = null;

        public BH()
        {
            this.db = new BanHang();
        }

        public int CapnhatHD(Orders o)
        {
            //var cs = TimHD(o.OrderID);
            db.Entry(o).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }

        public Employees CapnhatNhanvien(Employees c)
        {
            var cs = db.Employees.SingleOrDefault(x => x.TenDN == c.TenDN && x.MatKhau == c.MatKhau);
            if (c.Picture == null)
            {
                c.Picture = (cs).Picture;
            }
            db.Entry(cs).CurrentValues.SetValues(c);
            db.SaveChanges();
            return c;
        }

        //cập nhật sản phẩm
        public int CapnhatSP(Products p)
        {
            var cs = Sanpham(p.ProductID);
            if (p.Picture == null)
            {
                p.Picture = cs.Picture;
            }
            if (p.Picture1 == null)
            {
                p.Picture1 = cs.Picture1;
            }
            if (p.Picture2 == null)
            {
                p.Picture2 = cs.Picture2;
            }
            if (p.Picture3 == null)
            {
                p.Picture3 = cs.Picture3;
            }
            if (p.Picture4 == null)
            {
                p.Picture4 = cs.Picture4;
            }
            db.Entry(p).State = EntityState.Modified;
            db.SaveChanges();
            return 1;
        }

        //cập nhật tài khoản
        public Customers CapnhatTK(Customers c)
        {
            var cs= TimKh(c.TenDN, c.MatKhau);
            if (c.Picture == null)
            {
                c.Picture = (cs).Picture;
            }
            db.Entry(cs).CurrentValues.SetValues(c);
            db.SaveChanges();
            return c;
        }

        //đăng kí
        public int Dangki(string tendn, string matkhau,string ten)
        {

            try
            {
                if (db.Customers.FirstOrDefault(x => x.TenDN == tendn.Trim()) != null)
                {
                    return 0;
                }
                else
                {
                    db.Customers.Add(new Customers() { TenDN=tendn,MatKhau=matkhau,CustomerName=ten});
             //db.Database.ExecuteSqlCommand("insert into Customers values()");

                    db.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {

                return 0;
            }

        }
        public string Dangki_Facebook(Customers cs)
        {
                var user = db.Customers.SingleOrDefault(x => x.TenDN == cs.TenDN);
                if (user != null)
                {
                    return user.CustomerID;
                }
                else
                {
                db.Customers.Add(cs);
                //db.Database.ExecuteSqlCommand("INSERT INTO [dbo].[Customers] ([CustomerName],[TenDN],[MatKhau],[Picture],[Email],[phonenumber],[Address])VALUES(,,,,,,)");

                db.SaveChanges();
                return cs.CustomerID;
                }
            
           

        }
       
        //khách hàng đăng nhập
        public int Dangnhap(string tendn, string matkhau)
        {
            try
            {
                if (db.Customers.FirstOrDefault(x => x.TenDN ==tendn) != null)
                {

                    

                    var kh = db.Customers.Single(x => x.TenDN == tendn && x.MatKhau == matkhau);
                    if (kh != null)
                    {
                        return 1;
                    }
                    return 0;
                }
                return -1;
            }
            catch (Exception)
            {

                return 0;
            }

            
            
            
        }


        //danh sach danh muc cha
        public List<Category_Fathers> DanhsachDanhmuc_cha()
        {
            return db.Category_Fathers.Include(o => o.Category_Childs).ToList();
        }
        //danh mục con từ id cha
        public List<Category_Childs> DanhsachDanhmuc_Con(string idcha)
        {
            return db.Category_Childs.Where(x => x.CategoryFatherID == idcha).ToList();
        }

        //danh sach san pham ban chay
        public List<Products> DanhsachSanphamBanchay()
        {
            //var q = (from p in db.Products
            //        join o in db.Order_Details on p.ProductID equals o.ProductID into list1
            //        from l1 in list1.DefaultIfEmpty()
            //        group l1 by new { l1.ProductID ,l1.ProductName,p.Picture,p.Picture1,p.Picture2,l1.PricePresent} into g
            //        orderby g.Sum(x=>x.Quantity)
            //         select new Products {
            //            ProductID =g.Key.ProductID,
            //        ProductName=g.Key.ProductName,
            //        Picture=g.Key.Picture,
            //        Picture2=g.Key.Picture2,
            //        Picture1=g.Key.Picture1,
            //        PricePresent=g.Key.PricePresent
            //        }).ToList().Select(x=>new Products {
            //            ProductID = x.ProductID,
            //            ProductName = x.ProductName,
            //            Picture = x.Picture,
            //            Picture2 = x.Picture2,
            //            Picture1 = x.Picture1,
            //            PricePresent =234
            //        }).ToList();
            var q = db.Products.DefaultIfEmpty().GroupBy(x => new { x.ProductName,x.ProductID,x.Picture,x.Picture1,x.Picture2,x.PricePresent}).OrderByDescending(x=>x.Sum(g=>g.Order_Details.Sum(t=>t.Quantity))).Select(x=> new  {
                ProductName =x.Key.ProductName,
                ProductID=x.Key.ProductID,
                Picture = x.Key.Picture,
                Picture2 = x.Key.Picture2,
                Picture1 = x.Key.Picture1,
                PricePresent = x.Key.PricePresent
            }).ToList().Select(x=>new Products {
                ProductName = x.ProductName,
                ProductID = x.ProductID,
                Picture = x.Picture,
                Picture2 = x.Picture2,
                Picture1 = x.Picture1,
                PricePresent = x.PricePresent
            }).ToList();
            return q;
            //return db.Products.SqlQuery("select p.ProductID,p.ProductName,p.Picture,p.Picture1,p.Picture2,p.PricePresent,p.UnitPrice from Products p left join [Order Details] on p.ProductID=[Order Details].ProductID group by p.ProductID,p.Picture,p.Picture1,p.Picture2,p.PricePresent,p.UnitPrice,p.ProductName order by SUM(Quantity) desc").ToList();

        }

        public List<Products> DanhsachSanphamKM()
        {
            return db.Products.Where(x=>x.UnitPrice!=null && x.UnitPrice>x.PricePresent).OrderByDescending(x=>x.PostDate).ToList();

        }

        //danh sach san pham moi
        public List<Products> DanhsachSanphamMoi()
        {
            //return db.Products.OrderByDescending(x=>x.PostDate).ToList();
            //return (from sp in db.Products orderby sp.PostDate descending select sp).ToList() ;
            //return db.Products.SqlQuery("select * from Products").ToList();
            return db.Database.SqlQuery<Products>("select * from Products order by PostDate desc").ToList();
        }
        //danh muc cha tu san pham
        public Category_Childs DMCon(string idsp)
        {
            var s = Sanpham(idsp);
            return db.Category_Childs.SingleOrDefault(x=>x.CategoryChildID==s.CategoryChildID);
        }


        //danh sách hóa đơn từ cus id
        public List<Orders> DSHD(string cusID)
        {
            return db.Orders.Where(x => x.CustomerID == cusID).OrderByDescending(x=>x.OrderDate).ToList();
        }
        public List<Orders> DSHD()
        {
            return db.Orders.ToList();
        }
        //extention
        public bool KiemtraAnh(HttpPostedFileBase anh)
        {
            if (anh.ContentType.Contains("image"))
            {
                return true;
            }
            return false;
        }

       

        //tìm sản phẩm theo id
        public Products Sanpham(string id)
        {
            //return db.Products.Single(x=>x.ProductID==id);
            //return db.Products.SqlQuery("select * from Products where ProductID=@p0",id).SingleOrDefault();
            return db.Database.SqlQuery<Products>("select * from Products where ProductID=@p0",id).SingleOrDefault();

        }

        //sản phẩm theo chủng loại
        public List<Products> Sanpham_CL(string cl)
        {
            return db.Products.Where(x => x.Category_Childs.CategoryFatherID == cl).ToList();
        }
        //sản phẩm theo loaoij
        public List<Products> Sanpham_L(string l)
        {
            return db.Products.Where(x => x.Category_Childs.CategoryChildID == l).ToList();

        }
        //thêm liên lạc
        public int Themlienlac(Lienlac l)
        {
            db.Lienlac.Add(l);
            db.SaveChanges();
            return 1;
        }
       
        //Them nhân viên
        public int ThemNV(Employees e)
        { 
            db.Employees.Add(e);
            db.SaveChanges();
            return 1;
        }

        //thêm order
        public void ThemOrder(Orders o)
        {
            db.Orders.Add(o);
            db.SaveChanges();
           
        }
        //thêm chi tiết hóa đơn
        public void ThemOrderdetails(Order_Details o)
        {
            db.Order_Details.Add(o);
            db.SaveChanges();
        }
        //them quyen
        public Quyens TimQuyen(string tenvt, string tentw)
        {
            //var test = db.Quyens.single(x => x.TenTW == tentw && x.TenVT == tenvt);
            return db.Quyens.Single(x=>x.TenTW==tentw&&x.TenVT==tenvt);
        }

        public List<Quyens> ListQuyen()
        {
            return db.Quyens.ToList();
        }
        public bool ThemQuyen(Quyens q)
        {
            try
            {
                var a = db.Quyens.FirstOrDefault(x => x.TenTW == q.TenTW && x.TenVT == q.TenVT);
                if (a==null)
                {
                    db.Quyens.Add(q);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool SuaQuyen(Quyens q)
        {
            try
            {
            db.Entry(q).State = EntityState.Modified;
                
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //thêm sản phẩm
        public int ThemSP(Products p)
        {
            db.Products.Add(p);
            db.SaveChanges();
            return 1;
        }

        //tìm cthd từ id 
        public List<Order_Details> TimCTHD(string orID)
        {
            return db.Order_Details.Where(x=>x.OrderID==orID).Include(x=>x.Products).OrderByDescending(x=>x.Orders.OrderDate).ToList();
        }
        //tìm hd từ id
        public Orders TimHD(string orID)
        {
            return db.Orders.SingleOrDefault(x => x.OrderID == orID);

        }

        //tìm khách hàng tên dn, matkhau
        public Customers TimKh(string tendn, string matkhau)
        {
            return db.Customers.SingleOrDefault(x => x.TenDN == tendn && x.MatKhau == matkhau);
        }
        //tìm khách hàngt ten dang nhapp
        public Customers TimKh(string tendn)
        {
            return db.Customers.SingleOrDefault(x=>x.TenDN==tendn);
        }
        //tìm hóa đơn mới nhất
        public Orders timOrder_moinhat()
        {
            return db.Orders.OrderByDescending(x => x.OrderDate).Take(1).Single();
        }

        //tìm sản phẩm theo keysearch
        public List<Products> Timsanpham(string keysearch)
        {
            return db.Products.Where(x => x.ProductName.Contains(keysearch.Trim()) || x.ProductID.Contains(keysearch.Trim())).OrderByDescending(x => x.PostDate).ToList();
        }
        //xóa đơn hàng
        public void Xoadonhang(Orders o)
        {
            List<Order_Details> ds = db.Order_Details.Where(x=>x.OrderID==o.OrderID).ToList();
            foreach (var item in ds)
            {
                db.Order_Details.Remove(item);
            }
            db.Orders.Remove(o);
            db.SaveChanges();
        }

        public int XoaSP(string id)
        {
            var sp = db.Products.SingleOrDefault(x=>x.ProductID==id);
            db.Products.Remove(sp);
            db.SaveChanges();
            return 1;
        }

        //xóa tài khoản
        public int XoaTK(Customers c)
        {
            var cs = db.Customers.SingleOrDefault(x => x.CustomerID == c.CustomerID);
            if (cs!=null)
            {
                    var dss = db.Orders.Where(x => x.CustomerID == cs.CustomerID);
                    foreach (var item in dss)
                    {
                    List<Order_Details> ds = db.Order_Details.Where(x => x.OrderID == item.OrderID).ToList();
                    foreach (var t in ds)
                    {
                        db.Order_Details.Remove(t);
                    }
                    db.Orders.Remove(item);
                }

                
                   
                db.Customers.Remove(cs); 
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }
        //trang web
        public bool ThemTW(TrangWebs q)
        {
            try
            {
                var a = db.TrangWebs.FirstOrDefault(x=>x.TenTW==q.TenTW);
                if (a==null)
                {
                    db.TrangWebs.Add(q);
                    db.SaveChanges();
                    return true;
                }
                
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
          
        }

        public List<TrangWebs> ListTW()
        {
            return db.TrangWebs.ToList();
        }
        //Vai tro
        public List<VaiTros> ListVT()
        {
            return db.VaiTros.ToList();
        }

        public List<Lienlac> DSLienlac()
        {
            return db.Lienlac.ToList();
        }



        //public  List<Products> danhsach()
        //{
        //    return db.Products.Include(x=>new {x.Category_Childs.Category_Fathers.CategoryName,x.ProductName});
        //}
    }
}