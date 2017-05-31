using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTask.Web.Controllers
{
    public class PNLController : Controller
    {
        // GET: Passenger
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddPassenger()
        {
            return View();
        }
        public ActionResult EditPassenger()
        {
            return View();
        }
    }
}