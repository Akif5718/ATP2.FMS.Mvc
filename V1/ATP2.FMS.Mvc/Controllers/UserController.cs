using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FMS_Entities;
using FMS_Model;
using FMS_RepositoryOracle;
using FMS_Web_Framework.Base;
using Newtonsoft.Json;

namespace ATP2.FMS.Mvc.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
         public ActionResult OwnerForm()
        {

            return View();


        }
        [HttpPost]
        public ActionResult OwnerForm(OwnerInfo ownerInfo)
        {

            try
            {
                var result = ownerDao.Save(ownerInfo);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("OwnerForm", ownerInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerForm");

        }
        public ActionResult RegisterForm()
        {

            return View();
        }
        [HttpPost]
        public ActionResult RegisterForm(UserInfo userInfo)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterForm", userInfo);
            }

            try
            {
                var result = userDao.Save(userInfo);
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if(userInfo.UserType.Equals("Owner"))
                return RedirectToAction("OwnerForm", "User");

            else
            {
                return RedirectToAction("RegisterForm");

            }


        }

        public ActionResult LoginForm()
        {

            return View();


        }
        [HttpPost]
        public ActionResult LoginForm(UserInfo userInfo)
        {

            var obj = userDao.GetByEmail(userInfo.Email);
            try
            {
                var result = userDao.Login(userInfo.Email,userInfo.Password);
               
                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", userInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            var jasonUserInfo = JsonConvert.SerializeObject(obj.Data);
            FormsAuthentication.SetAuthCookie(jasonUserInfo,false);
            if (obj.Data.UserType.Equals("Owner"))
                return RedirectToAction("LoginForm");

            else
            {
                return RedirectToAction("LoginForm");

            }
        }

     

        public ActionResult WorkerForm()
        {
           return View();
        }
        [HttpPost]
        public ActionResult WorkerForm(WorkerInfo workerInfo)
        {

            try
            {
                var result = workerDao.Save(workerInfo);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", workerInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerForm", "User");

        }

        public ActionResult EducationForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EducationForm(EducationalBackground educationalBackground)
        {

            try
            {
                var result = eduDao.Save(educationalBackground);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", educationalBackground);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerForm", "User");

        }

        public ActionResult WorkHistoryForm()
        {
            return View();
        }
        [HttpPost]
        public ActionResult WorkHistoryForm(WorkHistory workHistory)
        {

            try
            {
                var result = workDao.Save(workHistory);

                if (result.HasError)
                {
                    ViewBag.Message = result.Message;
                    return View("RegisterForm", workHistory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("OwnerForm", "User");

        }

       
    }
}