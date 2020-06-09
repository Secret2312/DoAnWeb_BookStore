using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    public class KhachHangController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["MatkhauNhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = string.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);

            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = " Họ tên khách hàng không được để trống";
            }
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = " Tên đăng nhập không được để trống";
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = " Mật khẩu không được để trống";
            }
            if (string.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = " Mật khẩu nhập lại không được để trống";
            }
            if (string.IsNullOrEmpty(diachi))
            {
                ViewData["Loi5"] = " Địa chỉ không được để trống";
            }
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = " Email không được để trống";
            }
            if (string.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = " Điện thoại không được để trống";
            }
            else
            {
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh = DateTime.Parse(ngaysinh);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Dangnhap"); //move to Login page
            }
            return View();
        }

        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection)
        {
            var taikhoan = collection["Taikhoan"];
            var matkhau = collection["Matkhau"];
            if (ModelState.IsValid)
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(t => t.Taikhoan == taikhoan && t.Matkhau == matkhau);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "BookStore");
                }
                else
                {
                    ViewBag.Thongbao = "Sai tên đăng nhập hoặc mật khẩu";
                }
            }
            else
            {

            }
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["Taikhoan"] = null;
            return Redirect("/");
        }
    }

}