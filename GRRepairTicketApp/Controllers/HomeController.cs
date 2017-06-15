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