using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GRRepairTicketApp.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;

namespace GRRepairTicketApp.Controllers
{
    public class RepairTicketsController : Controller
    {
        private RepairTicketDBEntities db = new RepairTicketDBEntities();

        // GET: RepairTickets
        public ActionResult Index()
        {
            //creating variable to hold current user's userID
            var currentUser = User.Identity.GetUserId();
            //LINQ query for selecting only current user's info
            var repairTickets = from RepairTicket in db.RepairTickets
                                where RepairTicket.UserID == currentUser
                                select RepairTicket;


            return View(db.RepairTickets.ToList());
        }

        public JsonResult AdminOrder()
        {
            /*This SQL query will sort repair tickets by time created newest to oldest and show user name for admin landing page
            SELECT RepairTickets.RepairTicketID, AspNetUsers.UserName
                         FROM RepairTickets
                         INNER JOIN AspNetUsers ON RepairTickets.UserID = AspNetUsers.Id
             ORDER BY RepairTickets.Timestamp Desc;*/


            var repairTickets = from RepairTickets in db.RepairTickets
                                orderby
                                  RepairTickets.TimeStamp descending
                                select new
                                {
                                    RepairTickets.RepairTicketID,
                                    RepairTickets.UserID
                                };

            var output = JsonConvert.SerializeObject(repairTickets.ToList());
            return Json(output, JsonRequestBehavior.AllowGet);
        }
    


        // GET: RepairTickets/Details/5
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

        // GET: RepairTickets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepairTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RepairTicketID,UserID,ModelName,SerialNumber,Brand,ProblemDescription,EquipmentType,TimeStamp,Status")] RepairTicket repairTicket)
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

        // GET: RepairTickets/Edit/5
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

        // POST: RepairTickets/Edit/5
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

        // GET: RepairTickets/Delete/5
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

        // POST: RepairTickets/Delete/5
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
