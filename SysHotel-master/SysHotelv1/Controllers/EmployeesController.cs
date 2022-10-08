using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SysHotelv1.Models;

namespace SysHotelv1.Controllers
{
    [Authorize]

    public class EmployeesController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();
        SysHotelBusiness business = new SysHotelBusiness();

        // GET: Employees
        public ActionResult Index()
        {
            var employee = db.Employee.Include(e => e.BookingStatus).Include(e => e.Country);
            return View(employee.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,JobTitle,CountryId,BirthDate,HiredDate,Identification,Address,Phone,BookingStatusId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (business.ValidaCedula(employee.Identification) == true) {
                    
                    db.Employee.Add(employee);
                    db.SaveChanges();
                    Alert("Guardado con Exito", Utilities.NotificationType.success);
                    ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", employee.BookingStatusId);
                    ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", employee.CountryId);
                    return View(employee);
                }
                Alert("Ya existe un empleado con esa cedula", Utilities.NotificationType.warning);
            }

            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", employee.BookingStatusId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", employee.CountryId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", employee.BookingStatusId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", employee.CountryId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,JobTitle,CountryId,BirthDate,HiredDate,Identification,Address,Phone,BookingStatusId")] Employee employee)
        {
            if (ModelState.IsValid)
            {

                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                Alert("Guardado con exito", Utilities.NotificationType.warning);

            }
            ViewBag.BookingStatusId = new SelectList(db.BookingStatuses, "Id", "DescriptionStatus", employee.BookingStatusId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", employee.CountryId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
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
