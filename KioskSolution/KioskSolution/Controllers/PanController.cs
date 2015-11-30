using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KioskSolution.Controllers
{
    public class PanController : Controller
    {
        // GET: Pan
        public ActionResult AddPan()
        {
            return View();
        }

        public ActionResult ViewPan()
        {
            return View();
        }
    }
}