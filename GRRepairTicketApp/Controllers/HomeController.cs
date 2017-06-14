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
    public class HomeController : Controller
    {
        private RepairTicketDBEntities db = new RepairTicketDBEntities();

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult WelcomeAdmin()
        {
            var repairTickets = from RepairTicket in db.RepairTickets
                                orderby
                         RepairTicket.TimeStamp descending
                                select RepairTicket;
                                
            return View(repairTickets.ToList());
        }


        // GET: RepairTickets

        public ActionResult WelcomeCustomer()
        {
            //creating variable to hold current user's userID
            var currentUser = User.Identity.GetUserId();
            //LINQ query for selecting only current user's info
            var repairTickets = from RepairTicket in db.RepairTickets
                                where RepairTicket.UserID == currentUser
                                select RepairTicket;


            return View(repairTickets.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}