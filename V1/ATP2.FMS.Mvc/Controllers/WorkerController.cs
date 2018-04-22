using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATP2.FMS.Mvc.Controllers
{
    public class WorkerController : Controller
    {
        // GET: Worker
        public ActionResult WorkerProfile()
        {
            return View();
        }
        public ActionResult JobHome()
        {
            return View();
        }
    }
}