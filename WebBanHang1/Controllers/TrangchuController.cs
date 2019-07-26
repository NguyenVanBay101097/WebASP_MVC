using ASPSnippets.GoogleAPI;
using BotDetect.Web.Mvc;
using Facebook;
using PagedList;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using System.Web.UI;

namespace WebBanHang1.Controllers
{
    public class TrangchuController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }
        WebBanHang1.Models.BH db = new Models.BH();
        public ActionResult Index()
        {
            return View();
        }
        //partial danh mục
        [ChildActionOnly]
        //[OutputCache(Duration =60)]
        public  ActionResult Partial_danhmuc()
        {
            var dsdm = db.DanhsachDanhmuc_cha();
            return PartialView("~/Views/Partial_view/Danhmuc.cshtml",dsdm) ;
        }
        //partial san pham moi
        [ChildActionOnly]
        public ActionResult Partial_Sanphammoi(int soluong, int page=1)
        {
            int pagesize = 8;
            var dssp = db.DanhsachSanphamMoi().ToPagedList(page, pagesize);
            if (soluong>0)
            {
                dssp = db.DanhsachSanphamMoi().Take(soluong).ToPagedList(page, pagesize);


            }

            else
            {
                 dssp = db.DanhsachSanphamMoi().ToPagedList(page, pagesize);

            }
            return PartialView("~/Views/Partial_view/DS_Sanphammoi.cshtml", dssp);

        }
        //partial san pham ban chay
        [ChildActionOnly]
        public ActionResult Partial_SanphamBanchay(int soluong, int page = 1)
        {

            int pagesize = 8;
            var dssp = db.DanhsachSanphamBanchay().ToPagedList(page, pagesize);
            if (soluong > 0)
            {
                dssp = db.DanhsachSanphamBanchay().Take(soluong).ToPagedList(page, pagesize);


            }

            else
            {
                dssp = db.DanhsachSanphamBanchay().ToPagedList(page, pagesize);

            }
            return PartialView("~/Views/Partial_view/DS_SanphamBanchay.cshtml", dssp);


        }
        //partial dang nhap
        public ActionResult Dangnhap()
        {


            if (TempData["url"] == null)
            {


                TempData["url"] = Request.UrlReferrer;
            }
            else
            {
                TempData.Keep("url");

            }
            if (Session["customer"]!=null)
            {
                return Redirect("~/Trangchu");
            }
           
            return View();

        }
        //đăng nhập facebook
        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token",new {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code=code
            });
            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email = me.email;
                string username = me.email;
                string firstname = me.first_name;
                string middlename = me.middle_name;
                string lastname = me.last_name;
                var user = new WebBanHang1.Models.Customers();
                user.TenDN = username;
                user.CustomerName = firstname + " " + middlename + " " + lastname;
                user.Email = email;
                user.MatKhau = email;
                var kq = db.Dangki_Facebook(user);
                if (kq!=null)
                {
                    Session["customer"] = db.TimKh(user.TenDN, user.MatKhau);

                }
            }
            return Redirect(TempData["url"].ToString());



        }
        public ActionResult DangnhapFacebook()
        {
            var fb = new FacebookClient();
            var loginurl = fb.GetLoginUrl(new {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type="code",
                scope="email",
            });
            return Redirect(loginurl.AbsoluteUri);
        }
        //đăng nhập google
       [HttpPost]
        [ValidateAntiForgeryToken]
        public void LoginWithGooglePlus()
        {
            GoogleConnect.ClientId = "508536128158-3v4078lig5vbt6j727okq6ie2b45tg0n.apps.googleusercontent.com";
            GoogleConnect.ClientSecret = "UQlFGFFRQq_Ztfw4FbqzZh_D";
            GoogleConnect.RedirectUri = Request.Url.AbsoluteUri.Split('?')[0];
            GoogleConnect.Authorize("profile", "email");
        }
        internal class GoogleProfile
        {
            public string Id { get; set; }
            public string DisplayName { get; set; }
            public ImageProfile Image { get; set; }
            public List<Email> Emails { get; set; }
            public string Gender { get; set; }
            public string ObjectType { get; set; }
        }
        internal class Email
        {
            public string Value { get; set; }
            public string Type { get; set; }
        }
        internal class ImageProfile
        {
            public string Url { get; set; }
        }
        [ActionName("LoginWithGooglePlus")]
        public ActionResult LoginWithGooglePlusConfirmed()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                string code = Request.QueryString["code"];
                string json = GoogleConnect.Fetch("me", code);
                GoogleProfile profile = new JavaScriptSerializer().Deserialize<GoogleProfile>(json);


                
                WebBanHang1.Models.Customers khachHang = new WebBanHang1.Models.Customers()
                {
                    
                    CustomerName = profile.DisplayName,
                    Email = profile.Emails.Find(email => email.Type == "account").Value,
                   MatKhau= profile.Emails.Find(email => email.Type == "account").Value,
                   TenDN= profile.Emails.Find(email => email.Type == "account").Value
                };


                if (db.TimKh(khachHang.TenDN,khachHang.MatKhau) != null)
                {
                    Session["customer"] = db.TimKh(khachHang.TenDN,khachHang.MatKhau);
                    return Redirect(TempData["url"].ToString());
                }
                db.Dangki_Facebook(khachHang);
                Session["customer"] = db.TimKh(khachHang.TenDN, khachHang.MatKhau);

            }
            if (Request.QueryString["error"] == "access_denied")
            {
                return Content("access_denied");
            }
            return RedirectToAction("Index", "Trangchu");

        }


       
        //đăng nhập
        [HttpPost]
        public ActionResult Dangnhap(WebBanHang1.Models.Customers cs)
        {
            var  ct = db.Dangnhap(cs.TenDN,cs.MatKhau);
            if (ct==-1)
            {
             TempData["err_tendn"]=   "<script>swal('sai tên đăng nhập !')</script>";
                return View();
                //sai tên dn
            }
            if (ct==0)
            {
                TempData["err_matkh"] = "<script>swal('sai mật khẩu !')</script>";
                return View();
                //sai mật khẩu
            }
            if (ct==1)
            {
                if (TempData["url"]==null)
                {
                    TempData["url"] = Request.UrlReferrer;
                }
                Session["customer"] = db.TimKh(cs.TenDN, cs.MatKhau);
                return Redirect(TempData["url"].ToString());
            }
            return View();

        }
        //đăng xuất
        public ActionResult Dangxuat()
        {
            
            Session.Remove("customer");
            TempData["url"] = Request.UrlReferrer;
            return Redirect(TempData["url"].ToString());

        }
        //đang kí
        public ActionResult Dangki()
        {

            return View();

        }
        //đăng kí
        [HttpPost]
        [CaptchaValidation("cap", "ExampleCaptcha", "captcha không đúng!")]
        public ActionResult Dangki(WebBanHang1.Models.Customers cs,string mk)
        {
          
            if (!ModelState.IsValid)
            {
                TempData["err_captcha"] = "<script>swal('captcha nhập không đúng !')</script>";
                return View();
            }
           
            if (cs.MatKhau != mk)
            {
                TempData["err_matkh1"] = "<script>swal('Mật khẩu không trùng khớp !')</script>";
                return View();

            }
            
            var ct = db.Dangki(cs.TenDN, cs.MatKhau,cs.CustomerName);
            if (ct == 0)
            {
                TempData["err_tendn"] = "<script>swal('Tên đăng nhập đã tồn tại !')</script>";
                return View();
            }
           
            
            if (ct == 1)
            {
                Session["customer"] = db.TimKh(cs.TenDN, cs.MatKhau);
               
                return RedirectToAction("Index", "Trangchu");
            }
            return View();

        }
        //giới thiệu
        public ActionResult Gioithieu()
        {
            return View();
        }
        //tin tuc
        public ActionResult Khuyenmai(int page = 1)
        {
            int pagesize = 8;
            var dssp = db.DanhsachSanphamKM().ToPagedList(page, pagesize);
           

            
            return View(dssp);
        }
        //Lienlac
        public ActionResult Lienlac()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Lienlac(WebBanHang1.Models.Lienlac l)
        {
           
            db.Themlienlac(l);
            TempData["thanhcong"] = "<script>swal('đã gửu thành công,chúng tôi sẽ phản hồi qua gmail!');</script>";

            return RedirectToAction("","Trangchu");
        }
        //tìm kiếm
        public  ActionResult Timkiem(string keysearch,int page=1)
        {
            int pagesize = 8;
            TempData["controller"] = "Trangchu";
            TempData["action"] = "Timkiem";
            ViewBag.keysearch = keysearch;
            var ds = db.Timsanpham(keysearch).ToPagedList(page,pagesize);
            return View(ds);
        }
        //autocomplete keysearch
        [HttpPost]
        public JsonResult AutocompleteKeysearch(string Prefix)
        {
            //Note : you can bind same list from database  
            List<WebBanHang1.Models.Products> ObjList = new List<WebBanHang1.Models.Products>();
            ObjList = db.DanhsachSanphamMoi();
            //Searching records from list using LINQ query  
            var CityName = (from N in ObjList
                            where N.ProductName.StartsWith(Prefix)
                            select new { N.ProductName });
            return Json(CityName, JsonRequestBehavior.AllowGet);
        }
     
    }
}