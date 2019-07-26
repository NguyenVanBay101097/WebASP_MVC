using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang1.Models;
namespace WebBanHang1.Models
{
    public class Gio:List<Products>, IGio
    {
        public double TongTien
        {
            get
            {
                var s = 0.0;
                foreach (var sp in this)
                {
                   
                    s += (double)((sp.PricePresent) * sp.SoLuong);
                }
                return s;
            }
        }

        public int SoMatHang
        {
            get
            {
                return this.Count;
            }
        }

        public void Them(Products sp, int soluong = 1)
        {
            var kq = Tim(sp.ProductID);
            if (kq == null)
            {
                sp.SoLuong = soluong;
                this.Add(sp);
            }
            else
            {
                kq.SoLuong += soluong;
            }
        }

        public void Xoa(string maSP)
        {
            this.Remove(this.Tim(maSP));
        }

        public Products Tim(string ma)
        {
            foreach (var s in this)
            {
                if (s.ProductID == ma)
                    return s;
            }
            return null;
        }

        public void Xoa()
        {
            this.Clear();
        }

        public List<Products> DSSP
        {
            set { this.DSSP = null; }
            get
            {
                return this;
            }
        }

        public int SoLuong
        {
            get
            {
                var tong = 0;
                foreach (Products d in this.DSSP)
                {
                    tong = tong + d.SoLuong;
                }
                return tong;
            }
        }
    }
}