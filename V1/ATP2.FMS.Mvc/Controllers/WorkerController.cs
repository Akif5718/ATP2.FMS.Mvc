using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMS_Entities;
using FMS_Framework;
using FMS_Model;
using FMS_Web_Framework.Base;
using Newtonsoft.Json;

namespace ATP2.FMS.Mvc.Controllers
{
    public class WorkerController : BaseController
    {
        // GET: Worker
        public ActionResult WorkerProfile()
        {

            var workerInfo = userDao.GetById(JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name).UserId);
            //   var ownerCMP = workerDao.GetByID(JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name).UserId);
            var workerVM = new Worker();
            workerVM.Balance = workerInfo.Data.Balance;
            workerVM.City = workerInfo.Data.City;
            //  workerVM.RatePerHour = ownerCMP.Data.RatePerHour;
            //workerVM.CompanyName = ownerCMP.Data.CompanyName;
            workerVM.Country = workerInfo.Data.Country;
            workerVM.DateofBrith = workerInfo.Data.DateofBrith;
            workerVM.Email = workerInfo.Data.Email;
            workerVM.FristName = workerInfo.Data.FristName;
            workerVM.LastName = workerInfo.Data.LastName;
            workerVM.JoinDate = workerInfo.Data.JoinDate;
            workerVM.ProPic = workerInfo.Data.ProPic;
            workerVM.State = workerInfo.Data.State;
            workerVM.UserId = workerInfo.Data.UserId;
            workerVM.UserType = workerInfo.Data.UserType;
            var result = selectedWorkerDao.GetAllUser(CurrentUser.User.UserId);
            foreach (var selectedWorker in result)
            {
                var result2 = postProjectDao.GetByID(selectedWorker.PostId);
                workerVM.PostAProjects.Add(result2.Data);
            }
         
            return View(workerVM);
        }

        public ActionResult EditProfileWorker()
        {
            //
            var result = userDao.GetById(CurrentUser.User.UserId);
            var workerVM = new Worker();
            workerVM.UserInfo = result.Data;
            var result2 = workerDao.GetByID(CurrentUser.User.UserId);
            workerVM.RatePerHour = result2.Data.RatePerHour;
            workerVM.EarnedMoney = result2.Data.EarnedMoney;


            return View(workerVM);
        }

        [HttpPost]
        public ActionResult EditProfileWorker(Worker workerVM)
        {
            UserInfo u = new UserInfo();
            u.UserId = CurrentUser.User.UserId;
            if (workerVM.ConfirmPassword == null)
            {
                u.Password = CurrentUser.User.Password;

            }
            else
            {
                u.Password = workerVM.ConfirmPassword;
            }
            u.FristName = workerVM.FristName;
            u.LastName = workerVM.LastName;
            // u.DateofBrith = ownerVM.DateofBrith;
            u.Country = workerVM.Country;
            u.City = workerVM.City;
            u.State = workerVM.State;
            var result = userDao.Save(u);

            WorkerInfo o = new WorkerInfo();
            o.RatePerHour = workerVM.RatePerHour;
            o.EarnedMoney = workerVM.EarnedMoney;
            o.UserId = CurrentUser.User.UserId;
            var result2 = workerDao.Save(o);


            return RedirectToAction("WorkerProfile");
        }

        public ActionResult Deleteacount()
        {
            var result = userDao.Delete(CurrentUser.User.UserId);
            return RedirectToAction("RegisterForm", "User");
        }



    }
}