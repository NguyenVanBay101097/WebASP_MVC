using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WebBanHang1.Models
{
    interface IBH
    {
        List<Category_Fathers> DanhsachDanhmuc_cha();
        List<Category_Childs> DanhsachDanhmuc_Con(string idcha);
        List<Products> DanhsachSanphamMoi();
        List<Products> DanhsachSanphamKM();
        List<Products> DanhsachSanphamBanchay();
        List<Products> Sanpham_CL(string cl);
        List<Products> Sanpham_L(string l);
        int Dangnhap(string tendn, string matkhau);
        int Dangki(string tendn, string matkhau,string ten);
        Customers TimKh(string tendn,string matkhau);
        Customers TimKh(string tendn);
        Products Sanpham(string id);
        List<Products> Timsanpham(string keysearch);
        void ThemOrder(Orders o);
        Orders timOrder_moinhat();
        List<WebBanHang1.Models.Orders> DSHD(string cusID);
        Orders TimHD(string orID);
        List<Order_Details> TimCTHD(string orID);
        void ThemOrderdetails(Order_Details o);
        void Xoadonhang(Orders o);
        Customers CapnhatTK(Customers c);
        Employees CapnhatNhanvien(Employees c);
        int XoaTK(Customers c);
        int XoaSP(string id);
        int Themlienlac(Lienlac l);
        int CapnhatSP(Products p);
        int CapnhatHD(Orders o);
        int ThemSP(Products p);
        Category_Childs DMCon(string idsp);
        bool KiemtraAnh(HttpPostedFileBase anh);
        int ThemNV(Employees e);
        bool ThemQuyen(Quyens q);
        Quyens TimQuyen(string tenvt,string tentw);
        bool SuaQuyen(Quyens q);
        List<Quyens> ListQuyen();
        bool ThemTW(TrangWebs tw);
        List<TrangWebs> ListTW();
        List<VaiTros> ListVT();
        List<Lienlac> DSLienlac();
    }
}
