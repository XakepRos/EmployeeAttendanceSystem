using EmpAttendance.Models;
using EmpAttendance.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmpAttendance.Controllers
{
    [Authorize]
    public class DesignationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public virtual ActionResult Index()
        {
            var data = db.Designations.Select(x => new DesignationViewModel()
            {
                Id = x.Id,
                DesignationName = x.DesignationName
            }).ToList();
            return View(data);
        }



        [HttpGet]
        public virtual ActionResult CreateDesignation()
        {
            return PartialView();
        }

        [HttpPost]
        public virtual  JsonResult CreateDesignation(DesignationViewModel model)
        {
            try
            {
                Designation entity = new Designation();
                entity.DesignationName = model.DesignationName;
                var result =  db.Designations.Add(entity);
                db.SaveChanges();

                return Json(new { success =true, message = "success" }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public virtual ActionResult EditDesignation(int id)
        {
            var designation = db.Designations.Find(id);
            var model = new DesignationViewModel();
            model.DesignationName = designation.DesignationName;
            model.Id = designation.Id;
            return PartialView(model);
        }

        [HttpPost]
        public virtual JsonResult EditDesignation(DesignationViewModel model)
        {
            try
            {
                var entity = db.Designations.Find(model.Id);
                if(entity==null)
                {
                    return Json(new { success = false, message ="Not found" }, JsonRequestBehavior.AllowGet);
                }
                entity.DesignationName = model.DesignationName;
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
              
                    return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
              
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public virtual JsonResult DeleteDesignation(int id)
        {
            try
            {
                Designation des = db.Designations.Find(id);
                db.Designations.Remove(des);
                db.SaveChanges();
             
                    return Json(new { success = true, message = "Deleted" }, JsonRequestBehavior.AllowGet);
               
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}