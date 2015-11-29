using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KioskSolution.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult AddCustomer()
        {
            ViewBag.Title = "Add Customer";
            return View();
        }

        public ActionResult ViewCustomer()
        {
            ViewBag.Title = "View Customer";
            return View();
        }

        public ActionResult CustomerCardRequest()
        {
            ViewBag.Title = "Customer Card Request";
            return View();
        }

        public ActionResult ConfirmToken()
        {
            ViewBag.Title = "Confirm Token";
            return View();
        }

        public ActionResult CustomerCardRequestReport()
        {
            ViewBag.Title = "Customer Card Request Report";
            return View();
        }
    }
}