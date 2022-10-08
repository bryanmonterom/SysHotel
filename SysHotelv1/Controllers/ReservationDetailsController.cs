using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SysHotelv1.Models;
using SysHotelv1.Utilities;

namespace SysHotelv1.Controllers
{
    [Authorize]

    public class ReservationDetailsController : BaseController
    {
        private SysHotelDataContext db = new SysHotelDataContext();
        private SysHotelBusiness business = new SysHotelBusiness();
        // GET: ReservationDetails
        public ActionResult Index()
        {
            var reservationDetails = db.ReservationDetails.Include(r => r.Reservation).Include(r => r.Rooms);
            return View(reservationDetails.ToList());
        }

        public JsonResult GetClientName(int idReservation)
        {

            var reservationDetails = db.Reservations.Include("Clients").Where(a => a.Id == idReservation).FirstOrDefault();
            var name = reservationDetails.Clients.FullName + reservationDetails.Clients.LastName;
            return Json(name, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult GetDetails(int idReservation)
        {
            if (idReservation == null)
            {
                var msg = Utilities.Utilities.ErrorHandling("Detalles de Reservaciones", "", "Id Reservacion", ErrorsCode.NotFound);
                Alert(msg, Utilities.NotificationType.warning);
            }
            var reservationDetails = business.GetReservationDetailsList(idReservation);
            var reserv = db.Reservations.Find(idReservation);
            ViewBag.DaysQty = reserv.CheckOut.Subtract(reserv.CheckIn).Days;
            return PartialView("ReservationTableDetails", reservationDetails);
        }

        // GET: ReservationDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationDetails reservationDetails = db.ReservationDetails.Find(id);
            if (reservationDetails == null)
            {
                return HttpNotFound();
            }
            return View(reservationDetails);
        }

        // GET: ReservationDetails/Create
        public ActionResult Create(int? id)
        {
            if (id != null)
            {
                var reservation = db.Reservations.Include("Clients").Where(a=> a.Id == id).FirstOrDefault();
                ViewBag.ReservationId = new SelectList(Utilities.Utilities.GetClients(), "Value", "Text", id);
                ViewBag.RoomId = new SelectList(Utilities.Utilities.GetRooms(reservation.CheckIn, reservation.CheckOut), "Value", "Text");
                ViewBag.Name = reservation.Clients.FullName +" "+ reservation.Clients.LastName;
                return View();

            }
            var msg = Utilities.Utilities.ErrorHandling("Detalles de Reservaciones", "", "Id Reservacion", ErrorsCode.NotFound);
            ViewBag.RoomId = new SelectList(Utilities.Utilities.GetRooms(), "Value", "Text");
            ViewBag.ReservationId = new SelectList(Utilities.Utilities.GetClients(), "Value", "Text");
            Alert(msg, NotificationType.warning);
            return View();

        }



        // POST: ReservationDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomId,ReservationId,ChildQty,AdultQty")] ReservationDetails reservationDetails)
        {
            ViewBag.ReservationId = new SelectList(Utilities.Utilities.GetClients(), "Value", "Text", reservationDetails.ReservationId);
            var reservation = db.Reservations.Find(reservationDetails.ReservationId);
            ViewBag.RoomId = new SelectList(Utilities.Utilities.GetRooms(reservation.CheckIn, reservation.CheckOut), "Value", "Text");

            if (ModelState.IsValid)
            {
                if (!business.ValidateOccupation(reservationDetails.RoomId, reservationDetails.AdultQty+reservationDetails.ChildQty))
                {
                    Alert("Hay demasiadas personas para este tipo de habitacion", NotificationType.warning);
                    return View(reservationDetails);
                }
                db.ReservationDetails.Add(reservationDetails);
                db.SaveChanges();
                string msg = Utilities.Utilities.ErrorHandling("Detalle de Reservacion", "", "", ErrorsCode.NoError);
                Alert(msg, NotificationType.success);
                return View(reservationDetails);
            }
            return View(reservationDetails);
        }

        // GET: ReservationDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationDetails reservationDetails = db.ReservationDetails.Find(id);
            if (reservationDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "RoomNumber", reservationDetails.ReservationId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNumber", reservationDetails.RoomId);
            return View(reservationDetails);
        }

        // POST: ReservationDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomId,ReservationId,ChildQty,AdultQty")] ReservationDetails reservationDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservationDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReservationId = new SelectList(db.Reservations, "Id", "RoomNumber", reservationDetails.ReservationId);
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomNumber", reservationDetails.RoomId);
            return View(reservationDetails);
        }

        public ActionResult ReservationTableDetails(int? id)
        {
            var reservationDetails = db.ReservationDetails.Include("Reservation").Include("Rooms").ToList();
            return View(reservationDetails);
        }

        // GET: ReservationDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservationDetails reservationDetails = db.ReservationDetails.Find(id);
            if (reservationDetails == null)
            {
                return HttpNotFound();
            }
            return View(reservationDetails);
        }

        // POST: ReservationDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservationDetails reservationDetails = db.ReservationDetails.Find(id);
            db.ReservationDetails.Remove(reservationDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteReservation (int id, int idReservation)
        {
             if (id == null || idReservation == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ReservationDetails reservationDetails = db.ReservationDetails.Find(id);
            db.ReservationDetails.Remove(reservationDetails);
            db.SaveChanges();
             string msg = Utilities.Utilities.ErrorHandling("Detalle de Reservacion", "", "", ErrorsCode.NoError);
            var a  = Alert(msg, NotificationType.success);
            return RedirectToAction("GetDetails", new { idReservation= idReservation});
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
