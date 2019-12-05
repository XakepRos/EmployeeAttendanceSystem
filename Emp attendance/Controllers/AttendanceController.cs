using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EmpAttendance.Models;
using EmpAttendance.ViewModel;

namespace EmpAttendance.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attendances
        public ActionResult Index()
        {
            try
            {
                var employees = db.Attendances.Include(e => e.EmployeeDetails).Select(t => new AttendanceViewModel()
                {
                    Id = t.Id,
                    CreatedDate = t.CreatedDate,
                    Employee = t.EmployeeDetails == null ? null : t.EmployeeDetails.Name,
                    InTime = t.InTime,
                    OutTime = t.OutTime
                });
                var emp = employees.ToList();
                return View(emp);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        // GET: Attendances/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attendance employee = db.Attendances.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            AttendanceViewModel model = new AttendanceViewModel();
            model.Id = employee.Id;
            model.InTime = employee.InTime;
            model.OutTime = employee.OutTime;
            model.EmpId = employee.EmpId;
            model.Employee = employee.EmployeeDetails == null ? null : employee.EmployeeDetails.Name;
            model.CreatedDate = employee.CreatedDate;
            return View(model);
        }

        // GET: Attendances/Create
        public ActionResult CreateAttendance()
        {
            ViewBag.EmpId = new SelectList(db.Employees, "Id", "Name");
            return PartialView();
        }

        // POST: Attendances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateAttendance(AttendanceViewModel employee)
        {
            try
            {
                Attendance model = new Attendance();
                model.InTime = employee.InTime;
                model.OutTime = employee.OutTime;
                model.Id = employee.Id;
                model.EmpId = employee.EmpId;
                model.CreatedDate = DateTime.Now;
                db.Attendances.Add(model);
                db.SaveChanges();


                return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


            //ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", employee.DesignationId);
            //return View(employee);
        }

        // GET: Attendances/Edit/5
        public ActionResult EditAttendance(int? id)
        {
            if (id == null)
            {
                return Json(new { success = true, message = "Not find" }, JsonRequestBehavior.AllowGet);
            }
            Attendance employee = db.Attendances.Find(id);
            if (employee == null)
            {
                return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
            }
            AttendanceViewModel model = new AttendanceViewModel();
            model.InTime = employee.InTime;
            model.OutTime = employee.OutTime;
            model.Id = employee.Id;
            model.EmpId = employee.EmpId;

            ViewBag.EmpId = new SelectList(db.Employees, "Id", "Name", employee.EmpId);
            return PartialView(model);
        }

        // POST: Attendances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditAttendance( Attendance employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = db.Attendances.Find(employee.Id);
                    if (entity == null)
                    {
                        return Json(new { success = false, message = "Not found" }, JsonRequestBehavior.AllowGet);

                    }
                    entity.InTime = employee.InTime;
                    entity.OutTime = employee.OutTime;
                    entity.Id = employee.Id;
                    entity.EmpId = employee.EmpId;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = false, message = "Not Saved" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }


        }

    

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Attendance employee = db.Attendances.Find(id);
            db.Attendances.Remove(employee);
            db.SaveChanges();

            return Json(new { success = true, message = "Deleted" }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}