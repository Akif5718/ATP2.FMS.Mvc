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
    public class OwnerController : BaseController
    {
       

        public ActionResult OwnerProfile()
        {
            var ownerInfo = userDao.GetById(JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name).UserId);
            var ownerCMP = ownerDao.GetByID(JsonConvert.DeserializeObject<UserInfo>(User.Identity.Name).UserId);
            Owner ownerVM = new Owner();
            ownerVM.Balance = ownerInfo.Data.Balance;
            ownerVM.City = ownerInfo.Data.City;
            ownerVM.CompanyAddress = ownerCMP.Data.CompanyAddress;
            ownerVM.CompanyName = ownerCMP.Data.CompanyName;
            ownerVM.Country = ownerInfo.Data.Country;
            ownerVM.DateofBrith = ownerInfo.Data.DateofBrith;
            ownerVM.Email = ownerInfo.Data.Email;
            ownerVM.FristName = ownerInfo.Data.FristName;
            ownerVM.LastName = ownerInfo.Data.LastName;
            ownerVM.JoinDate = ownerInfo.Data.JoinDate;
            ownerVM.ProPic = ownerInfo.Data.ProPic;
            ownerVM.State = ownerInfo.Data.State;
            ownerVM.UserId = ownerInfo.Data.UserId;
            ownerVM.UserType = ownerInfo.Data.UserType;

            var result = postProjectDao.GetAllUser(CurrentUser.User.UserId);
            ownerVM.PostAProject = result;
            return View(ownerVM);


        }

        public ActionResult EditProfile()
        {
            var result = userDao.GetById(CurrentUser.User.UserId);
            Owner ownerVM = new Owner();
            ownerVM.UserInfo = result.Data;
            var result2 = ownerDao.GetByID(CurrentUser.User.UserId);
            ownerVM.CompanyAddress = result2.Data.CompanyAddress;
            ownerVM.CompanyCode = result2.Data.CompanyCode;
            ownerVM.CompanyName = result2.Data.CompanyName;
            ownerVM.Position = result2.Data.Position;

            return View(ownerVM);
        }
       
        [HttpPost]     
        public ActionResult EditProfile(Owner ownerVM)
        {
            UserInfo u=new UserInfo();
            u.UserId = CurrentUser.User.UserId;
            if (ownerVM.ConfirmPassword == null)
            {
                u.Password = CurrentUser.User.Password;

            }
            else
            {
                u.Password = ownerVM.ConfirmPassword;
            }
            u.FristName = ownerVM.FristName;
            u.LastName = ownerVM.LastName;
           // u.DateofBrith = ownerVM.DateofBrith;
            u.Country = ownerVM.Country;
            u.City= ownerVM.City;
            u.State = ownerVM.State;
            var result = userDao.Save(u);

            OwnerInfo o=new OwnerInfo();
            o.CompanyName = ownerVM.CompanyName;
            o.CompanyCode = ownerVM.CompanyCode;
            o.CompanyAddress = ownerVM.CompanyAddress;
            o.Position = ownerVM.Position;
            o.UserId = CurrentUser.User.UserId;
            var result2 = ownerDao.Save(o);
           

            return RedirectToAction("OwnerProfile");
        }

        public ActionResult Deleteacount()
        {
            var result = userDao.Delete(CurrentUser.User.UserId);
            return RedirectToAction("RegisterForm", "User");
        }
        
        public ActionResult ProjectList()
        {
            var result = postProjectDao.GetAll();
            var result2 = skillsDao.GetAll();
            ProjectListModel projectListModel=new ProjectListModel();
            projectListModel.PostAProjects = result;
            projectListModel.Skills = result2;

            return View(projectListModel);
        }

        [HttpPost]
        public ActionResult ProjectList(ProjectSkills skill)
        {
            ProjectListModel projectListModel = new ProjectListModel();

            var result = projectSkillDao.GetAllskill(skill.SkillID);
            foreach (var projectSkillse in result)
            {
                var result2 = postProjectDao.GetByID(projectSkillse.PostID);
                projectListModel.PostAProjects.Add(result2.Data);

            }
            var result3 = skillsDao.GetAll();
            projectListModel.Skills = result3;
            return View(projectListModel);
        }

        public ActionResult ProjectList1()
        {


            return RedirectToAction("ProjectList");
        }
    }
}