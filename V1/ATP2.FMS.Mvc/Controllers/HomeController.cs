using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FMS_Entities;
using Newtonsoft.Json;

namespace ATP2.FMS.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            var userInfo = JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name);
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            FormsAuthentication.SignOut();
            return View();
        }
    }
}