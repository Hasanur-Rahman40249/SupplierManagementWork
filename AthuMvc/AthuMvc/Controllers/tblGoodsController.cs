using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AthuMvc.Models;
using CrystalDecisions.CrystalReports.Engine;
using PagedList;

namespace AthuMvc.Controllers
{
    
    public class tblGoodsController : Controller
    {
        
        AppDbContext db = new AppDbContext();
        //[Authorize(Roles = "A,V")]
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? page)
        {
            List<GoodsViewModel> userList = db.TblGoods.Select(u => new GoodsViewModel
            {
                GoodsId = u.GoodsId,
                GoodsName = u.GoodsName,
                GoodsPrice = u.GoodsPrice,
                PDT = u.PDT,
                ImageName = u.ImageName,
                ImageUrl = u.ImageUrl
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

            var list = userList;
            if (!string.IsNullOrEmpty(SearchString))
            {
                list = list.Where(u => u.GoodsName.ToUpper().
                Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(u => u.GoodsName).ToList();
                    break;
                default:
                    list = list.OrderBy(u => u.GoodsName).ToList();
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
        public ActionResult Create(GoodsViewModel vobj)
        {
            var result = false;
            try
            {
                tblGoods obj;
                if (vobj.GoodsId == 0)
                {
                    obj = new tblGoods();
                    obj.GoodsName = vobj.GoodsName;
                    obj.GoodsPrice = vobj.GoodsPrice;
                    obj.PDT = vobj.PDT;
                    string fileName = Path.GetFileNameWithoutExtension(vobj.ImageFile.FileName);
                    string extension = Path.GetExtension(vobj.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    vobj.ImageUrl = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/" + fileName));
                    vobj.ImageFile.SaveAs(fileName);
                    obj.ImageName = vobj.ImageName;
                    obj.ImageUrl = vobj.ImageUrl;
                    db.TblGoods.Add(obj);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    obj = db.TblGoods.SingleOrDefault(u => u.GoodsId == vobj.GoodsId);
                    obj.GoodsName = vobj.GoodsName;
                    obj.GoodsPrice = vobj.GoodsPrice;
                    obj.PDT = vobj.PDT;
                    string fileName = Path.GetFileNameWithoutExtension(vobj.ImageFile.FileName);
                    string extension = Path.GetExtension(vobj.ImageFile.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    vobj.ImageUrl = "~/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Images/" + fileName));
                    vobj.ImageFile.SaveAs(fileName);
                    obj.ImageName = vobj.ImageName;
                    obj.ImageUrl = vobj.ImageUrl;
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
            tblGoods obj = db.TblGoods.SingleOrDefault(u => u.GoodsId == id);
            GoodsViewModel vobj = new GoodsViewModel();
            vobj.GoodsName = obj.GoodsName;
            vobj.GoodsPrice = obj.GoodsPrice;
            vobj.PDT = obj.PDT;
            vobj.ImageName = obj.ImageName;
            vobj.ImageUrl = obj.ImageUrl;
            vobj.GoodsId = obj.GoodsId;

            return View(vobj);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            tblGoods obj = db.TblGoods.SingleOrDefault(u => u.GoodsId == id);
            GoodsViewModel vobj = new GoodsViewModel();
            vobj.GoodsName = obj.GoodsName;
            vobj.GoodsPrice = obj.GoodsPrice;
            vobj.PDT = obj.PDT;
            vobj.ImageName = obj.ImageName;
            vobj.ImageUrl = obj.ImageUrl;
            vobj.GoodsId = obj.GoodsId;
            return View(vobj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            tblGoods obj = db.TblGoods.SingleOrDefault(u => u.GoodsId == id);
            if (obj != null)
            {
                db.TblGoods.Remove(obj);
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
            tblGoods obj = db.TblGoods.SingleOrDefault(u => u.GoodsId == id);
            GoodsViewModel vobj = new GoodsViewModel();
            vobj.GoodsName = obj.GoodsName;
            vobj.GoodsPrice = obj.GoodsPrice;
            vobj.PDT = obj.PDT;
            vobj.ImageName = obj.ImageName;
            vobj.ImageUrl = obj.ImageUrl;
            vobj.GoodsId = obj.GoodsId;
            ViewBag.Details = "Show";
            return PartialView("_UserDetails", vobj);
        }
    }
}
