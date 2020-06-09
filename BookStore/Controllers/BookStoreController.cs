using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;
using PagedList;
using PagedList.Mvc;

namespace BookStore.Controllers
{
    public class BookStoreController : Controller
    {
        dbQLBansachDataContext data = new dbQLBansachDataContext();

        private List<SACH> LaySachMoi(int count)
        {
            return data.SACHes.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }

        // GET: BookStore
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);

            var sachmoi = LaySachMoi(data.SACHes.Count());
            return View(sachmoi.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Chude()
        {
            var chude = from cd in data.CHUDEs select cd;
            return PartialView(chude);
        }

        public ActionResult Nhaxuatban()
        {
            var nhaxb = from nxb in data.NHAXUATBANs select nxb;
            return PartialView(nhaxb);
        }

        public ActionResult SPTheochude(int id)
        {
            var sach = from s in data.SACHes where s.MaCD == id select s;
            return View(sach);
        }

        public ActionResult SPTheoNXB(int id)
        {
            var nhaxb = from nxb in data.SACHes where nxb.MaNXB == id select nxb;
            return View(nhaxb);
        }

        public ActionResult Details(int id)
        {
            var sach = from s in data.SACHes where s.Masach == id select s;
            return View(sach.Single());
        }
    }
}