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
    //[Authorize]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            try
            {
                var employees = db.Employees.Include(e => e.DesignationDetails).Select(t => new EmployeeViewModel()
                {
                    Id = t.Id,
                    DesignationId = t.DesignationId,
                    Designation = t.DesignationDetails == null ? null : t.DesignationDetails.DesignationName,
                    DateOfBirth = t.DateOfBirth,
                    Name = t.Name,
                    //Age = CalculateAge(t.DateOfBirth)
                }) ;

                var emp = employees.ToList().Select(t => new EmployeeViewModel()
                {
                    Id = t.Id,
                    DesignationId = t.DesignationId,
                    Designation = t.Designation,
                    DateOfBirth = t.DateOfBirth,
                    Name = t.Name,
                    Age = CalculateAge(t.DateOfBirth)
                }) ;
                return View(emp);
            }
            catch (Exception ex)
            {

                throw;
            }          
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            if(dateOfBirth==null)
            {
                return 0;
            }
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }

            EmployeeViewModel model = new EmployeeViewModel();
            model.Id = employee.Id;
            model.DesignationId = employee.DesignationId;
            model.DateOfBirth = employee.DateOfBirth;
            model.Name = employee.Name;
            model.Designation = employee.DesignationDetails==null?null:employee.DesignationDetails.DesignationName;
            return View(model);
        }

        // GET: Employees/Create
        public ActionResult CreateEmployee()
        {
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName");
            return PartialView();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateEmployee(EmployeeViewModel employee)
        {
            try
            {
                  Employee model = new Employee();
                    model.DateOfBirth = employee.DateOfBirth;
                    model.DesignationId = employee.DesignationId;
                    model.Id = employee.Id;
                    model.Name = employee.Name;

                    db.Employees.Add(model);
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

        // GET: Employees/Edit/5
        public ActionResult EditEmployee(int? id)
        {
            if (id == null)
            {
                return Json(new { success = true, message = "Not find" }, JsonRequestBehavior.AllowGet);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return Json(new { success = true, message = "success" }, JsonRequestBehavior.AllowGet);
            }
            EmployeeViewModel model = new EmployeeViewModel();
            model.Id = employee.Id;
            model.DesignationId = employee.DesignationId;
            model.DateOfBirth = employee.DateOfBirth;
            model.Name = employee.Name;
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "DesignationName", employee.DesignationId);
            return PartialView(model);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult EditEmployee([Bind(Include = "Id,Name,DesignationId,DateOfBirth")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = db.Employees.Find(employee.Id);
                    if (entity == null)
                    {
                        return Json(new { success = false, message = "Not found" }, JsonRequestBehavior.AllowGet);

                    }
                    entity.DateOfBirth = employee.DateOfBirth;
                    entity.DesignationId = employee.DesignationId;
                    entity.Id = employee.Id;
                    entity.Name = employee.Name;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                  
                }
                return Json(new { success = true, message = "Success" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
          
         
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            EmployeeViewModel model = new EmployeeViewModel();
            model.Id = employee.Id;
            model.DesignationId = employee.DesignationId;
            model.DateOfBirth = employee.DateOfBirth;
            model.Name = employee.Name;
            return View(model);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
