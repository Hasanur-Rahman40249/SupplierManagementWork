using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AthuMvc.Models;
using PagedList;

namespace AthuMvc.Controllers
{
    [Authorize]
    public class tblSupplierController : Controller
    {
        
     AppDbContext db = new AppDbContext();

        [Authorize(Roles = "A,V")]
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            List<SupplierViewModel> SmList = db.tblSuppliers.Select(s => new SupplierViewModel
            {
                SupplierId = s.SupplierId,
                SupplierName = s.SupplierName,
                Address = s.Address,
                Email = s.Email,
                GoodsId = s.tblGoods.GoodsId,
                GoodsName = s.tblGoods.GoodsName
            }).ToList();
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            var list = SmList;
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(s => s.SupplierName.ToUpper().Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(u => u.SupplierName).ToList();
                    break;
                default:
                    list = list.OrderBy(u => u.SupplierName).ToList();
                    break;
            }
            int PageSize = 3;
            int PageNumber = (page ?? 1);

            return View(list.ToPagedList(PageNumber, PageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(SupplierViewModel Smvobj)
        {
            var result = false;
            try
            {
                tblSupplier obj;
                if (Smvobj.SupplierId == 0)
                {
                    obj = new tblSupplier();
                    obj.SupplierName = Smvobj.SupplierName;
                    obj.Address = Smvobj.Address;
                    obj.Email = Smvobj.Email;
                    obj.GoodsId = Smvobj.GoodsId;

                    //string fileName = Path.GetFileNameWithoutExtension(vobj.ImageFile.FileName);
                    //string extension = Path.GetExtension(vobj.ImageFile.FileName);
                    //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //vobj.ImageUrl = "~/Images/" + fileName;
                    //fileName = Path.Combine(Server.MapPath("~/Images/" + fileName));
                    //vobj.ImageFile.SaveAs(fileName);
                    //obj.ImageName = vobj.ImageName;
                    //obj.ImageUrl = vobj.ImageUrl;
                    db.tblSuppliers.Add(obj);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    obj = db.tblSuppliers.SingleOrDefault(u => u.SupplierId == Smvobj.SupplierId);
                    obj.SupplierName = Smvobj.SupplierName;
                    obj.Address = Smvobj.Address;
                    obj.Email = Smvobj.Email;
                    obj.GoodsId = Smvobj.GoodsId;


                    //string fileName = Path.GetFileNameWithoutExtension(vobj.ImageFile.FileName);
                    //string extension = Path.GetExtension(vobj.ImageFile.FileName);
                    //fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    //vobj.ImageUrl = "~/Images/" + fileName;
                    //fileName = Path.Combine(Server.MapPath("~/Images/" + fileName));
                    //vobj.ImageFile.SaveAs(fileName);
                    //obj.ImageName = vobj.ImageName;
                    //obj.ImageUrl = vobj.ImageUrl;
                    db.SaveChanges();
                    result = true;

                }
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            tblSupplier obj = db.tblSuppliers.SingleOrDefault(u => u.SupplierId == id);
            SupplierViewModel vobj = new SupplierViewModel();
            vobj.SupplierName = obj.SupplierName;
            vobj.Address = obj.Address;
            vobj.Email = obj.Email;
            vobj.GoodsId = obj.GoodsId;



            return View(vobj);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            tblSupplier obj = db.tblSuppliers.SingleOrDefault(u => u.SupplierId == id);
            SupplierViewModel vobj = new SupplierViewModel();
            vobj.SupplierName = obj.SupplierName;
            vobj.Address = obj.Address;
            vobj.Email = obj.Email;
            vobj.GoodsId = obj.GoodsId;

            return View(vobj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            tblSupplier obj = db.tblSuppliers.SingleOrDefault(u => u.SupplierId == id);
            if (obj != null)
            {
                db.tblSuppliers.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }

        }

        public PartialViewResult Details(int id)
        {
            tblSupplier obj = db.tblSuppliers.SingleOrDefault(u => u.SupplierId == id);
            SupplierViewModel vobj = new SupplierViewModel();
            vobj.SupplierName = obj.SupplierName;
            vobj.Address = obj.Address;
            vobj.Email = obj.Email;
            vobj.GoodsId = obj.GoodsId;


            ViewBag.Details = "Show";
            return PartialView("_SemesterDetails", vobj);
        }
    }
}
