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
            //JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name).UserId
            var workerInfo = userDao.GetById(1);
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
            var result = selectedWorkerDao.GetAll(CurrentUser.User.UserId);
            foreach (var selectedWorker in result)
            {
                var result2 = postProjectDao.GetByID(selectedWorker.PostId);
                workerVM.PostAProjects.Add(result2.Data);
            }
         
            return View(workerVM);
        }

    }
}