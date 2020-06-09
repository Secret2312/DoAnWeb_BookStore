using BookStore.Models;
using PagedList;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.Controllers
{
    public class AdminController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sach(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 7;
            //return View(data.SACHes.ToList());
            return View(data.SACHes.ToList().OrderBy(t => t.Masach).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Login(FormCollection collection)
        {
            dbQLBansachDataContext data = new dbQLBansachDataContext();

            var tendn = collection["form-username"];
            var matkhau = collection["form-password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Admin ad = data.Admins.SingleOrDefault(t => t.UserAdmin == tendn && t.PassAdmin == matkhau);
                if (ad != null)
                {
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult themmoisach()
        {
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(t => t.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(t => t.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        public ActionResult themmoisach(SACH sach, HttpPostedFileBase fileUpload)
        {
            var filename = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Images"), filename);
            if (System.IO.File.Exists(path))
            {
                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
            }
            else
            {
                fileUpload.SaveAs(path);
            }
            ViewBag.MaCD = new SelectList(data.CHUDEs.ToList().OrderBy(t => t.TenChuDe), "MaCD", "TenChude");
            ViewBag.MaNXB = new SelectList(data.NHAXUATBANs.ToList().OrderBy(t => t.TenNXB), "MaNXB", "TenNXB");
            return View();
        }

        public ActionResult List_Chude(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            
            return View(data.CHUDEs.ToList().OrderBy(t => t.TenChuDe).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult List_Nhaxuatban(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            //return View(data.SACHes.ToList());

            return View(data.NHAXUATBANs.ToList().OrderBy(t => t.MaNXB).ToPagedList(pageNumber, pageSize));
        }
    }
}