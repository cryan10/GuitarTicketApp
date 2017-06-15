using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GRRepairTicketApp.Models;
using Microsoft.AspNet.Identity;

namespace GRRepairTicketApp.Controllers
{
    public class RepairTicketsCustomerController : Controller
    {
        private RepairTicketDBEntities1 db = new RepairTicketDBEntities1();

        // GET: RepairTicketsCustomer
        public ActionResult Index()
        {
            //creating variable to hold current user's userID
            var currentUser = User.Identity.GetUserId();
            //LINQ query for selecting only current user's info
            var repairTickets = from RepairTicket in db.RepairTickets
                                where RepairTicket.UserID == currentUser
                                select RepairTicket;


            return View(repairTickets.ToList());
        }

        // GET: RepairTicketsCustomer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairTicket repairTicket = db.RepairTickets.Find(id);
            if (repairTicket == null)
            {
                return HttpNotFound();
            }
            return View(repairTicket);
        }

        // GET: RepairTicketsCustomer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairTicketsCustomer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RepairTicketID,UserID,ModelName,SerialNumber,Brand,ProblemDescription,Equipment,TimeStamp,Progress")] RepairTicket repairTicket)
        {
            if (ModelState.IsValid)
            {
                repairTicket.UserID = User.Identity.GetUserId();
                repairTicket.TimeStamp = DateTime.Now;
                db.RepairTickets.Add(repairTicket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(repairTicket);
        }

        // GET: RepairTicketsCustomer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairTicket repairTicket = db.RepairTickets.Find(id);
            if (repairTicket == null)
            {
                return HttpNotFound();
            }
            return View(repairTicket);
        }

        // POST: RepairTicketsCustomer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RepairTicketID,UserID,ModelName,SerialNumber,Brand,ProblemDescription,EquipmentType,TimeStamp,Status")] RepairTicket repairTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(repairTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(repairTicket);
        }

        // GET: RepairTicketsCustomer/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RepairTicket repairTicket = db.RepairTickets.Find(id);
            if (repairTicket == null)
            {
                return HttpNotFound();
            }
            return View(repairTicket);
        }

        // POST: RepairTicketsCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RepairTicket repairTicket = db.RepairTickets.Find(id);
            db.RepairTickets.Remove(repairTicket);
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
