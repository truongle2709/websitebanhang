using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using PagedList;

namespace WebApplication1.Controllers
{
    public class SanPhamController : Controller
    {
        Colorshop2Entities db = new Colorshop2Entities();
        // GET: SanPham
        public ActionResult Index()
        {
            var list = db.Products.ToList();
            return View(list);
        }
        public  ActionResult DanhSachSanPham(/*int categoryID=0*/int? page,string sortOrder)
        {
            //if (categoryID!=0)
            //{
            //    var category = db.Categories.Find(categoryID);
            //    var model1 = category.Products.ToList();
            //    return View(model1);
            //}
            ViewBag.CurentSorr = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "Price_desc" : "Price";
            var model2 = from s in db.Products select s;
            switch (sortOrder)
            {
                case "name_desc":
                    model2 = model2.OrderByDescending(s => s.Productname);
                    break;
                case "Price":
                    model2 = model2.OrderBy(s => s.Price);
                    break;
                case "Price_desc":
                    model2 = model2.OrderByDescending(s => s.Price);
                    break;
                default:
                    model2 = model2.OrderBy(s => s.Productname);
                    break;
            }
            if (page == null) page = 1;
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            
            return  View(model2.ToPagedList(pageNumber,pageSize));
        }
        public  ActionResult CategoryMenu()
        {
            var model = db.Categories;
            return PartialView("_Category",model);
        }
        public ActionResult ChiTietSanPham(int id)
        {
            var model = db.Products.Where(n =>n.Id==id).FirstOrDefault();
            return View(model);
        }
        public ActionResult PromotionProduct()
        {
            return View();
        }
    }
}