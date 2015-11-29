using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KioskSolution.Controllers
{
    public class FunctionController : Controller
    {
        // GET: Function
        public ActionResult AddFunction()
        {
            return View();
        }

        public ActionResult ViewFunction()
        {
            return View();
        }
    }
}