using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FMS_Entities;
using FMS_Model;
using FMS_Web_Framework.Base;

namespace ATP2.FMS.Mvc.Controllers
{
    public class OwnerController : BaseController
    {
       

        public ActionResult OwnerProfile()
        {
            var ownerInfo = userDao.GetById(1);
            var ownerCMP = ownerDao.GetByID(1);
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
            return View(ownerVM);


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