using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang1.Models
{
    public interface IGio
    {
        double TongTien { get; }
        int SoMatHang { get; }
        int SoLuong { get; }

        void Them(Products sp, int soluong = 1);
        void Xoa(string maSP);
        Products Tim(string ma);
        void Xoa();
        List<Products> DSSP { get; }
    }
}