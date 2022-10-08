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
    public class ClientsController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();
        SysHotelBusiness business = new SysHotelBusiness();

        // GET: Clients
        public ActionResult Index()
        {
            var clients = db.Clients.Include(c => c.Country);
            return View(clients.ToList());
        }

        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Identification,LastName,Email,IdCountry,Phone,Direccion")] Clients clients)
        {
            if (ModelState.IsValid)
            {

                if (business.ValidaCedula(clients.Identification) == true)
                {

                    db.Clients.Add(clients);
                    db.SaveChanges();
                    Alert("Guardado con Exito", Utilities.NotificationType.success);
                    return View(clients);
                }
                Alert("Ya existe un cliente con esta cedula", Utilities.NotificationType.warning);

            }

            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", clients.IdCountry);
            return View(clients);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", clients.IdCountry);
            return View(clients);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Identification,LastName,Email,IdCountry,Phone,Direccion")] Clients clients)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clients).State = EntityState.Modified;
                db.SaveChanges();
                Alert("Guardado con Exito", Utilities.NotificationType.success);
            }
            ViewBag.IdCountry = new SelectList(db.Countries, "Id", "Name", clients.IdCountry);
            return View(clients);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clients clients = db.Clients.Find(id);
            if (clients == null)
            {
                return HttpNotFound();
            }
            return View(clients);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clients clients = db.Clients.Find(id);
            db.Clients.Remove(clients);
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
