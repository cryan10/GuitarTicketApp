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
        private RepairTicketDBEntities1 db = new RepairTicketDBEntities1();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}